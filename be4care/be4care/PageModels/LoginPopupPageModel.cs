using be4care.Helpers;
using be4care.Services;
using Plugin.Connectivity;
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
        private IHStructServices _hstructServices;
        private IDoctorServices _doctorServices;
        private IDocumentServices _documentServices;
        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }
        public LoginPopupPageModel(IRestServices _restServices, IDialogService _dialogService, IUserServices _userServices, IHStructServices _hstructServices, IDoctorServices _doctorServices, IDocumentServices _documentServices)
        {
            this._dialogService = _dialogService;
            this._restServices = _restServices;
            this._userServices = _userServices;
            this._hstructServices = _hstructServices;
            this._doctorServices = _doctorServices;
            this._documentServices = _documentServices;
        }
      
        public ICommand validate => new Command(login);
        public ICommand inscription => new Command(inscriptionButton);
        

        private async void login(object obj)
        {

            var t = Utils.EntryCheck.checkentries(user, password);
            if (!t.Item1)
            {
                _dialogService.ShowMessage(t.Item2,true);
            }
            else
            {
                isEnabled = false;
                isBusy = true;
                await Task.Run( () =>
                {
                    if (_restServices.GetAccessToken(user, password))
                    {
                        password = String.Empty;
                        //get user data
                        var _user = _restServices.GetMyProfile();
                        _userServices.SaveUser(_user);
                        _doctorServices.DeleteDoctors();
                        _hstructServices.DeleteStructs();
                        _documentServices.DeleteDocuments();
                         ButtonBar.initBar();
                    }
                    else
                    {
                        _dialogService.ShowMessage("Verifiez vos donnés",true);
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

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            if (!CrossConnectivity.Current.IsConnected)
            {
                _dialogService.ShowMessage("Verifier votre connection internet",true);
            }
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Console.WriteLine("login  page model init");

            isBusy = false;
            isEnabled = true;
        }
    }
}
