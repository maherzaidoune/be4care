using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace be4care.Services
{
    class DialogService : FreshMvvm.FreshBasePageModel, IDialogService
    {
        public  void ShowMessage(string message, string title, string buttonText,bool hasButton)
        {
             Device.BeginInvokeOnMainThread(async () =>
            {
                await PopupNavigation.Instance.PopAllAsync();
                await PopupNavigation.Instance.PushAsync(new Pages.PopUp.ShowErrorPage(message, title, buttonText, hasButton));
                if (!hasButton)
                {
                    await Task.Delay(1500);
                    await PopupNavigation.Instance.PopAllAsync(true);
                }
            });
            
        }  
    }
}
