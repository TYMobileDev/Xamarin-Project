using PacificCoral.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PacificCoral.Views
{
    public partial class CustomerAccountView : ContentPage
    {
        public CustomerAccountView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
