using Akavache;
using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.Helpers
{
    public static class Settings
    {

        private static string authToken = string.Empty;

        public static string AuthToken
        {
            get {
                try
                {
                    //var token = BlobCache.Secure.GetObject<string>("token").ToString();
                        BlobCache.Secure.GetObject<string>("token")
                                .Subscribe(x => authToken = x, ex => Console.WriteLine("No token!"));
                    Console.WriteLine("token saved as : "+ authToken);
                    return authToken;
                }
                catch
                {
                    return null;
                }
                 }
            set {
                 authToken = value;
                 BlobCache.Secure.InsertObject("token", authToken);
                 Console.WriteLine("saved " +value);
            }
        }
    }
}
