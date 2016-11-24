using PacificCoral.Model;
using PacificCoral.Views;
using PacificCoral.Extensions;
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

namespace PacificCoral.ViewModels
{
    [ImplementPropertyChanged]
    public class DashBoardViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public DashBoardViewModel() { }

        public DashBoardViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            var sales = new ObservableCollection<SalesModel>();
            for (var i = 0; i < 3; i++)
            {
                sales.Add(new SalesModel() { Code = "421127", Shrimp = "SHRIMP WHT 71/90", Sep1 = "SEP::63451", Sep2 = "SEP::63451", GL = "G(L)(21,335)" });
            }
            Sales = sales;

            //init fake data for chart
            ChartItems  = new ObservableCollection<ChartSourceItem>();
            ChartItems.Add(new ChartSourceItem() { X = 2, Y = 100 });
            ChartItems.Add(new ChartSourceItem() { X = 4, Y = 200 });
            ChartItems.Add(new ChartSourceItem() { X = 6, Y = 160 });
            ChartItems.Add(new ChartSourceItem() { X = 8, Y = 110 });
            ChartItems.Add(new ChartSourceItem() { X = 10, Y = 300 });
            ChartItems.Add(new ChartSourceItem() { X = 12, Y = 400 });
            ChartItems.Add(new ChartSourceItem() { X = 14, Y = 450 });
            ChartItems.Add(new ChartSourceItem() { X = 16, Y = 460 });
        }
        public ObservableCollection<ChartSourceItem> ChartItems { get; set; }
        public ObservableCollection<SalesModel> Sales { get; set; }
        public ICommand ViewAccountsCommand
        {
            get { return new DelegateCommand(async () => { await _navigationService.NavigateAsync<AccountsView>(); }); }
        }
        public ICommand DetailsCommand
        {
            get { return new DelegateCommand(async () => { await _navigationService.NavigateAsync<DashBoard2View>(); }); }
        }
    }
}
