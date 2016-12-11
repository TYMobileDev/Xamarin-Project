using System;
using System.Globalization;
using Xamarin.Forms;

namespace PacificCoral
{
	public class TrackItemTypeToObjectConverter : IValueConverter
	{
		public object TripObj { get; set; }

		#region -- IValueConverter implementation --

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var type = (ETrackItemType)value;
			if (type == ETrackItemType.Trip)
				return TripObj;
			else
				throw new Exception("Invalid ETrackItemType value");
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
