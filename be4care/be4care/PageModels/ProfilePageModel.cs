using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class ProfilePageModel : FreshMvvm.FreshBasePageModel
    {
        public string label { get; set; }
        public ProfilePageModel()
        {

        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
