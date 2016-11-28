using System;
using System.Globalization;
using Xamarin.Forms;
namespace PacificCoral
{
	public class IntToObjectConverter : IValueConverter
	{
		public object DefaultValue { get; set; }
		public object Val0 { get; set; }
		public object Val1 { get; set; }
		public object Val2 { get; set; }
		public object Val3 { get; set; }
		public object Val4 { get; set; }

		#region -- IValueConverter implementation --

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var intV = (int)value;
			switch (intV)
			{
				case 0:
					return Val0;
				case 1:
					return Val1;
				case 2:
					return Val2;
				case 3:
					return Val3;
				case 4:
					return Val4;
				default:
					return DefaultValue;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
