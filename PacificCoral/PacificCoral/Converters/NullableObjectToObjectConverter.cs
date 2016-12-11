using System;
using System.Globalization;
using Xamarin.Forms;
namespace PacificCoral
{
	public class NullableObjectToObjectConverter : IValueConverter
	{
		public object IfNullObj { get; set; }
		public object IfNotNullObj { get; set; }

		#region -- IValueConverter implementation --

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value == null ? IfNullObj : IfNotNullObj;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
