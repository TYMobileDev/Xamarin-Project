using System;
using System.Globalization;
using Xamarin.Forms;

namespace PacificCoral
{
	public class ObjectToBoolConverter : IValueConverter
	{
		#region -- IValueConverter implementation

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value != null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
