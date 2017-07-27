namespace NutraBiotics.Services
{
    using System.Threading.Tasks;

	public class DialogService
    {
		public async Task ShowMessage(string title, string message)
		{
			await App.Current.MainPage.DisplayAlert(title, message, "Aceptar");
		}

		public async Task<bool> ShowConfirm(string title, string message)
		{
			return await App.Current.MainPage.DisplayAlert(title, message, "Si", "No");
		}
    }
}