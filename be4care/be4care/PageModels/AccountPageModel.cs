using be4care.Models;
using FreshMvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class AccountPageModel : FreshBasePageModel
    {
        public  class ViewElement
        {
            public string image { get; set; }
            public string label { get; set; }
            public int position { get; set; }
        }

        //public bool isVisible { get; set; }
        private IList<ViewElement> _views { get; set; }
        public IList<ViewElement> views { get {
                return _views ?? (_views  = new List<ViewElement>
                    {
                        new ViewElement{ image = "phonebook.png" , label = "Mon Profile",position= 0},
                        new ViewElement{ image = "phonebook.png" , label = "Répertoire", position= 1},
                        new ViewElement{ image = "Hand.png" , label = "A propos", position= 2},
                        new ViewElement{ image = "file.png" , label = "Mentions Légales", position= 3},
                        new ViewElement{ image = "bubble.png" , label = "Contacts", position= 4}
                    });
            }  }
        public  ViewElement selected
        {
            get
            {
                return null;
            }
            set
            {
                if (value.position == 0)
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await CoreMethods.PushPageModel<ProfilePageModel>();
                        RaisePropertyChanged();
                    });
                if (value.position == 1)
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await CoreMethods.PushPageModel<ContactPageModel>();
                        RaisePropertyChanged();
                    });
                if (value.position == 2)
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await CoreMethods.PushPageModel<AboutPageModel>();
                        RaisePropertyChanged();
                    });
                if (value.position == 3)
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await CoreMethods.PushPageModel<LegalPageModel>();
                        RaisePropertyChanged();
                    });
                if (value.position == 4)
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await CoreMethods.PushPageModel<AboutDevPageModel>();
                        RaisePropertyChanged();
                    });

            }
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            //if (isVisible)
            //{
            //    //if(views == null)
            //    //{
                    
            //    //}
            //    isVisible = false;
            //}
        }
        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
            //isVisible = true;
        }

        public AccountPageModel()
        {

        }

        public  override void Init(object initData)
        {
            base.Init(initData);
            //isVisible = true;
            
        }
    }
}
