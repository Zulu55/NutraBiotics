namespace NutraBiotics
{
    using Views;
	using Xamarin.Forms;
    using Services;
    using Models;
    using ViewModels;

    public partial class App : Application
    {
        #region Attributes
        DataService dataService;
        #endregion

        #region Properties
        public static NavigationPage Navigator
        {
            get;
            set;
        }

        public static MasterDetailPage Master
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public App()
        {
            InitializeComponent();

			dataService = new DataService();

            var user = dataService.First<User>(false);
            if (user != null && user.IsRemembered)
            {
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.User = user;
                mainViewModel.Orders = new OrdersViewModel();
                MainPage = new MasterPage();
            }
            else
            {
				MainPage = new LoginPage();
			}
        }
        #endregion

        #region Methods
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        #endregion
    }
}
