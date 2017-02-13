using System;
using System.Threading.Tasks;
using System.Windows.Input;
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

		private Task OnLogoutCommandAsync()
		{
			return _navigationService.NavigateAsync("DashBoardView");
		}

		#endregion
	}
}

