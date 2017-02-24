using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PacificCoral.Extensions;
using PacificCoral.Helpers;

namespace PacificCoral.Data
{
	public class AsyncDataHelper<TModel, TWhere, TOrder, TMaster, TContains>
	{

		//enumRefreshTableStatus enumRefreshStatus = enumRefreshTableStatus.NotRefreshing;
		DateTime _lastRefreshTime = DateTime.MinValue;
		TimeSpan _refreshInterval = new TimeSpan(12, 0, 0);
		bool _isRefreshing = false;
		Expression<Func<TModel, TWhere, bool>> _wherePredicate = null;
		Expression<Func<TModel, List<TContains>, bool>> _whereContainsPredicate = null;

		Expression<Func<TModel, TOrder>> _orderClause = null;
		IMobileServiceSyncTable<TModel> _table;
		Func<Task> _refreshMethod;

		Func<string, Task<ObservableCollection<TMaster>>> _funcFilteredMasterTable = null;
		Func<ObservableCollection<TMaster>, string, List<TContains>> _funcGetMasterCollection = null;
		enumOrderDirection _orderDirection = enumOrderDirection.Ascending;
		public AsyncDataHelper(MobileServiceClient client, Expression<Func<TModel, TWhere, bool>> WhereClause = null, Expression<Func<TModel, TOrder>> OrderClause = null, bool isIncremental = true, enumOrderDirection OrderDirection = enumOrderDirection.Ascending, TimeSpan? RefreshInterval = null)
		{
			//    DataManager.DefaultManager.lstDataTables.Add(this);
			_table = client.GetSyncTable<TModel>();
			_wherePredicate = WhereClause;
			_orderClause = OrderClause;
			_orderDirection = OrderDirection;
			if (RefreshInterval != null)
			{
				_refreshInterval = (TimeSpan)RefreshInterval;
			}
			if (isIncremental)
			{
				_refreshMethod = refreshIncrementalTable;
			}
			else
			{
				_refreshMethod = refreshPurgeTable;
			}
		}
		public AsyncDataHelper(MobileServiceClient client, Expression<Func<TModel, TWhere, bool>> WhereClause = null, Expression<Func<TModel, TOrder>> OrderClause = null, bool isIncremental = true, Expression<Func<TModel, List<TContains>, bool>> WhereContainsClause = null, Func<string, Task<ObservableCollection<TMaster>>> funcMasterTable = null, Func<ObservableCollection<TMaster>, string, List<TContains>> detailList = null, TimeSpan? RefreshInterval = null)
		{
			//   lstDataHelpers.Add(this);
			_table = client.GetSyncTable<TModel>();
			_wherePredicate = WhereClause;
			_whereContainsPredicate = WhereContainsClause;
			_orderClause = OrderClause;
			_funcFilteredMasterTable = funcMasterTable;
			_funcGetMasterCollection = detailList;
			if (RefreshInterval != null)
			{
				_refreshInterval = (TimeSpan)RefreshInterval;
			}
			if (isIncremental)
			{
				_refreshMethod = refreshIncrementalTable;
			}
			else
			{
				_refreshMethod = refreshPurgeTable;
			}
		}

		public Task Refresh()
		{
			return _refreshMethod();
		}
		public async Task Purge()
		{
			await _table.PurgeAsync();
		}
		public async Task<ObservableCollection<TModel>> GetTable()
		{
			await _refreshMethod();
			return new ObservableCollection<TModel>(await _table.ToEnumerableAsync());
		}
		public async Task<ObservableCollection<TModel>> GetLocalTable()
		{
			return new ObservableCollection<TModel>(await _table.ToEnumerableAsync());

		}

		public async Task<ObservableCollection<TModel>> GetFilteredTable(TWhere filterVal)
		{
			try
			{
				await _refreshMethod();
				// replace lambda parameter with passed filter value
				var body = _wherePredicate.Body.ReplaceParameter(_wherePredicate.Parameters[1], Expression.Constant(filterVal));
				var lambda = Expression.Lambda<Func<TModel, bool>>(body, _wherePredicate.Parameters[0]);
				if (_orderClause != null)
				{
					if (_orderDirection == enumOrderDirection.Ascending)
					{
						var o = await _table.Where(lambda).OrderBy(_orderClause).ToEnumerableAsync();
						return new ObservableCollection<TModel>(o);
					}
					else
					{
						var o = await _table.Where(lambda).OrderByDescending(_orderClause).ToEnumerableAsync();
						return new ObservableCollection<TModel>(o);
					}
				}
				else
				{
					var o = await _table.Where(lambda).ToEnumerableAsync();
					return new ObservableCollection<TModel>(o);
				}

			}
			catch (Exception ex)
			{

			}
			return null;

		}

		public async Task<ObservableCollection<TModel>> GetTableFromMasterFilter(string filter)
		{
			try
			{
				await _refreshMethod();
				// get master table filtered
				var l = await _funcFilteredMasterTable(filter);
				// convert to list of keys
				var l2 = _funcGetMasterCollection(l, filter);
				// now, can return detail from contained keys
				var body = _whereContainsPredicate.Body.ReplaceParameter(_whereContainsPredicate.Parameters[1], Expression.Constant(l2));
				var lambda = Expression.Lambda<Func<TModel, bool>>(body, _whereContainsPredicate.Parameters[0]);
				if (_orderClause != null)
				{
					if (_orderDirection == enumOrderDirection.Ascending)
					{
						var o = await _table.Where(lambda).OrderBy(_orderClause).ToEnumerableAsync();
						return new ObservableCollection<TModel>(o);
					}
					else
					{
						var o = await _table.Where(lambda).OrderByDescending(_orderClause).ToEnumerableAsync();
						return new ObservableCollection<TModel>(o);
					}
				}
				else
				{
					var o = await _table.Where(lambda).ToEnumerableAsync();
					return new ObservableCollection<TModel>(o);
				}

			}
			catch (Exception ex)
			{

			}
			finally
			{
			}
			return null;
		}

		private async Task refreshIncrementalTable()
		{
			if (!Authentication.DefaultAthenticator.IsAuthenticated) return;
			if (_isRefreshing) return;
			if (DateTime.Now.Subtract(_lastRefreshTime) < _refreshInterval) return;
			// bring in incremental syncs
			try
			{
				//  enumRefreshStatus = enumRefreshTableStatus.Begin;
				_isRefreshing = true;
				await _table.PullAsync(typeof(TModel).Name + Settings.LastPurgeSequence.ToString(), _table.CreateQuery());
				_lastRefreshTime = DateTime.Now;
			}
			catch (Exception ex)
			{

			}
			finally
			{
				// enumRefreshStatus  = enumRefreshTableStatus.End;
				_isRefreshing = false;
			}
		}
		private async Task refreshPurgeTable()
		{
			if (!Authentication.DefaultAthenticator.IsAuthenticated) return;
			//  if (enumRefreshStatus != enumRefreshTableStatus.NotRefreshing) return;
			if (_isRefreshing) return;
			if (DateTime.Now.Subtract(_lastRefreshTime) < _refreshInterval) return;
			// bring in incremental syncs
			try
			{
				// enumRefreshStatus = enumRefreshTableStatus.Begin;
				_isRefreshing = true;
				await _table.PurgeAsync();
				await _table.PullAsync(null, _table.CreateQuery());
				_lastRefreshTime = DateTime.Now;
			}
			catch (Exception ex)
			{

			}
			finally
			{
				//   enumRefreshStatus = enumRefreshTableStatus.End;
				_isRefreshing = false;
			}
		}

	}

}