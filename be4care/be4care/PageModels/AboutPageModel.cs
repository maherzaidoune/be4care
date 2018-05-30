using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class AboutPageModel : FreshMvvm.FreshBasePageModel
    {
        public ICommand backClick => new Command(backClickbutton);

        private void backClickbutton(object obj)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CoreMethods.PopPageModel();
                RaisePropertyChanged();
            });
        }
        public AboutPageModel()
        {

        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }

    }
}
