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
        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }
        private User user;
        public ICommand addDoc => new Command(addDocument);
        public Document document { get; set; }

        private async void addDocument(object obj)
        {
            isEnabled = false;
            isBusy = true;
            var url = "nothing  to show";
            var result = "nothing  to show";
            await Task.Run(async () =>
            {
                try
                {
                    url = await upload(user);
                    Console.WriteLine("AddDocPageModel "+url);
                    result = await analyse(url);
                    document = new Document { url = url, text = result };
                    if(result  != null)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await CoreMethods.PushPageModel<DocDetailsPageModel>(document);
                            RaisePropertyChanged();
                        });
                    }
                    else
                    {
                        isBusy = false;
                        isEnabled = true;
                    }
                    
                }
                catch (Exception e)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Console.WriteLine("Error add document method " + e.StackTrace);
                    });
                    isBusy = false;
                    isEnabled = true;
                }
            });
           

        }

        public  AddDocPageModel(IRestServices _restServices,IUserServices _userServices)
        {
                this._restServices = _restServices;
               this._userServices = _userServices;
        }

        public IUserServices _UserServices { get; }

        public async override void Init(object initData)
        {
            base.Init(initData);
            Console.WriteLine("addDocPageModel init method called ! ");
            user = await _userServices.GetUser();
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
