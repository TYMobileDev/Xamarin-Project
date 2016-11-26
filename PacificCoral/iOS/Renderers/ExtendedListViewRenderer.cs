using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using PacificCoral;
using PacificCoral.iOS;

[assembly:ExportRenderer(typeof(ExtendedListView), typeof(ExtendedListViewRenderer))]
namespace PacificCoral.iOS
{
	public class ExtendedListViewRenderer : ListViewRenderer
	{
		#region -- Overrides --

		protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged(e);
			Control.SectionIndexColor = Color.FromHex("#4A4A4A").ToUIColor();
			Control.TintColor = Color.FromHex("#4A4A4A").ToUIColor();
		}


		#endregion

		#region -- Private helpers --



		#endregion
	}
}
