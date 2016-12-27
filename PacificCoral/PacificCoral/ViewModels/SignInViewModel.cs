using PacificCoral.Views;
using PacificCoral.Extensions;
using System;
using System.Collections.Generic;
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
    public class SignInViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public SignInViewModel() { }

        public SignInViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand SignInCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    //await _navigationService.NavigateAsync<DashBoardView>();
					await _navigationService.NavigateAsync("/Root");
                });
            }
        }
    }
}
