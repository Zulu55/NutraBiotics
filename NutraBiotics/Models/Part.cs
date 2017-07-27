namespace NutraBiotics.Models
{
    using GalaSoft.MvvmLight.Command;
    using NutraBiotics.Services;
    using NutraBiotics.ViewModels;
    using SQLite.Net.Attributes;
    using System.Windows.Input;

    public class Part
	{
        #region Services
        NavigationService navigationService;
        #endregion

        #region Properties
        [PrimaryKey]
        public int PartId { get; set; }

        public string PartNum { get; set; }

        public string PartDescription { get; set; }

        public string Picture { get; set; }
        #endregion

        #region Constructor
        public Part()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return PartId;
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
            newOrderViewModel.Part = this;
            await navigationService.Back();
        }
        #endregion
    }
}