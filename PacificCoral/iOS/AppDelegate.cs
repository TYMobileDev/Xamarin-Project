using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Syncfusion.SfChart.XForms.iOS.Renderers;
using HockeyApp.iOS;
using NControl.Controls.iOS;
using FFImageLoading.Forms.Touch;
using FFImageLoading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using KeyboardOverlap.Forms.Plugin.iOSUnified;

namespace PacificCoral.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			#if !DEBUG

			var manager = BITHockeyManager.SharedHockeyManager;
			manager.Configure("05c5c35d5d1a4bf49a1c72e592e17038");
			manager.StartManager();

			#endif

			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
			Authentication.DefaultAthenticator.Init(new AuthentificateImplementation());

            global::Xamarin.Forms.Forms.Init();
			KeyboardOverlapRenderer.Init();
			Xamarin.FormsMaps.Init();
			NControls.Init();
			NControl.iOS.NControlViewRenderer.Init();
            //initialising of chart renderer
            new SfChartRenderer();
			CachedImageRenderer.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
