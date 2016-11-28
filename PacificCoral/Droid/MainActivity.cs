using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Syncfusion.SfChart.XForms.Droid;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;
using Acr.UserDialogs;
using NControl.Controls.Droid;

namespace PacificCoral.Droid
{
    [Activity(Label = "PacificCoral", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
		private const string HOCKEY_APP_KEY = "ba40a396342544f8abd152a3bc97f2e3";
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
			#if !DEBUG

			CrashManager.Register(this, HOCKEY_APP_KEY);
			MetricsManager.Register(Application, HOCKEY_APP_KEY);

			#endif

            global::Xamarin.Forms.Forms.Init(this, bundle);
			UserDialogs.Init(Application);
			NControls.Init();
			NControl.Droid.NControlViewRenderer.Init();
            //initialising of chart renderer
            new SfChartRenderer();
            LoadApplication(new App());
        }
    }
}

