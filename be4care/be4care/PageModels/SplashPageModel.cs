﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace be4care.PageModels
{
    class SplashPageModel : FreshMvvm.FreshBasePageModel
    {
        public async override void Init(object initData)
        {
            base.Init(initData);
            await System.Threading.Tasks.Task.Delay(4000);
            await  CoreMethods.PushPageModel<onBoardingPageModel>();
            RaisePageWasPopped();
        }
        

    }
}
