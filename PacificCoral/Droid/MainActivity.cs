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
using FFImageLoading.Forms.Droid;

using Microsoft.WindowsAzure.MobileServices;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Android.Content;
using Android.Webkit;

[assembly: UsesFeature("android.hardware.wifi", Required = false)]
namespace PacificCoral.Droid
{
    [Activity(Label = "PacificCoral", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IAuthenticate
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
			Xamarin.FormsMaps.Init(this, bundle);
			UserDialogs.Init(Application);
			NControls.Init();
			NControl.Droid.NControlViewRenderer.Init();
            // Initialize the authenticator before loading the app.
            Authentication.DefaultAthenticator.Init((IAuthenticate)this);
            //initialising of chart renderer
            new SfChartRenderer();
			CachedImageRenderer.Init();
            LoadApplication(new App());
        }

        public async Task<bool> Authenticate()
        {
            return await Authentication.DefaultAthenticator.Auth(new PlatformParameters(this));
		}

		public async Task<bool> Logout()
		{
			CookieManager.Instance.RemoveAllCookie();

			var res = await Authentication.DefaultAthenticator.Logout();
			return res;
		}

        // required for Azure AD Auth to fire
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
		}
	}
}

