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
using PacificCoral.Controls;
using PacificCoral.Control;
using NControl.Controls;

namespace PacificCoral.ViewModels
{
	public class DashBoardViewModel : BasePageViewModel
	{
		private readonly INavigationService _navigationService;
		private string _currentOpco = "";

		public DashBoardViewModel(INavigationService navigationService)
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
			InitAccordionSource();

			Opcos = DataManager.DefaultManager.OPCOs;

			//Opcos = new ObservableCollection<RepOpcoMap>()
			//{
			//	new RepOpcoMap()
			//	{
			//		OPCO = "Opco 1",
			//	},
			//	new RepOpcoMap()
			//	{
			//		OPCO = "Opco 2",
			//	},
			//	new RepOpcoMap()
			//	{
			//		OPCO = "Opco 3",
			//	},
			//	new RepOpcoMap()
			//	{
			//		OPCO = "Opco 4",
			//	},
			//};
		}

		#region -- Public properties --

		//cell, view
		private IList<AccordionModel> _AccordionSource;
		public IList<AccordionModel> AccordionSource
		{
			get { return _AccordionSource; }
			set { SetProperty(ref _AccordionSource, value); }
		}

		private ObservableCollection<OpcoSalesSummaries> _OpcoSalesChartItems;
		public ObservableCollection<OpcoSalesSummaries> OpcoSalesChartItems 
		{ 
			get { return _OpcoSalesChartItems; } 
			set { SetProperty(ref _OpcoSalesChartItems, value); }
		}

		private ObservableCollection<DeviationSummary> _DeviationSummaryItems;
		public ObservableCollection<DeviationSummary> DeviationSummaryItems 
		{ 	
			get { return _DeviationSummaryItems; }
			set { SetProperty(ref _DeviationSummaryItems, value); }
		}

		private ObservableCollection<LostSalesPCS> _LostSalesPCSItems;
		public ObservableCollection<LostSalesPCS> LostSalesPCSItems 
		{ 
			get { return _LostSalesPCSItems; }
			set { SetProperty(ref _LostSalesPCSItems, value); } 
		}

		private ObservableCollection<RepOpcoMap> _Opcos;
		public ObservableCollection<RepOpcoMap> Opcos 
		{ 
			get { return _Opcos; }
			set { SetProperty(ref _Opcos, value); }

		}

