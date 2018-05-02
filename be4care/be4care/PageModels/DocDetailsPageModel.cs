using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class DocDetailsPageModel : FreshMvvm.FreshBasePageModel
    {
        public DocDetailsPageModel()
        {

        }

        public override void Init(object initData)
        {
            base.Init(initData);
            var result = initData as string;
            Console.WriteLine(result);
        }
    }
}
