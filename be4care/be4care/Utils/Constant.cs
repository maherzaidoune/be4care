using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.Utils
{
    public class Constant
    {
        private static string _url = "https://peaceful-forest-76384.herokuapp.com/api/users/";
        private static string _urlLogin = "https://peaceful-forest-76384.herokuapp.com/api/users/login/";
        //need access token
        private static string _urlLogout = "https://peaceful-forest-76384.herokuapp.com/api/users/logout";
        //me
        private static string _urlme = "https://peaceful-forest-76384.herokuapp.com/api/users/me/";



        public static string url
        {
            get
            {
                return _url;
            }
        }
        public static string urlLogin
        {
            get
            {
                return _urlLogin;
            }
        }
        public static string urlLogout
        {
            get
            {
                return _urlLogout;
            }
        }
        public static string urlme
        {
            get
            {
                return _urlme;
            }
        }

    }
}
