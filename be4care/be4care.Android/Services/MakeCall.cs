using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using be4care.Droid.Services;
using Xamarin.Forms;


[assembly: Dependency(typeof(MakeCall))]
namespace be4care.Droid.Services
{
    class MakeCall : IMakeCall
    {
        public void Call(string number)
        {
            Device.OpenUri(new Uri(String.Format("tel:{0}", number)));
        }
    }
}