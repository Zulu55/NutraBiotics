namespace NutraBiotics.Models
{
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using ViewModels;

    public class GridOrderDetail
    {
		#region Services
        DialogService dialogService;
		#endregion

		#region Properties
		public int PartId { get; set; }

		public int PriceListPartId { get; set; }

		public string PartNum { get; set; }

        public string PartDescription { get; set; }

        public decimal BasePrice { get; set; }

        public double Quantity { get; set; }

        public double Discount { get; set; }

        public decimal Value
        {
            get
            {
                return BasePrice * (decimal)Quantity * (decimal)(1 - Discount);
            }
        }
        #endregion

        #region Constructors
        public GridOrderDetail()
        {
            dialogService = new DialogService();
        }
        #endregion

        #region Commands
        public ICommand DeleteProductCommand
        {
            get { return new RelayCommand(DeleteProduct); }
        }

        async void DeleteProduct()
        {
			var answer = await dialogService.ShowConfirm(
				"Confirmación",
				"¿Está seguro de borrar el registro?");

			if (answer)
			{
                var newOrderViewModel = NewOrderViewModel.GetInstance();
                newOrderViewModel.GridOrderDetails.Remove(this);
                newOrderViewModel.Total = newOrderViewModel
                    .GridOrderDetails.Sum(god => god.Value);
			}
		}
        #endregion
    }
}
