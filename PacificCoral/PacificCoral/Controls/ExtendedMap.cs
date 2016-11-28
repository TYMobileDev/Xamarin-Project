using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace PacificCoral
{
	public class ExtendedMap : Map
	{
		public ExtendedMap()
		{
			//TODO: remove^ it just for testing
			if (Device.OS == TargetPlatform.iOS)
			{
				Pins.Add(new Pin
				{
					Position = new Position(55.6761, 12.5683),
					Label = "Pin"
				});
				this.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(55.6761, 12.5683), Distance.FromKilometers(10)));
			}
		}

		#region -- Public properties --

		public static readonly BindableProperty PinLocationProperty =
			BindableProperty.Create(nameof(PinLocation), typeof(PinLocation), typeof(PinLocation), default(PinLocation));

		public PinLocation PinLocation
		{
			get { return (PinLocation)GetValue(PinLocationProperty); }
			set { SetValue(PinLocationProperty, value); }
		}

		#endregion
	}
}
