namespace NutraBiotics.Models
{
    using System;
    using System.Collections.Generic;
    using SQLite.Net.Attributes;
    using NutraBiotics.Services;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using NutraBiotics.ViewModels;

    public class PriceListPart
	{
        #region Services
        NavigationService navigationService;
        #endregion

        #region Properties
        [PrimaryKey]
        public int PriceListPartId { get; set; }

        public int PriceListId { get; set; } 

        public string ListCode { get; set; }

        public int PartId { get; set; } 

        public string PartNum { get; set; }

        public string PartDescription { get; set; }

        public decimal BasePrice { get; set; }

        #endregion

        #region Constructor
        public PriceListPart()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return PriceListPartId;
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
            newOrderViewModel.PriceListPart = this;
            await navigationService.Back();
        }
        #endregion
    }
}