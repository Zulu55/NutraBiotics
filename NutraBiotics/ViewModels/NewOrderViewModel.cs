namespace NutraBiotics.ViewModels
{
    using System;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Services;

    public class NewOrderViewModel
    {
		#region Services
		NavigationService navigationService;
        #endregion

        #region Constructors
        public NewOrderViewModel()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands
        public ICommand SearchCustomerCommand
        {
            get { return new RelayCommand(SearchCustomer); }
        }

        async void SearchCustomer()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.SearchCustomer = new SearchCustomerViewModel();
            await navigationService.Navigate("SearchCustomerPage");
        }
        #endregion
    }
}
