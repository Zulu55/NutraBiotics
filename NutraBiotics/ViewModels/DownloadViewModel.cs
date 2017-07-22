namespace NutraBiotics.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using Xamarin.Forms;
    using Data;

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
		#endregion

		#region Attributes
		double progress;
		bool isRunning;
		bool isEnabled;
        string message;
		#endregion

		#region Properties
		public string Message
		{
			set
			{
				if (message != value)
				{
					message = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Message)));
				}
			}
			get
			{
				return message;
			}
		}

		public bool IsRunning
		{
			set
			{
				if (isRunning != value)
				{
					isRunning = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRunning)));
				}
			}
			get
			{
				return isRunning;
			}
		}

		public bool IsEnabled
		{
			set
			{
				if (isEnabled != value)
				{
					isEnabled = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
				}
			}
			get
			{
				return isEnabled;
			}
		}

		public double Progress
		{
			set
			{
				if (progress != value)
				{
					progress = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Progress)));
				}
			}
			get
			{
				return progress;
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
            IsRunning = true;
            IsEnabled = false;

            Progress = 0;
			var url = Application.Current.Resources["URLAPI"].ToString();

			Message = "Descargando clientes...";
			var customers = await DownloadMaster<Customer>(url, "/api/Customers");

			Message = "Descargando sucursales...";
			var shipTos = await DownloadMaster<ShipTo>(url, "/api/ShipToes");

			Message = "Descargando contactos...";
			var contacts = await DownloadMaster<Contact>(url, "/api/Contacts");

            var records = customers.Count + shipTos.Count + contacts.Count;
			
            Message = "Guardando clientes localmente...";

            if (customers != null && customers.Count > 0)
            {
                dataService.DeleteAll<Contact>();
                foreach (var customer in customers)
                {
					dataService.Insert(customer);
					Progress += 1 / records;
                }
            }

			Message = "Guardando sucursales localmente...";

			if (shipTos != null && shipTos.Count > 0)
			{
                dataService.DeleteAll<ShipTo>();
				foreach (var shipTo in shipTos)
				{
					dataService.Insert(shipTos);
					Progress += 1 / records;
				}
			}


			Message = "Proceso finalizado...";

			IsRunning = false;
			IsEnabled = true;

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
