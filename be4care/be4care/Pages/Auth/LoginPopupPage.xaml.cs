using be4care.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace be4care.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPopupPage : ContentPage
    {
		public LoginPopupPage ()
		{
            if (Device.RuntimePlatform == Device.Android)
                NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
		}
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width > height)
            {
                image.IsVisible = false;
                bigstack.Padding = new Thickness(10, 30, 10, 10);
            }
            else
            {
                image.IsVisible = true;
                bigstack.Padding = new Thickness(10, 70, 10, 10);
            }
        }

    }
}