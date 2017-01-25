using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PacificCoral.Helpers
{

    public interface INetworkConnection
    {
        bool IsConnected { get; }
        bool IsOnline { get; }
        void CheckNetworkConnection();
    }
    public class NetworkConnection : INetworkConnection
    {
        //static public NetworkConnection defaultConnection = (NetworkConnection)DependencyService.Get<INetworkConnection>();
        static  INetworkConnection  defaultConnection = DependencyService.Get<INetworkConnection>();
        public NetworkConnection()
        {


        }
        static public INetworkConnection DefaultConnection
        {
            get
            {
                return defaultConnection;
            }
            set
            {
                defaultConnection = value;
            }
        }

        public bool IsConnected
        {
            get
            {
                return ((INetworkConnection)defaultConnection).IsConnected;
            }
        }

        public bool IsOnline
        {
            get
            {
                
                return ((INetworkConnection)defaultConnection).IsOnline;
            }
        }

        public void CheckNetworkConnection()
        {
            ((INetworkConnection)defaultConnection).CheckNetworkConnection();
        }
    }
}
