using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace be4care.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SplashPage : ContentPage
    {
        public SplashPage()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            await image.ScaleTo(1, 500, Easing.Linear);
            await image.ScaleTo(0.8, 800, Easing.Linear);
            await image.ScaleTo(1.2, 800, Easing.Linear);
            await image.ScaleTo(1, 800, Easing.Linear);
        }
        
    }
}