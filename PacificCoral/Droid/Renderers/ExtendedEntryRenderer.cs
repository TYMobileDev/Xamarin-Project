using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using PacificCoral;
using PacificCoral.Droid;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace PacificCoral.Droid
{
	public class ExtendedEntryRenderer : EntryRenderer
	{
		#region -- Overrides --

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);
			UpdateBorder();
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (e.PropertyName == nameof(ExtendedEntry.BorderWidthProperty))
				UpdateBorder();
		}

		#endregion

		#region -- Private helpers --

		private void UpdateBorder()
		{
			var el = Element as ExtendedEntry;
			if (el == null || Control == null)
				return;
			if (el.BorderWidth == 0)
				Control.Background = null;
		}

		#endregion
	}
}
