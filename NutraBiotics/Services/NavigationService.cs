namespace NutraBiotics.Services
{
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
    }
}
