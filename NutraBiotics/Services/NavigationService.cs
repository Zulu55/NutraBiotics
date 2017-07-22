namespace NutraBiotics.Services
{
    using System.Threading.Tasks;
    using Views;

	public class NavigationService
    {
        public void SetMainPage(string pageName)
        {
            switch (pageName)
            {
				case "MasterPage":
					App.Current.MainPage = new MasterPage();
					break;
				case "LoginPage":
					App.Current.MainPage = new LoginPage();
					break;
			}
        }

        public async Task Navigate(string pageName)
        {
            App.Master.IsPresented = false;

            switch (pageName)
            {
                case "DownloadPage":
                    await App.Navigator.PushAsync(new DownloadPage());
                    break;
            }
        }
    }
}
