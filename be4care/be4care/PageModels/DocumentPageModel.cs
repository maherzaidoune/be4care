using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class DocumentPageModel : FreshMvvm.FreshBasePageModel
    {
        public DocumentPageModel()
        {
            Console.WriteLine("document  page model construct");

        }
        public override void Init(object initData)
        {
            base.Init(initData);
            Console.WriteLine("document  page model init");

        }
    }
}
