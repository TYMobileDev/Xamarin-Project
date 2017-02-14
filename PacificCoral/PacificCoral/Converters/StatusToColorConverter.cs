using System;
using System.Globalization;
using Xamarin.Forms;

namespace PacificCoral
{
	public class StatusToColorConverter : IValueConverter
	{
		#region -- IValueConverter implementation --

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var status = (EOrderStatus)value;
			if (status == EOrderStatus.Open)
				return Color.Yellow;
			else if (status == EOrderStatus.Confirmed)
				return Color.FromHex("#7ED321");
			else if (status == EOrderStatus.Invoiced)
				return Color.Red;
			else
				throw new Exception("Invalid EOrderStatus value");
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
