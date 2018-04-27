using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class ContactPageModel : FreshMvvm.FreshBasePageModel
    {
        public ContactPageModel()
        {

        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
