using be4care.Models;
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
                Console.WriteLine("setter called");
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
            views = new List<ViewElement>
        {
            new ViewElement{ image = "contactlist.png" , label = "Mon Profile",position= 0},
            new ViewElement{ image = "contactlist.png" , label = "Répertoire", position= 1},
            new ViewElement{ image = "contactlist.png" , label = "A propos", position= 2},
            new ViewElement{ image = "info.png" , label = "Mentions Légales", position= 3},
            new ViewElement{ image = "contact.png" , label = "Contacts", position= 4},
        };
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            
            
        }
    }
}
