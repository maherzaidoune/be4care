using be4care.Helpers;
using be4care.Models;
using be4care.Services;
using FreshMvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class EditProfilePageModel: FreshMvvm.FreshBasePageModel
    {
        public IEnumerable<string> Data { get; } = new List<string>() { "Homme", "Femme" };
        private IRestServices _restServices;
        private IDialogService _dialogService;
        private IUserServices _userServices;
        private User _user { get; set; }
        public User user { get {
                return _user ?? (_user = GetUser());
            } }
        public string email { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string num { get; set; }
        public string sex { get; set; }
        public string pUrl { get; set; }
        public DateTime date { get; set; }
        public string username { get; set; }
        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }
        public bool isVisible { get; set; }

        public ICommand save => new Command(saveButton);
        public ICommand backClick => new Command(back);
        public ICommand changephoto => new Command(photoEdit);

        private void photoEdit(object obj)
        {
            Task.Run(async () =>
            {
                isEnabled = false;
                isBusy = true;
                var url = await _restServices.Upload(user);
                if (!string.IsNullOrEmpty(url))
                {
                    pUrl = url;
                }
                isBusy = false;
                isEnabled = true;
            });
        }

        private void back(object obj)
        {
            App.Current.MainPage.Navigation.PopModalAsync();
        }


        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            if (isVisible)
            {
                isVisible = false;
            }
        }
        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
            isVisible = true;
        }

        public User GetUser()
        {
            Task.Run(async () =>
            {
                try
                {
                    _user = await _userServices.GetUser();
                }
                catch
                {
                    _dialogService.ShowMessage("Erreur", true);
                    await App.Current.MainPage.Navigation.PopModalAsync();
                }
                email = user.email;
                nom = user.name;
                prenom = user.lastName;
                num = user.phNumber;
                date = user.bDate;
                username = user.username;
                pUrl = user.pUrl;
                if (user.sex)
                {
                    sex = "Homme"; }
                else
                {
                    sex = "Femme";
                }
            }).Wait();
            return _user;
        }
        private void saveButton(object obj)
        {
            isEnabled = false;
            isBusy = true;
            var user = new User
            {
                email = email,
                name = nom,
                lastName = prenom,
                phNumber = num,
                bDate = date,
                username = username,
                pUrl = pUrl ,
                sex = sex.Equals("Homme")
            };
             Task.Run(async () =>
                {
                    if (!(_restServices.UpdateProfile(user)))
                    {
                        //Console.WriteLine("error updating  profile , user save for  local db");
                        _dialogService.ShowMessage("Erreur : Veuillez réessayer plus tard",true);
                        isBusy = false;
                        isEnabled = true;
                    }
                    else
                    {
                        _userServices.SaveUser(user);
                        MessagingCenter.Send(this, "updateProfile");
                        await App.Current.MainPage.Navigation.PopModalAsync();
                        isBusy = false;
                        isEnabled = true;
                    }
                });
            
        }

        public EditProfilePageModel(IRestServices _restServices, IDialogService _dialogService, IUserServices _userServices)
        {
            this._dialogService = _dialogService;
            this._restServices = _restServices;
            this._userServices = _userServices;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            _user = GetUser();
            isVisible = true;
            isBusy = false;
            isEnabled = true;
        }
    }
}
