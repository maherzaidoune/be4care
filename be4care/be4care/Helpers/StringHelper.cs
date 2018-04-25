﻿using System;
using System.Collections.Generic;
using System.Text;

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


    }
}
