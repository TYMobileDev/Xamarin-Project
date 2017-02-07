using PacificCoral.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PacificCoral.Views
{
    public partial class DashBoardView : ContentPage
    {
        public DashBoardView()
        {
            try
            {
                InitializeComponent();
                NavigationPage.SetHasNavigationBar(this, false);
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }       
    }
}
