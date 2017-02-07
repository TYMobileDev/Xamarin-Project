using System;
using Prism.Navigation;
using System.Collections.Generic;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using PacificCoral.Model;

namespace PacificCoral
{
	public class OrdersViewModel : BasePageViewModel
	{
		private readonly INavigationService _navigationService;

		public OrdersViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			Init();

            initializeTables();

		}

		#region -- Public properties --

        public ObservableCollection<POMaster > POMasters { get; set; }
		private IEnumerable<OrderModel> _Orders;
        private string _currentOpco;

        public IEnumerable<OrderModel> Orders
		{
			get { return _Orders; }
			set { SetProperty(ref _Orders, value); }
		}



        public ICommand AddCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnAddCommandAsync); }
		}

		public ICommand SearchCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnSearchCommandAsync); }
		}

		public ICommand OrderSelectedCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnOrderSelectedCommandAsync); }
		}

		public ICommand BackCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnBackCommandAsync); }
		}

		#endregion

		#region -- Private helpers --

        private async Task initializeTables()
        {
            if (Globals.CurrentOpco == string.Empty) return;
            POMasters = await DataManager.DefaultManager.POMasterTable.GetFilteredTable(Globals.CurrentOpco);
        }
		private void Init()
		{
            Title = String.Format("{0} - ORDERS", Globals.CurrentOpco.Trim());
			var orders = new List<OrderModel>();
			var r = new Random();
			for (int i = 0; i < 10; i++)
			{
				var order = new OrderModel
				{
					Status = i % 2 == 0 ? EOrderStatus.Confirmed : EOrderStatus.Invoiced,
					DeliveryStatus = EOrderDeliveryStatus.Delivered,
					CustomerNumber = 158823 + i * 10 + r.Next(1000)
				};
				orders.Add(order);
			}

			Orders = orders;
		}

		private async Task OnAddCommandAsync()
		{

		}

		private async Task OnSearchCommandAsync()
		{

		}

		private async Task OnOrderSelectedCommandAsync()
		{

		}

		private Task OnBackCommandAsync()
		{
			return _navigationService.GoBackAsync();
		}

		#endregion
	}
}
