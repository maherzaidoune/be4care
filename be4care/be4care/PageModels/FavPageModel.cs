using be4care.Models;
using be4care.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class FavPageModel : FreshMvvm.FreshBasePageModel
    {
        public IList<FavoriteGroupe> favorites { get; set; }
        public IList<Favorite> favs;
        private IFavServices _favServices;
        

        public FavPageModel(IFavServices _favServices)
        {
            Console.WriteLine("fav  page model construct");
            this._favServices = _favServices;
        }
        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            FavoriteGroupe documents = new FavoriteGroupe("Documents");
            FavoriteGroupe doctors = new FavoriteGroupe("Doctors");
            FavoriteGroupe hstructs = new FavoriteGroupe("Health Structs");
            try
            {
                Task.Run(async () =>
                {
                    documents.AddRange(await _favServices.GetFavDocumentAsync());
                    doctors.AddRange(await _favServices.GetFavDoctorsAsync());
                    hstructs.AddRange(await _favServices.GetFavHealthStructAsync());
                });
            }
            catch
            {

            }
            favorites = new List<FavoriteGroupe>();
            favorites.Add(documents);
            favorites.Add(doctors);
            favorites.Add(hstructs);
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            Console.WriteLine("fav  page model init");
            //FavoriteGroupe documents = new FavoriteGroupe("Documents");
            //FavoriteGroupe doctors = new FavoriteGroupe("Doctors");
            //FavoriteGroupe hstructs = new FavoriteGroupe("Health Structs");
            //try
            //{
            //    Task.Run(async () =>
            //    { 
            //        documents.AddRange(await _favServices.GetFavDocumentAsync());
            //        doctors.AddRange(await _favServices.GetFavDoctorsAsync());
            //        hstructs.AddRange(await _favServices.GetFavHealthStructAsync());
            //    });
            //}
            //catch
            //{

            //}
            //favorites = new List<FavoriteGroupe>();
            //favorites.Add(documents);
            //favorites.Add(doctors);
            //favorites.Add(hstructs);
        }
    }
}
