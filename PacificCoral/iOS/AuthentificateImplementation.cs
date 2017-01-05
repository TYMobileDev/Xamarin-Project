using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using UIKit;
using Acr.Support.iOS;

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

		#endregion
	}
}
