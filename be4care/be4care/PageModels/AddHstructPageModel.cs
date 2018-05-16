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
    class AddHstructPageModel : FreshMvvm.FreshBasePageModel
    {
        private IRestServices _restServices;
        private IHStructServices _hStructServices;
        private IDialogService _dialogSservices;

        public string fullName { get; set; }
        public string adress { get; set; }
        public string phNumber { get; set; }
        public string email { get; set; }
        public bool star { get; set; }
        public string id { get; set; }

        public bool isEdit { get; set; }

        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }
        public bool unstar { get; set; }

        public void OnstarChanged()
        {
            unstar = !star;
        }

        public ICommand save => new Command(addHstruct);
        public ICommand backClick => new Command(back);

        private void back(object obj)
        {
            if (!isEdit)
                App.Current.MainPage.Navigation.PopModalAsync();
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    CoreMethods.RemoveFromNavigation();
                    await CoreMethods.PopPageModel();
                    RaisePropertyChanged();
                });
            }
        }

        private void addHstruct(object obj)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                isEnabled = false;
                isBusy = true;
            });
            var hstruct = new HealthStruct()
            {
                fullName = fullName,
                adress = adress,
                phNumber = phNumber,
                email = email,
                star = star
            };

            if (!isEdit)
            {
            Task.Run(async () =>
            {
                try
                {
                    if (await _restServices.AddHealthStruct(hstruct))
                    {
                        await _hStructServices.SaveStruct(hstruct);
                        MessagingCenter.Send(this, "HstructUpdated");
                        _dialogSservices.ShowMessage(fullName + " a été ajouté avec succès ", false);
                       
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await App.Current.MainPage.Navigation.PopModalAsync();
                        });
                    }
                }
                catch
                {
                    Console.WriteLine("error adding new hstruct");
                    _dialogSservices.ShowMessage("Erreur , veuillez essayer plus tard", true);
                }
            });
                   
            }
            if (isEdit)
            {
                Task.Run(async () =>
                {
                hstruct.id = id;
                try
                {
                   
                    if (_restServices.UpdateHStruct(hstruct))
                    {
                        await _hStructServices.UpdateHStructAsync(hstruct);
                        MessagingCenter.Send(this, "HstructUpdated");
                        _dialogSservices.ShowMessage(fullName + " a été modifié avec succès ", false);
                        
                    }
                    
                }
                catch
                {
                    Console.WriteLine("error adding new hstruct");
                    _dialogSservices.ShowMessage("Erreur , veuillez essayer plus tard", true);
                }
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await CoreMethods.PopPageModel();
                    RaisePropertyChanged();
                });
                });
            }  

            isBusy = false;
            isEnabled = true;

        }

        public AddHstructPageModel(IRestServices _restServices, IHStructServices _hStructServices, IDialogService _dialogSservices)
        {
            this._dialogSservices = _dialogSservices;
            this._hStructServices = _hStructServices;
            this._restServices = _restServices;
        }
        public override void Init(object initData)
        {
            base.Init(initData); 
            if (initData != null)
            {
                var hstruct = initData as HealthStruct;
                isEdit = true;
                fullName = hstruct.fullName;
                adress = hstruct.adress;
                phNumber = hstruct.phNumber;
                email = hstruct.email;
                star = hstruct.star;
                unstar = hstruct.unstar;
                id = hstruct.id;
            }
            else
            {
                isEdit = false;
                star = false;
            }
            isBusy = false;
            isEnabled = true;
        }
    }
}
