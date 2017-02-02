using PacificCoral.Model;
using PacificCoral.Views;
using PacificCoral.Extensions;
using PacificCoral.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PropertyChanged;
using Xamarin.Forms;
using System.Runtime.InteropServices.WindowsRuntime;

namespace PacificCoral.ViewModels
{
	public class AccountsViewModel: BasePageViewModel
    {
        private readonly INavigationService _navigationService;
        private string _title;

        public  AccountsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitializeData();
        }
        private async Task InitializeData()
        {
            var l = await DataManager.DefaultManager.CustomersTable.GetFilteredTable(Globals.CurrentOpco);
            var sorted = l.OrderBy(p => p.CustomerName).GroupBy(p => p.NameSort).
                Select(gp => new Grouping<string, Customers>(gp.Key, gp));
            Customer = new ObservableCollection<Helpers.Grouping<string, Model.Customers>>(sorted);

            Title = Globals.CurrentOpco.Trim() + " - Customers";
        }

		#region -- Public properties --

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                SetProperty<string>(ref _title, value);
            }
        }
        public ObservableCollection<Grouping<string, Customers>> Customer {
            get;
            set; }

		public ICommand CustomerSelectedCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnCustomerSelectedCommandAsync); }
		}

		public ICommand BackCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnBackCommandAsync); }
		}

		public ICommand AddCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnAddCommandAsync); }
		}

		#endregion


		#region -- Private helpers --


		private Task OnCustomerSelectedCommandAsync(object customerObj)
		{
			var model = customerObj as CustomerModel;
			var param = new NavigationParameters();
			param.Add(nameof(CustomerModel), model);
			return _navigationService.NavigateAsync<CustomerAccountView>(param);
		}

		private Task OnBackCommandAsync()
		{
			return _navigationService.GoBackAsync();
		}

		private Task OnAddCommandAsync()
		{
			return _navigationService.NavigateAsync<AccountView>();
		}

		#endregion

    }
}
