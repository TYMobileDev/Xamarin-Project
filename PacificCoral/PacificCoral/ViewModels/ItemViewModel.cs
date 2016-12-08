using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using PacificCoral.Model;

namespace PacificCoral
{
	public class ItemViewModel : BaseDetailPageViewModel<SalesModel>
	{
		public ItemViewModel(INavigationService navigationService) : base(navigationService)
		{
			//TODO: fix that
			Title = "Initial Visit";
		}

		#region -- Public properties --


		#endregion

		#region -- Private helpers --

		#endregion
	}
}
