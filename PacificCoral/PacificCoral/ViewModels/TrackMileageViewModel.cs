using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using System.Collections.Generic;

namespace PacificCoral
{
	public class TrackMileageViewModel : BasePageViewModel
	{
		public readonly INavigationService _navigationService;

		public TrackMileageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			Title = "Track mileage";
			var r = new Random();
			var items = new List<TrackItemModel>();
			for (int i = 0; i < 10; i++)
			{
				var item = new TrackItemModel
				{
					Type = ETrackItemType.Trip,
					From = "Norrebro stret, 5",
					To = "Norrebro stret, 5",
					Lenght = 300,
					Duration = TimeSpan.FromHours(3)
				};
				items.Add(item);
			}
			Items = items;
		}

		#region -- Public properties --

		private IEnumerable<TrackItemModel> _Items;

		public IEnumerable<TrackItemModel> Items
		{
			get { return _Items; }
			set { SetProperty(ref _Items, value); }
		}

		public ICommand ItemSelectedCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnItemSelectedCommandAsync); }
		}

		public ICommand BackCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnBackCommandAsync); }
		}

		#endregion

		#region -- Private helpers --

		private async Task OnItemSelectedCommandAsync(object itemObj)
		{

		}

		private Task OnBackCommandAsync()
		{
			return _navigationService.GoBackAsync();
		}

		#endregion
	}
}
