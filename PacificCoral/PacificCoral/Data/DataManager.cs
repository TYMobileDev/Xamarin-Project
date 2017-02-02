#define OFFLINE_SYNC_ENABLED

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using PacificCoral.Model;
//using System.Net.Http;

using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Collections.ObjectModel;
using PacificCoral.Helpers;
using System.Linq.Expressions;

namespace PacificCoral
{
    public class DataManager
    {
        static DataManager defaultManager = new DataManager();
        MobileServiceClient client;

        const string offlineDbPath = @"localstore2.db";

        // declare sync tables here
        IMobileServiceSyncTable<RepOpcoMap> opcoTable;
        IMobileServiceSyncTable<PODetail> poDetailTable;

        public Data.AsyncDataHelper<OpcoSalesSummaries,string, int,object, object> OpcoSalesSummaryTable;
        public Data.AsyncDataHelper<LostSalesPCS,string , double, object, object> LostSalesPCSTable;
        public Data.AsyncDataHelper<DeviationMaster,string, DateTime, object, object> DeviationMasterTable;
        public Data.AsyncDataHelper<DeviationSummary,string, int, object, object> DeviationSummaryTable;
        public Data.AsyncDataHelper<DeviationDetails,string, int, Model.DeviationMaster, string>DeviationDetailTable;
        public Data.AsyncDataHelper<ItemCodes,string, string, object, object> ItemCodesTable;
        public Data.AsyncDataHelper<POMaster,string, DateTime , object, object> POMasterTable;
        public Data.AsyncDataHelper<PODetail, int,string, POMaster, int> PODetailTable;
        public Data.AsyncDataHelper<Customers, string, string, object, object> CustomersTable;
        public Data.AsyncDataHelper<CustomerCodes, int, string, Customers, int> CustomerCodesTable;

        // store static tables for easy reference
        ObservableCollection<RepOpcoMap> opcoTableSnapshot;

        // some tables are only generated nightly on the server, so no reason to pull changes more than once a day. 
        // store last refresh times
        DateTime OpcoSalesSummaryUpdatedAt = DateTime.MinValue;
        DateTime LostSalesUpdatedAt = DateTime.MinValue;

        bool OpcoTableRefreshed = false;
        bool OpcoTableRefreshing = false;


