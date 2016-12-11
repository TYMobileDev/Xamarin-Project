using System;
using System.Globalization;
using Xamarin.Forms;

namespace PacificCoral
{
	public class OrderDeliveryStatusToObjectConverter : IValueConverter
	{
		public object DeliveredObj { get; set; }

		#region -- IValueConverter implementation --

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var status = (EOrderDeliveryStatus)value;
			if (status == EOrderDeliveryStatus.Delivered)
				return DeliveredObj;
			else
				throw new Exception("Invalid EOrderDeliveryStatus value");
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
