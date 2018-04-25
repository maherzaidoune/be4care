using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace be4care.Services
{
    class DialogService : FreshMvvm.FreshBasePageModel, IDialogService
    {
        public  async void login()
        {
            await PopupNavigation.Instance.PopAllAsync();
            await PopupNavigation.Instance.PushAsync(new Pages.PopUp.LoginPopupPage());
        }

        public async void ShowMessage(string message, string title, string buttonText,bool hasButton)
        {
            await PopupNavigation.Instance.PopAllAsync();
            await PopupNavigation.Instance.PushAsync(new Pages.PopUp.ShowErrorPage(message,title,buttonText,hasButton));
            if (!hasButton)
            {
                await Task.Delay(1500);
                await PopupNavigation.Instance.PopAllAsync(true);
            }
        }  
    }
}
