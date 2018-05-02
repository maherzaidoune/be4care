﻿using be4care.Models;
using be4care.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class ProfilePageModel : FreshMvvm.FreshBasePageModel
    {
        [AddINotifyPropertyChangedInterface]
        public class detail
        {
            public string menu { get; set; }
            public string info { get; set; }
        }

        public ICommand photochange => new Command(photoclicked);
        public ICommand edit => new Command(editUser);
        public ICommand backClick => new Command(backClickbutton);

        private void backClickbutton(object obj)
        {
            CoreMethods.PushPageModel<AccountPageModel>();
            RaisePropertyChanged();
        }

        private User user;

        private void editUser(object obj)
        {
            Console.WriteLine("photclicked !");
            CoreMethods.PushPageModel<EditProfilePageModel>(user);
            RaisePropertyChanged();
        }

        private void photoclicked(object obj)
        {
            Console.WriteLine("photclicked !");
        }

        private IUserServices _userServices;
        private IRestServices _restServices;
        public List<detail> details { get; set; }

        public ProfilePageModel(IUserServices _userServices, IRestServices _restServices)
        {
            this._userServices = _userServices;
            this._restServices = _restServices;
        }

        public string convertSexe(bool sex)
        {
            if (sex)
                return "Homme";
            return "Femme";
        }
        public override async void Init(object initData)
        {
            base.Init(initData);
            try
            {
                user = await _userServices.GetUser();
                Console.WriteLine("cant get user from db");
            }
            catch
            {
                user =  _restServices.GetMyProfile();
            }
            details = new List<detail>()
            {
                new detail {menu = "Identifiant" ,  info=user.email },
                new detail {menu = "Nom" ,  info= user.name },
                new detail {menu = "Prenom" ,  info = user.lastName },
                new detail {menu = "Numéro de telephone" ,  info=user.phNumber },
                new detail {menu = "Date de naissance" ,  info= user.bDate.ToString() },
                new detail {menu = "sexe" ,  info=  convertSexe(user.sex)}
            };

        }
    }
}
