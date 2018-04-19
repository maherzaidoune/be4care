using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace be4care.views.onBoarding
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class onBoarding : ContentPage
	{
        private View[] _views;

        public onBoarding()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            _views = new View[]
            {
                new FirstPage(),
                new SecondPage(),
                new ThirdPage()
            };
            Carousel.ItemsSource = _views;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private void OnCarouselPositionSelected(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            var currentView = _views[e.NewValue];           
        }
    }
}