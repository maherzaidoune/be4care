using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace be4care.Helpers
{
    public static class StringHelper
    {
        //truncate text for UI
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static int IndexOfNth(string str, string value, int nth = 1)
        {
            if (nth <= 0)
                throw new ArgumentException("Can not find the zeroth index of substring in string. Must start with 1");
            int offset = str.IndexOf(value);
            for (int i = 1; i < nth; i++)
            {
                if (offset == -1) return -1;
                offset = str.IndexOf(value, offset + 1);
            }
            return offset;
        }


        public static string TruncateCharAtIndex(this string value, string characterToTruncate, int characterAtIndex)
        {
            if (string.IsNullOrEmpty(value)) return value;

            var index = IndexOfNth(value, characterToTruncate, characterAtIndex);
            return value.Substring(0, index + 1);
        }





        //REGEX OCR text

        public static  string getDate(string s)
        {
            Regex r = new Regex(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$");
            Match m = r.Match(s.ToLower());
            if (m.Success)
            {
                return m.Value;
            }
            else
            {
                 r = new Regex(@"\[0*([1-9]|[12][0-9]|3[01]) (janvier|février|mars|avril|mai|juin|juillet|août|septembre|octobre|novembre|décembre) (201[4-9]|20[2-9][0-9]|2[1-9][0-9]{2}|[3-9][0-9]{3}) ((?:0?[0-9]|1[0-9]|2[0-3]){2}:(?:0?[0-9]|[1-5][0-9]))");
                 m = r.Match(s);
                if (m.Success)
                {
                    return m.Value;
                }
            }
            return String.Empty;
        }
        public static string getDr(string s)
        {
            string[] words = s.Split(' ','/');
            var doctor = String.Empty;
            for (int i = 0;i<words.Length; i++)
            {
                if (words[i].Equals("Dr") || words[i].Equals("Dr(e)")|| words[i].Equals("Docteur"))
                {
                    for(int j = i; j < i + 3; j++)
                    {
                         doctor += " " + words[j].ToString();
                    }
                    return doctor;
                }
            }
            return doctor;
        }
        public static string gettype(string s)
        {
            return null;
        }
        public static string getHStructure(string s)
        {
            return null;
        }
        public static string getplace(string s)
        {
            string[] words = s.Split(' ', '/');
            var place = String.Empty;
            string date = getDate(s);
            for (int i = 0; i < words.Length; i++)
            {
                if (date.Contains(words[i]))
                {
                    place = words[i - 1].ToString();
                    return place;
                }
            }
            return place;
        }
        public static string getnote(string s)
        {
            return s;
        }


    }
}
