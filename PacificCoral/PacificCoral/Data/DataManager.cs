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

namespace PacificCoral
{
    public class DataManager
    {
        static DataManager defaultManager = new DataManager();
        MobileServiceClient client;

        const string offlineDbPath = @"localstore2.db";

        // declare sync tables here
        IMobileServiceSyncTable<RepOpcoMap> opcoTable;
        IMobileServiceSyncTable<OpcoSalesSummaries> opcoSalesSummariesTable;
        IMobileServiceSyncTable<LostSalesPCS  > lostSalesPCSTable;
        IMobileServiceSyncTable<DeviationMaster> deviationMasterTable;
        IMobileServiceSyncTable<DeviationDetails> deviationDetailsTable;
        IMobileServiceSyncTable<DeviationSummary> deviationSummaryTable;

        // store static tables for easy reference
        ObservableCollection<RepOpcoMap> opcoTableSnapshot;
        ObservableCollection<LostSalesPCS> lostSalesPCSTableSnapshot;

        // some tables are only generated nightly on the server, so no reason to pull changes more than once a day. 
        // store last refresh times
        DateTime OpcoSalesSummaryUpdatedAt = DateTime.MinValue;
        DateTime LostSalesUpdatedAt = DateTime.MinValue;

        bool OpcoTableRefreshed = false;
        bool OpcoSalesTableRefreshed = false;
        bool LostSalesTableRefreshed = false;
        bool DeviationSummaryRefreshed = false;

