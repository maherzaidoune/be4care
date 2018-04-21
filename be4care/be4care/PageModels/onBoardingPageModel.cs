using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.PageModels
{

    class onBoardingPageModel : FreshMvvm.FreshBasePageModel
    {
        public Xamarin.Forms.View[] viewList { get; set; }
        public onBoardingPageModel()
        {

        }

        public override void Init(object initData)
        {
            base.Init(initData);
            viewList = new Xamarin.Forms.View[]
            {
                new Pages.onBoarding.FirstPage(),
                new Pages.onBoarding.SecondPage(),
                new Pages.onBoarding.ThirdPage()
            };
        }
        

    }
}
