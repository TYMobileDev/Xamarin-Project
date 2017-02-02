using System;
using System.Globalization;
using Xamarin.Forms;

namespace PacificCoral
{
    class FirstLetterOfStringConverter: IValueConverter  
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString().Trim().Length < 1)
                return string.Empty;
            else
            {
                string s=value.ToString()[0].ToString().ToUpper();
                return s;
           }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
