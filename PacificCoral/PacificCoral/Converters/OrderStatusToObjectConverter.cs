using System;
using System.Globalization;
using Xamarin.Forms;
namespace PacificCoral
{
	public class OrderStatusToObjectConverter : IValueConverter
	{
		public object OpenObj { get; set; }
		public object ConfirmedObj { get; set; }
		public object InvoicedObj { get; set; }

		#region -- IValueConverter implementation --

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var status = (EOrderStatus)value;
			if (status == EOrderStatus.Open)
				return OpenObj;
			else if (status == EOrderStatus.Confirmed)
				return ConfirmedObj;
			else if (status == EOrderStatus.Invoiced)
				return InvoicedObj;
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
