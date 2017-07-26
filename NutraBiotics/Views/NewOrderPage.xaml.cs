
namespace NutraBiotics.Views
{
    using System;
    using ViewModels;
    using Xamarin.Forms;

    public partial class NewOrderPage : ContentPage
    {
        public NewOrderPage()
        {
            InitializeComponent();
        }

        async void OnPartComplete(object sender, EventArgs e)
        {
            var newOrderViewModel = NewOrderViewModel.GetInstance();
            await newOrderViewModel.PartCompleted();
        }
    }
}
