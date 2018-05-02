using Acr.UserDialogs;

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
    }
}
