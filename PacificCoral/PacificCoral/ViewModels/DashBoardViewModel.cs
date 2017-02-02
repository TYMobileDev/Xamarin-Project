﻿using PacificCoral.Model;
using PacificCoral.Views;
using PacificCoral.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace PacificCoral.ViewModels
{

    
	public class DashBoardViewModel : BasePageViewModel
    {
        private readonly INavigationService _navigationService;
        private string _currentOpco = "";
        public  DashBoardViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            var sales = new ObservableCollection<SalesModel>();
            for (var i = 0; i < 15; i++)
            {
                sales.Add(new SalesModel() { Code = "421127", Shrimp = "SHRIMP WHT 71/90", Sep1 = "SEP::63,451", Sep2 = "SEP::63,451", GL = "G(L)(21,335)" });
            }
            Sales = sales;
            
            //init fake data for chart
            RefreshDashboardTables();

            Opcos = DataManager.DefaultManager.OPCOs;
            
        }

		#region -- Public properties --

        private void RefreshDashboardTables()
        {
            OpcoSalesChartItemsAsync();

            LostSalesPCSItemsAsync();

            DeviationSummaryItemsAsync();
        }

        public ObservableCollection<OpcoSalesSummaries> OpcoSalesChartItems { get; set; }

        private async void OpcoSalesChartItemsAsync()
        {
            try
            {
                _currentOpco  = await DataManager.DefaultManager.GetCurrentOpcoAsync();
                if (_currentOpco == null || _currentOpco.Trim() == "") return;
              //  OpcoSalesChartItems  = await DataManager.DefaultManager.getOpcoSalesSummaryForOpcoAsync(_currentOpco );
                OpcoSalesChartItems = await DataManager.DefaultManager.OpcoSalesSummaryTable.GetFilteredTable(_currentOpco);
                // calculate growth
                double p1 = OpcoSalesChartItems.Where(p => p.Period >= 9).Sum(p => p.LBS);
                double p2 = OpcoSalesChartItems.Where(p => p.Period >= 6 && p.Period<9).Sum(p => p.LBS);
                Revenue = string.Format("QTR {0} = {1:N0} LBS", p1 > p2 ? "Growth" : "Loss", Math.Abs(p1 - p2));
            }
            catch (Exception ex)
            {

            }
        }
        public ObservableCollection<DeviationSummary> DeviationSummaryItems {
            get;
            set; }

        public ObservableCollection<LostSalesPCS > LostSalesPCSItems
        {
            get;
            set; }

        private async void LostSalesPCSItemsAsync()
        {
            try
            {
                _currentOpco  = await DataManager.DefaultManager.GetCurrentOpcoAsync();
                if (_currentOpco == null)
                    return;
                LostSalesPCSItems = await DataManager.DefaultManager.LostSalesPCSTable.GetFilteredTable(_currentOpco);
              
            }
            catch (Exception ex)
            {

            }
        }

        private async void DeviationSummaryItemsAsync()
        {
            try
            {
                DeviationSummaryItems = await DataManager.DefaultManager.DeviationSummaryTable.GetFilteredTable(Authentication.DefaultAthenticator.CurrentUserID);

            }
            catch (Exception ex)
            {

            }
        }

        public ObservableCollection<RepOpcoMap > Opcos {
            get;
            set; }

        public ObservableCollection<SalesModel> Sales { get; set; }

        public string CurrentOpco
        {
            get
            {
                return Globals.CurrentOpco == string.Empty ? "No Opco Selected" : Globals.CurrentOpco;
            }
            set
            {
                Globals.CurrentOpco = value;
                SetProperty<string>(ref _currentOpco, value);
                RefreshDashboardTables(); // update chart, lost sales, etc with current opco- calls asyn methods
            }
        }

		private string _Revenue;
		public string Revenue
		{
			get { return _Revenue; }
			set { SetProperty( ref _Revenue, value);}
		}

        public ICommand ChartTappedCommand
        {
            get { return SingleExecutionCommand.FromFunc(OnViewOrdersCommandAsync ); }
        }

        private Task OnChartTapGestureCommandAsync()
        {
            throw new NotImplementedException();
        }

        public ICommand ViewTrackMileageCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnViewTrackMileageCommandAsync); }
		}

		public ICommand ViewAccountsCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnViewAccountsCommandAsync); }
		}

		public ICommand ViewOrdersCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnViewOrdersCommandAsync); }
		}

		public ICommand DetailsCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnDetailsCommandAsync); }
		}

		#endregion

		#region -- Private helpers --

		private Task OnViewTrackMileageCommandAsync()
		{
			return _navigationService.NavigateAsync("TrackMileageView", animated: false);
		}

		private Task OnViewAccountsCommandAsync()
		{
			return _navigationService.NavigateAsync("AccountsView", animated: false);
		}

		private Task OnViewOrdersCommandAsync()
		{
			return _navigationService.NavigateAsync("OrdersView", animated: false);
		}

		private Task OnDetailsCommandAsync()
		{
			return _navigationService.NavigateAsync("DashBoard2View", animated: false);
		}

		#endregion
    }
}
