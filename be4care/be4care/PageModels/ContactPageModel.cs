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
        public IList<Doctor> doctors { get; set; }
        public IList<HealthStruct> healthStructs { get; set; }
        public List<ContactGroup> contacts { get; set; }

        public ICommand edit => new Command(contactSettings);

        private void contactSettings(object obj)
        {
            MessagingCenter.Subscribe<AddDoctorPageModel>(this, "doctorupdated", docCantactUpdated);
            MessagingCenter.Subscribe<AddHstructPageModel>(this, "HstructUpdated", hstCantactUpdated);
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
                await updateCantact();
            });
        }

        private void doctorlistupdated(DoctorsListPageModel obj)
        {
            Task.Run(async () =>
            {
                await updateCantact();
            });
        }

        private void docCantactUpdated(AddDoctorPageModel obj)
        {
            Task.Run(async () =>
            {
                await updateCantact();
            });
        }
        private void hstCantactUpdated(AddHstructPageModel obj)
        {
            Task.Run( async() =>
            {
                 await updateCantact();
            });
        }

        public Contact selectedContact
        {
            get
            {
                return null;
            }
            set
            {
                CoreMethods.PushPageModel<ContactDetailsPageModel>(value);
                RaisePropertyChanged();
            }
        }
        public ICommand backClick => new Command(backClickbutton);

        private void backClickbutton(object obj)
        {
            CoreMethods.PushPageModel<AccountPageModel>();
            RaisePropertyChanged();
        }


        public ContactPageModel(IRestServices _restServices,IDoctorServices _doctorServices,IHStructServices _hStructServices)
        {
            this._restServices = _restServices;
            this._doctorServices = _doctorServices;
            this._hStructServices = _hStructServices;
        }


        protected  override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Task.Run(async () =>
            {
                await updateCantact();

            });
        }

        public async Task updateCantact()
        {
            await Task.Run(async () =>
            {
                try
                {
                    doctors = await _doctorServices.GetDoctors();
                    if (doctors == null || doctors.Count ==0)
                    {
                        doctors = await _restServices.GetDoctorsAsync();
                        if (doctors != null && doctors.Count > 0)
                            _doctorServices.SaveDoctors(doctors);
                        else
                            doctors = new List<Doctor>();
                    }
                }
                catch
                {
                    Console.WriteLine("error getting doctors from local database");
                    doctors = await _restServices.GetDoctorsAsync();
                    if (doctors != null && doctors.Count > 0)
                        _doctorServices.SaveDoctors(doctors);
                    else
                        doctors = new List<Doctor>();

                }
                try
                {
                    healthStructs = await _hStructServices.GetStructs();
                    if (healthStructs == null || healthStructs.Count==0)
                    {
                        healthStructs = await _restServices.GetHealthStructs();
                        if (healthStructs != null && healthStructs.Count > 0)
                            _hStructServices.SaveStructs(healthStructs);
                        else
                            healthStructs = new List<HealthStruct>();
                    }
                }
                catch
                {
                    Console.WriteLine("error getting health structs from local database");
                    healthStructs = await _restServices.GetHealthStructs();
                    if(healthStructs != null && healthStructs.Count > 0)
                        _hStructServices.SaveStructs(healthStructs);
                    else
                        healthStructs = new List<HealthStruct>();
                }
                
            var groupDoc = new ContactGroup("Médecin");
            var groupHealth = new ContactGroup("Structure de Santé");
            if(doctors.Count>0)
                groupDoc.AddRange(doctors.OrderBy(d => !d.star));
            if(healthStructs.Count>0)
                groupHealth.AddRange(healthStructs.OrderBy(d => !d.star));

            contacts = new List<ContactGroup>();
                contacts.Add(groupDoc);
                contacts.Add(groupHealth);
                
            });
        }

    }
}
