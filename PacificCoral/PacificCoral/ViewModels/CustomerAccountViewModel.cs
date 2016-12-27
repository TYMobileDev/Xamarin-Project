using PacificCoral.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Mvvm;
using Prism.Navigation;
using PacificCoral.Extensions;

namespace PacificCoral.ViewModels
{
	public class CustomerAccountViewModel: BasePageViewModel, INavigationAware
    {
        private readonly INavigationService _navigationService;

        public CustomerAccountViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            var sales = new ObservableCollection<SalesModel>()
            {
                new SalesModel() 
                {
                    Code = "157195",
                    Title = "EMPIRE",
                    IndicatorUrl = "active.png",
                    Shrimp = "SHRIM TGR 8/12 HDLS/ON",
					Prospect = true
                },
                 new SalesModel()
                {
                    Code = "157205",
                    Title = "EMPIRE",
                    IndicatorUrl = "notactive.png",
                    Shrimp = "SHRIM TGR 21/25 RPDT/ON",
					Deviated = true
                },
                 new SalesModel()
                {
                    Code = "421096",
                    Title = "BAYWINDS",
                    IndicatorUrl = "active.png",
                    Shrimp = "SHRIM TGR 31/35 RPDT/ON/PFOS FR",
                }
            };

			var visits = new ObservableCollection<VisitModel>()
			{
				new VisitModel
				{
					Title = "Initial Visit",
					TimeStr = "3.00 PM",
					DateStr = "October, 23th",
					Description = "Presented Product"
				},
				new VisitModel
				{
					Title = "Follow Up Visit",
					TimeStr = "3.00 PM",
					DateStr = "October, 23th",
					Description = "Showed grouper."
				}

			};

			var tasks = new ObservableCollection<TaskModel>()
			{
				new TaskModel
				{
					Title = "Task 1",
					Description = "Replace one bag of shrimp",
					TimeStr = "3.00 PM",
					DateStr = "October, 23th",
					IsDone = true
				},
				new TaskModel
				{
					Title = "Task 2",
					Description = "Replace one bag of shrimp",
					TimeStr = "3.00 PM",
					DateStr = "October, 23th"
				}

			};
         
            Sales = sales;
			Visits = visits;
			Tasks = tasks;
			Title = "PFG Atlanta";
        }

		#region -- Public properties --

		private int _ActivePageIndex;

		public int ActivePageIndex
		{
			get { return _ActivePageIndex; }
			set { SetProperty(ref _ActivePageIndex, value); }
		}

		private IEnumerable<SalesModel> _Sales;

		public IEnumerable<SalesModel> Sales
		{
			get { return _Sales; }
			set { SetProperty(ref _Sales, value); }
		}

		private IEnumerable<VisitModel> _Visits;

		public IEnumerable<VisitModel> Visits
		{
			get { return _Visits; }
			set { SetProperty(ref _Visits, value); }
		}

		private IEnumerable<TaskModel> _Tasks;

		public IEnumerable<TaskModel> Tasks
		{
			get { return _Tasks; }
			set { SetProperty(ref _Tasks, value); }
		}

		public ICommand BackCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnBackCommandAsync); }
		}

		public ICommand ActionCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnActionCommandAsync); }
		}

		public ICommand ShowCustomerLocationCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnShowCustomerLocationCommandAsync); }
		}

		public ICommand ItemSelectedCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnItemSelectedCommandAsync); }
		}

		public ICommand VisitSelectedCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnVisitSelectedCommandAsync); }
		}

		public ICommand TaskSelectedCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnTaskSelectedCommandAsync); }
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
			var model = parameters.Get<CustomerModel>(nameof(CustomerModel));
			if (model != null)
			{
				//TODO: use model
			}
		}

		#endregion

		#region -- Private helpers --

		private async Task OnBackCommandAsync()
		{
			await _navigationService.GoBackAsync();
		}

		private async Task OnActionCommandAsync()
		{

		}

		private async Task OnShowCustomerLocationCommandAsync()
		{
			await _navigationService.NavigateAsync<PinLocationView>();
		}

		private Task OnItemSelectedCommandAsync(object obj)
		{
			var param = new NavigationParameters();
			param.Add(nameof(SalesModel), obj);
			return _navigationService.NavigateAsync<ItemView>(param);
		}

		private Task OnVisitSelectedCommandAsync(object obj)
		{
			var param = new NavigationParameters();
			param.Add(nameof(VisitModel), obj);
			return _navigationService.NavigateAsync<VisitView>(param);
		}

		private Task OnTaskSelectedCommandAsync(object obj)
		{
			var param = new NavigationParameters();
			param.Add(nameof(TaskModel), obj);
			return _navigationService.NavigateAsync<TaskView>(param);
		}

		#endregion
	}
}
