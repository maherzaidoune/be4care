namespace be4care.Utils
{
    public class Constant
    {
        private static string _url = "https://peaceful-forest-76384.herokuapp.com/api/users/";
        private static string _urlLogin = "https://peaceful-forest-76384.herokuapp.com/api/users/login/";
        private static string _urlLogout = "https://peaceful-forest-76384.herokuapp.com/api/users/logout";
        private static string _urlme = "https://peaceful-forest-76384.herokuapp.com/api/users/me";
        private static string _urlgetDoctors = "https://peaceful-forest-76384.herokuapp.com/api/users/me/doctors";
        private static string _urlgetDocuments = "https://peaceful-forest-76384.herokuapp.com/api/users/me/documents";
        private static string _urlgetHealthStruct = "https://peaceful-forest-76384.herokuapp.com/api/users/me/healthStructs";
        private static string _urlAnalyse = "https://peaceful-forest-76384.herokuapp.com/api/documents/analyse";
        private static string _urlDisconnect = "https://peaceful-forest-76384.herokuapp.com/api/users/logout";
        private static string _urlAllDoctors = "https://peaceful-forest-76384.herokuapp.com/api/doctors";
        private static string _urlAllHstruct = "https://peaceful-forest-76384.herokuapp.com/api/healthStructs";
        private static string _urlAddDocFromDb = "https://peaceful-forest-76384.herokuapp.com/api/users/addDoctor";
        private static string _urlAddHstruct = "https://peaceful-forest-76384.herokuapp.com/api/users/addHealthStruct";




        public static string url
        {
            get
            {
                return _url;
            }
        }
        public static string urlAddHstruct
        {
            get
            {
                return _urlAddHstruct;
            }
        }
        public static string urlAddDocFromDb
        {
            get
            {
                return _urlAddDocFromDb;
            }
        }
        public static string urlAllDoctors
        {
            get
            {
                return _urlAllDoctors;
            }
        }
        public static string urlAllHstruct
        {
            get
            {
                return _urlAllHstruct;
            }
        }
        public static string urlDisconnect
        {
            get
            {
                return _urlDisconnect;
            }
        }

        public static string urlAnalyse
        {
            get
            {
                return _urlAnalyse;
            }
        }
        public static string urlgetDoctors
        {
            get
            {
                return _urlgetDoctors;
            }
        }
        public static string urlgetDocuments
        {
            get
            {
                return _urlgetDocuments;
            }
        }
        public static string urlgetHealthStruct
        {
            get
            {
                return _urlgetHealthStruct;
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
