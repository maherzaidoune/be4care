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
      
    }
}