﻿using be4care.Models;
using be4care.Services;
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
        public User user { get; set; }
        public string email { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string num { get; set; }
        public string sex { get; set; }
        public DateTime date { get; set; }
        public string username { get; set; }
        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }

        public ICommand save => new Command(saveButton);
        public ICommand backClick => new Command(backClickbutton);

        private void backClickbutton(object obj)
        {
            CoreMethods.PopPageModel();
            RaisePropertyChanged();
        }

        private  void saveButton(object obj)
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
                sex = sex.Equals("Homme")
            };
            _userServices.SaveUser(user);
            Task.Run(() =>
            {
                if (!(_restServices.UpdateProfile(user)))
                {
                    //Console.WriteLine("error updating  profile , user save for  local db");
                    _dialogService.ShowMessage("Erreur", null, null, false);
                    isBusy = false;
                    isEnabled = true;
                }
            });
            CoreMethods.PushPageModel<ProfilePageModel>();
            RaisePropertyChanged();
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
            user = initData as User;
            email = user.email;
            nom = user.name;
            prenom = user.lastName;
            num = user.phNumber;
            date = user.bDate;
            username = user.username;
            if (user.sex)
            { sex = "Homme"; }
            else
            {
                sex = "Femme";
            }
            isBusy = false;
            isEnabled = true;


        }
    }
}