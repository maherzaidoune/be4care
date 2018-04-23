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
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width > height)
            {
                this.inscriptonLayout.Padding = new Thickness(0, 5, 0, 0);
                this.inscriptonLayout.Orientation = StackOrientation.Horizontal;
            }
            else
            {
                this.inscriptonLayout.Padding = new Thickness(0, 30, 0, 0);
                this.inscriptonLayout.Orientation = StackOrientation.Vertical;
            }
        }
    }
}