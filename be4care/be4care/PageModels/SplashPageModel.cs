using be4care.Helpers;
using be4care.Models;
using be4care.Services;
using BottomBar.XamarinForms;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace be4care.PageModels
{
    class SplashPageModel : FreshMvvm.FreshBasePageModel
    {
        private IDialogService _dialogServices;
        private IRestServices _restServices;
        private IUserServices _userServices;
        private IHStructServices _hstructServices;
        private IDoctorServices _doctorServices;
        public SplashPageModel(IDialogService _dialogServices, IRestServices _restServices,IUserServices _userServices,IHStructServices _hstructServices,IDoctorServices _doctorServices)
        {
            this._dialogServices = _dialogServices;
            this._restServices = _restServices;
            this._hstructServices = _hstructServices;
            this._doctorServices = _doctorServices;
        }
        public  async override void Init(object initData)
        {
            base.Init(initData);
            await Task.Delay(3000);
            var auth = Settings.AuthToken;
            if (!CrossConnectivity.Current.IsConnected)
            {
                _dialogServices.ShowMessage("verifier votre connection internet");
            }
            Console.WriteLine("auth : " + auth);
           
            if (string.IsNullOrEmpty(auth))
            {
                Console.WriteLine("auth empty , user is not connected");
                _doctorServices.DeleteDoctors();
                _hstructServices.DeleteStructs();
                await CoreMethods.PushPageModel<onBoardingPageModel>();
                RaisePropertyChanged();
            }
            else
            {
                if (_restServices.Init())
                {
                    Console.WriteLine("Token : " + auth);
                    await ButtonBar.initBar();
                }
                else { 
                    Settings.AuthToken = String.Empty;
                    _doctorServices.DeleteDoctors();
                     _hstructServices.DeleteStructs();
                    await CoreMethods.PushPageModel<LoginPopupPageModel>();
                    RaisePropertyChanged();
                    }
            }
        }

    }
}
