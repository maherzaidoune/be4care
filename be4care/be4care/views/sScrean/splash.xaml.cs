using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace be4care.views.sScrean
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class splash : ContentPage
    { 
		public splash()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await image.ScaleTo(0.8, 1000, Easing.Linear);
            await image.ScaleTo(1, 200, Easing.Linear);
            await image.ScaleTo(1.2, 1300, Easing.Linear);
            await Navigation.PushAsync(new views.onBoarding.onBoarding());
        }
    }
}