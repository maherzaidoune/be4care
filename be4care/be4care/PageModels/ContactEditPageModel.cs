using be4care.Models;
using PropertyChanged;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class ContactEditPageModel : FreshMvvm.FreshBasePageModel
    {

        public ICommand edit => new Command(editContact);
        public ICommand delete => new Command(deleteContact);
        public ICommand annuler => new Command(back);

        private void back(object obj)
        {
            PopupNavigation.Instance.PopAllAsync();
        }

        public Contact contact { get; set; }

        private void deleteContact(object obj)
        {
            PopupNavigation.Instance.PopAllAsync();
            MessagingCenter.Send(this, "delete");
        }

        private void editContact(object obj)
        {
            PopupNavigation.Instance.PopAllAsync();
            MessagingCenter.Send(this, "editcontact");
        }

        public ContactEditPageModel(Contact contact)
        {
            this.contact = contact;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
