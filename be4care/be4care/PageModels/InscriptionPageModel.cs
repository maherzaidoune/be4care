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


        private async  void validateButtonClicked(object obj)
        {
            isEnabled = false;
            isBusy = true;
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
                    isEnabled = true;
                    isBusy = false;
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
            await CoreMethods.PushPageModel<LoginPopupPageModel>();
            RaisePageWasPopped();
        }

        public InscriptionPageModel(IDialogService  _dialogservices,IRestServices _restService)
        {
            this._dialogservices = _dialogservices;
            this._RestService = _restService;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            //restore user date
            acceptTerms = false;
            isEnabled = true;
            isBusy = false;

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
