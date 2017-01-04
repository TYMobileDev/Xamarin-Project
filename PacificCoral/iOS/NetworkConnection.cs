using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using SystemConfiguration;
using CoreFoundation;
using PacificCoral.iOS;
using PacificCoral.Helpers;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PacificCoral.iOS.NetworkConnection))]
namespace PacificCoral.iOS
{
    public class NetworkConnection : INetworkConnection
    {

/*        public NetworkConnection()
        {
            InternetConnectionStatus();
        }*/
        public bool IsConnected { get; set; }
        public void CheckNetworkConnection()
        {
            InternetConnectionStatus();
        }
        public bool IsOnline
        {
            get
            {

                System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
                try
                {
                    System.Net.NetworkInformation.PingReply pr = p.Send("8.8.8.8", 128);
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

        private void UpdateNetworkStatus()
        {
            if (InternetConnectionStatus())
            {
                IsConnected = true;
            }
            else if (LocalWifiConnectionStatus())
            {
                IsConnected = true;
            }
            else
            {
                IsConnected = false;
            }
        }

        private event EventHandler ReachabilityChanged;
        private void OnChange(NetworkReachabilityFlags flags)
        {
            var h = ReachabilityChanged;
            if (h != null)
                h(null, EventArgs.Empty);
        }

        private NetworkReachability defaultRouteReachability;
        private bool IsNetworkAvailable(out NetworkReachabilityFlags flags)
        {
            if (defaultRouteReachability == null)
            {
                defaultRouteReachability = new NetworkReachability(new IPAddress(0));
            //    defaultRouteReachability.SetCallback(OnChange);
                defaultRouteReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
            }
            if (!defaultRouteReachability.TryGetFlags(out flags))
                return false;
            return IsReachableWithoutRequiringConnection(flags);
        }

        private NetworkReachability adHocWiFiNetworkReachability;
        private bool IsAdHocWiFiNetworkAvailable(out NetworkReachabilityFlags flags)
        {
            if (adHocWiFiNetworkReachability == null)
            {
                adHocWiFiNetworkReachability = new NetworkReachability(new IPAddress(new byte[] { 169, 254, 0, 0 }));
            //    adHocWiFiNetworkReachability.SetCallback(OnChange);
                adHocWiFiNetworkReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
            }

            if (!adHocWiFiNetworkReachability.TryGetFlags(out flags))
                return false;

            return IsReachableWithoutRequiringConnection(flags);
        }

        public static bool IsReachableWithoutRequiringConnection(NetworkReachabilityFlags flags)
        {
            // Is it reachable with the current network configuration?
            bool isReachable = (flags & NetworkReachabilityFlags.Reachable) != 0;

            // Do we need a connection to reach it?
            bool noConnectionRequired = (flags & NetworkReachabilityFlags.ConnectionRequired) == 0;

            // Since the network stack will automatically try to get the WAN up,
            // probe that
            if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
                noConnectionRequired = true;

            return isReachable && noConnectionRequired;
        }

        private bool InternetConnectionStatus()
        {
            NetworkReachabilityFlags flags;
            bool defaultNetworkAvailable = IsNetworkAvailable(out flags);
            if (defaultNetworkAvailable && ((flags & NetworkReachabilityFlags.IsDirect) != 0))
            {
                return false;
            }
            else if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
            {
                return true;
            }
            else if (flags == 0)
            {
                return false;
            }

            return true;
        }

        private bool LocalWifiConnectionStatus()
        {
            NetworkReachabilityFlags flags;
            if (IsAdHocWiFiNetworkAvailable(out flags))
            {
                if ((flags & NetworkReachabilityFlags.IsDirect) != 0)
                    return true;
            }
            return false;
        }
    }
}