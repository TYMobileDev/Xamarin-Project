using PacificCoral.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacificCoral.Helpers;

using Xamarin.Forms;

namespace PacificCoral.Views
{
    public partial class SignInView : ContentPage
    {
        public SignInView()
        {
            InitializeComponent();

            //BindingContext = new SignInViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
