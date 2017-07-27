namespace NutraBiotics.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Services;

    public class SearchCustomerViewModel : INotifyPropertyChanged
    {
		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Services
		DialogService dialogService;
		NavigationService navigationService;
        #endregion

        #region Attributes
        bool _isRefreshing;
        ObservableCollection<Grouping<string, Customer>> _customers;
        List<Customer> customers;
        string _filter;
        #endregion

        #region Properties
        public ObservableCollection<Grouping<string, Customer>> Customers
        {
			set
			{
				if (_customers != value)
				{
					_customers = value;
					PropertyChanged?.Invoke(
						this,
						new PropertyChangedEventArgs(nameof(Customers)));
				}
			}
			get
			{
				return _customers;
			}
		}

        public bool IsRefreshing
        {
        	set
        	{
                if (_isRefreshing != value)
        		{
        			_isRefreshing = value;
        			PropertyChanged?.Invoke(
                        this, 
                        new PropertyChangedEventArgs(nameof(IsRefreshing)));
        		}
        	}
        	get
        	{
        		return _isRefreshing;
        	}
        }

        public string Filter
        {
			set
			{
				if (_filter != value)
				{
					_filter = value;
                    if (string.IsNullOrEmpty(_filter))
                    {
                        ReloadCustomer();
                    }
                    else
                    {
                        Search();
                    }

                    PropertyChanged?.Invoke(
						this,
						new PropertyChangedEventArgs(nameof(Filter)));
				}
			}
			get
			{
				return _filter;
			}

		}
        #endregion

		#region Constructors
        public SearchCustomerViewModel(List<Customer> customers)
        {
            this.customers = customers;

			dialogService = new DialogService();
			navigationService = new NavigationService();

            ReloadCustomer();
		}
        #endregion

        #region Methods
        void ReloadCustomer()
        {
            Customers = new ObservableCollection<Grouping<string, Customer>>(
            	customers
            	.OrderBy(c => c.Names)
            	.GroupBy(c => c.Names[0].ToString(), c => c)
            	.Select(g => new Grouping<string, Customer>(g.Key, g)));
		}
        #endregion

        #region Commands
        public ICommand SearchCommand
        {
            get { return new RelayCommand(Search); }
        }

        void Search()
        {
			Customers = new ObservableCollection<Grouping<string, Customer>>(
				customers
                .Where(c => c.Names.ToLower().Contains(Filter.ToLower()))
				.OrderBy(c => c.Names)
				.GroupBy(c => c.Names[0].ToString(), c => c)
				.Select(g => new Grouping<string, Customer>(g.Key, g)));
		}
        #endregion
    }
}