using System;
using Android.Views;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using PacificCoral;
using PacificCoral.Droid;

[assembly: ExportRenderer(typeof(ExtendedSearchBar), typeof(ExtendedSearchBarRenderer))]
namespace PacificCoral.Droid
{
	public class ExtendedSearchBarRenderer : SearchBarRenderer
	{
		#region -- Overrides --

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.SearchBar> e)
		{
			base.OnElementChanged(e);
			int searchPlateId = Context.Resources.GetIdentifier("android:id/search_plate", null, null);
			ViewGroup viewGroup = (ViewGroup)Control.FindViewById(searchPlateId);
			viewGroup.SetBackgroundColor( Android.Graphics.Color.Transparent);
		}

		#endregion
	}
}
