using System;
using Prism.Navigation;

namespace PacificCoral
{
	public class ProfileViewModel : BasePageViewModel
	{
		private readonly INavigationService _navigationService;

		public ProfileViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;			
		}
	}
}
