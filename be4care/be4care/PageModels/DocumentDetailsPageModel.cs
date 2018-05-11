using be4care.Models;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
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

        public ICommand backClick => new Command(backClickbutton);

        private void backClickbutton(object obj)
        {
            //CoreMethods.PushPageModel<AddDocPageModel>();
            CoreMethods.PopPageModel();
            RaisePropertyChanged();
        }

        public DocumentDetailsPageModel()
        {

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
                new detail {menu = "Date de  document" ,  info=doc.date.ToString() },
                new detail {menu = "Professionnel de  santé" ,  info= doc.dr },
                new detail {menu = "Type  de document" ,  info = doc.type },
                new detail {menu = "Structure de  santé" ,  info=doc.HStructure },
                new detail {menu = "Lieu" ,  info= doc.place },
                new detail {menu = "Notes" ,  info= doc.note}
            };
        }
    }
}
