using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace be4care.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class onBoardingPage : ContentPage
	{

        public onBoardingPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

    }
}