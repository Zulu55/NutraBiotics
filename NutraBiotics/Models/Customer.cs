namespace NutraBiotics.Models
{
    using System.Collections.Generic;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;
    using ViewModels;

    public class Customer
    {
        #region Services
        NavigationService navigationService;
        #endregion

        #region Properties
        [PrimaryKey]
        public int CustomerId { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string PhoneNum { get; set; }

        public string Names { get; set; }

        public string LastNames { get; set; }

        public bool CreditHold { get; set; }

        public string TermsCode { get; set; }

        public string Terms { get; set; }

		[OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
		public List<OrderHeader> OrderHeaders { get; set; }
		#endregion

		#region Constructor
		public Customer()
        {
            navigationService = new NavigationService();
        }
		#endregion

		#region Methods
		public override int GetHashCode()
		{
			return CustomerId;
		}
		#endregion

		#region Commands
		public ICommand SelectRecordCommand
        {
            get { return new RelayCommand(SelectRecord); }
        }

        async void SelectRecord()
        {
            var newOrderViewModel = NewOrderViewModel.GetInstance();
            newOrderViewModel.Customer = this;
            newOrderViewModel.LoadPriceLists();
            await navigationService.Back();
        }
        #endregion
    }
}
