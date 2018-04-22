using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class InscriptionPageModel : FreshMvvm.FreshBasePageModel
    {
        public ICommand backClick => new Command(backButtonClick);
        public ICommand validate => new Command(validateButtonClicked);

        private void validateButtonClicked(object obj)
        {
            var t = Utils.EntryCheck.checkentries(num, email, password, acceptTerms);
            if (!t.Item1)
                CoreMethods.DisplayAlert("Erreur ", t.Item2, "Ok");
            else
            {
                CoreMethods.DisplayAlert("Erreur ", Services.RestServices.inscription(email, password), "Ok");

            }

        }

        public string email { get; set; }
        public string num { get; set; }
        public string password { get; set; }
        public bool acceptTerms { get; set; }

        private async void backButtonClick(object obj)
        {
            //save data
            Application.Current.Properties["email"] = email;
            Application.Current.Properties["num"] = num;
            await CoreMethods.PushPageModel<onBoardingPageModel>();
            RaisePageWasPopped();
        }

        public InscriptionPageModel()
        {

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
