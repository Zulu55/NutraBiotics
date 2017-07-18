namespace NutraBiotics.ViewModels
{
    using System.ComponentModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Services;

    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        DialogService dialogService;
		string email;
		string password;
		bool isRunning;
		bool isEnabled;
		bool isRemembered;
		#endregion

		#region Properties
		public string Email
		{
			set
			{
				if (email != value)
				{
					email = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
				}
			}
			get
			{
				return email;
			}
		}

		public string Password
		{
			set
			{
				if (password != value)
				{
					password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
				}
			}
			get
			{
				return password;
			}
		}

		public bool IsRunning
		{
			set
			{
				if (isRunning != value)
				{
					isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRunning)));
				}
			}
			get
			{
				return isRunning;
			}
		}

		public bool IsEnabled
		{
			set
			{
				if (isEnabled != value)
				{
					isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
				}
			}
			get
			{
				return isEnabled;
			}
		}

		public bool IsRemembered
		{
			set
			{
				if (isRemembered != value)
				{
					isRemembered = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRemembered)));
				}
			}
			get
			{
				return isRemembered;
			}
		}
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            dialogService = new DialogService();
            IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get { return new RelayCommand(Login); }
        }

        async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar un email.");
                return;
            }

			if (string.IsNullOrEmpty(Password))
			{
				await dialogService.ShowMessage("Error", "Debes ingresar una contraseña.");
				return;
			}
		}
        #endregion

    }
}
