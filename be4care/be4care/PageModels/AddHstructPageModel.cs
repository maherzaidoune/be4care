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

        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }

        public ICommand save => new Command(addHstruct);
        public ICommand backClick => new Command(back);

        private void back(object obj)
        {
            App.Current.MainPage.Navigation.PopModalAsync();
        }

        private void addHstruct(object obj)
        {
            isEnabled = false;
            isBusy = true;

            Task.Run(async () =>
            {
                try
                {
                    var hstruct = new HealthStruct()
                    {
                        fullName = fullName,
                        adress = adress,
                        phNumber = phNumber,
                        email = email,
                        star = star
                    };

                    if (await _restServices.AddHealthStruct(hstruct))
                    {
                        await _hStructServices.SaveStruct(hstruct);
                        MessagingCenter.Send(this, "HstructUpdated");
                        _dialogSservices.ShowMessage(fullName + " a été ajouté avec succès ");
                    }
                }
                catch
                {
                    Console.WriteLine("error adding new Doctor");
                    _dialogSservices.ShowMessage("Erreur , veuillez essayer plus tard");
                }
                isBusy = false;
                isEnabled = true;
                await App.Current.MainPage.Navigation.PopModalAsync();

            });

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
            isBusy = false;
            isEnabled = true;
        }
    }
}
