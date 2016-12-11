using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PacificCoral.Model;
using Prism.Navigation;
using PacificCoral.Extensions;

namespace PacificCoral
{
	public class InventoryViewModel : BasePageViewModel
	{
		private readonly INavigationService _navigationService;

		public InventoryViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			var sales = new ObservableCollection<SalesModel>()
			{
				new SalesModel()
				{
					Code = "157195",
					Title = "EMPIRE",
					IndicatorUrl = "active.png",
					Shrimp = "SHRIM TGR 8/12 HDLS/ON",
					Prospect = true
				},
				 new SalesModel()
				{
					Code = "157205",
					Title = "EMPIRE",
					IndicatorUrl = "notactive.png",
					Shrimp = "SHRIM TGR 21/25 RPDT/ON",
					Deviated = true
				},
				 new SalesModel()
				{
					Code = "421096",
					Title = "BAYWINDS",
					IndicatorUrl = "active.png",
					Shrimp = "SHRIM TGR 31/35 RPDT/ON/PFOS FR",
				}
			};
			Title = "Inventory";
			Sales = sales;
		}

		#region -- Public properties --

		private IEnumerable<SalesModel> _Sales;

		public IEnumerable<SalesModel> Sales
		{
			get { return _Sales; }
			set { SetProperty(ref _Sales, value); }
		}

		public ICommand ItemSelectedCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnItemSelectedCommandAsync); }
		}

		#endregion


		#region -- Private helpers --

		private Task OnItemSelectedCommandAsync(object itemObj)
		{
			var item = itemObj as SalesModel;
			var param = new NavigationParameters();
			param.Add(nameof(SalesModel), item);
			return _navigationService.NavigateAsync<InventoryItemView>(param);
		}

		#endregion
	}
}
