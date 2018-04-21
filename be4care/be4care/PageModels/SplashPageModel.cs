using System;
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
            Console.WriteLine("init ...........");
            await System.Threading.Tasks.Task.Delay(2000);
            await  CoreMethods.PushPageModel<onBoardingPageModel>();
            RaisePageWasPopped();
        }

    }
}
