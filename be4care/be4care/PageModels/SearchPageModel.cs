﻿using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class SearchPageModel : FreshMvvm.FreshBasePageModel
    {
        public SearchPageModel()
        {
            Console.WriteLine("search  page model construct");
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            Console.WriteLine("search  page model construct");
        }
    }
}
