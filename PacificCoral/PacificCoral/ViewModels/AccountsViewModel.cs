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
    [ImplementPropertyChanged]
    public class AccountsViewModel: BindableBase
    {
        private readonly INavigationService _navigationService;
        private CustomerModel _selectedCustomer;

        public AccountsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Customers = PageTypeGroup();
        }
        public AccountsViewModel() { }

        public CustomerModel SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if (_selectedCustomer != value)
                {
                    //terrible logic! need to be changed
                    _selectedCustomer = null;
                    _navigationService.NavigateAsync<CustomerAccountView>();
                }
            }
        }

        public ICommand BackCommand { get { return new DelegateCommand(async () => { await _navigationService.GoBackAsync(); }); } }
        public List<PageTypeGroup> Customers { get; set; }

        private static List<PageTypeGroup> PageTypeGroup()
        {
            var list = new List<PageTypeGroup>
            {
                new PageTypeGroup ("A"){
                    new CustomerModel()
                    {
                        PhotoUrl = "customerPhoto.png",
                        UserName = "User Name Z",
                        Address = "Miami, the USA",
                        PhoneNumber = "+39876534677"
                    },
                    new CustomerModel()
                    {
                        PhotoUrl = "customerPhoto.png",
                        UserName = "User Name W",
                        Address = "Miami, the USA",
                        PhoneNumber = "+39876534677"
                    },
                    new CustomerModel()
                    {
                        PhotoUrl = "customerPhoto.png",
                        UserName = "User Name C",
                        Address = "Miami, the USA",
                        PhoneNumber = "+39876534677"
                    }
                },
                new PageTypeGroup ("B"){
                    new CustomerModel()
                    {
                        PhotoUrl = "customerPhoto.png",
                        UserName = "User Name D",
                        Address = "Miami, the USA",
                        PhoneNumber = "+39876534677"
                    },
                    new CustomerModel()
                    {
                        PhotoUrl = "customerPhoto.png",
                        UserName = "User Name A",
                        Address = "Miami, the USA",
                        PhoneNumber = "+39876534677"
                    },
                    new CustomerModel()
                    {
                        PhotoUrl = "customerPhoto.png",
                        UserName = "User Name F",
                        Address = "Miami, the USA",
                        PhoneNumber = "+39876534677"
                    }
                }
            };

			foreach (var item in list)
			{
				item.Sort( (x, y) => x.UserName.CompareTo(y.UserName));

			}

			return list;
        }
    }
}
