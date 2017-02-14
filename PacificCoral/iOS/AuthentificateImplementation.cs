using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using UIKit;
using Acr.Support.iOS;
using Foundation;

namespace PacificCoral.iOS
{
	public class AuthentificateImplementation : IAuthenticate
	{
		#region -- IAuthenticate implementation --

		public async Task<bool> Authenticate()
		{
			var res = await Authentication.DefaultAthenticator.Auth(new PlatformParameters(UIApplication.SharedApplication.GetTopViewController()));
			return res;
		}

		public async Task<bool> Logout()
		{
			foreach (var cookie in NSHttpCookieStorage.SharedStorage.Cookies)
			{
				NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);
			}

			var res = await Authentication.DefaultAthenticator.Logout();
			return res;
		}

		#endregion
	}
}
