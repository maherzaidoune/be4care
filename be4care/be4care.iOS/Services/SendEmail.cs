using be4care.iOS.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;


[assembly: Dependency(typeof(SendEmail))]
namespace be4care.iOS.Services
{
    class SendEmail : ISendEmail
    {
        public void send(string adr)
        {
            Device.OpenUri(new Uri(String.Format("mailto:{0}", adr)));
        }
    }
}
