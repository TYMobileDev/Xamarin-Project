using System;
using PacificCoral.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageRenderer))]
namespace PacificCoral.iOS
{
	public class TabbedPageRenderer : TabbedRenderer
	{
		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);

			TabBar.TintColor = UIColor.White;

			//TODO: work only for iOS 10+ 
			if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
				TabBar.UnselectedItemTintColor = Color.FromRgb(251, 215, 169).ToUIColor();

			TabBar.BarTintColor = StyleManager.GetAppResource<Color>("DefaultMainColor").ToUIColor();
		}
	}
}
