using System;
using PacificCoral.Views;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace PacificCoral
{
	public class RootPage : TabbedPage, INavigationAware
	{
		private Page ProfileView = new ProfileView();
		private Page DashBoardView = new DashBoardView();
		private Page InventoryView = new InventoryView();

		public RootPage()
		{
			ViewModelLocator.SetAutowireViewModel(this, true);

			this.BarBackgroundColor = Color.Yellow;
			this.BarTextColor = Color.Yellow;

			ProfileView.SetTabColor(Color.FromHex("#FF5252"));

			Children.Add(ProfileView);
			Children.Add(DashBoardView);
			Children.Add(InventoryView);
			FixedMode = true;
			BarTheme = BarThemeTypes.DarkWithAlpha;
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

		public enum BarThemeTypes { Light, DarkWithAlpha, DarkWithoutAlpha }

		public bool FixedMode { get; set; }
		public BarThemeTypes BarTheme { get; set; }

		public void RaiseCurrentPageChanged()
		{
			OnCurrentPageChanged();
		}
	}
}
