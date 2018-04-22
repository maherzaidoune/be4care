using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.Utils
{
    public class Constant
    {
        private static string _url = "http://peaceful-forest-76384.herokuapp.com/api/";
        public static string url { get {
                return _url;
            } }
    }
}
