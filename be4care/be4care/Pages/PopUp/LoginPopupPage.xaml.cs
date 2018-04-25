using be4care.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace be4care.Pages.PopUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
		public LoginPopupPage ()
		{
			InitializeComponent ();
            BindingContext = new PageModels.LoginPopupPageModel();
		}
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width > height)
            {
                this.buttonStack.Orientation = StackOrientation.Horizontal;
            }
            else
            {
                this.buttonStack.Orientation = StackOrientation.Vertical;
            }
        }

    }
}