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

    public class SearchShipToViewModel : INotifyPropertyChanged
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
		ObservableCollection<Grouping<string, ShipTo>> _shipToes;
		List<ShipTo> shipToes;
		string _filter;
		#endregion

		#region Properties
		public ObservableCollection<Grouping<string, ShipTo>> ShipToes
		{
			set
			{
				if (_shipToes != value)
				{
					_shipToes = value;
					PropertyChanged?.Invoke(
						this,
						new PropertyChangedEventArgs(nameof(ShipToes)));
				}
			}
			get
			{
				return _shipToes;
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
						ReloadShipToes();
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
		public SearchShipToViewModel(List<ShipTo> shipToes)
        {
			this.shipToes = shipToes;

			dialogService = new DialogService();
			navigationService = new NavigationService();

			ReloadShipToes();
		}
		#endregion

		#region Methods
		void ReloadShipToes()
		{
			ShipToes = new ObservableCollection<Grouping<string, ShipTo>>(
				shipToes
				.OrderBy(s => s.ShipToName)
				.GroupBy(s => s.ShipToName[0].ToString(), s => s)
				.Select(g => new Grouping<string, ShipTo>(g.Key, g)));
		}
		#endregion

		#region Commands
		public ICommand SearchCommand
		{
			get { return new RelayCommand(Search); }
		}

		void Search()
		{
			ShipToes = new ObservableCollection<Grouping<string, ShipTo>>(
				shipToes
                .Where(s => s.ShipToName.ToLower().Contains(Filter.ToLower()))
				.OrderBy(s => s.ShipToName)
				.GroupBy(s => s.ShipToName[0].ToString(), s => s)
				.Select(g => new Grouping<string, ShipTo>(g.Key, g)));
		}
		#endregion
	}
}
