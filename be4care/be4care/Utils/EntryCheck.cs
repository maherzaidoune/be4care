using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.Utils
{
    public class EntryCheck
    {
        public static bool checkMail(String text)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(text);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool checknum(string num)
        {
            return true;
        }

        public static bool checkPass(string pass)
        {
            if (string.IsNullOrEmpty(pass))
                return false;
            if (pass.Length < 4)
                return false;
            return true;
        }

        
        public static Tuple<bool,string> checkentries(string num,string text,string pass,bool terms)
        {

            if (!terms)
                return new Tuple<bool, string>(false, "Acceptez les conditions d'utilisation");

            var msg = string.Empty;
            var accepted = true;

            if (!checkMail(text)) {
                if(string.IsNullOrEmpty(msg))
                    msg += "Email invalid ";
                msg += "\r\nEmail invalid";
                accepted = false;
            }
            if (!(checknum(num)))
            {
                if (string.IsNullOrEmpty(msg))
                    msg += "Numéro de telephone invalid ";
                msg += "\r\nNuméro de telephone invalid ";
                accepted = false;
            }
            if (!(checkPass(pass)))
            {
                if (string.IsNullOrEmpty(msg))
                    msg += "Password invalid ";
                msg += "\r\nPassword invalid ";
                accepted = false;
            }
            return new Tuple<bool, string>(accepted, msg);


        }
        public static Tuple<bool, string> checkentries(string email, string pass)
        {
            var msg = string.Empty;
            var accepted = true;

            if (!checkMail(email))
            {
                msg += "Email invalid ";
                accepted = false;
            }
            if (!(checkPass(pass)))
            {
                msg += "\r\nPassword invalid ";
                accepted = false;
            }
            return new Tuple<bool, string>(accepted, msg);
        }
    }
}
