using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AsNum.XFControls.Droid;
using ImageCircle.Forms.Plugin.Droid;

namespace be4care.Droid
{
    [Activity(Label = "be4care", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            AsNumAssemblyHelper.HoldAssembly();
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            ImageCircleRenderer.Init();
            LoadApplication(new App());
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        
        public override void OnBackPressed()
        {
            // This prevents a user from being able to hit the back button and leave the login page.
            
            return;

        }
        

    }
}

