using PacificCoral.Views;
using PacificCoral.Extensions;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Acr.UserDialogs;
using System.Net.Http;
using System.Collections.Generic;
using System;
using PacificCoral.Helpers;

namespace PacificCoral.ViewModels
{
	public class SignInViewModel : BasePageViewModel
	{
		private readonly INavigationService _navigationService;
		private bool waitVisible = false;

		public SignInViewModel() { }

		public SignInViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		public ICommand SignInCommand
		{
			get
			{
				return new DelegateCommand(async () =>
				{
					await LoginAsync();
				});
			}
		}

		private async Task LoginAsync()
		{
			try
			{
				// check if phone is online
				NetworkConnection.DefaultConnection.CheckNetworkConnection();
				if (NetworkConnection.DefaultConnection.IsOnline)
				{
					if (Authentication.DefaultAthenticator != null)
					{
						WaitVisible = true;
						await Authentication.DefaultAthenticator.Authenticator.Authenticate();
						WaitVisible = false;
					}
					if (Authentication.DefaultAthenticator.IsAuthenticated)
					{
						// var s = await api();
						await _navigationService.NavigateAsync<DashBoardView>();
					}
					else // authentication failure
					{
						if (Authentication.DefaultAthenticator.UserInfo == null)
						{
							// Did not authenticate with azure
							UserDialogs.Instance.Alert("Login Failure.  Please try again.", "Login Failure");

						}
						else
						{
							// unable to login client
							Settings.LastLoggedinUser = Authentication.DefaultAthenticator.UserInfo.DisplayableId;
							UserDialogs.Instance.Alert("WARNING:  Failure logging in application to server.  Application will run in OFFLINE mode with limitted functionality.", "Login Problems");
							await _navigationService.NavigateAsync<DashBoardView>();
						}
					}
				}
				else // offline
				{
					// If has previous login, use in offline mode
					if (Settings.LastLoggedinUser != string.Empty)
					{
						UserDialogs.Instance.Alert("WARNING:  Failure logging in application to server.  Application will run in OFFLINE mode with limitted functionality.", "Conneciton Error");
						await _navigationService.NavigateAsync<DashBoardView>();
					}
					else
					{
						// no default user, so cannot use in offline mode.
						UserDialogs.Instance.Alert("The phone is not connected to the internet and is unable to log into the server.  Check Wifi and cellular connection, and try again.", "Connection Error");
					}
				}
			}
			catch (Exception ex)
			{

			}

		}
		public async Task<List<string>> api()
		{
			try
			{
				var s = await DataManager.DefaultManager.CurrentClient.InvokeApiAsync<List<string>>("Data/GetOpcosForCurrentUser", HttpMethod.Get, new Dictionary<string, string>());
				return s;
			}
			catch (Exception ex)
			{

			}
			return null;
		}

		public bool WaitVisible
		{
			get { return waitVisible; }
			set { SetProperty<bool>(ref waitVisible, value); }
		}
	}
}
