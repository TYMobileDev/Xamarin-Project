using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PacificCoral.Droid;
using PacificCoral.Helpers;


using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: Dependency(typeof(PacificCoral.Droid.NetworkConnection))]
namespace PacificCoral.Droid
{
    public class NetworkConnection : INetworkConnection
    {
        public bool IsConnected { get; set; }
        public bool IsOnline { get; set; }

        public void CheckNetworkConnection()
        {
            var connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
            var activeNetworkInfo = connectivityManager.ActiveNetworkInfo;
            if (activeNetworkInfo != null && activeNetworkInfo.IsConnectedOrConnecting)
            {
                IsConnected = true;
                IsOnline = isOnline();
            }
            else
            {
                IsConnected = false;
                IsOnline = false;
            }
        }
        private bool isOnline()
        {
            try
            {
                Java.Lang.Process p1 = Java.Lang.Runtime.GetRuntime().Exec("ping -c 1 8.8.8.8");
                int returnVal = p1.WaitFor();
                bool reachable = (returnVal == 0);
                return reachable;
            }
            catch (Exception e)
            {
                // TODO Auto-generated catch block
                //e.printStackTrace();
            }
            return false;
        }
    }
}