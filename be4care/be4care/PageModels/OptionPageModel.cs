using be4care.Services;
using PropertyChanged;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using be4care.Pages;
using FreshMvvm;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class OptionPageModel 
    {
        public string title { get; set; }
        private IRestServices _restServices;
        public ICommand editprofile => new Command(edit);
        public ICommand diconnect => new Command(deconnecter);
        public ICommand delete => new Command(deleteme);
        public ICommand annuler => new Command(annulerpopup);

        private  void annulerpopup(object obj)
        {
             PopupNavigation.Instance.PopAllAsync();
        }

        private void deleteme(object obj)
        {
            if (_restServices.Delete())
            {
                Console.WriteLine("Account deleted");
                PopupNavigation.Instance.PopAllAsync();
                //App.Current.MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<LoginPopupPageModel>());
                var rootPage = FreshMvvm.FreshPageModelResolver.ResolvePageModel<SplashPageModel>();
                App.Current.MainPage = new FreshMvvm.FreshNavigationContainer(rootPage);
            }
        }

        private void deconnecter(object obj)
        {
            if (_restServices.Disconnect())
            {
                Console.WriteLine("Disconnected");
                PopupNavigation.Instance.PopAllAsync();
                //App.Current.MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<LoginPopupPageModel>());
                var rootPage = FreshMvvm.FreshPageModelResolver.ResolvePageModel<SplashPageModel>();
                App.Current.MainPage = new FreshMvvm.FreshNavigationContainer(rootPage);
            }

        }

        private async void edit(object obj)
        {
            Device.BeginInvokeOnMainThread(async  () =>
            {
                 await PopupNavigation.Instance.PopAllAsync();
                 App.Current.MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<EditProfilePageModel>());

            });
            
        }

        public OptionPageModel(string  title,IRestServices _restServices)
        {
            this.title = title;
            this._restServices = _restServices;
        }
    }
}
