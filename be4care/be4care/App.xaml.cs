using Akavache;
using Akavache.Sqlite3;
using be4care.Helpers;
using be4care.PageModels;
using be4care.Services;
using PropertyChanged;
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
        private IRestServices _restServices;

        public App ()
		{
            BlobCache.ApplicationName = "be4care";
            BlobCache.EnsureInitialized();
            SetUpIOC();
            //InitializeComponent();
            _restServices = new RestServices();

            var auth = Settings.AuthToken;

            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //    _dialogServices.ShowMessage("Verifier votre connection internet", true);
            //}

            if (string.IsNullOrEmpty(auth))
            {
                if (auth == null)
                {
                    Device.BeginInvokeOnMainThread( () =>
                    {
                        //await CoreMethods.PushPageModel<LoginPopupPageModel>();
                        //RaisePropertyChanged();
                        var rootPage = FreshMvvm.FreshPageModelResolver.ResolvePageModel<PageModels.LoginPopupPageModel>();
                        MainPage = new FreshMvvm.FreshNavigationContainer(rootPage);
                    });
                }
                else
                {
                    Console.WriteLine("auth empty , user is not connected");
                    Device.BeginInvokeOnMainThread( () =>
                    {
                        //await CoreMethods.PushPageModel<onBoardingPageModel>();
                        //RaisePropertyChanged();
                        var rootPage = FreshMvvm.FreshPageModelResolver.ResolvePageModel<PageModels.onBoardingPageModel>();
                        MainPage = new FreshMvvm.FreshNavigationContainer(rootPage);
                    });
                } 
            }
            else
            {
                     if (_restServices.Init())
                    {
                        Console.WriteLine("Token : " + auth);
                        ButtonBar.initBar();
                    }
                    else { 
                    Settings.AuthToken = String.Empty;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var rootPage = FreshMvvm.FreshPageModelResolver.ResolvePageModel<PageModels.LoginPopupPageModel>();
                        MainPage = new FreshMvvm.FreshNavigationContainer(rootPage);
                    });

                     }
                }



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
