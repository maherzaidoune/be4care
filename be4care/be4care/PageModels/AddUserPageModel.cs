using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class AddUserPageModel : FreshMvvm.FreshBasePageModel
    {
        public string email { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string num { get; set; }
        public string sex { get; set; }
        public DateTime date { get; set; }
        public ICommand backClick => new Command(backButtonClick);

        private async void backButtonClick(object obj)
        {
            await CoreMethods.PushPageModel<InscriptionPageModel>();
            RaisePageWasPopped();
        }

        public IEnumerable<string> Data { get; } = new List<string>() { "Homme", "Femme" };
        public AddUserPageModel()
        {
            Console.WriteLine("AddUserDetailsPageModel");
            sex = "Homme";
            date = DateTime.Today;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            var t = initData as Tuple<string,string>;
            email = t.Item1;
            num = t.Item2;
        }
    }
}
