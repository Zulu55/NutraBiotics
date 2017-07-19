namespace NutraBiotics.ViewModels
{
    using System.Collections.ObjectModel;

	public class MainViewModel
    {
        public LoginViewModel Login
        {
            get;
            set;
        }

        public ObservableCollection<MenuItemViewModel> Menu
        {
            get;
            set;
        }

        public MainViewModel()
        {
            Login = new LoginViewModel();

            Menu = new ObservableCollection<MenuItemViewModel>();
            LoadMenu();
        }

        void LoadMenu()
        {
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
    }
}
