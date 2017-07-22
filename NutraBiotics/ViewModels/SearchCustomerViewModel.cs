namespace NutraBiotics.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class SearchCustomerViewModel : INotifyPropertyChanged
    {
		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Services
		DialogService dialogService;
		ApiService apiService;
		NetService netService;
		NavigationService navigationService;
        #endregion

        #region Attributes
        bool isRefreshing;
        #endregion

        #region Properties
        public ObservableCollection<CustomerItemViewModel> Customers
		{
			get;
			set;
		}

		public bool IsRefreshing
		{
			set
			{
                if (isRefreshing != value)
				{
					isRefreshing = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRefreshing)));
				}
			}
			get
			{
				return isRefreshing;
			}
		}
		#endregion

		#region Constructors
		public SearchCustomerViewModel()
        {
			dialogService = new DialogService();
			apiService = new ApiService();
			netService = new NetService();
			navigationService = new NavigationService();
			
            Customers = new ObservableCollection<CustomerItemViewModel>();

            LoadCustomers();
        }
        #endregion

        #region Methods
        async Task LoadCustomers()
        {
			var connection = await netService.CheckConnectivity();
			if (!connection.IsSuccess)
			{
				await dialogService.ShowMessage("Error", connection.Message);
				return;
			}

            IsRefreshing = true;

			var url = Application.Current.Resources["URLAPI"].ToString();
            var controller = "/api/Customers";
			var response = await apiService.GetList<Customer>(url, controller);
			if (!response.IsSuccess)
			{
				IsRefreshing = false;
				await dialogService.ShowMessage("Error", response.Message);
				return;
			}

            ReloadCustomer((List<Customer>)response.Result);

            IsRefreshing = false;
		}

        void ReloadCustomer(List<Customer> customers)
        {
            Customers.Clear();
            foreach (var customer in customers)
            {
                Customers.Add(new CustomerItemViewModel 
                { 
                    Address = customer.Address,
                    City = customer.City,
                    Country = customer.Country,
                    CreditHold = customer.CreditHold,
                    CustomerId = customer.CustomerId,
                    LastNames = customer.LastNames,
                    Names = customer.Names,
                    PhoneNum = customer.PhoneNum,
                    State = customer.State,
                    Terms = customer.Terms,
                    TermsCode = customer.TermsCode,
                });
            }
        }
        #endregion
    }
}
