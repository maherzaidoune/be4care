using be4care.Helpers;
using be4care.Services;
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
            var auth = Settings.AuthToken;

            //simulate intialise api 
            await System.Threading.Tasks.Task.Delay(3000);
            if (!CrossConnectivity.Current.IsConnected)
            {
                 _dialogServices.ShowMessage("verifier votre connection internet", "Erreur", null, false);
            }
            // if (string.IsNullOrEmpty(auth))
            await CoreMethods.PushPageModel<onBoardingPageModel>();
           // else
            //    await CoreMethods.PushPageModel<InscriptionPageModel>();

            RaisePageWasPopped();
        }
        

    }
}
