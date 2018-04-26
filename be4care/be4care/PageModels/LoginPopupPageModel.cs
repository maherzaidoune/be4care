﻿using be4care.Services;
using PropertyChanged;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class LoginPopupPageModel : FreshMvvm.FreshBasePageModel
    { 

        private IRestServices _restServices;
        private IDialogService _dialogService;
        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }

        public LoginPopupPageModel(IRestServices _restServices, IDialogService _dialogService)
        {
            this._dialogService = _dialogService;
            this._restServices = _restServices;
        }
      
        public ICommand validate => new Command(login);
        public ICommand inscription => new Command(inscriptionButton);

        private async void login(object obj)
        {
            isEnabled = false;
            isBusy = true;
            Console.WriteLine(  "login invoked");
            Console.WriteLine( "true :" + isBusy);
            Console.WriteLine(  "login invoked");

            await Task.Run(() =>
            {
                if (_restServices.GetAccessToken(user, password))
                {
                    _dialogService.ShowMessage("Connecté", "Succes", null, false);
                    
                }
                else
                {
                    _dialogService.ShowMessage("Verifiez vos donné", "Erreur", null, false);
                    
                }
                isBusy = false;
                isEnabled = true;
                Console.WriteLine("true :" + isEnabled);

            });
        }

        private  void inscriptionButton(object obj)
        {
            CoreMethods.PushPageModel<InscriptionPageModel>();
            RaisePageWasPopped();
        }
        public string user { get; set; }
        public string password { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);
            isBusy = false;
            isEnabled = true;
        }
    }
}