namespace NutraBiotics.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
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
        PriceListPart _pricelistpart;
        ShipTo _shipTo;
		Contact _contact;
        Part _part;
        string _partNum;
		ObservableCollection<PriceList> _priceLists;
		ObservableCollection<GridOrderDetail> _gridOrderDetails;
		int _priceListId;
        double _quantity;
        double _discount;
        decimal _total;
		#endregion

		#region Properties
		public decimal Total
		{
			set
			{
				if (_total != value)
				{
					_total = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Total)));
				}
			}
			get
			{
				return _total;
			}
		}

		public ObservableCollection<GridOrderDetail> GridOrderDetails
		{
			set
			{
				if (_gridOrderDetails != value)
				{
					_gridOrderDetails = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GridOrderDetails)));
				}
			}
			get
			{
				return _gridOrderDetails;
			}
		}

		public string PartNum
		{
			set
			{
				if (_partNum != value)
				{
					_partNum = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PartNum)));
				}
			}
			get
			{
				return _partNum;
			}
		}

		public double Quantity
		{
			set
			{
				if (_quantity != value)
				{
					_quantity = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Quantity)));
				}
			}
			get
			{
				return _quantity;
			}
		}

		public double Discount
		{
			set
			{
				if (_discount != value)
				{
					_discount = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Discount)));
				}
			}
			get
			{
				return _discount;
			}
		}

		public int PriceListId
		{
			set
			{
				if (_priceListId != value)
				{
					_priceListId = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PriceListId)));
				}
			}
			get
			{
				return _priceListId;
			}
		}

		public ObservableCollection<PriceList> PriceLists
		{
			set
			{
				if (_priceLists != value)
				{
					_priceLists = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PriceLists)));
				}
			}
			get
			{
				return _priceLists;
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

        public PriceListPart PriceListPart
        {
            set
            {
                if (_pricelistpart != value)
                {
                    _pricelistpart = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PriceListPart)));
                }
            }
            get
            {
                return _pricelistpart;
            }
        }

        public Part Part
		{
			set
			{
				if (_part != value)
				{
					_part = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Part)));
				}
			}
			get
			{
				return _part;
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
            PriceLists = new ObservableCollection<PriceList>();

            GridOrderDetails = new ObservableCollection<GridOrderDetail>();

			Quantity = 1;
		}
		#endregion

		#region Singleton
		static NewOrderViewModel instance;

		public static NewOrderViewModel GetInstance()
		{
			return instance;
		}
        #endregion

        #region Methods
        public void LoadPriceLists()
        {
            var customerPriceLists = dataService
                .Get<CustomerPriceList>(false)
                .Where(cpl => cpl.CustomerId == Customer.CustomerId)
                .ToList();

            var priceLists = dataService.Get<PriceList>(false).ToList();

            var qry = (from cpl in customerPriceLists
                       join pl in priceLists on cpl.PriceListId equals pl.PriceListId
                       where pl.Active
                       select new { pl }).ToList();

            var list = qry.Select(q => new PriceList
            {
                Active = q.pl.Active,
                ListCode = q.pl.ListCode,
                ListDescription = q.pl.ListDescription,
                PriceListId = q.pl.PriceListId,
            }).ToList();

            PriceLists = new ObservableCollection<PriceList>(list);
        }

        public async Task PartCompleted()
		{
            var pricelistpart = dataService
                   .Get<PriceListPart>(false)
                   .Where(s => s.PriceListId == PriceListId && s.PartNum == PartNum)
                   .FirstOrDefault();
            
            if (pricelistpart == null)
            {
                await dialogService.ShowMessage(
                    "Error",
                    "Codigo erroneo para esta lista de precios");
                PriceListPart = null;
                return;
            }

            PartNum = pricelistpart.PartNum;
            PriceListPart = pricelistpart;
        }
		#endregion                                         

		#region Commands
        public ICommand AddProductToOrderCommand
        {
			get { return new RelayCommand(AddProductToOrder); }
		}

        async void AddProductToOrder()
        {
            if (PriceListPart == null)
			{
				await dialogService.ShowMessage(
					"Error",
					"Debe ingresar un producto.");
				return;
			}

			if (Quantity <= 0)
			{
				await dialogService.ShowMessage(
					"Error",
					"Debe ingresar una cantidad mayor a cero.");
				return;
			}

            GridOrderDetails.Add(new GridOrderDetail
            {
                BasePrice = PriceListPart.BasePrice,
                Discount = Discount / 100,
                PartDescription = PriceListPart.PartDescription,
                PartNum = PriceListPart.PartNum,
                PartId = PriceListPart.PartId,
                Quantity = Quantity,
            });

            PartNum = null;
            PriceListPart = null;
            Quantity = 1;
            Discount = 0;
            Total = GridOrderDetails.Sum(god => god.Value);
		}

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

        public ICommand SearchPriceListPartCommand
        {
            get { return new RelayCommand(SearchPriceListPart); }
        }

        async void SearchPriceListPart()
        {
            try
            {
                if (PriceLists == null)
                {
                    await dialogService.ShowMessage(
                        "Error",
                        "Primero debes seleccionar una lista de precios.");
                    return;
                }

                IsRunning = true;

                var pricelistparts = dataService
                    .Get<PriceListPart>(false)
                    .Where(s => s.PriceListId == PriceListId)
                    .ToList();
                
                IsRunning = false;

                if (pricelistparts == null)
                {
                    await dialogService.ShowMessage(
                        "Error",
                        "Debes descargar maestros o la lista de precios no tiene partes asociadas aun.");
                    return;
                }

                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.SearchPriceListPart = new SearchPriceListPartViewModel(pricelistparts);
                await navigationService.Navigate("SearchPriceListPartPage");
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
