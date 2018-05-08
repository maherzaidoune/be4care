using be4care.Services;
using PropertyChanged;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using be4care.Models;
using System.Threading.Tasks;
using Plugin.Connectivity;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class InscriptionPageModel : FreshMvvm.FreshBasePageModel
    {
        private readonly IDialogService _dialogservices;
        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }
        public ICommand backClick => new Command(backButtonClick);
        public ICommand validate => new Command(validateButtonClicked);

        private  void validateButtonClicked(object obj)
        {
            var t = Utils.EntryCheck.checkentries(num, email, password, acceptTerms);
            if (!t.Item1)
            {
                _dialogservices.ShowMessage(t.Item2,true);
            } else if (!(passverif.Equals(password)))
            {
                _dialogservices.ShowMessage("Verifiez votre mot de passe",true);
            }
            else
            {
                isEnabled = false;
                isBusy = true;
                Task.Run( () =>
                {
                    if (_RestService.Inscription(email,password))
                    {
                        var me = new User { email = email, phNumber = num };
                        _userServices.SaveUser(me);
                        try
                        {
                            _doctorServices.DeleteDoctors();
                            _hstructServices.DeleteStructs();
                            _documentServices.DeleteDocuments();
                        }
                        catch
                        {
                            Console.WriteLine("iscription page : error deleting old data");
                        }
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await CoreMethods.PushPageModel<AddUserPageModel>();
                            RaisePageWasPopped();
                        });
                    }
                    password = String.Empty;
                    isEnabled = true;
                    isBusy = false;
                });
            }

        }

        public string email { get; set; }
        public string num { get; set; }
        public string password { get; set; }
        public string passverif { get; set; }
        public bool acceptTerms { get; set; }
        private IRestServices _RestService { get; set; }
        public IUserServices _userServices { get; set; }
        private IHStructServices _hstructServices;
        private IDoctorServices _doctorServices;
        private IDocumentServices _documentServices;

        private async void backButtonClick(object obj)
        { 
            await CoreMethods.PushPageModel<LoginPopupPageModel>();
            RaisePageWasPopped();
        }

        public InscriptionPageModel(IDialogService  _dialogservices,IRestServices _restService,IUserServices _userServices, IHStructServices _hstructServices, IDoctorServices _doctorServices, IDocumentServices _documentServices)
        {
            this._dialogservices = _dialogservices;
            this._RestService = _restService;
            this._userServices = _userServices;
            this._hstructServices = _hstructServices;
            this._doctorServices = _doctorServices;
            this._documentServices = _documentServices;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            if (!CrossConnectivity.Current.IsConnected)
            {
                _dialogservices.ShowMessage("Verifier votre connection internet",true);
            }
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            //restore user date
            acceptTerms = false;
            isEnabled = true;
            isBusy = false;
        }

    }

}
