using System;
using System.Globalization;
using Xamarin.Forms;
namespace PacificCoral
{
	public class DateTimeConverter : IValueConverter
	{
		#region -- IValueConverter implementation --

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return "October, 23th, 3:00 pm";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
