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
        public string user { get; set; }
        public string password { get; set; }
        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }
        //public bool visible { get; set; }

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
        

        private  void login(object obj)
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
                Task.Run(async () =>
                {
                    await Task.Run(() =>
                    {
                        _doctorServices.DeleteDoctors();
                        _hstructServices.DeleteStructs();
                        _documentServices.DeleteDocuments();
                    });
                    if (_restServices.GetAccessToken(user, password))
                    {
                        await Task.Run(() =>
                        {
                            ButtonBar.initBar();
                        });
                        _dialogService.ShowMessage("Connecté", false);
                        var _user = _restServices.GetMyProfile();
                        _userServices.SaveUser(_user);
                        password = String.Empty;
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
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CoreMethods.PushPageModel<InscriptionPageModel>();
                RaisePageWasPopped();
            });
            
        }


        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            //if (visible)
            //{
            //    if (!CrossConnectivity.Current.IsConnected)
            //    {
            //        _dialogService.ShowMessage("Verifier votre connection internet", true);
            //    }
            //    visible = false;
            //}  
        }
        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
            //visible = true;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            //visible = true;
            isBusy = false;
            isEnabled = true;
            
        }
    }
}
