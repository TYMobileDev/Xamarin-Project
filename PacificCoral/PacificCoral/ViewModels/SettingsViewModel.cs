using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Prism.Navigation;
using Xamarin.Forms;

namespace PacificCoral
{
	public class SettingsViewModel : BasePageViewModel
	{
		private readonly INavigationService _navigationService;

		public SettingsViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		#region -- Public properties --

		public ICommand SettingsCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnSettingsCommandAsync); }
		}

		public ICommand BackCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnBackCommandAsync); }
		}

		public ICommand EditDetailsCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnEditDetailsCommandAsync); }
		}

		public ICommand LogoutCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnLogoutCommandAsync); }
		}

		#endregion

		#region -- Private helpers --

		private Task OnEditDetailsCommandAsync()
		{
			return _navigationService.NavigateAsync("DashBoardView");
		}

		private Task OnBackCommandAsync()
		{
			return _navigationService.GoBackAsync();
		}

		private Task OnSettingsCommandAsync()
		{
			return _navigationService.NavigateAsync("DashBoardView");
		}

		private async Task OnLogoutCommandAsync()
		{
			if (Authentication.DefaultAthenticator != null)
			{
				await Authentication.DefaultAthenticator.Authenticator.Logout();

				if (!Authentication.DefaultAthenticator.IsAuthenticated)
				{
					await _navigationService.NavigateAsync("SignInView");
				}
			}
			else
			{
				UserDialogs.Instance.Alert("Logout Failure.  Please try again.", "Logout Failure");
			}
		}

		#endregion
	}
}

