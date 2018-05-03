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
            Console.WriteLine("fav  page model construct");

        }
        public override void Init(object initData)
        {
            base.Init(initData);
            Console.WriteLine("fav  page model init");

        }
    }
}
