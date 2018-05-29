using Acr.UserDialogs;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Extensions;
using be4care.Pages;

namespace be4care.Services
{
    class DialogService : FreshMvvm.FreshBasePageModel, IDialogService
    {
        public  void ShowMessage(string message,bool error)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                var toastConfig = new ToastConfig(message);
                toastConfig.SetDuration(1500);
                if (error)
                {
                    toastConfig.SetBackgroundColor(System.Drawing.Color.Red);
                }
                else
                {
                    toastConfig.SetBackgroundColor(System.Drawing.Color.LimeGreen);
                }
                toastConfig.MessageTextColor = System.Drawing.Color.White;
                toastConfig.SetPosition(ToastPosition.Bottom);
                Device.BeginInvokeOnMainThread(() =>
                {
                    UserDialogs.Instance.Toast(toastConfig);
                });
            });
            
        }

        public void verifier()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new VerifPopPage());
            });
        }
    }
}
