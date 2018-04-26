using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using PropertyChanged;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class onBoardingPageModel : FreshMvvm.FreshBasePageModel
    {

        public bool isBack { get; set; }
        public int myPosition { get; set; }
        public Xamarin.Forms.View[] viewList { get; set; }

        public ICommand CarousalPositionChanged => new Xamarin.Forms.Command(PositionChanged);
        public ICommand backClicked => new Xamarin.Forms.Command(backLabelClick);
        public ICommand closeClicked => new Xamarin.Forms.Command(closeLabelClick);

        private async void closeLabelClick(object obj)
        {
            await CoreMethods.PushPageModel<LoginPopupPageModel>();
            RaisePageWasPopped();
        }

        private void backLabelClick(object obj)
        {
            if (isBack)
                myPosition -= 1;
        }

        private void PositionChanged(object obj)
        {

            isBack = myPosition != 0;
            
        }

        public onBoardingPageModel()
        {

        }

        public override void Init(object initData)
        {
            base.Init(initData);

           
            myPosition = 0;
            
            viewList = new Xamarin.Forms.View[]
            {
                new Pages.onBoarding.FirstPage(),
                new Pages.onBoarding.SecondPage(),
                new Pages.onBoarding.ThirdPage()
            };
        }
       




    }
}
