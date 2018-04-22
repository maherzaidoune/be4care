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
                return new Tuple<bool, string>(false, "Acceptez les conditions d'utilisation ");
            var msg = " Erreur";
            var accepted = true;

            if (!checkMail(text)) {
                msg += "\r\n Email invalid";
                accepted = false;
            }
            if (!(checknum(num)))
            {
                msg += "\r\n Numéro de telephone invalid";
                accepted = false;
            }
            if (!(checkPass(pass)))
            {
                msg += "\r\n Password invalid";
                accepted = false;
            }

            return new Tuple<bool, string>(accepted, msg);


        }
    }
}
