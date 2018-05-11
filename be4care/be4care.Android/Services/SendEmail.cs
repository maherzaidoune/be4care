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


[assembly: Dependency(typeof(SendEmail))]
namespace be4care.Droid.Services
{
    class SendEmail : ISendEmail
    {
        public void send(string adr)
        {
            Device.OpenUri(new Uri(String.Format("mailto:{0}", adr)));
        }
    }
}