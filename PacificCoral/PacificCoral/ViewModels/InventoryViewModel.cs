using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PacificCoral.Model;

namespace PacificCoral
{
	public class InventoryViewModel : BasePageViewModel
	{
		public InventoryViewModel()
		{
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

		#endregion


		#region -- Private helpers --


		#endregion
	}
}
