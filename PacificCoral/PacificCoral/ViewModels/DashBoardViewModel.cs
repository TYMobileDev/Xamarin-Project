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

namespace PacificCoral.ViewModels
{
	public class DashBoardViewModel : BasePageViewModel
    {
        private readonly INavigationService _navigationService;

        public DashBoardViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            var sales = new ObservableCollection<SalesModel>();
            for (var i = 0; i < 5; i++)
            {
                sales.Add(new SalesModel() { Code = "421127", Shrimp = "SHRIMP WHT 71/90", Sep1 = "SEP::63,451", Sep2 = "SEP::63,451", GL = "G(L)(21,335)" });
            }
            Sales = sales;

            //init fake data for chart
            ChartItems  = new ObservableCollection<ChartSourceItem>();
            ChartItems.Add(new ChartSourceItem() { X = 2, Y = 100 });
            ChartItems.Add(new ChartSourceItem() { X = 4, Y = 200 });
            ChartItems.Add(new ChartSourceItem() { X = 6, Y = 160 });
            ChartItems.Add(new ChartSourceItem() { X = 8, Y = 110 });
            ChartItems.Add(new ChartSourceItem() { X = 10, Y = 300 });
            ChartItems.Add(new ChartSourceItem() { X = 12, Y = 400 });
            ChartItems.Add(new ChartSourceItem() { X = 14, Y = 450 });
            ChartItems.Add(new ChartSourceItem() { X = 16, Y = 460 });
        }

		#region -- Public properties --

        public ObservableCollection<ChartSourceItem> ChartItems { get; set; }

        public ObservableCollection<SalesModel> Sales { get; set; }

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
			return _navigationService.NavigateAsync<TrackMileageView>();
		}

		private Task OnViewAccountsCommandAsync()
		{
			return _navigationService.NavigateAsync<AccountsView>();
		}

		private Task OnViewOrdersCommandAsync()
		{
			return _navigationService.NavigateAsync<OrdersView>();
		}

		private Task OnDetailsCommandAsync()
		{
			return _navigationService.NavigateAsync<DashBoard2View>();;
		}

		#endregion
    }
}
