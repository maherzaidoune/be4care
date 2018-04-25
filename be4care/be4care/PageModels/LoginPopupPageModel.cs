using be4care.Services;
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

        private IRestServices _restServices;
        private IDialogService _dialogService;

        public LoginPopupPageModel()
        {
            this._restServices = new RestServices();
            this._dialogService = new DialogService();
        }
      
        public ICommand validate => new Command(login);
        public ICommand retour => new Command(retourClicked);

        private void login(object obj)
        {

            if ( _restServices.GetAccessToken(user, password))
            {
                _dialogService.ShowMessage("Connecté", "Succes", null, false);
            }
            else
            {

                _dialogService.ShowMessage("Verifiez vos donné", "Erreur", null, false);
            }

        }

        private async void retourClicked(object obj)
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
        public string user { get; set; }
        public string password { get; set; }

        
    }
}
