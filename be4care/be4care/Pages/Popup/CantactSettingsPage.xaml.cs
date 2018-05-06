using be4care.PageModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace be4care.Pages.Popup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CantactSettingsPage : PopupPage
    {
		public CantactSettingsPage ()
		{
			InitializeComponent ();
            BindingContext = new CantactSettingsPageModel();
		}
	}
}