using System;
using PacificCoral.Views;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace PacificCoral
{
	public class RootPage : TabbedPage, INavigationAware
	{
		//private Page ProfileView = new NavigationPage(new ProfileView()) { Icon ="accounttabbar", Title = "1" };
		//private Page DashBoardView = new NavigationPage(new DashBoardView()) { Icon = "dashboardtabbar", Title = "2" };
		//private Page InventoryView = new NavigationPage(new InventoryView()) { Icon = "itemstabbar", Title = "3" };

		private Page ProfileView = new ProfileView() { Icon = "accounttabbar", Title = " " };
		private Page DashBoardView = new DashBoardView() { Icon = "dashboardtabbar", Title = "  " };
		private Page InventoryView = new InventoryView() { Icon = "itemstabbar", Title = "   " };


		public RootPage()
		{
			ViewModelLocator.SetAutowireViewModel(this, true);

			Children.Add(ProfileView);
			Children.Add(DashBoardView);
			Children.Add(InventoryView);
		}

		#region -- INavigationAware implementation --

		public void OnNavigatedFrom(NavigationParameters parameters)
		{

		}

		public void OnNavigatingTo(NavigationParameters parameters)
		{

		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{
			this.CurrentPage = DashBoardView;
		}

		#endregion
	}
}
