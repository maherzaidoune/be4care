using be4care.Models;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class DocumentDetailsPageModel : FreshMvvm.FreshBasePageModel
    {
        public Document doc { get; set; }
        public DocumentDetailsPageModel()
        {

        }
        public override void Init(object initData)
        {
            base.Init(initData);
            doc = initData as Document;
        }
    }
}
