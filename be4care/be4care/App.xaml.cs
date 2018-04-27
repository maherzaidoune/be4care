using be4care.Services;
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
            SetUpIOC();

            InitializeComponent();
            var rootPage = FreshMvvm.FreshPageModelResolver.ResolvePageModel<PageModels.SplashPageModel>();
            MainPage = new FreshMvvm.FreshNavigationContainer(rootPage);
		}

        void SetUpIOC()
        {
            FreshMvvm.FreshIOC.Container.Register<IDialogService, DialogService>();
            FreshMvvm.FreshIOC.Container.Register<IRestServices, RestServices>();
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
