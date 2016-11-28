using System;
using Xamarin.Forms;
using PacificCoral;
using PacificCoral.Droid;
using Xamarin.Forms.Maps.Android;
using Android.Gms.Maps;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Gms.Maps.Model;

[assembly: ExportRenderer(typeof(ExtendedMap), typeof(ExtendedMapRenderer))]
namespace PacificCoral.Droid
{
	public class ExtendedMapRenderer : MapRenderer, IOnMapReadyCallback
	{
		GoogleMap _googleMap;
		private float _defaultZoomLevel = 17;

		private ExtendedMap MapElement
		{
			get { return (ExtendedMap)Element; }
		}

		#region -- Overrides --

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Maps.Map> e)
		{
			base.OnElementChanged(e);
			MapView mapView = Control as MapView;
			if (e.NewElement != null)
			{
				mapView.GetMapAsync(this);
			}
			UpdateOverlays();
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (e.PropertyName == ExtendedMap.PinLocationProperty.PropertyName)
				UpdateOverlays();
		}

		#endregion

		#region -- IOnMapReadyCallback implementation --

		public void OnMapReady(GoogleMap googleMap)
		{
			_googleMap = googleMap;
			_googleMap.AnimateCamera(CameraUpdateFactory.ZoomTo(_defaultZoomLevel));
			_googleMap.MarkerDragEnd += OnMarkerDragEnd;
			_googleMap.MarkerClick += OnMarkerClick;
			_googleMap.UiSettings.ZoomControlsEnabled = false;
			UpdateOverlays();
		}

		#endregion


		#region -- Private helpers --

		private void OnMarkerDragEnd(object sender, GoogleMap.MarkerDragEndEventArgs e)
		{
			var m = e.Marker;
			if (MapElement != null && MapElement.PinLocation != null)
			{
				MapElement.PinLocation.Latitude = m.Position.Latitude;
				MapElement.PinLocation.Longitude = m.Position.Longitude;
			}
		}

		private void OnMarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
		{
			
		}

		private void UpdateOverlays()
		{
			if (_googleMap != null && MapElement != null)
			{
				_googleMap.Clear();
				if (MapElement.PinLocation != null)
				{
					var pinLocation = MapElement.PinLocation;
					GC.Collect();
					var icon = BitmapDescriptorFactory.FromResource(Resource.Drawable.pinLocationMarker);
					var options = new MarkerOptions();
					options
						.SetPosition(new LatLng(pinLocation.Latitude, pinLocation.Longitude))
						.SetIcon(icon)
						.Anchor(0.5f, 1.0f)
						.Draggable(true)
						.InfoWindowAnchor(0.5f, 0);
					_googleMap.AddMarker(options);
					var cu = CameraUpdateFactory.NewLatLng(new LatLng(pinLocation.Latitude, pinLocation.Longitude));
					_googleMap.AnimateCamera(cu);
				}
			}
		}

		#endregion
	}
}
