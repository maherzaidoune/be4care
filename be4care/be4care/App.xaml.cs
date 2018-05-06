using Akavache;
using Akavache.Sqlite3;
using be4care.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace be4care
{
	public partial class App : Application
	{
		public App ()
		{
            BlobCache.ApplicationName = "AkavacheExperiment";
            BlobCache.EnsureInitialized();
            SetUpIOC();

            //InitializeComponent();
            var rootPage = FreshMvvm.FreshPageModelResolver.ResolvePageModel<PageModels.SplashPageModel>();
            MainPage = new FreshMvvm.FreshNavigationContainer(rootPage);
		}

        void SetUpIOC()
        {
            FreshMvvm.FreshIOC.Container.Register<IDialogService, DialogService>();
            FreshMvvm.FreshIOC.Container.Register<IRestServices, RestServices>();
            FreshMvvm.FreshIOC.Container.Register<IUserServices, UserServices>();
            FreshMvvm.FreshIOC.Container.Register<IDoctorServices, DoctorServices>();
            FreshMvvm.FreshIOC.Container.Register<IHStructServices, HStructServices>();
            FreshMvvm.FreshIOC.Container.Register<IDocumentServices, DocumentServices>();
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
