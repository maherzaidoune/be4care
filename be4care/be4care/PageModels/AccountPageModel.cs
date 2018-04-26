using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class AccountPageModel : FreshMvvm.FreshBasePageModel
    {
        public AccountPageModel()
        {
                
        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
