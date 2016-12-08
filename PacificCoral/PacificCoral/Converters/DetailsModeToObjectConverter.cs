using System;
using System.Globalization;
using Xamarin.Forms;

namespace PacificCoral
{
	public class DetailsModeToObjectConverter : IValueConverter
	{
		public object ViewObj { get; set; }
		public object EditObj { get; set; }
		public object AddObj { get; set; }

		#region -- IValueConverter implementation --

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var mode = (DetailsMode)value;
			if (mode == DetailsMode.View)
				return ViewObj;
			else if (mode == DetailsMode.Edit)
				return EditObj;
			else if (mode == DetailsMode.Add)
				return AddObj;
			else
				throw new ArgumentException("value");
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
