﻿namespace NutraBiotics.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;

    public class MainViewModel
    {
		#region Services
		NavigationService navigationService;
		DialogService dialogService;
		DataService dataService;
		#endregion

		#region Properties
		public User User
        {
            get;
            set;
        }

        public LoginViewModel Login
        {
            get;
            set;
        }

        public DownloadViewModel Download
        {
            get;
            set;
        }

        public NewOrderViewModel NewOrder
        {
            get;
            set;
        }

        public SearchCustomerViewModel SearchCustomer
        {
            get;
            set;
        }

        public SearchShipToViewModel SearchShipTo
        {
            get;
            set;
        }

		public SearchContactViewModel SearchContact
		{
			get;
			set;
		}

        public SearchPriceListPartViewModel SearchPriceListPart
        {
            get;
            set;
        }

        public OrdersViewModel Orders
        {
            get;
            set;
        }

        public ObservableCollection<MenuItemViewModel> Menu
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;

            navigationService = new NavigationService();
            dialogService = new DialogService();
            dataService = new DataService();

            Login = new LoginViewModel();

            Menu = new ObservableCollection<MenuItemViewModel>();
            LoadMenu();
        }
        #endregion

        #region Singleton
        static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion

        #region Methods
        void LoadMenu()
        {
			Menu.Add(new MenuItemViewModel
			{
				Icon = "ic_cloud_download.png",
				PageName = "DownloadPage",
				Title = "Descargar Maestros",
			});

			Menu.Add(new MenuItemViewModel
			{
				Icon = "ic_people.png",
				PageName = "CustomersPage",
				Title = "Clientes",
			});

			Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_settings.png",
                PageName = "SettingsPage",
                Title = "Configuración",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_monetization_on.png",
                PageName = "PortfoliosPage",
                Title = "Cartera",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_contact_phone.png",
                PageName = "ContactsPage",
                Title = "Contactos",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_exit_to_app.png",
                PageName = "LoginPage",
                Title = "Cerrar Sesión",
            });
        }
        #endregion

        #region Commands
        public ICommand SaveOrderCommand
        {
			get { return new RelayCommand(SaveOrder); }
		}

        async void SaveOrder()
        {
			if (NewOrder.Customer == null)
			{
				await dialogService.ShowMessage(
					"Error",
					"Debes de seleccionar un cliente.");
				return;
			}

			if (NewOrder.ShipTo == null)
			{
				await dialogService.ShowMessage(
					"Error",
					"Debes de seleccionar una sucursal.");
				return;
			}

			if (NewOrder.Contact == null)
			{
				await dialogService.ShowMessage(
					"Error",
					"Debes de seleccionar un contacto.");
				return;
			}

			if (NewOrder.GridOrderDetails.Count == 0)
			{
				await dialogService.ShowMessage(
					"Error",
					"Debes adicionar detalle.");
				return;
			}

			var answer = await dialogService.ShowConfirm(
				"Confirmación",
				"¿Está seguro de guardar la orden?");

            if (!answer)
            {
                return;
            }

            GoSaveOrder();
            ClearOrderFields();

            await dialogService.ShowMessage(
                "Confirmación", 
                "Orden guardada localmente OK");
		}

        void ClearOrderFields()
        {
			NewOrder.Customer = null;
			NewOrder.ShipTo = null;
			NewOrder.Contact = null;
			NewOrder.PriceListId = 0;
			NewOrder.Total = 0;
			NewOrder.Part = null;
			NewOrder.PartNum = null;
			NewOrder.GridOrderDetails.Clear();
		}

        void GoSaveOrder()
        {
            try
            {
                var orderHeader = new OrderHeader
                {
                    ContactId = NewOrder.Contact.ContactId,
                    CreditHold = NewOrder.Customer.CreditHold,
                    CustomerId = NewOrder.Customer.CustomerId,
                    Date = DateTime.Now,
                    IsSync = false,
                    Observations = string.Empty,
                    SalesCategory = string.Empty,
                    ShipToId = NewOrder.ShipTo.ShipToId,
                    TermsCode = NewOrder.Customer.TermsCode,
                    UserId = User.UserId,
                };

                dataService.Insert(orderHeader);
                int i = 0;

                foreach (var detail in NewOrder.GridOrderDetails)
                {
                    var orderDetail = new OrderDetail
                    {
                        Discount = detail.Discount,
                        OrderLine = ++i,
                        OrderQty = detail.Quantity,
                        PartId = detail.PartId,
                        PartNum = detail.PartNum,
                        SalesOrderHeaderId = orderHeader.SalesOrderHeaderId,
                        TaxAmt = 0,
                        UnitPrice = detail.BasePrice,
                    };

                    dataService.Insert(orderDetail);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public ICommand DeleteOrderCommand
        {
			get { return new RelayCommand(DeleteOrder); }
		}

        async void DeleteOrder()
        {
            if (NewOrder.GridOrderDetails.Count == 0)
            {
                return;
            }

            var answer = await dialogService.ShowConfirm(
                "Confirmación", 
                "¿Está seguro de borrar lo que lleva de la orden?");

            if (answer)
            {
                NewOrder.Total = 0;
                NewOrder.GridOrderDetails.Clear();
            }
        }

        public ICommand NewOrderCommand
        {
            get { return new RelayCommand(GotoNewOrder); }
        }

        async void GotoNewOrder()
        {
            NewOrder = new NewOrderViewModel();
            await navigationService.Navigate("NewOrderTab");
        }
        #endregion
    }
}
