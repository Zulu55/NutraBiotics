namespace NutraBiotics
{
    using Views;
	using Xamarin.Forms;

	public partial class App : Application
    {
        public static NavigationPage Navigator
        {
            get;
            set;
        }

        public App()
        {
            InitializeComponent();

			MainPage = new LoginPage();
			//MainPage = new MasterPage();
		}

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
    }
}
