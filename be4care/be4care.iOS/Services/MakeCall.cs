using be4care.iOS.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;


[assembly: Dependency(typeof(MakeCall))]
namespace be4care.iOS.Services
{
    class MakeCall : IMakeCall
    {
        public void Call(string number)
        {
            Device.OpenUri(new Uri(String.Format("tel:{0}", number)));
        }
    }
}
