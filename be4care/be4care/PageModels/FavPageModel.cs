using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class FavPageModel : FreshMvvm.FreshBasePageModel
    {
        public FavPageModel()
        {

        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
