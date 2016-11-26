using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using PacificCoral;
using PacificCoral.iOS;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace PacificCoral.iOS
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
				Control.BorderStyle = UIKit.UITextBorderStyle.None;
			else
				Control.BorderStyle = UIKit.UITextBorderStyle.RoundedRect;
			Control.Layer.BorderWidth = (nfloat)el.BorderWidth;
		}

		#endregion
	}
}