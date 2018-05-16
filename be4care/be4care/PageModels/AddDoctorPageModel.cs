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
        public string id { get; set; }
        public bool unstar { get; set; }
        public bool isEdit { get; set; }

        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }

        private IRestServices _restServices;
        private IDoctorServices _doctorServices;
        private IDialogService _dialogSservices;

        public ICommand save => new Command(addDoctor);
        public ICommand backClick => new Command(back);
        
        public void OnstarChanged()
        {
            unstar = !star;
        }

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


        private void addDoctor(object obj)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                isEnabled = false;
                isBusy = true;
            });
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
            if(!isEdit)
            {
                Task.Run(async () =>
                {
                   
                    try
                    {
                        if (await _restServices.AddDoctor(doctor))
                        {
                            await _doctorServices.SaveDoctor(doctor);
                            MessagingCenter.Send(this, "doctorupdated");
                            _dialogSservices.ShowMessage(fullName + " a été ajouté avec succès ", false);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("error adding new Doctor " );
                        _dialogSservices.ShowMessage("Erreur , veuillez essayer plus tard", true);
                    }
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await App.Current.MainPage.Navigation.PopModalAsync();
                    });
                });  
            }

            if (isEdit)
            {
                Task.Run(async () =>
                {
                    doctor.id = id;
                    try
                    {

                        if (_restServices.UpdateDoctor(doctor))
                        {
                            await _doctorServices.UpdateDoctor(doctor);
                            MessagingCenter.Send(this, "doctorupdated");
                            _dialogSservices.ShowMessage(fullName + " a été modifié avec succès ", false);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("error editing doctor ");
                        _dialogSservices.ShowMessage("Erreur , veuillez essayer plus tard", true);
                    }
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await CoreMethods.PopPageModel();
                        CoreMethods.RemoveFromNavigation();
                        RaisePropertyChanged();
                    });
                });  
            }
            isBusy = false;
            isEnabled = true;

        }
            
                       

        public AddDoctorPageModel( IRestServices _restServices,IDoctorServices _doctorServices,IDialogService _dialogSservices)
        {
            this._doctorServices = _doctorServices;
            this._restServices = _restServices;
            this._dialogSservices = _dialogSservices;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            Console.WriteLine( "init method called addDoctorPageModel \n");
            if(initData != null)
            {
                var doctor = initData as Doctor; 
                fullName = doctor.fullName;
                adress = doctor.adress;
                phNumber = doctor.phNumber;
                email = doctor.email;
                healthStruct = doctor.healthStruct;
                specialite = doctor.specialite;
                note = doctor.note;
                star = doctor.star;
                unstar = doctor.unstar;
                id = doctor.id;
                isEdit = true; 
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
