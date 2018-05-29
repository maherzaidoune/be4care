using be4care.Models;
using be4care.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using System.Windows.Input;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class FavPageModel : FreshMvvm.FreshBasePageModel
    {
        public IList<FavoriteGroupe> favorites { get; set; }
        public IList<Favorite> favs;
        private IDocumentServices _documentServices;
        private IDoctorServices _doctorServices;
        private IHStructServices _hStructServices;
        private IRestServices _restServices;

        public Favorite selected
        {
            get
            {
                return null;
            }
            set
            {
                if (value != null)
                {
                    if (value is Contact)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await CoreMethods.PushPageModel<ContactDetailsPageModel>(value);
                            RaisePropertyChanged();
                        });
                    }
                    else if (value is Document)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await CoreMethods.PushPageModel<DocumentDetailsPageModel>(value);
                            RaisePropertyChanged();
                        });
                    }
                }
            }
        }

        public bool isVisible { get; private set; }

        public FavPageModel(IDocumentServices _documentServices, IDoctorServices _doctorServices, IHStructServices _hStructServices, IRestServices _restServices)
        {
            Console.WriteLine("fav  page model construct");
            this._doctorServices = _doctorServices;
            this._documentServices = _documentServices;
            this._hStructServices = _hStructServices;
            this._restServices = _restServices;

        }
        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            if (isVisible)
            {
                isVisible = false;
                MessagingCenter.Unsubscribe<DocumentPageModel>(this, "DocumentareUpdated");
                MessagingCenter.Unsubscribe<ContactPageModel>(this, "Contactupdated");
            }
            
        }
        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
            MessagingCenter.Subscribe<DocumentPageModel>(this, "DocumentareUpdated", updateDocs);
            MessagingCenter.Subscribe<ContactPageModel>(this, "Contactupdated", updateContact);
            //selected = null;
            isVisible = true;
        }

        private void updateContact(ContactPageModel obj)
        {
            Task.Run(async () =>
            {
                await initView();
            });
        }

        private void updateDocs(DocumentPageModel obj)
        {
            Task.Run(async () =>
            {
                await initView();
            });
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            isVisible = true;
            Task.Run(async () =>
            {
                await initView();
            });
        }

        public ICommand initViewCommand => new Command(async () => await ExecuteInitView());

        private Task ExecuteInitView()
        {
            throw new NotImplementedException();
        }

        public async Task initView()
        {
            FavoriteGroupe documents = new FavoriteGroupe("Documents");
            FavoriteGroupe doctors = new FavoriteGroupe("Médecin");
            FavoriteGroupe hstructs = new FavoriteGroupe("Structures de santé");
            
            await Task.Run(async () =>
            {
                try
                {
                    var docs = await _documentServices.GetDocuments();
                    if (docs == null || docs.Count == 0)
                    {
                        docs = new List<Document>();
                        docs = await _restServices.GetDocumentsAsync();
                    }
                    if (docs != null)
                        documents.AddRange(docs.Where(d => d.star));
                }
                catch
                {
                    var docs = await _restServices.GetDocumentsAsync();
                    if (!(docs == null))
                        documents.AddRange(docs.Where(d => d.star));
                }
                try
                {
                    var docs = await _doctorServices.GetDoctors();
                    if (docs == null || docs.Count == 0)
                    {
                        docs = new List<Doctor>();
                        docs = await _restServices.GetDoctorsAsync();
                    }
                    if (docs != null)
                        doctors.AddRange(docs.Where(d => d.star));
                }
                catch
                {
                    var docs = await _restServices.GetDoctorsAsync();
                    if (!(docs == null))
                        documents.AddRange(docs.Where(d => d.star));
                }
                try
                {
                    var docs = await _hStructServices.GetStructs();
                    if (docs == null || docs.Count == 0)
                    {
                        docs = new List<HealthStruct>();
                        docs = await _restServices.GetHealthStructs();
                    }
                    if (docs != null)
                        hstructs.AddRange(docs.Where(d => d.star));
                }
                catch
                {
                    var docs = await _restServices.GetHealthStructs();
                    if (!(docs == null))
                        hstructs.AddRange(docs.Where(d => d.star));
                }
            });
            
            favorites = new List<FavoriteGroupe>();
            favorites.Add(documents);
            favorites.Add(doctors);
            favorites.Add(hstructs);
        }
    }
}