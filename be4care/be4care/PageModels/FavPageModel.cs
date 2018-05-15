using be4care.Models;
using be4care.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class FavPageModel : FreshMvvm.FreshBasePageModel
    {
        public IList<FavoriteGroupe> favorites { get; set; }
        public IList<Favorite> favs;
        private  IDocumentServices _documentServices;
        private IDoctorServices _doctorServices;
        private IHStructServices _hStructServices;
        private IRestServices _restServices;


        public FavPageModel(IDocumentServices _documentServices,IDoctorServices _doctorServices,IHStructServices _hStructServices,IRestServices _restServices)
        {
            Console.WriteLine("fav  page model construct");
            this._doctorServices = _doctorServices;
            this._doctorServices = _doctorServices;
            this._hStructServices = _hStructServices;
            this._restServices = _restServices;

        }
        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            initView();
        }
        public  override void Init(object initData)
        {
            base.Init(initData);
        }
        public void initView()
        {
            Console.WriteLine("fav  page model init");
            FavoriteGroupe documents = new FavoriteGroupe("Documents");
            FavoriteGroupe doctors = new FavoriteGroupe("Doctors");
            FavoriteGroupe hstructs = new FavoriteGroupe("Health Structs");
            Task.Run(async () =>
            {
                try
                {
                    var docs = await _documentServices.GetDocuments();
                    if (docs == null || docs.Count == 0)
                        docs = new List<Document>();
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
                        docs = new List<Doctor>();
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
                        docs = new List<HealthStruct>();
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
