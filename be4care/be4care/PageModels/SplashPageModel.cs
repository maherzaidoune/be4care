using be4care.Helpers;
using be4care.Models;
using be4care.Services;
using BottomBar.XamarinForms;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace be4care.PageModels
{
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
            await System.Threading.Tasks.Task.Delay(3000);    
            var auth =  Settings.AuthToken;
            if (!CrossConnectivity.Current.IsConnected)
            {
                _dialogServices.ShowMessage("verifier votre connection internet", "Erreur", null, false);
            }
            Console.WriteLine("auth : "  + auth);
            
            if (string.IsNullOrEmpty(auth))
            {
                Console.WriteLine("auth empty , user is not connected");
                await CoreMethods.PushPageModel<onBoardingPageModel>();
                RaisePropertyChanged();
            }
            else
            {
                if (_restServices.Init())
                {
                    Console.WriteLine("Token : " + auth);
                    Console.WriteLine("user connected");
                    await ButtonBar.initBar();
                }
                else { 
                Settings.AuthToken = String.Empty;
                await CoreMethods.PushPageModel<LoginPopupPageModel>();
                RaisePropertyChanged();
                    }
            }
        }

    }
}
