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

namespace PacificCoral.ViewModels
{
    [ImplementPropertyChanged]
    public class CustomerAccountViewModel: BindableBase
    {
        private readonly INavigationService _navigationService;

        public CustomerAccountViewModel() { }

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

                },
                 new SalesModel()
                {
                    Code = "157205",
                    Title = "EMPIRE",
                    IndicatorUrl = "notactive.png",
                    Shrimp = "SHRIM TGR 21/25 RPDT/ON",
                },
                 new SalesModel()
                {
                    Code = "421096",
                    Title = "BAYWINDS",
                    IndicatorUrl = "active.png",
                    Shrimp = "SHRIM TGR 31/35 RPDT/ON/PFOS FR",
                }
            };

         
            Sales = sales;
        }

        public ObservableCollection<SalesModel> Sales { get; set; }
        public ICommand BackCommand { get { return new DelegateCommand(async () => { await _navigationService.GoBackAsync(); }); } }
    }
}
