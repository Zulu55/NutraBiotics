namespace NutraBiotics.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Services;

	public class MenuItemViewModel
    {
		#region Attributes
		NavigationService navigationService;
		DataService dataService;
		#endregion

		#region Properties
		public string Icon
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string PageName
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public MenuItemViewModel()
        {
            navigationService = new NavigationService();
            dataService = new DataService();
        }
        #endregion

        #region Commands
        public ICommand NavigateCommand
        {
            get { return new RelayCommand(Navigate); }
        }

        void Navigate()
        {
            if (PageName == "LoginPage")
            {
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.User.IsRemembered = false;
                dataService.Update(mainViewModel.User);
                navigationService.SetMainPage("LoginPage");
            }
        }
        #endregion
    }
}
