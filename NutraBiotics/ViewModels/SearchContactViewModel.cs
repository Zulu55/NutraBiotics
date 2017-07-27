namespace NutraBiotics.ViewModels
{
    using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows.Input;
	using GalaSoft.MvvmLight.Command;
	using NutraBiotics.Helpers;
	using NutraBiotics.Models;
	using NutraBiotics.Services;

	public class SearchContactViewModel : INotifyPropertyChanged
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
		ObservableCollection<Grouping<string, Contact>> _contacts;
		List<Contact> contacts;
		string _filter;
		#endregion

		#region Properties
		public ObservableCollection<Grouping<string, Contact>> Contacts
		{
			set
			{
				if (_contacts != value)
				{
					_contacts = value;
					PropertyChanged?.Invoke(
						this,
						new PropertyChangedEventArgs(nameof(Contacts)));
				}
			}
			get
			{
				return _contacts;
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
						ReloadContacts();
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
		public SearchContactViewModel(List<Contact> contacts)
		{
			this.contacts = contacts;
			dialogService = new DialogService();
			navigationService = new NavigationService();
			ReloadContacts();
		}
		#endregion

		#region Commands
		public ICommand SearchCommand
		{
			get { return new RelayCommand(Search); }
		}

		void Search()
		{
			Contacts = new ObservableCollection<Grouping<string, Contact>>(
				 contacts
				.Where(s => s.Name.ToLower().Contains(Filter.ToLower()))
				.OrderBy(s => s.Name)
				.GroupBy(s => s.Name[0].ToString(), s => s)
				.Select(g => new Grouping<string, Contact>(g.Key, g)));
		}
		#endregion

		#region Methods
		void ReloadContacts()
		{
			Contacts = new ObservableCollection<Grouping<string, Contact>>(
			contacts
			.OrderBy(s => s.Name)
			.GroupBy(s => s.Name[0].ToString(), s => s)
			.Select(g => new Grouping<string, Contact>(g.Key, g)));
		}
		#endregion
	}
}