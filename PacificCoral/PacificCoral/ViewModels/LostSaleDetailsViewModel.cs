using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using System.Collections.Generic;

namespace PacificCoral
{
	public class LostSaleDetailsViewModel : BasePageViewModel, INavigationAware
	{
		private readonly INavigationService _navigationService;

		public LostSaleDetailsViewModel (INavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		#region -- Public properties --

		private string _DetailsTitle;

		public string DetailsTitle
		{
			get { return _DetailsTitle; }
			set { SetProperty(ref _DetailsTitle, value); }
		}

		private string _DetailsSubtitle;

		public string DetailsSubtitle
		{
			get { return _DetailsSubtitle; }
			set { SetProperty(ref _DetailsSubtitle, value); }
		}

		private IEnumerable<GroupItem> _Items;

		public IEnumerable<GroupItem> Items
		{
			get { return _Items; }
			set { SetProperty(ref _Items, value); }
		}

		public ICommand BackCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnBackCommandAsync); }
		}

		#endregion

		#region -- INavigationAware implementation --

		public void OnNavigatedFrom(NavigationParameters parameters)
		{
			
		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{
			
		}

		public void OnNavigatingTo(NavigationParameters parameters)
		{
			//TODO: remove stub
			DetailsTitle = "PFG ATLANTA SALES SEP - OCT";
			DetailsSubtitle = "421153 - SHRIMP WHT 71/90 RPD/TOFF PHOS FR";
			var items = new GroupItem();
			items.StartTitle = "Sep";
			items.EndTitle = "Oct";
			var r = new Random();
			for (int i = 0; i < 10; i++)
			{
				var item = new LostSaleDetaileItem
				{
					Customer = $"Customer #{i+1}",
					Start = r.Next(100),
					End = r.Next(100)

				};
				items.Add(item);
			}

			Items = new List<GroupItem>
			{
				items
			};
		}

		#endregion

		#region -- Prviate helpers --

		private Task OnBackCommandAsync()
		{
			return _navigationService.GoBackAsync();
		}

		#endregion

		public class GroupItem : List<LostSaleDetaileItem>
		{
			public string StartTitle { get; set; }
			public string EndTitle { get; set; }
		}
	}
}