        private DataManager()
        {
            try
            {
                this.client = new MobileServiceClient(Constants.ApplicationURL);

                var store = new MobileServiceSQLiteStore(offlineDbPath);
                // define all tables to be stored locally.  Sync and non-synced
                store.DefineTable<RepOpcoMap>();
                store.DefineTable<OpcoSalesSummaries>();
                store.DefineTable<LostSalesPCS>();
                store.DefineTable<DeviationDetails>();
                store.DefineTable<DeviationSummary>();
                store.DefineTable<DeviationMaster>();
                store.DefineTable<ItemCodes>();
                store.DefineTable<POMaster>();
                store.DefineTable<PODetail>();
                store.DefineTable<Customers>();
                store.DefineTable<CustomerCodes>();
                //Initializes the SyncContext using the default IMobileServiceSyncHandler.

                this.client.SyncContext.InitializeAsync(store);

                this.opcoTable = client.GetSyncTable<RepOpcoMap>();

                this.poDetailTable = client.GetSyncTable<PODetail>();
                OpcoSalesSummaryTable = new Data.AsyncDataHelper<OpcoSalesSummaries,string, int,object , object>(client, (p, s) => p.OPCO.ToUpper().Trim() == s.ToUpper().Trim(), p=>p.Period , false, enumOrderDirection.Ascending);
                LostSalesPCSTable = new Data.AsyncDataHelper<Model.LostSalesPCS,string, double, object, object>(client, (p, s) => p.OPCO.ToUpper().Trim() == s.ToUpper().Trim(), p => p.GainLoss, false, enumOrderDirection.Ascending);
                DeviationMasterTable = new Data.AsyncDataHelper<Model.DeviationMaster,string, DateTime, object, object >(client, (p, s) => p.SyscoHouse.Trim().ToUpper() == s.Trim().ToUpper(), p => p.EndDate, true,  enumOrderDirection.Ascending,new TimeSpan(0, 15, 0));
                DeviationSummaryTable = new Data.AsyncDataHelper<Model.DeviationSummary, string,int,object, object>(client, (p, s) => p.Representative.ToUpper().Trim() == s.Trim().ToUpper(), null, false, enumOrderDirection.Ascending );
                DeviationDetailTable = new Data.AsyncDataHelper<Model.DeviationDetails,string, int, Model.DeviationMaster, string>(client,
                    (p,s) =>p.DeviationNumber==s,null, true,
                    (p, o) => o.Contains(p.DeviationNumber),
                    DeviationMasterTable.GetFilteredTable,
                    (oc, s) => oc.Where (p => p.SyscoHouse.Trim().ToUpper() == s.Trim().ToUpper()).Select(p => p.DeviationNumber).ToList(),
                    new TimeSpan(0, 15, 0)
                    );
                ItemCodesTable = new Data.AsyncDataHelper<Model.ItemCodes,string, string, object, object>(client, null, p => p.ItemCode,true, enumOrderDirection.Ascending, new TimeSpan(0, 15, 0));
                POMasterTable = new Data.AsyncDataHelper<Model.POMaster,string, DateTime, object, object>(client, (p, s) => p.OPCO.ToUpper().Trim() == s.ToUpper().Trim(), p => p.PODate, true, enumOrderDirection.Descending , new TimeSpan(0,15,0));
                PODetailTable = new Data.AsyncDataHelper<Model.PODetail, int, string, Model.POMaster, int>(client,
                    (p, s) => p.MasterKey == s,  // general where predicate
                    p => p.ItemCode, // order by
                    true, // incremental updates
                    (p, o) => o.Contains(p.MasterKey),  // contains where predicate
                    POMasterTable.GetFilteredTable, // mater table
                    (oc, s) => oc.Where(p => p.OPCO.Trim().ToUpper() == s.Trim().ToUpper()).Select(p => p.key).ToList(), // generate contains list
                    new TimeSpan(0, 15, 0));
                CustomersTable = new Data.AsyncDataHelper<Model.Customers, string, string, object, object>(client, (p, s) => p.OPCO.Trim().ToUpper() == s.Trim().ToUpper(), p => p.CustomerName, true, enumOrderDirection.Ascending, new TimeSpan(0,15,0));
                CustomerCodesTable = new Data.AsyncDataHelper<CustomerCodes, int, string, Customers, int>(client,
                    (p, s) => p.CustomerNumber == s,
                    p => p.CustomerCode,
                    true,
                    (p,o)=>o.Contains(p.CustomerNumber),
                    CustomersTable.GetFilteredTable,
                    (oc, s) => oc.Where(p => p.OPCO.Trim().ToUpper() == s.Trim().ToUpper()).Select(p=>p.CustomerNumber).ToList());

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


        public async void initializeStoreAsync()
        {

            //  push any changes from local stores
            await SyncAsync();

            // retreive updated table from server async
            OpcoSalesSummaryTable.Refresh();
            LostSalesPCSTable.Refresh();
            DeviationMasterTable.Refresh();
            DeviationSummaryTable.Refresh();



        }


        public async Task<string> GetCurrentOpcoAsync()
        {
            try
            {
                if (Globals.CurrentOpco != string.Empty)
                {
                    return Globals.CurrentOpco;
                }
                else
                {
                    if (Settings.LastOPCO != string.Empty && Settings.LastOPCO!=null)
                    {
                        System.Diagnostics.Debug.WriteLine(Settings.LastOPCO);
                        // verify in current list of opcos.
                        //    var o = await this.opcoTable.Where(p => p.OPCO.ToUpper() == Settings.LastOPCO.ToUpper()).Select(p => p.OPCO.ToUpper()).ToListAsync();
                        var o = opcoTableSnapshot.Where(p => p.OPCO.ToUpper() == Settings.LastOPCO.ToUpper()).Select(p => p.OPCO.ToUpper()).ToList();
                        if (o.Count > 0)
                        {
                            Globals.CurrentOpco = o[0].ToString();
                            return Globals.CurrentOpco;
                        }
                    }
                    // if gets to here, then no currentopco, no lastopco that matches.  get first opco in list
                    var op = await this.opcoTable.Select(p => p.OPCO.ToUpper()).ToListAsync();
                    if (op.Count > 0)
                    {
                        Globals.CurrentOpco = op[0].ToString();
                        return Globals.CurrentOpco;
                    }
                    // no opcos
                    return string.Empty;

                }
            }
            catch (Exception ex)
            {

            }
            return string.Empty;
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


        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await this.client.SyncContext.PushAsync();

            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }
            catch (Exception ex)
            {

            }

            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        //     await error.CancelAndDiscardItemAsync();
                    }

                    // Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
        }


        #region OPCOTable
        public ObservableCollection<RepOpcoMap> OPCOs
        {
            get
            {
                return opcoTableSnapshot;
            }
        }
        public async Task initalizeOpcoTable()
        {

            try
            {
                // load opco table
                if (!OpcoTableRefreshed)
                {
                    await updateOpcoTableAsync();
                }
                var ot = await opcoTable.Where(p => p.Representative == Authentication.DefaultAthenticator.UserInfo.DisplayableId.ToUpper()).ToListAsync();
                ot = ot.GroupBy(p => p.OPCO).Select(p => p.First()).ToList();
                opcoTableSnapshot = new ObservableCollection<Model.RepOpcoMap>(ot);
            }
            catch (Exception ex)
            {

            }
        }
        private async Task updateOpcoTableAsync()
        {
            if (!Authentication.DefaultAthenticator.IsAuthenticated) return;
            if (OpcoTableRefreshing ) return;
            try
            {
                OpcoTableRefreshing = true;
                await opcoTable.PurgeAsync();
                await opcoTable.PullAsync(null, opcoTable.CreateQuery());
                var ot = await opcoTable.ToListAsync();
                ot = ot.GroupBy(p => p.OPCO).Select(p => p.First()).ToList();
                opcoTableSnapshot = new ObservableCollection<Model.RepOpcoMap>(ot);
                OpcoTableRefreshed = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                OpcoTableRefreshing = false;
            }
        }
        #endregion OPCOTable


    }
}
