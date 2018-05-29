using Akavache;
using be4care.Helpers;
using be4care.Services;
using Plugin.Connectivity;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace be4care
{
    public partial class App : Application
	{
        public App ()
		{
            BlobCache.ApplicationName = "be4care";
            BlobCache.EnsureInitialized();
            InitializeComponent();
            SetUpIOC();
            Device.BeginInvokeOnMainThread(() =>
            {
                var rootPage = FreshMvvm.FreshPageModelResolver.ResolvePageModel<PageModels.checkLoginPageModel>();
                MainPage = new FreshMvvm.FreshNavigationContainer(rootPage);
            });
            
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
            //BlobCache.ApplicationName = "be4care";
            //BlobCache.EnsureInitialized();
        }

        protected override void OnSleep ()
		{
            //BlobCache.Shutdown().Wait();
        }
        protected override void OnResume ()
		{
            
        }
    }
}
