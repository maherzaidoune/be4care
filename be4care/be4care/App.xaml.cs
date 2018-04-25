using Akavache;
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

            BlobCache.ApplicationName = "be4care";

            SetUpIOC();

            InitializeComponent();
            var rootPage = FreshMvvm.FreshPageModelResolver.ResolvePageModel<PageModels.InscriptionPageModel>();
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
            BlobCache.Shutdown().Wait();
        }

        protected override void OnResume ()
		{
            // Handle when your app resumes
        }
    }
}
