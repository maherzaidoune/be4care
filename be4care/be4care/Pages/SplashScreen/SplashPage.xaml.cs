using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace be4care.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SplashPage : ContentPage
    {
        public SplashPage()
		{
            if (Device.RuntimePlatform == Device.Android)
                NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            await image.ScaleTo(1, 500, Easing.Linear);
            await image.ScaleTo(0.8, 500, Easing.Linear);
            await image.ScaleTo(1.1, 500, Easing.Linear);
        }
        
    }
}