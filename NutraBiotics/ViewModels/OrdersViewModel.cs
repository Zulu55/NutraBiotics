namespace NutraBiotics.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using Services;
    using Models;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;

    public class OrdersViewModel : INotifyPropertyChanged
    {
		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Services
		DataService dataService;
		#endregion

		#region Attributes
		bool _isRefreshing;
		ObservableCollection<OrderHeader> _orderList;
		#endregion

		#region Properties
		public ObservableCollection<OrderHeader> OrderList
		{
			set
			{
				if (_orderList != value)
				{
					_orderList = value;
					PropertyChanged?.Invoke(
						this,
						new PropertyChangedEventArgs(nameof(OrderList)));
				}
			}
			get
			{
				return _orderList;
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
        #endregion

        #region Constructors
        public OrdersViewModel()
        {
            instance = this;

            dataService = new DataService();
        }
		#endregion

		#region Singleton
		static OrdersViewModel instance;

		public static OrdersViewModel GetInstance()
		{
			if (instance == null)
			{
				return new OrdersViewModel();
			}

			return instance;
		}
		#endregion

		#region Methods
        public void RefreshOrders()
        {
            var orders = dataService.Get<OrderHeader>(true);
            OrderList = new ObservableCollection<OrderHeader>(orders);
        }
        #endregion
    }
}
