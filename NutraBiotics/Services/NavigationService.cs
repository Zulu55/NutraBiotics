﻿namespace NutraBiotics.Services
{
    using System.Threading.Tasks;
    using Views;
    using Xamarin.Forms;

	public class NavigationService
    {
        public void SetMainPage(string pageName)
        {
            switch (pageName)
            {
				case "MasterPage":
                    Application.Current.MainPage = new MasterPage();
					break;
				case "LoginPage":
					Application.Current.MainPage = new LoginPage();
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
				case "NewOrderTab":
					await App.Navigator.PushAsync(new NewOrderTab());
					break;
				case "SearchCustomerPage":
					await App.Navigator.PushAsync(new SearchCustomerPage());
					break;
				case "SearchShipToPage":
					await App.Navigator.PushAsync(new SearchShipToPage());
					break;
				case "SearchContactPage":
					await App.Navigator.PushAsync(new SearchContactPage());
					break;

                case "SearchPriceListPartPage":
                    await App.Navigator.PushAsync(new SearchPriceListPartPage());
                    break;
            }
        }

        public async Task Back()
        {
            await App.Navigator.PopAsync();
        }
    }
}
