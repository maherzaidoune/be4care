using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AsNum.XFControls.Droid;
using Acr.UserDialogs;
using Plugin.CurrentActivity;
using FFImageLoading.Forms.Droid;

namespace be4care.Droid
{
    [Activity(Label = "be4care", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            AsNumAssemblyHelper.HoldAssembly();
            UserDialogs.Init(this);
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            global::Xamarin.Forms.Forms.SetFlags("FastRenderers_Experimental");
            global::Xamarin.Forms.Forms.Init(this, bundle);
            CachedImageRenderer.Init(true);
            LoadApplication(new App());
        }
        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
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

