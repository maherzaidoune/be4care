using be4care.Models;
using be4care.Pages;
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
    class DocumentPageModel : FreshMvvm.FreshBasePageModel
    {

        public IList<Document> documents { get; set; }
        private IDocumentServices _documentSerives;
        private IRestServices _restServices;
        private IDialogService _dialogServices;

        public string tri { get; set; }

        public IList<DocumentsGroupe> dateDocs { get; set; }
        public IList<DocumentsGroupe> drDocs { get; set; }
        public IList<DocumentsGroupe> typeDocs { get; set; }
        public IList<DocumentsGroupe> hsDocs { get; set; }
        public IList<DocumentsGroupe> list { get; set; }
        public IList<DocumentsGroupe> temp { get; set; }

        public ICommand trichange => new Command(TriOption);

        private void TriOption(object obj)
        {
            MessagingCenter.Subscribe<TriOptionPageModel,int>(this, "triepar",trierpar );
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new TriOptionPage());
            });
        }

        private void trierpar(TriOptionPageModel arg1, int arg2)
        {
            if (arg2 == 0)
            {
                tri = "Date de  Document";
                list = dateDocs;
            }
            if (arg2 == 1)
            {
                tri = "Professionnel de santé";
                list = drDocs;
            }
            if (arg2 == 2)
            {
                tri = "Type de document";
                list = typeDocs;
            }
            if (arg2 == 3)
            {
                tri = "Structure de santé";
                list = hsDocs;
            }
            Application.Current.Properties["tri"] = tri;

            Task.Run(async () =>
            {
                await UpdateView();
            });
        }

        public bool checkduplicate(IList<DocumentsGroupe> list,DocumentsGroupe group)
        {
            foreach (DocumentsGroupe g in list)
            {
                if (list.Count == 0)
                    return true;
                if (g.title.Equals(group.title))
                    return false;
            }
            return true;
        }

        public void InitGroups(IList<Document> documents,string tri)
        {
            list = new List<DocumentsGroupe>();
            if (documents == null || documents.Count == 0)
            {
                return;

            }

            if (tri.Equals("Date de  Document"))
            {
                DocumentsGroupe group = null;
                dateDocs = new List<DocumentsGroupe>();
                DateTime date = DateTime.MinValue;
                foreach (Document d in documents)
                {
                    if (d.date != date)
                    {
                        date = d.date;
                        date.AddMonths(1);
                        group = new DocumentsGroupe(date.ToString("MM/dd/yyyy"));

                        if (!checkduplicate(dateDocs, group))
                            continue;

                        foreach (Document docs in documents)
                        {
                            if (d.date == docs.date)
                            {
                                group.Add(docs);
                            }
                        }
                        dateDocs.Add(group);
                    }
                }

                list = dateDocs;
            }
            if (tri.Equals("Professionnel de santé"))
            {
                DocumentsGroupe group = null;
                drDocs = new List<DocumentsGroupe>();
                string dr = string.Empty;
                foreach (Document d in documents)
                {
                    if (!d.dr.Equals(dr))
                    {
                        dr = d.dr;
                        group = new DocumentsGroupe(dr);

                        if (!checkduplicate(drDocs, group))
                            continue;

                        foreach (Document docs in documents)
                        {
                            
                            if (d.dr.Equals(docs.dr))
                            {
                                group.Add(docs);
                            }
                        }
                       drDocs.Add(group);
                    }
                }

                list = drDocs;
            }
            if (tri.Equals("Type de document"))
            {
                DocumentsGroupe group = null;
                typeDocs = new List<DocumentsGroupe>();
                string type = string.Empty;
                foreach (Document d in documents)
                {
                    if (!d.type.Equals(type))
                    {
                        type = d.type;
                        group = new DocumentsGroupe(type);
                        if (!checkduplicate(typeDocs, group))
                            continue;
                        foreach (Document docs in documents)
                        {
                            
                            if (d.type.Equals(docs.type))
                            {
                                group.Add(docs);
                            }
                        }
                        typeDocs.Add(group);
                    }
                }

                list = typeDocs;
            }
            if (tri.Equals("Structure de santé"))
            {
                DocumentsGroupe group = null;
                hsDocs = new List<DocumentsGroupe>();
                string str = string.Empty;
                foreach (Document d in documents)
                {
                    if (!d.HStructure.Equals(str))
                    {
                        str = d.HStructure;
                        group = new DocumentsGroupe(str);
                        if (!checkduplicate(hsDocs, group))
                            continue;
                        foreach (Document docs in documents)
                        {
                            if (d.HStructure.Equals(docs.HStructure))
                            {
                                group.Add(docs);
                            }
                        }
                        hsDocs.Add(group);
                    }
                }

                list = hsDocs;
            }
        }



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


        public DocumentPageModel(IDocumentServices _documentSerives, IRestServices _restServices, IDialogService _dialogServices)
        {
            Console.WriteLine("document  page model construct");
            this._documentSerives = _documentSerives;
            this._restServices = _restServices;
            this._dialogServices = _dialogServices;
            if (Application.Current.Properties.ContainsKey("tri"))
            {
                tri = Application.Current.Properties["tri"] as string;
            }

            if (string.IsNullOrEmpty(tri))
                tri = "Professionnel de santé";
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            Console.WriteLine("viewIsAppearing ");
        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            MessagingCenter.Subscribe<DocDetailsPageModel>(this, "documentadded", update);
            MessagingCenter.Subscribe<DocDetailsPageModel>(this, "documentnoadded", UpdateAndSave);
            MessagingCenter.Subscribe<DocumentDetailsPageModel>(this, "documentupdated", documentUpdated);
            Task.Run(async () =>
            {
                await UpdateView();
            });
            // move updatevie w 
            Console.WriteLine("init ");

        }

        private void documentUpdated(DocumentDetailsPageModel obj)
        {
            Task.Run(async () =>
            {
                await UpdateView();
            });
        }

        private void update(DocDetailsPageModel obj)
        {
            Task.Run(async () =>
            {
                await UpdateView();
            });
        }

        public async Task UpdateView()
        {
            documents = new List<Document>();
            await Task.Run(async () => {
                try
                {
                    documents = await _documentSerives.GetDocuments();
                    if (documents == null || documents.Count == 0)
                    {
                        documents = await _restServices.GetDocumentsAsync();
                        if(!(documents == null || documents.Count == 0))
                            _documentSerives.SaveDocuments(documents);
                    }
                }
                catch
                {
                    Console.WriteLine("error getting documents from local database");
                    documents = await _restServices.GetDocumentsAsync();
                    if (!(documents == null || documents.Count == 0))
                        _documentSerives.SaveDocuments(documents);
                }
                InitGroups(documents, tri);
            });
        }

        public void UpdateAndSave(DocDetailsPageModel obj)
        {
            documents = new List<Document>();
            Task.Run(async () => {
                try
                {
                    documents = await _restServices.GetDocumentsAsync();
                    if (!(documents == null || documents.Count == 0))
                        _documentSerives.SaveDocuments(documents);
                }
                catch
                {
                    _dialogServices.ShowMessage("Erreur : erreur lors de l'enregistrement du document en  cache", true);
                }
                InitGroups(documents, tri);
            });
        }

    }

    
}
