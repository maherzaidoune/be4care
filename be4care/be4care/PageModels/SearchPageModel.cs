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
        private IList<Document> _document { get; set; }
        public IList<Document> document { get; set; }
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
                document = _document;
            }
            else
            {
                document = _document.Where(d =>  (d.doctor != null  && d.doctor.Contains(search)) ||( d.HStructure != null && d.HStructure.Contains(search)) || (d.note != null && d.note.Contains(search)) || (d.place != null && d.place.Contains(search))).ToList();
            }
        }

        public SearchPageModel(IDocumentServices _documentSerives, IRestServices _restServices)
        {
            this._documentSerives = _documentSerives;
            this._restServices = _restServices;
        }
        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e); 
        }

        public async Task<IList<Document>> GetDocuments()
        {
            var docs = await _documentSerives.GetDocuments();
            try
            {
                    if (docs == null || docs.Count == 0)
                    {
                        docs = await _restServices.GetDocumentsAsync();
                        if (!(docs == null || docs.Count == 0))
                            _documentSerives.SaveDocuments(docs);
                        else
                            return null;
                    }
                }
                catch
                {
                    docs = await _restServices.GetDocumentsAsync();
                    if (!(docs == null || docs.Count == 0))
                        _documentSerives.SaveDocuments(docs);
                    else
                        return null;
                }
            return docs;
        }

        private Task refresh;

        async Task Refresh()
        {
            _document = await GetDocuments();
            document = _document;
        }
        public Task initView()
        {
            if (refresh?.IsCompleted ?? true)
            {
                refresh = Refresh();
            }
            return refresh;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            initView();
        }
    }
}
