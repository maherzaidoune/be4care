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
    class DocumentOptionPageModel : FreshMvvm.FreshBasePageModel
    {
        public Document document { get; set; }
        public ICommand edit => new Command(editDoc);

        private void editDoc(object obj)
        {
            PopupNavigation.Instance.PopAllAsync();
            MessagingCenter.Send(this, "editdoc");
        }

        public ICommand delete => new Command(deleteDoc);

        private void deleteDoc(object obj)
        {
            PopupNavigation.Instance.PopAllAsync();
            MessagingCenter.Send(this, "delete");
        }

        public ICommand annuler => new Command(back);

        private void back(object obj)
        {
            PopupNavigation.Instance.PopAllAsync();
        }

        public DocumentOptionPageModel(Document document)
        {
            this.document = document;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }

    }
}
