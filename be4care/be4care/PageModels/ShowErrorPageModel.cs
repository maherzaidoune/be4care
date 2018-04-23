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
    class ShowErrorPageModel
    {
        public ICommand errorButton => new Command(retourClicked);

        private async void retourClicked(object obj)
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}