        private DataManager()
        {
            try
            {
                this.client = new MobileServiceClient(Constants.ApplicationURL);

                var store = new MobileServiceSQLiteStore(offlineDbPath);
                // define all tables to be stored locally.  Sync and non-synced
                store.DefineTable<RepOpcoMap >();
                store.DefineTable<OpcoSalesSummaries>();
                store.DefineTable<LostSalesPCS>();
                store.DefineTable<DeviationDetails >();
                store.DefineTable<DeviationSummary >();
             //   store.DefineTable<DeviationMaster >();
                //Initializes the SyncContext using the default IMobileServiceSyncHandler.

                this.client.SyncContext.InitializeAsync(store);

                this.opcoTable = client.GetSyncTable<RepOpcoMap>();
                this.opcoSalesSummariesTable = client.GetSyncTable<OpcoSalesSummaries>();
                this.lostSalesPCSTable = client.GetSyncTable<LostSalesPCS>();
                this.deviationMasterTable = client.GetSyncTable<DeviationMaster>();
                this.deviationDetailsTable = client.GetSyncTable<DeviationDetails>();
                this.deviationSummaryTable = client.GetSyncTable<DeviationSummary>();
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
            refreshOpcoSalesSummaryTable();
            refreshLostSalesPCSTable();
            refreshDeviationSummaryTable();
            refreshDeviationMasterTable();
        }


        public async Task<string> GetCurrentOpcoAsync()
        {

            if (Globals.CurrentOpco != string.Empty)
            {
                return Globals.CurrentOpco;
            }
            else
            {
                if (Settings.LastOPCO != string.Empty)
                {
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
            //    await this.opcoTable.PullAsync(null, this.opcoTable.CreateQuery());
            //    await this.opcoSalesSummariesTable.PullAsync(null, this.opcoSalesSummariesTable.CreateQuery());

                //await this.todoTable.PullAsync(
                //    //The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
                //    //Use a different query name for each unique query in your program
                //    "allTodoItems",
                //    this.todoTable.CreateQuery());
                //// calling purge and pull without a query name will cause the entire table to be refreshed, not just incremental updates
                //await this.graphTable.PurgeAsync();
                //await this.graphTable.PullAsync(null, this.graphTable.CreateQuery());
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
            // load opco table
            if (!OpcoTableRefreshed)
            {
                await updateOpcoTableAsync();
            }
            var ot = await opcoTable.Where(p => p.Representative == Authentication.DefaultAthenticator.UserInfo.DisplayableId.ToUpper()).ToListAsync();
            ot = ot.GroupBy(p => p.OPCO).Select(p => p.First()).ToList();
            opcoTableSnapshot = new ObservableCollection<Model.RepOpcoMap>(ot);
        }
        private async Task updateOpcoTableAsync()
        {

            await opcoTable.PurgeAsync();
            await opcoTable.PullAsync(null, opcoTable.CreateQuery());
            var ot = await opcoTable.ToListAsync();
            ot = ot.GroupBy(p => p.OPCO).Select(p => p.First()).ToList();
            opcoTableSnapshot = new ObservableCollection<Model.RepOpcoMap>(ot);
            OpcoTableRefreshed = true;
        }
        #endregion OPCOTable

        #region DeviationSummary

        public async Task<ObservableCollection<DeviationSummary >> getDeviationSummaryAsync()
        {
            //store local store table
            try
            {
                if (!DeviationSummaryRefreshed )
                {
                    await refreshDeviationSummaryTable();
                }
                var o = await this.deviationSummaryTable.Where(p => p.Representative.ToUpper().Trim()==Authentication.DefaultAthenticator.CurrentUserID ).ToEnumerableAsync();
                // refresh local store from sql server async
                return new ObservableCollection<DeviationSummary >(o);
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task refreshDeviationSummaryTable()
        {
            // only update once per day
            if (DeviationSummaryRefreshed ) return;
            // invalidate offline table
            try
            {
                await this.deviationSummaryTable.PurgeAsync();
                await this.deviationSummaryTable.PullAsync(null, this.deviationSummaryTable .CreateQuery());
                DeviationSummaryRefreshed  = true;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region DeviationDetails

        public async Task<ObservableCollection<DeviationDetails>> getDeviationDetailsAsync()
        {
            //store local store table
            try
            {
                await refreshDeviationDetailsTable();
                var o = await this.deviationDetailsTable.ToEnumerableAsync();
                // refresh local store from sql server async
                return new ObservableCollection<DeviationDetails>(o);
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public async Task<ObservableCollection<DeviationDetails>> getDeviationDetailsForOpcoAsync(string OPCO)
        {
            //store local store table
            try
            {
                await refreshDeviationDetailsTable();
                var o = await this.deviationMasterTable.Where(p => p.SyscoHouse.Trim().ToUpper() == OPCO.Trim().ToUpper()).Select(p=>p.DeviationNumber ).ToListAsync();
                var r = await this.deviationDetailsTable.Where(p => o.Contains(p.DeviationNumber)).ToEnumerableAsync();
                // refresh local store from sql server async
                return new ObservableCollection<DeviationDetails>(r);
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task refreshDeviationDetailsTable()
        {

            // bring in incremental syncs
            try
            {
                await this.deviationDetailsTable.PullAsync("DeviationDetails", this.deviationDetailsTable.CreateQuery());
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region DeviationMaster

        public async Task<ObservableCollection<DeviationMaster>> getDeviationMasterAsync()
        {
            //store local store table
            try
            {
                await refreshDeviationMasterTable();
                var o = await this.deviationMasterTable.ToEnumerableAsync();
                // refresh local store from sql server async
                return new ObservableCollection<DeviationMaster>(o);
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public async Task<ObservableCollection<DeviationMaster>> getDeviationMasterForOpcoAsync(string OPCO)
        {
            //store local store table
            try
            {
                await refreshDeviationMasterTable();
                var o = await this.deviationMasterTable.Where(p=>p.SyscoHouse.Trim().ToUpper()==OPCO.Trim().ToUpper()).ToEnumerableAsync();
                // refresh local store from sql server async
                return new ObservableCollection<DeviationMaster>(o);
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task refreshDeviationMasterTable()
        {

            // bring in incremental syncs
            try
            {
                await this.deviationMasterTable.PullAsync("DeviationMaster", this.deviationMasterTable.CreateQuery());
            }
            catch (Exception ex)
            {

            }
        }

        #endregion 

        #region LostSalesTable

        public async Task<ObservableCollection<LostSalesPCS>> getLostSalesPCSForOpcoAsync(string opco)
        {
            //store local store table
            try
            {
                if (!LostSalesTableRefreshed)
                {
                    await refreshLostSalesPCSTable();
                }
                var o = await this.lostSalesPCSTable.Where(p => p.OPCO.ToUpper().Trim() == opco.ToUpper().Trim()).OrderBy(p => p.GainLoss).ToEnumerableAsync();
                // refresh local store from sql server async
                return new ObservableCollection<LostSalesPCS>(o);
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public async Task refreshLostSalesPCSTable()
        {
            // only update once per day
            if (DateTime.Now.Date == LostSalesUpdatedAt.Date) return;
            // invalidate offline table
            try
            {
                await this.lostSalesPCSTable.PurgeAsync();
                await this.lostSalesPCSTable.PullAsync(null, this.lostSalesPCSTable.CreateQuery());
                LostSalesUpdatedAt = DateTime.Now.Date;
                LostSalesTableRefreshed = true;
            }
            catch (Exception ex)
            {

            }
        }

        #endregion LostSalesTable
        #region OpcoSalesSummaryTable
        public async Task<ObservableCollection<OpcoSalesSummaries>> getOpcoSalesSummaryForOpcoAsync(string opco)
        {
            //store local store table
            try
            {
                if (!LostSalesTableRefreshed)
                {
                    await refreshOpcoSalesSummaryTable();
                }
                var o = await this.opcoSalesSummariesTable.Where(p => p.OPCO.ToUpper() == opco.ToUpper()).OrderBy(p => p.Period).ToEnumerableAsync();
                // refresh local store from sql server async
                return new ObservableCollection<OpcoSalesSummaries>(o);
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task refreshOpcoSalesSummaryTable()
        {
            // invalidate offline table, pull entire table from server for current user
            if (DateTime.Now.Date == OpcoSalesSummaryUpdatedAt.Date) return;
            try
            {
                await this.opcoSalesSummariesTable.PurgeAsync();
                await this.opcoSalesSummariesTable.PullAsync(null, this.opcoSalesSummariesTable.CreateQuery());
                OpcoSalesSummaryUpdatedAt = DateTime.Now.Date;
                OpcoSalesTableRefreshed = true;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion


    }
}
