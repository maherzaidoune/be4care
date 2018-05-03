﻿using be4care.Models;
using FreshMvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class AccountPageModel : FreshMvvm.FreshBasePageModel
    {
        [AddINotifyPropertyChangedInterface]
        public  class ViewElement
        {
            public string image { get; set; }
            public string label { get; set; }
            public int position { get; set; }
        }

        public List<ViewElement> views { get; set; }
        public  ViewElement selected
        {
            get
            {
                return null;
            }
            set
            {
                if(value.position == 0)
                    CoreMethods.PushPageModel<ProfilePageModel>();
                if (value.position == 1)
                    CoreMethods.PushPageModel<ContactPageModel>();
                if (value.position == 2)
                    CoreMethods.PushPageModel<AboutPageModel>();
                if (value.position == 3)
                    CoreMethods.PushPageModel<LegalPageModel>();
                if (value.position == 4)
                    CoreMethods.PushPageModel<AboutDevPageModel>();
                RaisePropertyChanged();
            }
        }


        public AccountPageModel()
        {
            Console.WriteLine("account  page model construct");

        }
        public  override void Init(object initData)
        {
            base.Init(initData);
            Console.WriteLine("account  page model init");

            views = new List<ViewElement>
        {
            new ViewElement{ image = "phonebook.png" , label = "Mon Profile",position= 0},
            new ViewElement{ image = "phonebook.png" , label = "Répertoire", position= 1},
            new ViewElement{ image = "Hand.png" , label = "A propos", position= 2},
            new ViewElement{ image = "file.png" , label = "Mentions Légales", position= 3},
            new ViewElement{ image = "bubble.png" , label = "Contacts", position= 4},
        };
        }
    }
}
