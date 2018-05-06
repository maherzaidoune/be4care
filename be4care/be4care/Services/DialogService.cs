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
        public  void ShowMessage(string message)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                var toastConfig = new ToastConfig(message);
                toastConfig.SetDuration(2000);
                toastConfig.MessageTextColor = System.Drawing.Color.Black;
                toastConfig.SetPosition(ToastPosition.Bottom);
                toastConfig.SetBackgroundColor(System.Drawing.Color.FromArgb(255, 112, 67));
                UserDialogs.Instance.Toast(toastConfig);
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
