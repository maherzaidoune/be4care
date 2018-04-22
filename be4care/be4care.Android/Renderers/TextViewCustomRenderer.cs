using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using be4care.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(TextViewCustomRenderer))]
namespace be4care.Droid.Renderers
{
    public class TextViewCustomRenderer : EntryRenderer
    {
        public TextViewCustomRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetTextColor(global::Android.Graphics.Color.White);
                Control.Gravity = GravityFlags.CenterVertical;
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Argb(200, 246, 162, 139));
                gd.SetCornerRadius(10);
                gd.SetStroke(2, global::Android.Graphics.Color.White);
                Control.SetBackgroundDrawable(gd);
                Control.SetPadding(20, 0, 0, 0);
            }
        }

    }
}