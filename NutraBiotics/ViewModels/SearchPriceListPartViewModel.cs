namespace NutraBiotics.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using NutraBiotics.Helpers;
    using NutraBiotics.Models;
    using NutraBiotics.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;

    public class SearchPriceListPartViewModel : INotifyPropertyChanged
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
        ObservableCollection<Grouping<string, PriceListPart>> _pricelistparts;
        List<PriceListPart> pricelistparts;
        string _filter;
        #endregion

        #region Properties
        public ObservableCollection<Grouping<string, PriceListPart>> PriceListParts
        {
            set
            {
                if (_pricelistparts != value)
                {
                    _pricelistparts = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(PriceListParts)));
                }
            }
            get
            {
                return _pricelistparts;
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
                        ReloadPart();
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

        #region Constructor
        public SearchPriceListPartViewModel(List<PriceListPart> pricelistparts)
        {
            this.pricelistparts = pricelistparts;

            dialogService = new DialogService();
            navigationService = new NavigationService();
            ReloadPart();
        }

        #endregion

        #region Methods
        void ReloadPart()
        {
            PriceListParts = new ObservableCollection<Grouping<string, PriceListPart>>(
                pricelistparts
                .OrderBy(c => c.PartDescription)
                .GroupBy(c => c.PartDescription[0].ToString(), c => c)
                .Select(g => new Grouping<string, PriceListPart>(g.Key, g)));
        }

        #endregion

        #region Commands
        public ICommand SearchCommand
        {
            get { return new RelayCommand(Search); }
        }

        void Search()
        {
            PriceListParts = new ObservableCollection<Grouping<string, PriceListPart>>(
                pricelistparts
                .Where(c => c.PartDescription.ToLower().Contains(Filter.ToLower()))
                .OrderBy(c => c.PartDescription)
                .GroupBy(c => c.PartDescription[0].ToString(), c => c)
                .Select(g => new Grouping<string, PriceListPart>(g.Key, g)));
        }
        #endregion
    }
}
