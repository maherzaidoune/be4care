using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace be4care
{
    class ColorHelper : IValueConverter
    {
        public  object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valueAsString = value.ToString();
            switch (valueAsString)
            {
                case (""):
                    {
                        return Color.Default;
                    }
                case ("Accent"):
                    {
                        return Color.Accent;
                    }
                default:
                    {
                        return Color.FromHex(value.ToString());
                    }
            }
        }
        public  object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
