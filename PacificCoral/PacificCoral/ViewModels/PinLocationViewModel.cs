using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;

namespace PacificCoral
{
	public class PinLocationViewModel : BasePageViewModel
	{
		private readonly INavigationService _navigationService;

		public PinLocationViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			Title = "Pin Location";
			PinLocation = new PinLocation
			{
				Latitude = 55.6761,
				Longitude = 12.5683
			};
		}

		#region -- Public properties --

		private PinLocation _PinLocation;

		public PinLocation PinLocation
		{
			get { return _PinLocation; }
			set { SetProperty(ref _PinLocation, value); }
		}

		public ICommand BackCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnBackCommandAsync); }
		}

		#endregion

		#region -- Private helpers --

		private async Task OnBackCommandAsync()
		{
			await _navigationService.GoBackAsync();
		}

		#endregion
	}
}
