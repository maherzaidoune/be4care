using be4care.iOS.Renderers;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchBar))]
namespace be4care.iOS.Renderers
{
    class CustomSearchBar : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            var searchbar = (UISearchBar)Control;
            if (e.NewElement != null)
            {
                searchbar.Layer.CornerRadius = 10;
                searchbar.Layer.BorderWidth = 1;
                searchbar.Layer.BorderColor = UIColor.FromRGB(240, 240, 240).CGColor;
            }
        }
    }
}
