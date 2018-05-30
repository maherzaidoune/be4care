using be4care.Models;
using be4care.Pages.Popup;
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
    class ContactPageModel : FreshMvvm.FreshBasePageModel
    {
        private IRestServices _restServices;
        private IDoctorServices _doctorServices;
        private IHStructServices _hStructServices;
        //public IList<Doctor> doctors { get; set; }
        //public IList<HealthStruct> healthStructs { get; set; }
        public IList<ContactGroup> contacts { get; set; }

        public ICommand edit => new Command(contactSettings);

        private void contactSettings(object obj)
        {
            MessagingCenter.Subscribe<DoctorsListPageModel>(this, "newDoctorAdd", doctorlistupdated);
            MessagingCenter.Subscribe<HstructListPageModel>(this, "newStructAdd", hstListCantactUpdated);

            Device.BeginInvokeOnMainThread(async () =>
            {
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new CantactSettingsPage());
            });
        }

        private void hstListCantactUpdated(HstructListPageModel obj)
        {
            Task.Run(async () =>
            {
                await initView();
            });
        }

        private void doctorlistupdated(DoctorsListPageModel obj)
        {
            Task.Run(async () =>
            {
                await initView();
            });
        }

        private void docCantactUpdated(AddDoctorPageModel obj)
        {
            Task.Run(async () =>
            {
                await initView();
            });
            MessagingCenter.Unsubscribe<AddDoctorPageModel>(this, "doctorupdated");
        }
        private void hstCantactUpdated(AddHstructPageModel obj)
        {
            Task.Run(async () =>
            {
                await initView();
            });
            MessagingCenter.Unsubscribe<AddHstructPageModel>(this, "HstructUpdated");
        }

        public Contact selectedContact
        {
            get
            {
                return null;
            }
            set
            {
                Device.BeginInvokeOnMainThread(async() =>
                {
                    await CoreMethods.PushPageModel<ContactDetailsPageModel>(value);
                    RaisePropertyChanged();
                    MessagingCenter.Subscribe<AddDoctorPageModel>(this, "doctorupdated", docCantactUpdated);
                    MessagingCenter.Subscribe<AddHstructPageModel>(this, "HstructUpdated", hstCantactUpdated);
                    MessagingCenter.Subscribe<ContactDetailsPageModel>(this, "delete", delete);
                });
                
                
            }
        }

        
        private void delete(ContactDetailsPageModel obj)
        {
            Task.Run(async () =>
            {
                await initView();
            });
            MessagingCenter.Unsubscribe<ContactDetailsPageModel>(this, "delete");
        }

        public ICommand backClick => new Command(backClickbutton);

        private void backClickbutton(object obj)
        {
            CoreMethods.PushPageModel<AccountPageModel>();
            RaisePropertyChanged();
        }


        public ContactPageModel(IRestServices _restServices, IDoctorServices _doctorServices, IHStructServices _hStructServices)
        {
            this._restServices = _restServices;
            this._doctorServices = _doctorServices;
            this._hStructServices = _hStructServices;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Task.Run(async () =>
            {
                await initView();

            });
        }

        private async Task<IList<Doctor>> GetDoctors()
        {
            try
            {
                var doctors = await _doctorServices.GetDoctors();
                if (doctors == null || doctors.Count == 0)
                {
                    doctors = await _restServices.GetDoctorsAsync();
                    if (doctors != null && doctors.Count > 0)
                        _doctorServices.SaveDoctors(doctors);
                    else
                        doctors = new List<Doctor>();
                }
                return doctors;
            }
            catch
            {
                var doctors = await _restServices.GetDoctorsAsync();
                if (doctors != null && doctors.Count > 0)
                    _doctorServices.SaveDoctors(doctors);
                else
                    doctors = new List<Doctor>();
                return doctors;
            }
        }
        private async Task<IList<HealthStruct>> GetHealthStructs()
        {
            try
            {
                var healthStructs = await _hStructServices.GetStructs();
                if (healthStructs == null || healthStructs.Count == 0)
                {
                    healthStructs = await _restServices.GetHealthStructs();
                    if (healthStructs != null && healthStructs.Count > 0)
                        _hStructServices.SaveStructs(healthStructs);
                    else
                        healthStructs = new List<HealthStruct>();
                }
                return healthStructs;
            }
            catch
            {
                var healthStructs = await _restServices.GetHealthStructs();
                if (healthStructs != null && healthStructs.Count > 0)
                    _hStructServices.SaveStructs(healthStructs);
                else
                    healthStructs = new List<HealthStruct>();
                return healthStructs;
            }
        }
        private async Task<IList<ContactGroup>> GetContacts()
        {
            var doctors = await GetDoctors();
            var healthStructs = await GetHealthStructs();
            var groupDoc = new ContactGroup("Médecin");
            var groupHealth = new ContactGroup("Structure de Santé");
            if (doctors.Count > 0)
                groupDoc.AddRange(doctors.OrderBy(d => !d.star));
            if (healthStructs.Count > 0)
                groupHealth.AddRange(healthStructs.OrderBy(d => !d.star));
            return new List<ContactGroup>
            {
                groupDoc,groupHealth
            };
        }

        private Task refresh;

        async Task Refresh()
        {
            contacts = await GetContacts();
            MessagingCenter.Send(this, "Contactupdated");
        }
        public Task initView()
        {
            if (refresh?.IsCompleted ?? true)
            {
                refresh = Refresh();
            }
            return refresh;
        }

    }
}