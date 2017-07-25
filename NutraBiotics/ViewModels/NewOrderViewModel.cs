namespace NutraBiotics.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;

    public class NewOrderViewModel : INotifyPropertyChanged
    {
		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Services
		NavigationService navigationService;
		DataService dataService;
        DialogService dialogService;
        #endregion

        #region Attributes
        bool _isRunning;
		Customer _customer;
		ShipTo _shipTo;
		Contact _contact;
		#endregion

		#region Properties
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

        public Customer Customer
        {
			set
			{
				if (_customer != value)
				{
					_customer = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Customer)));
				}
			}
			get
			{
				return _customer;
			}
		}

		public ShipTo ShipTo
		{
			set
			{
				if (_shipTo != value)
				{
					_shipTo = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShipTo)));
				}
			}
			get
			{
				return _shipTo;
			}
		}

		public Contact Contact
		{
			set
			{
				if (_contact != value)
				{
					_contact = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Contact)));
				}
			}
			get
			{
				return _contact;
			}
		}
		#endregion

		#region Constructors
		public NewOrderViewModel()
        {
            instance = this;

            navigationService = new NavigationService();
            dialogService = new DialogService();
            dataService = new DataService();
        }
		#endregion

		#region Singleton
		static NewOrderViewModel instance;

		public static NewOrderViewModel GetInstance()
		{
			return instance;
		}
		#endregion

		#region Commands
		public ICommand SearchCustomerCommand
        {
            get { return new RelayCommand(SearchCustomer); }
        }

        async void SearchCustomer()
        {
            IsRunning = true;
            var customers = dataService.Get<Customer>(false);
            IsRunning = false;

            if (customers == null || customers.Count == 0)
            {
                await dialogService.ShowMessage(
                    "Error", 
                    "Debes descargar maestros.");
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.SearchCustomer = new SearchCustomerViewModel(customers);
            await navigationService.Navigate("SearchCustomerPage");
        }

        public ICommand SearchShipToCommand
        {
			get { return new RelayCommand(SearchShipTo); }
		}

        async void SearchShipTo()
        {
            if (Customer == null)
            {
				await dialogService.ShowMessage(
					"Error",
					"Primero debes seleccionar un cliente.");
				return;
			}

			IsRunning = true;
			var shipToes = dataService
                .Get<ShipTo>(false)
                .Where(s => s.CustomerId == Customer.CustomerId)
                .ToList();
			IsRunning = false;

			if (shipToes == null || shipToes.Count == 0)
			{
				await dialogService.ShowMessage(
					"Error",
					"Debes descargar maestros o el cliente no tiene sucursales asociadas.");
				return;
			}

			var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.SearchShipTo = new SearchShipToViewModel(shipToes);
			await navigationService.Navigate("SearchShipToPage");
		}

        public ICommand SearchContactCommand
		{
			get { return new RelayCommand(SearchContact); }
		}

		async void SearchContact()
		{
			try
			{
				if (ShipTo == null)
				{
					await dialogService.ShowMessage(
						"Error",
						"Primero debes seleccionar un domicilio de embarque.");
					return;
				}

				IsRunning = true;
				var contacts = dataService
					.Get<Contact>(false)
					.Where(s => s.ShipToId == ShipTo.ShipToId)
					.ToList();

				IsRunning = false;
				if (contacts == null || contacts.Count == 0)
				{
					await dialogService.ShowMessage(
						"Error",
						"Debes descargar maestros o el domicilio de embarque no tiene contactos asociados.");
					return;
				}

				var mainViewModel = MainViewModel.GetInstance();
				mainViewModel.SearchContact = new SearchContactViewModel(contacts);
				await navigationService.Navigate("SearchContactPage");
			}
			catch (Exception ex)
			{
				await dialogService.ShowMessage(
						"Error",
						ex.Message);
				return;
			}
		}
        #endregion
    }
}
