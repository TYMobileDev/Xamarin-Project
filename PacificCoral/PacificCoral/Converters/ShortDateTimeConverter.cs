using System;
using System.Globalization;
using Xamarin.Forms;

namespace PacificCoral
{
	public class ShortDateTimeConverter : IValueConverter
	{
		#region -- IValueConverter implementation --

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			//TODO: implement
			return "10/12/2016";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
