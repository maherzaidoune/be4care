﻿using be4care.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using be4care.Models;
using System.Threading.Tasks;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class InscriptionPageModel : FreshMvvm.FreshBasePageModel
    {
        private readonly IDialogService _dialogservices;
        public ICommand backClick => new Command(backButtonClick);
        public ICommand validate => new Command(validateButtonClicked);
        public ICommand login => new Command(loginButton);

        private void loginButton(object obj)
        {
            _dialogservices.login();
        }


        private async  void validateButtonClicked(object obj)
        {
            var t = Utils.EntryCheck.checkentries(num, email, password, acceptTerms);
            if (!t.Item1)
            {
                _dialogservices.ShowMessage(t.Item2, "Erreur", "retour", true);
            }
            else
            {
                var me = new User { email = email, phNumber = num };
                await me.Save();
                await CoreMethods.PushPageModel<AddUserPageModel>();
                RaisePageWasPopped();
                await Task.Run(() =>
                {
                    if (_RestService.Inscription(email, password))
                    {
                        Console.WriteLine("Inscription done");
                    }
                });
                
            }

        }

        public string email { get; set; }
        public string num { get; set; }
        public string password { get; set; }
        public bool acceptTerms { get; set; }
        private IRestServices _RestService { get; }

        private async void backButtonClick(object obj)
        {
            //save data
            Application.Current.Properties["email"] = email;
            Application.Current.Properties["num"] = num;
            await CoreMethods.PushPageModel<onBoardingPageModel>();
            RaisePageWasPopped();
        }

        public InscriptionPageModel(IDialogService  _dialogservices,IRestServices _restService)
        {
            this._dialogservices = _dialogservices;
            _RestService = _restService;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            //restore user date
            acceptTerms = false;
            if (Application.Current.Properties.ContainsKey("num"))
            {
                 num = Application.Current.Properties["num"] as string;

            }
            if (Application.Current.Properties.ContainsKey("email"))
            {
                email = Application.Current.Properties["email"] as string;
            }

        }

    }

}
