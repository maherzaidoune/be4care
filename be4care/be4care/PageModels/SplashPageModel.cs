using be4care.Helpers;
using be4care.Models;
using be4care.Persistence;
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
        public SplashPageModel(IDialogService _dialogServices)
        {
            this._dialogServices = _dialogServices;
        }
        public async override void Init(object initData)
        {
            base.Init(initData);
            await System.Threading.Tasks.Task.Delay(3000);
            
            var auth = Settings.AuthToken;
            Console.WriteLine(auth);
 
            if (!CrossConnectivity.Current.IsConnected)
            {
                 _dialogServices.ShowMessage("verifier votre connection internet", "Erreur", null, false);
            }
            if (string.IsNullOrEmpty(auth))
            {
                Console.WriteLine("auth empty , user is not connected");
                await CoreMethods.PushPageModel<onBoardingPageModel>();
            }
            else
            {
                Console.WriteLine("user connected");
                await ButtonBar.initBar();
            }
        }
        

    }
}
