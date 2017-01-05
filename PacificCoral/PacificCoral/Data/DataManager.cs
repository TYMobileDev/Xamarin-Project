#define OFFLINE_SYNC_ENABLED

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Net.Http;
#if OFFLINE_SYNC_ENABLED
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
#endif

namespace PacificCoral
{
    public class DataManager
    {
        static DataManager defaultManager = new DataManager();
        MobileServiceClient client;

        private DataManager()
        {
            try
            {
                //var handler = new System.Net.Http.HttpClientHandler();
                //handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip |
                //     System.Net.DecompressionMethods.Deflate;
                this.client = new MobileServiceClient(Constants.ApplicationURL);
            }
            catch (Exception ex)
            {
				throw new Exception(ex.Message);
            }
        }

        public static DataManager DefaultManager
        {
            get
            {
                return defaultManager;
            }

            set
            {
                defaultManager = value;
            }
        }

        public MobileServiceClient CurrentClient
        {
            get
            {
                return client;
            }

            set
            {
                client = value;
            }
        }
    }
}
