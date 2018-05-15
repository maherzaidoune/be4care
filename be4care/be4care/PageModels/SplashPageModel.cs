using be4care.Helpers;
using be4care.Models;
using be4care.Services;
using BottomBar.XamarinForms;
using Plugin.Connectivity;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class SplashPageModel : FreshMvvm.FreshBasePageModel
    {
        private IDialogService _dialogServices;
        private IRestServices _restServices;
        public SplashPageModel(IDialogService _dialogServices, IRestServices _restServices)
        {
            this._dialogServices = _dialogServices;
            this._restServices = _restServices;
        }
        public  async override void Init(object initData)
        {
            base.Init(initData);

            await Task.Delay(3000);

            var auth = Settings.AuthToken;
            
            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //    _dialogServices.ShowMessage("Verifier votre connection internet",true);
            //}
            
            if (string.IsNullOrEmpty(auth))
            {
                Console.WriteLine("auth empty , user is not connected");
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await CoreMethods.PushPageModel<onBoardingPageModel>();
                    RaisePropertyChanged();
                });
                
            }
            else
            {
                if (_restServices.Init())
                {
                    Console.WriteLine("Token : " + auth);
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await ButtonBar.initBar();
                    });
                }
                else { 
                    Settings.AuthToken = String.Empty;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await CoreMethods.PushPageModel<LoginPopupPageModel>();
                        RaisePropertyChanged();
                    });
                    
                    }
            }
        }

    }
}
