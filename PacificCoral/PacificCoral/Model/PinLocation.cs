using System;
namespace PacificCoral
{
	public class PinLocation : ObservableObject
	{
		#region -- Public properties --

		private double _Latitude;

		public double Latitude
		{
			get { return _Latitude; }
			set { SetProperty(ref _Latitude, value); }
		}

		private double _Longitude;

		public double Longitude
		{
			get { return _Longitude; }
			set { SetProperty(ref _Longitude, value); }
		}

		#endregion
	}
}
