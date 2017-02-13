using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PacificCoral.Model;
using Prism.Navigation;
using Xamarin.Forms;

namespace PacificCoral
{
	public class DetailsViewModel : BasePageViewModel
	{
		private readonly INavigationService _navigationService;

		public DetailsViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;

			PurchaseOrder = "1233445";
			Customer = "PFG TAMPA";

			InitializeTables();
		}

		#region -- Public properties --

		private string _PurchaseOrder;
		public string PurchaseOrder
		{
			get { return _PurchaseOrder; }
			set { SetProperty(ref _PurchaseOrder, value); }
		}

		private string _Customer;
		public string Customer
		{
			get { return _Customer; }
			set { SetProperty(ref _Customer, value); }
		}

		private DateTime _PODate;
		public DateTime PODate
		{
			get { return _PODate; }
			set { SetProperty(ref _PODate, value); }
		}

		private DateTime _DeliverDate;
		public DateTime DeliverDate
		{
			get { return _DeliverDate; }
			set { SetProperty(ref _DeliverDate, value); }
		}

		private string _DeliveryMethod;
		public string DeliveryMethod
		{
			get { return _DeliveryMethod; }
			set { SetProperty(ref _DeliveryMethod, value); }
		}

		private string _POStatus;
		public string POStatus
		{
			get { return _POStatus; }
			set { SetProperty(ref _POStatus, value); }
		}

		private ObservableCollection<POMaster> _POMasters;
		public ObservableCollection<POMaster> POMasters 
		{ 
			get { return _POMasters; }
			set { SetProperty(ref _POMasters, value); }
		}

		private IEnumerable<OrderModel> _Orders;
		public IEnumerable<OrderModel> Orders
		{
			get { return _Orders; }
			set { SetProperty(ref _Orders, value); }
		}

		public ICommand BackCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnBackCommandAsync); }
		}

		#endregion

		#region -- Private helpers --

		private async void InitializeTables()
		{
			if (Globals.CurrentOpco == string.Empty) return;
			POMasters = await DataManager.DefaultManager.POMasterTable.GetFilteredTable(Globals.CurrentOpco);
			var orders = new List<OrderModel>();

			foreach(var item in POMasters)
			{
				var order = new OrderModel();

				if (item.Status == "Confirmation_Ack")
					order.Status = EOrderStatus.Confirmed;
				else if (item.Status == "Invoice_Ack")
					order.Status = EOrderStatus.Invoiced;
				else
					order.Status = EOrderStatus.Open;

				order.DeliveryStatus = EOrderDeliveryStatus.Delivered;
				order.CustomerNumber = Int32.Parse(item.PO);
				order.ShipDate = item.ShipDate;
				order.PODate = item.PODate;
				orders.Add(order);
			}

			Orders = orders;
		}

		private Task OnBackCommandAsync()
		{
			return _navigationService.GoBackAsync();
		}

		#endregion
	}
}

