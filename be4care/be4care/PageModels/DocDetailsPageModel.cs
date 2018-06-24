using be4care.Helpers;
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
    class DocDetailsPageModel : FreshMvvm.FreshBasePageModel
    {
        public Document document { get; set; }
        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }
        public bool isNew { get; set; }

        public ICommand save => new Command(saveDoc);

        private IRestServices _restService;
        private IDocumentServices _documentServices;
        private IDialogService _dialogServices;
        public ICommand backClick => new Command(backClickbutton);

        private void backClickbutton(object obj)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CoreMethods.PopPageModel();
                RaisePageWasPopped();
                RaisePropertyChanged();
            });
        }

        private void saveDoc(object obj)
        {
            isEnabled = false;
            isBusy = true;
            if (isNew)
            {
                Task.Run(async () => {
                    try
                    {
                        //document.date = DateTime.Now;
                        await _documentServices.SaveDocument(document);
                        MessagingCenter.Send(this, "documentadded");
                        if (_restService.addDocument(document))
                            {
                                _dialogServices.ShowMessage("document ajouté avec succes", false);
                        }
                        else
                            {
                                _dialogServices.ShowMessage("Erreur", true);
                            }
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            //await CoreMethods.PushPageModel<DocumentDetailsPageModel>(document);
                            await CoreMethods.PopPageModel();
                            //CoreMethods.RemoveFromNavigation();
                            RaisePropertyChanged();
                        });

                        isBusy = false;
                        isEnabled = true;
                    }
                    catch
                    {
                        isBusy = false;
                        isEnabled = true;
                    }
                });
            }
            else
            {
                Task.Run(async() =>
                {
                    try
                    {
                        await _documentServices.UpdateDocument(document);
                        MessagingCenter.Send(this, "documentadded");
                        if (_restService.UpdateDocument(document))
                        {
                            _dialogServices.ShowMessage(document.type + " a été modifié avec succès ", false);
                        }
                        else
                        {
                            _dialogServices.ShowMessage("Erreur , veuillez essayer plus tard", true);
                        }
                    }
                    catch
                    {
                        _dialogServices.ShowMessage("Erreur , veuillez essayer plus tard", true);
                    }
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await CoreMethods.PopPageModel();
                        //CoreMethods.RemoveFromNavigation();
                        RaisePropertyChanged();
                    });
                });

            }
            
            
        }

        public DocDetailsPageModel(IRestServices _restService,IDocumentServices _documentServices,IDialogService _dialogServices)
        {
            this._restService = _restService;
            this._documentServices = _documentServices;
            this._dialogServices = _dialogServices;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            if (initData != null)
            {
                var data = initData as Tuple<Document, bool>;
                //document = initData as Document;
                document = data.Item1;
                //document.dr = StringHelper.getDr(document.text);
                //document.place = StringHelper.getplace(document.text);
                //document.date = DateTime.Now;
                //document.note = StringHelper.getnote(document.text);
                isNew = data.Item2;
            }
            isBusy = false;
            isEnabled = true;
        }



    }
}
