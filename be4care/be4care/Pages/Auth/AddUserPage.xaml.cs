using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace be4care.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddUserPage : ContentPage
	{
		public AddUserPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
		}
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width > height)
            {
                this.adduserStack.Padding = new Thickness(0);
                this.buttonStack.Padding = new Thickness(0, 70, 0, 0);
                this.adduserStack.Orientation = StackOrientation.Horizontal;
            }
            else
            {
                this.adduserStack.Padding = new Thickness(0, 20, 0, 10);
                this.buttonStack.Padding = new Thickness(0);
                this.adduserStack.Orientation = StackOrientation.Vertical;
            }
        }
	}
}