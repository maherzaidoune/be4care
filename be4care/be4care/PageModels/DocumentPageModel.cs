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
    class DocumentPageModel : FreshMvvm.FreshBasePageModel
    {

        public IList<Document> documents { get; set; }
        private IDocumentServices _documentSerives;
        private IRestServices _restServices;

        public string tri { get; set; }

        public IList<DocumentsGroupe> dateDocs { get; set; }
        public IList<DocumentsGroupe> drDocs { get; set; }
        public IList<DocumentsGroupe> typeDocs { get; set; }
        public IList<DocumentsGroupe> hsDocs { get; set; }


        public Document selectedDoc
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


        public DocumentPageModel(IDocumentServices _documentSerives, IRestServices _restServices)
        {
            Console.WriteLine("document  page model construct");
            this._documentSerives = _documentSerives;
            this._restServices = _restServices;
            tri = "Professionnel";
        }

        protected  override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            UpdateView();
        }

        public override void Init(object initData)
        {
            base.Init(initData);
        }
        public void UpdateView()
        {
            Task.Run(async () => {
                try
                {
                    documents = await _documentSerives.GetDocuments();
                    if (documents == null)
                    {
                        documents = await _restServices.GetDocumentsAsync();
                        _documentSerives.SaveDocuments(documents);
                    }
                }
                catch
                {
                    Console.WriteLine("error getting doctors from local database");
                    documents = await _restServices.GetDocumentsAsync();
                    _documentSerives.SaveDocuments(documents);
                }


            });
        }
    }

    
}
