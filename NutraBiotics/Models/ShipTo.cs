namespace NutraBiotics.Models
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using ViewModels;
    using SQLite.Net.Attributes;

	public class ShipTo
    {
		#region Services
		NavigationService navigationService;
		#endregion

		#region Properties
		[PrimaryKey]
		public int ShipToId { get; set; }
		
        public int CustomerId { get; set; }
		
        public string ShipToName { get; set; }
		
        public string Country { get; set; }
		
        public string State { get; set; }
		
        public string City { get; set; }
		
        public string Address { get; set; }
		
        public string PhoneNum { get; set; }
		
        public string Email { get; set; }
		#endregion

		#region Constructor
		public ShipTo()
		{
			navigationService = new NavigationService();
		}
		#endregion

		#region Methods
		public override int GetHashCode()
        {
            return ShipToId;
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
			newOrderViewModel.ShipTo = this;
			await navigationService.Back();
		}
		#endregion
	}
}
