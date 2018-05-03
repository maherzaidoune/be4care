using be4care.Helpers;
using be4care.Services;
using PropertyChanged;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class LoginPopupPageModel : FreshMvvm.FreshBasePageModel
    { 
        private IRestServices _restServices;
        private IDialogService _dialogService;
        private IUserServices _userServices;
        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }
        public LoginPopupPageModel(IRestServices _restServices, IDialogService _dialogService, IUserServices _userServices)
        {
            this._dialogService = _dialogService;
            this._restServices = _restServices;
            this._userServices = _userServices;
        }
      
        public ICommand validate => new Command(login);
        public ICommand inscription => new Command(inscriptionButton);
        

        private async void login(object obj)
        {

            var t = Utils.EntryCheck.checkentries(user, password);
            if (!t.Item1)
            {
                _dialogService.ShowMessage(t.Item2);
            }
            else
            {
                isEnabled = false;
                isBusy = true;
                await Task.Run(async () =>
                {
                    if (_restServices.GetAccessToken(user, password))
                    {
                        password = String.Empty;
                        //get user data
                        var _user = _restServices.GetMyProfile();
                        _userServices.SaveUser(_user);
                        await ButtonBar.initBar();
                    }
                    else
                    {
                        _dialogService.ShowMessage("Verifiez vos donné");
                        isBusy = false;
                        isEnabled = true;
                    }
                });
            }
        }

        private  void inscriptionButton(object obj)
        {
            CoreMethods.PushPageModel<InscriptionPageModel>();
            RaisePageWasPopped();
            Console.WriteLine("login  page model construct");

        }
        public string user { get; set; }
        public string password { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);
            Console.WriteLine("login  page model init");

            isBusy = false;
            isEnabled = true;
        }
    }
}
