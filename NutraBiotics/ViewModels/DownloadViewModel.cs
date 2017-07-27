namespace NutraBiotics.ViewModels
{
	using System.Collections.Generic;
	using System.ComponentModel;
    using System.Threading;
	using System.Threading.Tasks;
	using System.Windows.Input;
	using GalaSoft.MvvmLight.Command;
	using Models;
	using Services;
	using Xamarin.Forms;
	using Data;
    using System.Linq;

    public class DownloadViewModel : INotifyPropertyChanged
	{
		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Services
		DialogService dialogService;
		ApiService apiService;
		NetService netService;
		DataService dataService;
		NavigationService navigationService;
		#endregion

		#region Attributes
		double _progress;
		bool _isRunning;
		bool _isEnabled;
		string _message;
		#endregion

		#region Properties
		public string Message
		{
			set
			{
				if (_message != value)
				{
					_message = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Message)));
				}
			}
			get
			{
				return _message;
			}
		}

		public bool IsRunning
		{
			set
			{
				if (_isRunning != value)
				{
					_isRunning = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRunning)));
				}
			}
			get
			{
				return _isRunning;
			}
		}

		public bool IsEnabled
		{
			set
			{
				if (_isEnabled != value)
				{
					_isEnabled = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
				}
			}
			get
			{
				return _isEnabled;
			}
		}

		public double Progress
		{
			set
			{
				if (_progress != value)
				{
					_progress = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Progress)));
				}
			}
			get
			{
				return _progress;
			}
		}
		#endregion

		#region Constructors
		public DownloadViewModel()
		{
			dialogService = new DialogService();
			apiService = new ApiService();
			netService = new NetService();
			dataService = new DataService();
			navigationService = new NavigationService();

			IsEnabled = true;
		}
		#endregion

		#region Commands
		public ICommand DownloadCommand
		{
			get { return new RelayCommand(Download); }
		}

		async void Download()
		{
			var answer = await dialogService.ShowConfirm(
				"Confirmación",
				"¿Está seguro de inicar una nueva descarga?");
			if (!answer)
			{
				return;
			}

			var connection = await netService.CheckConnectivity();
			if (!connection.IsSuccess)
			{
				await dialogService.ShowMessage("Error", connection.Message);
				return;
			}

			await BeginDownload();
		}
		#endregion

		#region Methods
		async Task BeginDownload()
		{
			try
			{
				IsRunning = true;
				IsEnabled = false;

				Progress = 0;
				int processes = 7 * 2;
				var url = Application.Current.Resources["URLAPI"].ToString();

                Message = "Descargando clientes...";
                var customers = await DownloadMaster<Customer>(url, "/api/Customers");
                Progress += (double)1 / processes;

                Message = "Descargando sucursales...";
                var shipTos = await DownloadMaster<ShipTo>(url, "/api/ShipToes");
                Progress += (double)1 / processes;

                Message = "Descargando contactos...";
                var contacts = await DownloadMaster<Contact>(url, "/api/Contacts");
                Progress += (double)1 / processes;

                Message = "Descargando Productos...";
                var parts = await DownloadMaster<Part>(url, "/api/Parts");
                Progress += (double)1 / processes;

                Message = "Descargando lista de precios...";
                var pricelist = await DownloadMaster<PriceList>(url, "/api/PriceLists");
                Progress += (double)1 / processes;

                Message = "Descargando lista de precios x parte...";
				var pricelistpart = await DownloadMaster<PriceListPart>(url, "/api/PriceListParts");
				Progress += (double)1 / processes;

				Message = "Descargando lista de precios x cliente...";
				var customerpricelist = await DownloadMaster<CustomerPriceList>(url, "/api/CustomerPriceLists");
				Progress += (double)1 / processes;

                Message = "Termino todas las descargas...";

                if (customers != null && customers.Count > 0)
                {
                    DeleteAndInsert(customers);
                    await Task.Delay(100);
                    Progress += (double)1 / processes;
                }

                if (shipTos != null && shipTos.Count > 0)
                {
                    DeleteAndInsert(shipTos);
                    await Task.Delay(100);
                    Progress += (double)1 / processes;
                }

                if (contacts != null && contacts.Count > 0)
                {
                    DeleteAndInsert(contacts);
                    await Task.Delay(100);
                    Progress += (double)1 / processes;
                }

                if (parts != null && parts.Count > 0)
                {
                    DeleteAndInsert(parts);
                    await Task.Delay(100);
                    Progress += (double)1 / processes;
                }

                if (pricelist != null && pricelist.Count > 0)
                {
                    DeleteAndInsert(pricelist);
                    await Task.Delay(100);
                    Progress += (double)1 / processes;
                }

                if (pricelistpart != null && pricelistpart.Count > 0)
				{
					DeleteAndInsert(pricelistpart);
					await Task.Delay(100);
					Progress += (double)1 / processes;
				}

				if (customerpricelist != null && customerpricelist.Count > 0)
				{
					DeleteAndInsert(customerpricelist);
					await Task.Delay(100);
					Progress += (double)1 / processes;
				}

                Message = "Proceso finalizado...";
                Progress = 1;

				IsRunning = false;
				IsEnabled = true;

				await dialogService.ShowMessage(
					"Confirmación",
					"Proceso finalizado con éxito.");
				await navigationService.Back();
			}
			catch (System.Exception ex)
			{

				Message = ex.Message; ;
			}
		}

		void DeleteAndInsert<T>(List<T> list) where T : class
		{
			using (var da = new DataAccess())
			{
				var oldRecords = da.GetList<T>(false);
				foreach (var oldRecord in oldRecords)
				{
					da.Delete(oldRecord);
				}

				foreach (var record in list)
				{
					da.Insert(record);
				}
			}
		}

		async Task<List<T>> DownloadMaster<T>(string url, string controller) where T : class
		{
			var response = await apiService.GetList<T>(url, controller);
			if (!response.IsSuccess)
			{
				Message = response.Message;
			}

			return (List<T>)response.Result;
		}
		#endregion
	}
}