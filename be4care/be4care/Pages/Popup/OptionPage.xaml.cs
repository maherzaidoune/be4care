using be4care.Services;
using Rg.Plugins.Popup.Pages;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace be4care.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OptionPage : PopupPage
    {
        RestServices rest ;
		public OptionPage (string title)
		{
			InitializeComponent ();
            rest = new RestServices();
            BindingContext = new PageModels.OptionPageModel(title,rest);

		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}