		private ObservableCollection<SalesModel> _Sales;
		public ObservableCollection<SalesModel> Sales 
		{ 
			get { return _Sales; }
			set { SetProperty(ref _Sales, value); }
		}

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
			set { SetProperty(ref _Revenue, value); }
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
			return _navigationService.NavigateAsync("TrackMileageView");
		}

		private Task OnViewAccountsCommandAsync()
		{
			return _navigationService.NavigateAsync("AccountsView");
		}

		private Task OnViewOrdersCommandAsync()
		{
			return _navigationService.NavigateAsync("OrdersView");
		}

		private Task OnDetailsCommandAsync()
		{
			return _navigationService.NavigateAsync("DashBoard2View");
		}

		private async void LostSalesPCSItemsAsync()
		{
			try
			{
				//_currentOpco = await DataManager.DefaultManager.GetCurrentOpcoAsync();
				//LostSalesPCSItems = await DataManager.DefaultManager.getLostSalesPCSForOpcoAsync(_currentOpco);
				LostSalesPCSItems = new ObservableCollection<LostSalesPCS>();
				for (var i = 0; i < 10; i++)
				{
					var sale = new LostSalesPCS()
					{
						ItemCode = "8754",
						Description = "SHRIMP WHT 71/90 SHRIMP WHT 71/90",
						Period2EndDate = DateTime.Today,
						GainLoss = 100,
						Period1BeginDate = DateTime.Today,
					};
					LostSalesPCSItems.Add(sale);
				}
			}
			catch (Exception ex)
			{

			}
		}

		private async void DeviationSummaryItemsAsync()
		{
			try
			{
				//DeviationSummaryItems = await DataManager.DefaultManager.getDeviationSummaryAsync();
				DeviationSummaryItems = new ObservableCollection<DeviationSummary>()
				{
					new DeviationSummary()
					{
						Submitted = 20,
						Active = 200,
						Expired = 100,
						Expiring = 28,	
					}
				};

			}
			catch (Exception ex)
			{

			}
		}

		private async void OpcoSalesChartItemsAsync()
		{
			try
			{
				//_currentOpco = await DataManager.DefaultManager.GetCurrentOpcoAsync();
				//OpcoSalesChartItems = await DataManager.DefaultManager.getOpcoSalesSummaryForOpcoAsync(_currentOpco);
				OpcoSalesChartItems = new ObservableCollection<OpcoSalesSummaries>()
				{
					new OpcoSalesSummaries()
					{
						LBS = 200,
						Period = 2,
					},
					new OpcoSalesSummaries()
					{
						LBS = 400,
						Period = 4,
					},
					new OpcoSalesSummaries()
					{
						LBS = 300,
						Period = 6,
					},
					new OpcoSalesSummaries()
					{
						LBS = 800,
						Period = 8,
					},
					new OpcoSalesSummaries()
					{
						LBS = 100,
						Period = 10,
					},
					new OpcoSalesSummaries()
					{
						LBS = 1000,
						Period = 12,
					},
				};

				// calculate growth
				double p1 = OpcoSalesChartItems.Where(p => p.Period >= 9).Sum(p => p.LBS);
				double p2 = OpcoSalesChartItems.Where(p => p.Period >= 6 && p.Period < 9).Sum(p => p.LBS);
				Revenue = string.Format("QTR {0} = {1:N0} LBS", p1 > p2 ? "Growth" : "Loss", Math.Abs(p1 - p2));
			}
			catch (Exception ex)
			{

			}
		}

		private void RefreshDashboardTables()
		{
			OpcoSalesChartItemsAsync();

			LostSalesPCSItemsAsync();

			DeviationSummaryItemsAsync();
		}

		private void InitAccordionSource()
		{
			AccordionSource = new List<AccordionModel>();

			var chartCell = new AccordionCell()
			{
				TitleCell = "Sales history",
			};

			var chartDataView = new ChartDataView()
			{
				//BindingContext = this,
			};
			//chartDataView.SetBinding(ChartDataView.OpcoSalesChartItemsProperty, nameof(OpcoSalesChartItems));

			var chartFirstAccord = new AccordionModel()
			{
				CellAccordion = chartCell,
				ViewAccordion = chartDataView,
			};

			var lostSalesPCSCell = new AccordionCell()
			{
				TitleCell = "Lost Sales",
			};

			var lostSalesPCSViewTwo = new ListView()
			{
				BindingContext = this,
				ItemTemplate = new DataTemplate(typeof(LostSalesPCSViewCell)),
				SeparatorVisibility = SeparatorVisibility.None,
				HasUnevenRows = true,
				Margin = new Thickness(5),
			};
			lostSalesPCSViewTwo.SetBinding(ListView.ItemsSourceProperty, nameof(LostSalesPCSItems));

			//var lostSalesPCSViewRepeaterListTwo = new RepeaterControl<LostSalesPCS>()
			//{
			//	ItemTemplate = new DataTemplate(typeof(LostSalesPCSViewCell)),
			//	BindingContext = this,
			//	Margin = new Thickness(5),
			//};
			//lostSalesPCSViewRepeaterListTwo.SetBinding(RepeaterControl<LostSalesPCS>.ItemsSourceProperty, nameof(LostSalesPCSItems));

			var lostSalesPCSSecondAccord = new AccordionModel()
			{
				CellAccordion = lostSalesPCSCell,
 				ViewAccordion = lostSalesPCSViewTwo
				//ViewAccordion = lostSalesPCSViewRepeaterListTwo,
			};

			var deviationsCell = new AccordionCell()
			{
				TitleCell = "Deviations",
			};

			var deviationsThirdAccord = new AccordionModel()
			{
				CellAccordion = deviationsCell,
				ViewAccordion = new DeviationsView(),
			};

			AccordionSource.Add(chartFirstAccord);
			AccordionSource.Add(lostSalesPCSSecondAccord);
			AccordionSource.Add(deviationsThirdAccord);
		}

		#endregion
	}
}
