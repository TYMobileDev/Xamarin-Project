using System;
using Prism.Navigation;

namespace PacificCoral
{
	public class AccountViewModel : BaseDetailPageViewModel<AccountModel>
	{
		public AccountViewModel(INavigationService navigationService) : base(navigationService)
		{
			Title = "Account";
		}

		#region -- Public properties --


		#endregion
	}
}
