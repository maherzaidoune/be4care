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
    class DocumentDetailsPageModel : FreshMvvm.FreshBasePageModel
    {
        [AddINotifyPropertyChangedInterface]
        public class detail
        {
            public string menu { get; set; }
            public string info { get; set; }
        }


        public Document doc { get; set; }
        public string pUrl { get; set; }
        public bool star { get; set; }
        public bool unstar { get; set; }
        public IList<detail> details { get; set; }
        private IRestServices _restService;
        private IDocumentServices _documentServices;
        private IDialogService _dialogServices;

        public ICommand backClick => new Command(backClickbutton);
        public ICommand fav => new Command(makenonfav);
        public ICommand unfav => new Command(makefav);
        public ICommand progress => new Command(progreechanged);

        private void progreechanged(object obj)
        {
            Console.WriteLine(obj);
        }

        private void backClickbutton()
        {
            CoreMethods.PopPageModel(); // pop instead of pushing previous page
            RaisePropertyChanged();
        }

        private void makenonfav(Object obj)
        {
            doc.star = false;
            star = doc.star;
            unstar = true;
            Task.Run(async () =>
            {
                
                try
                {
                    await _documentServices.UpdateDocument(doc);
                    _restService.UpdateDocument(doc);
                    MessagingCenter.Send(this, "documentupdated");
                    _dialogServices.ShowMessage("Retirer de la liste des favouris", false);
                }
                catch
                {
                    Console.WriteLine("error makenonfav ");
                    _dialogServices.ShowMessage("Erreur ", true);
                    star = true;
                    unstar = false;
                }
            });
            
        }

        private void makefav(Object obj)
        {
            doc.star = true;
            star = doc.star;
            unstar = false;
            Task.Run(async () =>
            {
                try
                {
                    //await _favServices.AddFavDocumentAsync(doc);
                    await _documentServices.UpdateDocument(doc);
                    _restService.UpdateDocument(doc);
                    MessagingCenter.Send(this, "documentupdated");
                    _dialogServices.ShowMessage("Ajouter au liste des favouris", false);
                }
                catch
                {
                    Console.WriteLine("error makefav ");
                    _dialogServices.ShowMessage("Erreur ", true);
                    star = false;
                    unstar = true;
                }
            });

        }

        public DocumentDetailsPageModel(IRestServices _restService,IDocumentServices _documentServices,IDialogService _dialogServices)
        {
            this._restService = _restService;
            this._documentServices = _documentServices;
            this._dialogServices = _dialogServices;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            doc = initData as Document;
            pUrl = doc.url;
            star = doc.star;
            unstar = doc.unstar;
            details = new List<detail>()
            {
                new detail {menu = "Date de  document" ,  info=doc.date.ToString("MM/dd/yyyy") },
                new detail {menu = "Professionnel de  santé" ,  info= doc.dr },
                new detail {menu = "Type  de document" ,  info = doc.type },
                new detail {menu = "Structure de  santé" ,  info=doc.HStructure },
                new detail {menu = "Lieu" ,  info= doc.place },
                new detail {menu = "Notes" ,  info= doc.note}
            };
        }
    }
}
