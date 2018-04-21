using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace be4care
{
	public partial class App : Application
	{
		public App ()
		{
			//InitializeComponent();
            var rootPage = FreshMvvm.FreshPageModelResolver.ResolvePageModel<PageModels.SplashPageModel>();
            MainPage = new FreshMvvm.FreshNavigationContainer(rootPage);
		}

		protected override void OnStart ()
		{
            // Handle when your app starts
        }

        protected override void OnSleep ()
		{
        }

        protected override void OnResume ()
		{
            // Handle when your app resumes
        }
    }
}
