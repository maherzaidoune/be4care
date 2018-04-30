using be4care.Services;
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
using be4care.Helpers;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class InscriptionPageModel : FreshMvvm.FreshBasePageModel
    {
        private readonly IDialogService _dialogservices;
        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }
        public ICommand backClick => new Command(backButtonClick);
        public ICommand validate => new Command(validateButtonClicked);

        private  void validateButtonClicked(object obj)
        {
            var t = Utils.EntryCheck.checkentries(num, email, password, acceptTerms);
            if (!t.Item1)
            {
                _dialogservices.ShowMessage(t.Item2, "Erreur",null, false);
            }
            else
            {
                isEnabled = false;
                isBusy = true;
                Task.Run( () =>
                {
                    if (_RestService.Inscription(email,password))
                    {
                        var me = new User { email = email, phNumber = num };
                        _userServices.SaveUser(me);
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await CoreMethods.PushPageModel<AddUserPageModel>();
                            RaisePageWasPopped();
                        });
                    }
                    password = String.Empty;
                    isEnabled = true;
                    isBusy = false;
                });
            }

        }

        public string email { get; set; }
        public string num { get; set; }
        public string password { get; set; }
        public bool acceptTerms { get; set; }
        private IRestServices _RestService { get; set; }
        public IUserServices _userServices { get; set; }

        private async void backButtonClick(object obj)
        { 
            await CoreMethods.PushPageModel<LoginPopupPageModel>();
            RaisePageWasPopped();
        }

        public InscriptionPageModel(IDialogService  _dialogservices,IRestServices _restService,IUserServices _userServices)
        {
            this._dialogservices = _dialogservices;
            this._RestService = _restService;
            this._userServices = _userServices;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            //restore user date
            acceptTerms = false;
            isEnabled = true;
            isBusy = false;
        }

    }

}
