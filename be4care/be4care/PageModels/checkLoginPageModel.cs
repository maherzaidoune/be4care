using be4care.Helpers;
using PropertyChanged;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class checkLoginPageModel : FreshMvvm.FreshBasePageModel
    {
        public bool refresh { get; set; }

        public checkLoginPageModel()
        {
        }
        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            
        }
        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            //Task.Run(async () =>
            //{
            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            //        refresh = true;
            //    });

            //    var auth = Settings.AuthToken;
            //    if (string.IsNullOrEmpty(auth))
            //    {
            //        if (auth == string.Empty)
            //        {
            //            Device.BeginInvokeOnMainThread(async () =>
            //            {
            //                await CoreMethods.PushPageModel<LoginPopupPageModel>();
            //                RaisePropertyChanged();
            //                //var rootPage = FreshMvvm.FreshPageModelResolver.ResolvePageModel<LoginPopupPageModel>();
            //                //App.Current.MainPage = new FreshMvvm.FreshNavigationContainer(rootPage);
            //            });
            //        }
            //        else
            //        {
            //            Device.BeginInvokeOnMainThread(async () =>
            //            {
            //                await CoreMethods.PushPageModel<onBoardingPageModel>();
            //                RaisePropertyChanged();
            //                //var rootPage = FreshMvvm.FreshPageModelResolver.ResolvePageModel<onBoardingPageModel>();
            //                //App.Current.MainPage = new FreshMvvm.FreshNavigationContainer(rootPage);
            //            });
            //        }
            //    }
            //    else
            //    {
            //        await Task.Run(() =>
            //        {
            //            ButtonBar.initBar();
            //        });
            //    }
            //    refresh = false;
            //});
            Device.BeginInvokeOnMainThread(async () =>
            {
                refresh = true;
                await Task.Run(() =>
                   {
                       ButtonBar.initBar();
                   });
                refresh = false;
            });

        }
    }
}
