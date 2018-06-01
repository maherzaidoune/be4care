using be4care.Models;
using be4care.Pages;
using be4care.Services;
using FreshMvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class ProfilePageModel : FreshMvvm.FreshBasePageModel
    {
        [AddINotifyPropertyChangedInterface]
        public class detail
        {
            public string menu { get; set; }
            public string info { get; set; }
        }
        public string pUrl { get; set; }
        public ICommand photochange => new Command(photoclicked);
        public ICommand edit => new Command(editUser);
        public ICommand backClick => new Command(backClickbutton);
        public bool isVisible { get; set; }
        private IUserServices _userServices;
        private IRestServices _restServices;
        private IDialogService _dialogServices;
        private IList<detail> _details { get; set; }
        public IList<detail> details
        {
            get
            {
                return _details ?? (_details = GetDetails());
            }
        }

        private void backClickbutton(object obj)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CoreMethods.PopPageModel();
                RaisePropertyChanged();
            });
        }

        private User user;

        private void editUser(object obj)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new OptionPage("profile"));
            });
        }

        public void updateProfile(EditProfilePageModel editProfilePage)
        {
            //Task.Run(async () =>
            //{
            //    await initView();
            //});
            _details = GetDetails();
        }

        private void photoclicked(object obj)
        {
            Console.WriteLine("photclicked !");
        }

        

        public ProfilePageModel(IUserServices _userServices, IRestServices _restServices,IDialogService _dialogServices)
        {
            this._userServices = _userServices;
            this._restServices = _restServices;
            this._dialogServices = _dialogServices;
            Console.WriteLine("profile constructor called");
        }

        public string convertSexe(bool sex)
        {
            if (sex)
                return "Homme";
            return "Femme";
        }
        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            if (isVisible)
            {
                MessagingCenter.Unsubscribe<EditProfilePageModel>(this, "updateProfile");
                isVisible = false;
            }
        }
        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
            MessagingCenter.Subscribe<EditProfilePageModel>(this, "updateProfile", updateProfile);
            isVisible = true;
        }

        public override  void Init(object initData)
        {
            base.Init(initData);
            isVisible = true;
        }

        public List<detail>  GetDetails()
        {
            try
            {
                Task.Run(async () =>
                {
                    user = await _userServices.GetUser();
                    if (user == null)
                    {
                        user = _restServices.GetMyProfile();
                        _userServices.SaveUser(user);
                    }
                    pUrl = user.pUrl;
                }).Wait(); 
            }
            catch
            {
                Console.WriteLine("cant get user from db");
                user = _restServices.GetMyProfile();
                _userServices.SaveUser(user);
            }
            return new List<detail>()
                {
                    new detail {menu = "Identifiant" ,  info=user.email },
                    new detail {menu = "Nom" ,  info= user.name },
                    new detail {menu = "Prenom" ,  info = user.lastName },
                    new detail {menu = "Numéro de telephone" ,  info=user.phNumber },
                    new detail {menu = "Date de naissance" ,  info= user.bDate.ToString("MM/dd/yyyy") },
                    new detail {menu = "sexe" ,  info=  convertSexe(user.sex)}
                };
        }
    }
}
