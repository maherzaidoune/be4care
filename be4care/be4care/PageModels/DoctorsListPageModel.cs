using be4care.Models;
using be4care.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class DoctorsListPageModel : FreshMvvm.FreshBasePageModel
    {
        [AddINotifyPropertyChangedInterface]
        public class DocList
        {
            public bool add { get; set; }
            public Doctor doctor { get; set; }
            public string fullName { get; set; }
        }
        private IRestServices _restServices;
        private IDoctorServices _doctorServices;
        private IHStructServices _hStructServices;
        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }
        public DocList selected { get; set; }

        public ICommand backClick => new Command(backClickbutton);

        private void backClickbutton(object obj)
        {
            Task.Run(async () =>
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
            });
        }


        public string search { get; set; }
        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
            }
        }


        public ICommand refresh => new Command(refreshing);

        private void refreshing(object obj)
        {
            IsRefreshing = true;
            Task.Run(async () =>
            {
                var docs = await _restServices.GetAllDoctors();

                if(docs != null)
                {
                    _doctorServices.saveAllDoctors(docs);
                    foreach (Doctor d in docs)
                    {
                        if (userdoctors != null && userdoctors.Count > 0)
                        {
                            foreach (Doctor ud in userdoctors)
                            {
                                if (d.id == ud.id)
                                {
                                    doctors.Add(new DocList { add = false, doctor = d, fullName = d.fullName });
                                    break;
                                }
                            }
                            doctors.Add(new DocList { add = true, doctor = d, fullName = d.fullName });
                        }
                        else
                        {
                            doctors.Add(new DocList { add = true, doctor = d, fullName = d.fullName });
                        }
                    }
                }

                list = doctors;
                IsRefreshing = false;
                
            });
            
        }

        public void OnsearchChanged()
        {
            if (string.IsNullOrEmpty(search))
            {
                list = doctors;
            }
            else
            {
                list = doctors.Where(d => d.fullName.Contains(search)).ToList();
            }

        }


        public ICommand addDoctor => new Command(addthisDoctor);

        private async void addthisDoctor(object obj)
        {
            isEnabled = false;
            isBusy = true;
            await Task.Run(async () =>
            {

                var doc = obj as DocList;
                var d = doc.doctor;
                if (await _restServices.AddDoctorFromDB(d.id)) {
                    await _doctorServices.SaveDoctor(d);
                    MessagingCenter.Send(this, "newDoctorAdd");
                    _dialogServices.ShowMessage(d.fullName + " a été ajouter a votre liste de contact",false);

                }
                else
                {
                    _dialogServices.ShowMessage("Erreur : Veuillez réessayer plus tard",true);
                }
                isBusy = false;
                isEnabled = true;
                doc.add = false;
                //refreshing(null);
            });
        }

        public IList<DocList> doctors { get; set; }
        public IList<DocList> list { get; set; }
        public IList<Doctor> userdoctors { get; set; }
        private IDialogService _dialogServices;

        public DoctorsListPageModel(IDialogService _dialogServices,IRestServices _restServices, IDoctorServices _doctorServices, IHStructServices _hStructServices)
        {
            this._restServices = _restServices;
            this._doctorServices = _doctorServices;
            this._hStructServices = _hStructServices;
            this._dialogServices = _dialogServices;
            doctors = new List<DocList>();
        }
        public override void Init(object initData)
        {
            base.Init(initData);

            isBusy = false;
            Task.Run(async () =>
            { 
                await UpdateList();
            });
        }

        public async Task UpdateList()
        {
            try
            {
                userdoctors = await _doctorServices.GetDoctors();
                if (userdoctors == null || userdoctors.Count == 0)
                    userdoctors = new List<Doctor>();
            }
            catch
            {
                Console.WriteLine("can't get user doctor list");
                userdoctors = null;
            }
            try
            {
                var docs = await _doctorServices.GetAllDoctors();
                if (docs == null)
                {
                    docs = await _restServices.GetAllDoctors();
                    _doctorServices.saveAllDoctors(docs);
                }

                foreach(Doctor d in docs)
                {
                    if (userdoctors != null && userdoctors.Count > 0)
                    {
                        foreach (Doctor ud in userdoctors)
                        {
                            if (d.id == ud.id)
                            {
                                doctors.Add(new DocList { add = false, doctor = d, fullName = d.fullName });
                                break;
                            }
                        }
                        doctors.Add(new DocList { add = true, doctor = d, fullName = d.fullName });
                    }
                    else
                    {
                        doctors.Add(new DocList { add = true, doctor = d, fullName = d.fullName });
                    }
                }
            }
            catch
            {
                Console.WriteLine("error getting doctors from local database");
                var docs = await _restServices.GetAllDoctors();
                if (_doctorServices.saveAllDoctors(docs))
                    Console.WriteLine("Doctors  saved");
                foreach (Doctor d in docs)
                {
                    foreach (Doctor ud in userdoctors)
                    {
                        if (d.id == ud.id)
                        {
                            doctors.Add(new DocList { add = false, doctor = d, fullName = d.fullName });
                            break;
                        }
                    }
                    doctors.Add(new DocList { add = true, doctor = d, fullName = d.fullName });
                }

            }
            isBusy = false;
            isEnabled = true;
            list = doctors;
            
        }
    }
}
