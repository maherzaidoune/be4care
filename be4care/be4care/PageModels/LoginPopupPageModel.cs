using PropertyChanged;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class LoginPopupPageModel
    {
       
        public LoginPopupPageModel()
        {
        }
        public ICommand validate => new Command(login);
        public ICommand retour => new Command(retourClicked);

        private void login(object obj)
        {
            var t = Task.Factory.StartNew(() => Console.WriteLine(Services.RestServices.login(user, password)));
        }

        private async void retourClicked(object obj)
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
        public string user { get; set; }
        public string password { get; set; }
    }
}
