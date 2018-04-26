using Akavache;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Reactive.Linq;
using be4care.Models;
using be4care.Services;
using System.Threading.Tasks;

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
        public string username { get; set; }
        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }
        public IEnumerable<string> Data { get; } = new List<string>() { "Homme", "Femme" };
        User user;
        private IRestServices _restServices;
        private IDialogService _dialogService;
        public ICommand backClick => new Command(backButtonClick);
        public ICommand validate => new Command(addUser);

        private async void addUser(object obj)
        {
            isEnabled = false;
            isBusy = true;
            user = (new User()).getUser();
            user.name = nom;
            user.lastName = prenom;
            user.sex = sex.Equals("Homme");
            user.bDate = date;
            user.username = username;
            await user.Save();
            await Task.Run(() =>
            {
                if (_restServices.UpdateProfile(user))
                {
                    Console.WriteLine("add user profile done");
                }
                isBusy = false;
                isEnabled = true;
            });
        }

        public AddUserPageModel(IRestServices _restServices,IDialogService _dialogService)
        {
            sex = "Homme";
            date = DateTime.Today;
            this._dialogService = _dialogService;
            this._restServices = _restServices;
        }

        private async void backButtonClick(object obj)
        {
            await CoreMethods.PushPageModel<InscriptionPageModel>();
            RaisePageWasPopped();
        }

        
        public override void Init(object initData)
        {
            base.Init(initData);
            isBusy = false;
            isEnabled = true;
        }
    }
}
