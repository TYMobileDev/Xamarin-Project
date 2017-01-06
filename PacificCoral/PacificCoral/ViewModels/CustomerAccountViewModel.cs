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

			var primaryID = new ObservableCollection<string>
			{
				"1233455",
				"3293920",
				"5784758"
			};

            var sales = new ObservableCollection<SalesModel>()
            {
                new SalesModel() 
                {
                    Code = "157195",
                    Title = "EMPIRE",
                    IndicatorUrl = "active.png",
                    Shrimp = "SHRIM TGR 8/12 HDLS/ON",
					NumberCasesShipped = "10",
					Usage = "Month to Month",
					Prospect = true,
					Description = "This is a Description",
					Substitute = "This is a Substitute",
					DeviatedYes = true,
					DeviatedNo = false,
				},
                 new SalesModel()
                {
                    Code = "157205",
                    Title = "EMPIRE",
                    IndicatorUrl = "notactive.png",
                    Shrimp = "SHRIM TGR 21/25 RPDT/ON",
					NumberCasesShipped = "20",
					Usage = "Month to Month",
					Deviated = true,
					Description = "This is a Description",
					Substitute = "This is a Substitute",
					DeviatedNo = true,
					DeviatedYes = false,
                },
                 new SalesModel()
                {
                    Code = "421096",
                    Title = "BAYWINDS",
                    IndicatorUrl = "active.png",
					NumberCasesShipped = "20",
					Usage = "Month to Month",
				    Shrimp = "SHRIM TGR 31/35 RPDT/ON/PFOS FR",
					Description = "This is a Description",
					Substitute = "This is a Substitute",
					DeviatedYes = true,
					DeviatedNo = false,
                }
            };

			var visits = new ObservableCollection<VisitModel>()
			{
				new VisitModel
				{
					Title = "Initial Visit",
					TimeStr = "3.00 PM",
					DateStr = "October, 23th",
					Description = "Presented Product",
					Date = DateTime.Now,
				},
				new VisitModel
				{
					Title = "Follow Up Visit",
					TimeStr = "3.00 PM",
					DateStr = "October, 23th",
					Description = "Showed grouper.",
					Date = DateTime.Now,
				}
			};

			var tasks = new ObservableCollection<TaskModel>()
			{
				new TaskModel
				{
					Title = "Task 1",
					Description = "Replace one bag of shrimp",
					TimeStr = "3.00 PM",
					IsDone = true,
					Date = DateTime.Now,
					DateStr = "December, 23th",
				},
				new TaskModel
				{
					Title = "Task 2",
					Description = "Replace one bag of shrimp",
					TimeStr = "3.00 PM",
					DateStr = "October, 23th",
					Date = DateTime.Now,
				},
				new TaskModel
				{
					Title = "Task 3",
					Description = "Replace one bag of shrimp",
					TimeStr = "9.00 PM",
					DateStr = "October, 23th",
					IsDone = true,
					Date = DateTime.Now,
				},
				new TaskModel
				{
					Title = "Task 4",
					Description = "Replace one bag of shrimp",
					Date = DateTime.Now,
					TimeStr = "5.00 PM",
					DateStr = "October, 20th"
				}
			};
         
            Sales = sales;
			Visits = visits;
			Tasks = tasks;
			PrimaryID = primaryID;
			Title = "PFG Atlanta";
        }

		#region -- Public properties --

		private int _ActivePageIndex;

		public int ActivePageIndex
		{
			get { return _ActivePageIndex; }
			set { SetProperty(ref _ActivePageIndex, value); }
		}

		private IEnumerable<string> _PrimaryID;

		public IEnumerable<string> PrimaryID
		{
			get { return _PrimaryID; }
			set { SetProperty(ref _PrimaryID, value); }
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
