using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using be4care.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchBar))]
namespace be4care.Droid.Renderers
{
    class CustomSearchBar : SearchBarRenderer
    {
        public CustomSearchBar(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
                Control.Background = ContextCompat.GetDrawable(context: Forms.Context, id: Resource.Drawable.custom_search_view);
        }
    }
}