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
    class AddDoctorPageModel : FreshMvvm.FreshBasePageModel
    {

        public string fullName { get; set; }
        public string adress { get; set; }
        public string phNumber { get; set; } 
        public string email { get; set; }
        public string healthStruct { get; set; }
        public bool star { get; set; }
        public string specialite { get; set; }
        public string note { get; set; }

        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }

        private IRestServices _restServices;
        private IDoctorServices _doctorServices;
        private IDialogService _dialogSservices;

        public ICommand save => new Command(addDoctor);
        public ICommand backClick => new Command(back);

        private void back(object obj)
        {
            App.Current.MainPage.Navigation.PopModalAsync();
        }

        private void addDoctor(object obj)
        {

            isEnabled = false;
            isBusy = true;
            
            Task.Run(async() =>
            {
                try
                {
                    var doctor = new Doctor()
                    {
                        fullName = fullName,
                        adress = adress,
                        phNumber = phNumber,
                        email = email,
                        healthStruct = healthStruct,
                        specialite = specialite,
                        note = note,
                        star = star
                    };

                    if (await _restServices.AddDoctor(doctor))
                    {
                        await _doctorServices.SaveDoctor(doctor);
                        MessagingCenter.Send(this, "doctorupdated");
                        _dialogSservices.ShowMessage(fullName + " a été ajouté avec succès ",false);
                    }
                }
                catch
                {
                    Console.WriteLine("error adding new Doctor");
                    _dialogSservices.ShowMessage("Erreur , veuillez essayer plus tard",true);
                }
                isBusy = false;
                isEnabled = true;
                await App.Current.MainPage.Navigation.PopModalAsync();

            });
            
        }

        public AddDoctorPageModel(IRestServices _restServices,IDoctorServices _doctorServices,IDialogService _dialogSservices)
        {
            this._doctorServices = _doctorServices;
            this._restServices = _restServices;
            this._dialogSservices = _dialogSservices;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            isBusy = false;
            isEnabled = true;
        }
    }
}
