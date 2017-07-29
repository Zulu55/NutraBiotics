namespace NutraBiotics.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;
    using Services;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using ViewModels;

    public class OrderHeader
	{
        #region Services
        DialogService dialogService;
        DataService dataService;
        #endregion

        #region Properties
        [PrimaryKey, AutoIncrement]
		public int SalesOrderHeaderId { get; set; }

		public int UserId { get; set; }

		[ForeignKey(typeof(Customer))]
		public int CustomerId { get; set; }

		public bool CreditHold { get; set; }

		public DateTime Date { get; set; }

		public string TermsCode { get; set; }

		public int ShipToId { get; set; }

		public int ContactId { get; set; }

		public string SalesCategory { get; set; }

		public string Observations { get; set; }

		public bool IsSync { get; set; }

		[OneToMany(CascadeOperations = CascadeOperation.All)]
		public List<OrderDetail> OrderDetails { get; set; }

		[ManyToOne]
		public Customer Customer { get; set; }

		public decimal Total
		{
			get
			{
				if (OrderDetails == null)
				{
					return 0;
				}

				return OrderDetails.Sum(od => od.Value);
			}
		}
        #endregion

        #region Constructors
        public OrderHeader()
        {
            dialogService = new DialogService();
            dataService = new DataService();
        }
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return SalesOrderHeaderId;
        }
        #endregion

        #region Commands
        public ICommand DeleteOrderCommand
        {
            get { return new RelayCommand(DeleteOrder); }
        }

        async void DeleteOrder()
        {
            if (IsSync)
            {
                await dialogService.ShowMessage(
                    "Error", 
                    "No puedes borrar un pedido ya sincronizado.");
                return;
            }

            var answer = await dialogService.ShowConfirm(
                "Confirmación", 
                "¿Está seguro de borrar el pedido?");

            if(!answer)
            {
                return;
            }

            dataService.Delete(this);
            var ordersViewModel = OrdersViewModel.GetInstance();
            ordersViewModel.OrderList.Remove(this);
        }
        #endregion
    }
}