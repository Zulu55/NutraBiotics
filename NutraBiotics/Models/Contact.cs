namespace NutraBiotics.Models
{
	using System.Windows.Input;
	using GalaSoft.MvvmLight.Command;
	using Services;
	using ViewModels;
	using SQLite.Net.Attributes;

	public class Contact
	{
		#region Services
		NavigationService navigationService;
		#endregion

		#region Properties
		[PrimaryKey]
		public int ContactId { get; set; }

		public int ShipToId { get; set; }

		public string Name { get; set; }

		public string Address1 { get; set; }

		public string PhoneNum { get; set; }

		public string Email { get; set; }
		#endregion

		#region Constructor
		public Contact()
		{
			navigationService = new NavigationService();
		}
		#endregion

		#region Methods
		public override int GetHashCode()
		{
			return ContactId;
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
			newOrderViewModel.Contact = this;
			await navigationService.Back();
		}
		#endregion
	}
}