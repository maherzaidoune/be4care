using be4care.Services;
using FreshMvvm;
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
    class CantactSettingsPageModel : FreshMvvm.FreshBasePageModel
    {
        public ICommand annuler => new Command(annulerpopup);
        public ICommand addDoc => new Command(addDocument);
        public ICommand addStruct => new Command(addHstruct);

        private void addHstruct(object obj)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await PopupNavigation.Instance.PopAllAsync();
                await PopupNavigation.Instance.PopAllAsync();
                await App.Current.MainPage.Navigation.PushModalAsync(FreshPageModelResolver.ResolvePageModel<AddDoctorPageModel>());
            });
        }

        private void addDocument(object obj)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await PopupNavigation.Instance.PopAllAsync();
                await PopupNavigation.Instance.PopAllAsync();
                await App.Current.MainPage.Navigation.PushModalAsync(FreshPageModelResolver.ResolvePageModel<AddDoctorPageModel>());
            });
        }

        private void annulerpopup(object obj)
        {
            PopupNavigation.Instance.PopAllAsync();
        }

        public CantactSettingsPageModel()
        {
            
        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }

    }
}
