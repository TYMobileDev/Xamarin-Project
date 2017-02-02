using PacificCoral.Model;
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
		}

		#region -- Public properties --

		//private ObservableCollection<AccordionSource> _AccordionSource;
		//public ObservableCollection<AccordionSource> AccordionSource
		//{
		//	get { return _AccordionSource; }
		//	set { SetProperty(ref _AccordionSource, value); }
		//}

		//cell, view
		private IList<AccordionModel> _AccordionSource;
		public IList<AccordionModel> AccordionSource
		{
			get { return _AccordionSource; }
			set { SetProperty(ref _AccordionSource, value); }
		}

		public ObservableCollection<OpcoSalesSummaries> OpcoSalesChartItems { get; set; }

		public ObservableCollection<DeviationSummary> DeviationSummaryItems { get; set; }

		public ObservableCollection<LostSalesPCS> LostSalesPCSItems { get; set; }

		public ObservableCollection<RepOpcoMap> Opcos { get; set; } = DataManager.DefaultManager.OPCOs;

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

		private async void LostSalesPCSItemsAsync()
		{
			try
			{
				_currentOpco = await DataManager.DefaultManager.GetCurrentOpcoAsync();
				//LostSalesPCSItems = await DataManager.DefaultManager.getLostSalesPCSForOpcoAsync(_currentOpco);
				LostSalesPCSItems = new ObservableCollection<LostSalesPCS>()
				{
					new LostSalesPCS()
					{
						ItemCode = "8754",
						Description = "SHRIMP WHT 71/90",
						Period2EndDate = DateTime.Today,
						GainLoss = 70,
					},
					new LostSalesPCS()
					{
						ItemCode = "8754",
						Description = "SHRIMP WHT 71/90",
						Period2EndDate = DateTime.Today,
						GainLoss = 70,
					}
				};

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
				_currentOpco = await DataManager.DefaultManager.GetCurrentOpcoAsync();
				OpcoSalesChartItems = await DataManager.DefaultManager.getOpcoSalesSummaryForOpcoAsync(_currentOpco);
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
			//AccordionSource = new ObservableCollection<AccordionSource>();

			//var chartFirstAccord = new AccordionSource()
			//{
			//	HeaderText = "Sales history",
			//	HeaderTextColor = Color.Black,
			//	HeaderBackGroundColor = Color.White,
			//	//ContentItems = chartViewOne
			//	ContentItems = new ChartDataViewCell(),
			//};
			//AccordionSource.Add(chartFirstAccord);

			//var lostSalesPCSViewTwo = new ListView()
			//{
			//	ItemsSource = LostSalesPCSItems,
			//	ItemTemplate = new DataTemplate(typeof(LostSalesPCSViewCell))
			//};
			//var lostSalesPCSSecondAccord = new AccordionSource()
			//{
			//	HeaderText = "Lost Sales",
			//	HeaderTextColor = Color.Black,
			//	HeaderBackGroundColor = Color.White,
			//	ContentItems = lostSalesPCSViewTwo,
			//};
			//AccordionSource.Add(lostSalesPCSSecondAccord);

			////var vViewLayout = new StackLayout()
			////{
			////	Children = {
			////		new Label { Text = "Static Content:" },
			////		new Label { Text = "Sales history" },
			////		new Label { Text = "Sales history" }
			////	}
			////};
			//var deviationsThirdAccord = new AccordionSource()
			//{
			//	HeaderText = "Deviations",
			//	HeaderTextColor = Color.Black,
			//	HeaderBackGroundColor = Color.White,
			//	ContentItems = new DeviationsView(),
			//};
			//AccordionSource.Add(deviationsThirdAccord);

			//var vViewLayout = new StackLayout()
			//{
			//	Children = {
			//		new Label { Text = "Static Content:" },
			//		new Label { Text = "Sales history" },
			//		new Label { Text = "Sales history" }
			//	}
			//};
			//var vViewAccord = new AccordionSource()
			//{
			//	HeaderText = "Deviations",
			//	HeaderTextColor = Color.Black,
			//	HeaderBackGroundColor = Color.White,
			//	ContentItems = vViewLayout,
			//};
			//AccordionSource.Add(vViewAccord);

			AccordionSource = new List<AccordionModel>();

			var viewLayout = new StackLayout()
			{
				Children = {
					new Label { Text = "Static Content:" },
					new Label { Text = "Sales history" },
					new Label { Text = "Sales history" }
				}
			};

			var cell = new AccordionCell()
			{
				Name = "Deviations",
			};

			var item = new AccordionModel()
			{
				CellAccordion = cell,
				ViewAccordion = viewLayout,
			};

			var cell1 = new AccordionCell()
			{
				Name = "Deviations fd",
			};

			var viewLayout1 = new StackLayout()
			{
				Children = {
					new Label { Text = "Static Content:" },
					new Label { Text = "Sales history" },
					new Label { Text = "Sales history" }
				}
			};

			var item1 = new AccordionModel()
			{
				CellAccordion = cell1,
				ViewAccordion = viewLayout1,
			};

			var item2 = new AccordionModel()
			{
				CellAccordion = cell,
				ViewAccordion = viewLayout,
			};

			AccordionSource.Add(item);
			AccordionSource.Add(item1);

			//real
			var deviationsCell = new AccordionCell()
			{
				Name = "Deviations",
			};

			var deviationsThirdAccord = new AccordionModel()
			{
				CellAccordion = deviationsCell,
				ViewAccordion = new DeviationsView(),
			};

			var lostSalesPCSCell = new AccordionCell()
			{
				Name = "Sales history",
			};

			var lostSalesPCSViewTwo = new ListView()
			{
				ItemsSource = LostSalesPCSItems,
				ItemTemplate = new DataTemplate(typeof(LostSalesPCSViewCell))
			};

			var lostSalesPCSSecondAccord = new AccordionModel()
			{
				CellAccordion = lostSalesPCSCell,
 				ViewAccordion = lostSalesPCSViewTwo
			};

			AccordionSource.Add(lostSalesPCSSecondAccord);
			AccordionSource.Add(deviationsThirdAccord);
		}

		#endregion
	}
}
