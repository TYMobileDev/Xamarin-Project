using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Acr.UserDialogs;

namespace PacificCoral
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate();
        //Task AuthenticateAsync();
    }

    // Azure AD OATH2 Authentication.  Requires specific version to be installed in nuget
    // Install-Package  Microsoft.IdentityModel.Clients.ActiveDirectory -Version 3.5.207081303-alpha
    public class Authentication
    {
        bool isAuthenticated = false;
        private MobileServiceUser user;
        private static Authentication defaultAthenticator = new Authentication();
        private UserInfo userInfo;
        public  IAuthenticate Authenticator { get; private set; }

        public static Authentication DefaultAthenticator
        {
            get
            {
                return defaultAthenticator;
            }

            set
            {
                defaultAthenticator = value;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return isAuthenticated;
            }

            set
            {
                isAuthenticated = value;
            }
        }

        public MobileServiceUser User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
            }
        }

        public UserInfo UserInfo
        {
            get
            {
                return userInfo;
            }

            set
            {
                userInfo = value;
            }
        }

        public  void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }

        public async Task<bool> Auth(IPlatformParameters  platform)
        {
            var success = false;
            isAuthenticated = false;
            string authority = "https://login.windows.net/danazarihotmail502.onmicrosoft.com";
            string resourceId = "75a34115-29ac-4a0c-87c5-821f861d9448";
            string clientId = "26fc813e-1331-4868-8c16-ec5962036f5b";
            string redirectUri = "https://pacificcoralmobileclient.azurewebsites.net/.auth/login/done";
            // string redirectUri = "https://localhost";
            try
            {
                UserInfo = null;
                AuthenticationContext ac = new AuthenticationContext(authority);
                AuthenticationResult ar = await ac.AcquireTokenAsync(resourceId, clientId,
                    new Uri(redirectUri), platform);
                userInfo = ar.UserInfo;
                JObject payload = new JObject();
                payload["access_token"] = ar.AccessToken;
                user = await DataManager.DefaultManager.CurrentClient.LoginAsync(
                    MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory, payload);
                isAuthenticated = true;
                Helpers.Settings.LastLoggedinUser = userInfo.DisplayableId;
                return true;
            }
            catch (Exception ex)
            {
               // UserDialogs.Instance.Alert(ex.ToString(), "Login Error");
            }
            return success;

        }

    }
}
