using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace be4care.Helpers
{
    public  class Settings
    {


        //private static string authToken = string.Empty;

        public static string AuthToken
        {
            get {
                try
                {
                    //working :)
                    return  CrossSecureStorage.Current.GetValue("token");
                }
                catch
                {
                    return String.Empty;
                }
                 }

            set {

                CrossSecureStorage.Current.SetValue("token", value);
                //authToken = value;
                Console.WriteLine("token saved to db as  : " +value);
            }
        }
    }
}
