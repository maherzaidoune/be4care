using be4care.Helpers;
using be4care.Models;
using be4care.Services;
using Plugin.Connectivity;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class SplashPageModel : FreshMvvm.FreshBasePageModel
    {
        private IRestServices _restServices;
        public SplashPageModel(IDialogService _dialogServices, IRestServices _restServices)
        {
            this._restServices = _restServices;
        }
        public  override void Init(object initData)
        {
            base.Init(initData);

            
        }

    }
}
