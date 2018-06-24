using be4care.Helpers;
using be4care.Models;
using be4care.Services;
using Plugin.Connectivity;
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
            try
            {
                url = await upload(user);
                Console.WriteLine("AddDocPageModel "+url);
                if (string.IsNullOrEmpty(url))
                {
                    if (!CrossConnectivity.Current.IsConnected)
                    {
                        _dialogServices.ShowMessage("Erreur : veuillez verifier votre connection internet ", true);
                    }
                    else
                    {
                        _dialogServices.ShowMessage("Erreur  ", true);
                    }
                    
                }
                else
                {
                    await Task.Run( () =>
                    {
                        result = analyse(url);
                        if (string.IsNullOrEmpty(result))
                        {
                            _dialogServices.ShowMessage("Erreur : Veuillez réessayer plus tard", true);
                        }
                        else
                        {
                            document = new Document {
                                url = url,
                                note = result,
                                date = DateTime.Now,
                                dr = StringHelper.getDr(result),
                                place = StringHelper.getHStructure(result),
                                type =  StringHelper.gettype(result)
                            };
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await CoreMethods.PushPageModel<DocDetailsPageModel>(new Tuple<Document,bool>(document,true));
                                RaisePropertyChanged();
                            });
                        }
                    });
                    
                }
                    
                isBusy = false;
                isEnabled = true;

            }
            catch (Exception e)
            {

                _dialogServices.ShowMessage("Erreur  : Veuillez réessayer plus tard", true);
                isBusy = false;
                isEnabled = true;
            }
           

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
                if (user == null)
                {
                    try
                    {
                        user = await _userServices.GetUser();
                        if (user == null)
                            user = _restServices.GetMyProfile();
                    }
                    catch
                    {
                        user = null;
                    }
                    
                }
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
        public  string analyse(string url)
        {
            {
                try
                {
                    var photo =  _restServices.Analyse(url);
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
