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

        public ICommand save => new Command(saveDoc);

        private IRestServices _restService;

        private void saveDoc(object obj)
        {
            isEnabled = false;
            isBusy = true;
            Task.Run(() => {
                try
                {
                    if ( _restService.addDocument(document))
                    {
                        Console.WriteLine("DocDetails : document  added succefuly" + document.url);
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await CoreMethods.PushPageModel<DocumentDetailsPageModel>(document);
                            RaisePropertyChanged();
                        });
                       
                    }
                    isBusy = false;
                    isEnabled = true;
                }
                catch
                {
                    Console.WriteLine("DocDetails : Error");
                    isBusy = false;
                    isEnabled = true;
                }
            });
            
        }

        public DocDetailsPageModel(IRestServices _restService)
        {
            this._restService = _restService;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            document = initData as Document;
            Console.WriteLine(document.text);
            document.dr = StringHelper.getDr(document.text);
            document.place = StringHelper.getplace(document.text);
            try
            {
                document.date = DateTime.Parse(StringHelper.getDate(document.text));
            }
            catch
            {
                document.date = DateTime.Now;
            }
            document.note = StringHelper.getnote(document.text);

            isBusy = false;
            isEnabled = true;
        }



    }
}
