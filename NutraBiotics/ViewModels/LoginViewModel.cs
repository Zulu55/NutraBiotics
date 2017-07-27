namespace NutraBiotics.ViewModels
{
    using System.ComponentModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using Xamarin.Forms;
    using Plugin.DeviceInfo;

    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        DialogService dialogService;
        ApiService apiService;
        NetService netService;
		NavigationService navigationService;
		DataService dataService;
		#endregion

		#region Attributes
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
            apiService = new ApiService();
            netService = new NetService();
            navigationService = new NavigationService();
            dataService = new DataService();

            IsRemembered = true;
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
                Password = null;
                return;
            }

			if (string.IsNullOrEmpty(Password))
			{
				await dialogService.ShowMessage("Error", "Debes ingresar una contraseña.");
				Password = null;
				return;
			}

            var connection = await netService.CheckConnectivity();
            if (!connection.IsSuccess)
            {
				await dialogService.ShowMessage("Error", connection.Message);
				return;
			}

			IsRunning = true;
			IsEnabled = false;

            var url = Application.Current.Resources["URLAPI"].ToString();
            var response = await apiService.Login(url, "/api/users/login", 
                Email, Password);

            IsRunning = false;
			IsEnabled = true;

            if (!response.IsSuccess)
            {
				await dialogService.ShowMessage("Error", response.Message);
				Password = null;
				return;
			}

            Email = null;
            Password = null;
			
            var user = (User)response.Result;
            user.IsRemembered = IsRemembered;
            user.Password = Password;
            dataService.DeleteAllAndInsert(user);

            var deviceId = CrossDevice.Hardware.DeviceId;

            await dialogService.ShowMessage(
                "Nutrabiotics", 
                string.Format(
                    "Bienvenido {0}, su IMEI es: {1}", 
                    user.FullName,
                    deviceId));

			var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.User = user;

            navigationService.SetMainPage("MasterPage");
		}
        #endregion
    }
}