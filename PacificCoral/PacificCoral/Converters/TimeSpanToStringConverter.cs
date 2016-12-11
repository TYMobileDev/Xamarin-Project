using System;
using System.Globalization;
using Xamarin.Forms;
namespace PacificCoral
{
	public class TimeSpanToStringConverter : IValueConverter
	{
		#region -- IValueConverter implementation --

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			//TODO: implement
			var tm = (TimeSpan)value;
			return tm.Hours + " Hours";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
