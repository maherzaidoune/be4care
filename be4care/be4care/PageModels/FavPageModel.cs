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
                selected = null;
            }
        }

        public bool isVisible { get; private set; }

        public FavPageModel(IDocumentServices _documentServices, IDoctorServices _doctorServices, IHStructServices _hStructServices, IRestServices _restServices)
        {
            this._doctorServices = _doctorServices;
            this._documentServices = _documentServices;
            this._hStructServices = _hStructServices;
            this._restServices = _restServices;

        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            if (isVisible)
            {
                //await initView();
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

        private  void updateContact(ContactPageModel obj)
        {
             initView();
        }

        private  void updateDocs(DocumentPageModel obj)
        {
              initView();
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            isVisible = true;
            initView();
            
        }

        
        private async Task<IList<Document>> GetDocuments()
        {
            try
            {
                var docs = await _documentServices.GetDocuments();
                if (docs == null || docs.Count == 0)
                {
                    //docs = new List<Document>();
                    docs = await _restServices.GetDocumentsAsync();
                }
                return docs;
            }
            catch
            {
                return null;
            }
            
        }
        private async Task<IList<HealthStruct>> GetHealthStructs()
        {
            try
            {
                var docs = await _hStructServices.GetStructs();
                if (docs == null || docs.Count == 0)
                {
                    docs = await _restServices.GetHealthStructs();
                }
                return docs;
            }
            catch
            {
                return null;
            }
        }
        private async Task<IList<Doctor>> GetDoctors()
        {
            try
            {
                var docs = await _doctorServices.GetDoctors();
                if (docs == null || docs.Count == 0)
                {
                    docs = await _restServices.GetDoctorsAsync();
                }
                return docs;
            }
            catch
            {
                return null;
            }
        }

        private async Task<IList<FavoriteGroupe>> GetFavorites()
        {
            var documents = await GetDocuments();
            var doctors = await GetDoctors();
            var healthstructs = await GetHealthStructs();
            var documentFav = new FavoriteGroupe("Documents");
            var doctorFav = new FavoriteGroupe("Médecin");
            var hstructsFav = new FavoriteGroupe("Structures de santé");
            documentFav.AddRange(documents?.Where(d => d.star));
            doctorFav.AddRange(doctors?.Where(d => d.star));
            hstructsFav.AddRange(healthstructs?.Where(d => d.star));
            return new List<FavoriteGroupe>()
            {
                documentFav,doctorFav,hstructsFav
            };
        }
        private Task refresh;

        async Task Refresh()
        {
            favorites = await GetFavorites();
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