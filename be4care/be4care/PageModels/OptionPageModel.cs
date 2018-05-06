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
    class OptionPageModel : FreshMvvm.FreshBasePageModel
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
                var rootPage = FreshPageModelResolver.ResolvePageModel<SplashPageModel>();
                App.Current.MainPage = new FreshNavigationContainer(rootPage);
            }
        }

        private void deconnecter(object obj)
        {
            if (_restServices.Disconnect())
            {
                Console.WriteLine("Disconnected");
                PopupNavigation.Instance.PopAllAsync();
                //App.Current.MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<LoginPopupPageModel>());
                var rootPage = FreshPageModelResolver.ResolvePageModel<LoginPopupPageModel>();
                App.Current.MainPage = new FreshNavigationContainer(rootPage);
            }

        }

        private  void edit(object obj)
        {
            Device.BeginInvokeOnMainThread(async  () =>
            {
                 await PopupNavigation.Instance.PopAllAsync();
                //App.Current.MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<EditProfilePageModel>());
                 await App.Current.MainPage.Navigation.PushModalAsync(FreshPageModelResolver.ResolvePageModel<EditProfilePageModel>());
            });
            
        }

        public OptionPageModel(string  title,IRestServices _restServices)
        {
            this.title = title;
            this._restServices = _restServices;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
