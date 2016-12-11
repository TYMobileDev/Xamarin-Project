using PacificCoral.Model;
using PacificCoral.Views;
using PacificCoral.Extensions;
using System;
using System.Collections.Generic;
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

        public AccountsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Customers = PageTypeGroup();
        }

		#region -- Public properties --

		private List<PageTypeGroup> _Customers;

		public List<PageTypeGroup> Customers
		{
			get { return _Customers; }
			set { SetProperty(ref _Customers, value); }
		}

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

		private static List<PageTypeGroup> PageTypeGroup()
		{
			var list = new List<PageTypeGroup>();
			var A = (int)'A';
			var r = new Random();
			for (int i = 0; i < 26; i++)
			{
				var letter = ((char)(A + i)).ToString();
				var group = new PageTypeGroup(letter);
				var customersCount = r.Next(1, 8);
				for (int j = 0; j < customersCount; j++)
				{
					group.Add(new CustomerModel()
					{
						PhotoUrl = "customerPhoto.png",
						UserName = letter + " User Name " + j,
						Address = "Miami, the USA",
						PhoneNumber = "+39876534677"
					});

				}
				list.Add(group);

			}
			list.Sort((x, y) => x.Title.CompareTo(y.Title));

			foreach (var item in list)
			{
				item.Sort((x, y) => x.UserName.CompareTo(y.UserName));

			}

			return list;
		}

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
