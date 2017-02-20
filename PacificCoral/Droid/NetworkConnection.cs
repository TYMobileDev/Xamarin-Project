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
using System.Net;


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

            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            try
            {
                System.Net.NetworkInformation.PingReply pr = p.Send("8.8.8.8", 1280);
                if (pr != null && pr.Status == System.Net.NetworkInformation.IPStatus.Success)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}