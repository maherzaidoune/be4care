using be4care.Models;
using be4care.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class AddDocPageModel : FreshMvvm.FreshBasePageModel
    {
        private IRestServices _restServices;
        private IUserServices _userServices;
        private IDialogService _dialogServices;
        private IDocumentServices _documentServices;
        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }
        private User user;
        public ICommand addDoc => new Command(addDocument);
        public Document document { get; set; }

        private async void addDocument(object obj)
        {
            isEnabled = false;
            isBusy = true;
            var url = string.Empty;
            var result = string.Empty; ;
            await Task.Run(async () =>
            {
                try
                {
                    url = await upload(user);
                    Console.WriteLine("AddDocPageModel "+url);
                    if (string.IsNullOrEmpty(url))
                        await Task.Delay(100);
                    result = await analyse(url);
                    if (string.IsNullOrEmpty(result))
                        await Task.Delay(100);
                    document = new Document { url = url, text = result };
                    if(!string.IsNullOrEmpty(result))
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await CoreMethods.PushPageModel<DocDetailsPageModel>(document);
                            RaisePropertyChanged();
                        });
                        _dialogServices.ShowMessage("document ajouté avec succes", false);
                    }
                    else
                    {
                        isBusy = false;
                        isEnabled = true;
                        _dialogServices.ShowMessage("Erreur : veuillez verifier votre connection internet ", true);

                    }

                }
                catch (Exception e)
                {

                    _dialogServices.ShowMessage("Erreur : veuillez verifier votre connection internet ", true);
                    isBusy = false;
                    isEnabled = true;
                }
            });
           

        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            
            //addDocument(null);

        }

        public  AddDocPageModel(IRestServices _restServices,IUserServices _userServices,IDialogService _dialogServices,IDocumentServices _documentServices)
        {
                this._restServices = _restServices;
               this._userServices = _userServices;
            this._dialogServices = _dialogServices;
        }

        public IUserServices _UserServices { get; }


        public override void Init(object initData)
        {
            base.Init(initData);
            Task.Run(async () =>
            {
                user = await _userServices.GetUser();
            });
            isBusy = false;
            isEnabled = true;
        }

        public async Task<string> upload(User user)
        {
            {
                try
                {
                    var photo = await _restServices.Upload(user);
                    return photo;
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR : " + e.StackTrace);
                    return null;
                }
            }
        }
        public async Task<string> analyse(string url)
        {
            {
                try
                {
                    var photo = await _restServices.Analyse(url);
                    return photo;
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR : " + e.StackTrace);
                    return null;
                }
            }
        }
    }
}
