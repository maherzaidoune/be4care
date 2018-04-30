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
using be4care.Helpers;

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
        private IUserServices _userServices;
        public ICommand close => new Command(closebutton);
        public ICommand validate => new Command(addUser);

        private async void addUser(object obj)
        {
            user = await  _userServices.GetUser();
            isEnabled = false;
            isBusy = true;
            user.name = nom;
            user.lastName = prenom;
            user.sex = sex.Equals("Homme");
            user.bDate = date;
            user.username = username;
            await Task.Run( async () =>
            {
                if(!(_restServices.UpdateProfile(user)))
                {
                    //Console.WriteLine("error updating  profile , user save for  local db");
                    _dialogService.ShowMessage("Erreur", null, null, false);
                    isBusy = false;
                    isEnabled = true;
                }
                _userServices.SaveUser(user);
                await ButtonBar.initBar();
            });
        }

        public AddUserPageModel(IRestServices _restServices,IDialogService _dialogService, IUserServices _userServices)
        {
            sex = "Homme";
            date = DateTime.Today;
            this._dialogService = _dialogService;
            this._restServices = _restServices;
            this._userServices = _userServices;
        }

        private async void closebutton(object obj)
        {
            await ButtonBar.initBar();
        }

        
        public override void Init(object initData)
        {
            base.Init(initData);
            isBusy = false;
            isEnabled = true;
        }
    }
}
