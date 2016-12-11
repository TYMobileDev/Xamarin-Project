using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using PacificCoral.Model;

namespace PacificCoral
{
	public class InventoryItemViewModel : BasePageViewModel, INavigationAware
	{
		private readonly INavigationService _navigationService;

		public InventoryItemViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			Title = "Item";
		}

		#region -- Public properties --

		private int _Count;

		public int Count
		{
			get { return _Count; }
			set { SetProperty(ref _Count, value); }
		}

		public ICommand EditCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnEditCommandAsync); }
		}

		private ICommand _DecrementCommand;

		public ICommand DecrementCommand
		{
			get { return _DecrementCommand; }
			set { SetProperty(ref _DecrementCommand, value); }
		}

		private ICommand _AddToOrderCommand;

		public ICommand AddToOrderCommand
		{
			get { return _AddToOrderCommand; }
			set { SetProperty(ref _AddToOrderCommand, value); }
		}

		public ICommand IncrementCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnIncrementCommandAsync); }
		}

		public ICommand BackCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnBackCommandAsync); }
		}

		#endregion

		#region -- INavigationAware imlementation --

		public void OnNavigatedFrom(NavigationParameters parameters)
		{
			
		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{
			
		}

		public void OnNavigatingTo(NavigationParameters parameters)
		{
			var model = parameters.Get<SalesModel>(nameof(SalesModel));
			if (model != null)
			{
				
			}
		}

		#endregion

		#region -- Private helpers --

		private void UpdateCommands()
		{
			if (Count > 0)
				AddToOrderCommand = SingleExecutionCommand.FromFunc(OnAddToOrderCommand);
			else
				AddToOrderCommand = null;
			if (Count > 0)
				DecrementCommand = SingleExecutionCommand.FromFunc(OnDecrementCommandAsync);
			else
				DecrementCommand = null;
		}

		private async Task OnEditCommandAsync()
		{

		}

		private async Task OnIncrementCommandAsync()
		{
			Count += 1;
			UpdateCommands();
		}

		private async Task OnDecrementCommandAsync()
		{
			if (Count > 0)
			Count -= 1;
			UpdateCommands();
		}

		private Task OnAddToOrderCommand()
		{
			return OnBackCommandAsync();
		}

		private Task OnBackCommandAsync()
		{
			return _navigationService.GoBackAsync();
		}

		#endregion
	}
}
