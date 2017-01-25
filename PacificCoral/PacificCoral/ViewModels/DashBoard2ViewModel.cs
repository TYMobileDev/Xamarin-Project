using PacificCoral.Model;
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
using PacificCoral.Extensions;

namespace PacificCoral.ViewModels
{
	public class DashBoard2ViewModel: BasePageViewModel
    {
        private readonly INavigationService _navigationService;

        public DashBoard2ViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            var sales = new ObservableCollection<SalesModel>();
            for (var i = 0; i < 4; i++)
            {
                sales.Add(new SalesModel() { Code = "421127", Shrimp = "SHRIMP WHT 71/90", Sep1 = "SEP::63451", Sep2 = "SEP::63451", GL = "G(L)(21,335)" });
            }
            Sales = sales;

            ChartItems = new ObservableCollection<ChartSourceItem>();
            ChartItems.Add(new ChartSourceItem() { X = 2, Y = 100 });
            ChartItems.Add(new ChartSourceItem() { X = 4, Y = 200 });
            ChartItems.Add(new ChartSourceItem() { X = 6, Y = 160 });
            ChartItems.Add(new ChartSourceItem() { X = 8, Y = 110 });
            ChartItems.Add(new ChartSourceItem() { X = 10, Y = 300 });
            ChartItems.Add(new ChartSourceItem() { X = 12, Y = 400 });
            ChartItems.Add(new ChartSourceItem() { X = 14, Y = 450 });
            ChartItems.Add(new ChartSourceItem() { X = 16, Y = 460 });
        }

		#region -- Public properties --

		public ObservableCollection<ChartSourceItem> ChartItems { get; set; }
		public ObservableCollection<SalesModel> Sales { get; set; }

		public ICommand DetailsCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnDetailsCommandAsync); }
		}

		public ICommand BackCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnBackCommandAsync); }
		}

		#endregion

		#region -- Private helpers --


		private Task OnDetailsCommandAsync()
		{
			return _navigationService.NavigateAsync<LostSaleDetailsView>();
		}

		private Task OnBackCommandAsync()
		{
			return _navigationService.GoBackAsync();
		}

		#endregion
    }
}
