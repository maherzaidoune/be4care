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
	public partial class InscriptionPage : ContentPage
	{

        public InscriptionPage ()
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
                this.inscriptonLayout.Padding = new Thickness(0);
                this.inscriptonLayout.Orientation = StackOrientation.Horizontal;
                this.buttonStack.Padding = new Thickness(0, 70, 0, 0);
            }
            else
            {
                this.inscriptonLayout.Padding = new Thickness(0, 50, 0, 0);
                this.inscriptonLayout.Orientation = StackOrientation.Vertical;
                this.buttonStack.Padding = new Thickness(0);
            }
        }
    }
}