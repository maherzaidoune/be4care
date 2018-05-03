using be4care.Models;
using be4care.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Console.WriteLine("contact  page model construct");
        }
        public async override void Init(object initData)
        {
            base.Init(initData);
            Console.WriteLine("contact  page model init");

            try
            {
                doctors = await _doctorServices.GetDoctors();
                if (doctors == null)
                    throw new Exception();
            }
            catch
            {
                Console.WriteLine("error getting doctors from local database");
                doctors = await _restServices.GetDoctorsAsync();
                if(_doctorServices.SaveDoctors(doctors))
                    Console.WriteLine("Doctors  saved");
            }
            try
            {
                healthStructs = await _hStructServices.GetStructs();
                if (healthStructs == null)
                    throw new Exception();
            }
            catch
            {
                Console.WriteLine("error getting health structs from local database");
                healthStructs = await _restServices.GetHealthStructs();
                _hStructServices.SaveStructs(healthStructs);
            }
            var groupDoc = new ContactGroup("Médecin");
            groupDoc.AddRange(doctors.OrderBy(d=> !d.star));
            var groupHealth = new ContactGroup("Structure de Santé");
            groupHealth.AddRange(healthStructs.OrderBy(d => !d.star));
            contacts = new List<ContactGroup>();
            contacts.Add(groupDoc);
            contacts.Add(groupHealth);
        }

    }
}
