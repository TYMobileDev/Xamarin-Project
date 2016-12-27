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
				TabBar.UnselectedItemTintColor = StyleManager.GetAppResource<Color>("DefaultMainColor").ToUIColor();

			TabBar.BarTintColor = UIColor.FromRGB(250, 183, 88);
		}
	}
}
