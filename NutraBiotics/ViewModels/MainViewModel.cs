﻿namespace NutraBiotics.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;

    public class MainViewModel
    {
        #region Services
        NavigationService navigationService;
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
        public ICommand NewOrderCommand
        {
            get { return new RelayCommand(GotoNewOrder); }
        }

        async void GotoNewOrder()
        {
            NewOrder = new NewOrderViewModel();
            await navigationService.Navigate("NewOrderPage");
        }
        #endregion
    }
}
