using be4care.Models;
using be4care.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class SearchPageModel : FreshMvvm.FreshBasePageModel
    {
        public bool loading { get; set; }
        public IList<Document> docs { get; set; }
        private IList<Document> _document { get; set; }
        public IList<Document> document {
            get
            {
                return _document ?? (_document = getDocuments());
            }
        }
        private IDocumentServices _documentSerives;
        private IRestServices _restServices;
        public Document selected
        {
            get
            {
                return null;
            }
            set
            {
                CoreMethods.PushPageModel<DocumentDetailsPageModel>(value);
                RaisePropertyChanged();
            }
        }

        public string search { get; set; }

        public void OnsearchChanged()
        {
            
            if (string.IsNullOrEmpty(search))
            {
                _document = docs;
            }
            else
            {
                _document = docs.Where(d =>  (d.doctor != null  && d.doctor.Contains(search)) ||( d.HStructure != null && d.HStructure.Contains(search)) || (d.note != null && d.note.Contains(search)) || (d.place != null && d.place.Contains(search))).ToList();
            }
        }

        public SearchPageModel(IDocumentServices _documentSerives, IRestServices _restServices)
        {
            //docs = new List<Document>();
            this._documentSerives = _documentSerives;
            this._restServices = _restServices;
        }
        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e); 
        }

        public IList<Document> getDocuments()
        {
            Task.Run(async () => {
                try
                {
                    docs = await _documentSerives.GetDocuments();
                    if (docs == null || docs.Count == 0)
                    {
                        loading = true;
                        docs = await _restServices.GetDocumentsAsync();
                        if (!(docs == null || docs.Count == 0))
                            _documentSerives.SaveDocuments(docs);
                        else
                            return;
                        loading = false;
                    }
                }
                catch
                {
                    loading = true;
                    Console.WriteLine("error getting documents from local database");
                    docs = await _restServices.GetDocumentsAsync();
                    if (!(docs == null || docs.Count == 0))
                        _documentSerives.SaveDocuments(docs);
                    else
                        return;
                    loading = false;
                }
            }).Wait();
            return docs;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
