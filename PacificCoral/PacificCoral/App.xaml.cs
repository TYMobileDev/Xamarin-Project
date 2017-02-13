using Prism.Unity;
using Xamarin.Forms;
using Microsoft.Practices.Unity;
using Acr.UserDialogs;
using Xamarin.Forms.Xaml;
using Prism.Navigation;
using PacificCoral.Views;
using PacificCoral.ViewModels;
using Plugin.Media.Abstractions;
using Plugin.Media;
using Plugin.Settings.Abstractions;
using Plugin.Settings;
using System;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PacificCoral
{
	public partial class App : PrismApplication
	{
		//TODO: remove it
		public static App Instance { get; set; }
		public INavigationService Navigation => NavigationService;

		public static T Resolve<T>()
		{
			return (Current as App).Container.Resolve<T>();
		}

		public App()
		{
			InitializeComponent();
			//MainPage = new SignInView
			//{
			//	BindingContext = Resolve<SignInViewModel>()
			//};
			MainPage = new DashBoardView();
		}

		#region -- Overrides --

		protected override void OnInitialized()
		{
			Instance = this;
		}

		protected override void RegisterTypes()
		{
			Container.RegisterInstance<INavigationService>(Container.Resolve<Prism.Unity.Navigation.UnityPageNavigationService>());
			Container.RegisterInstance<IUserDialogs>(UserDialogs.Instance);
			Container.RegisterInstance<ISettings>(CrossSettings.Current);
			Container.RegisterInstance<IMedia>(CrossMedia.Current);

			var userMocks = true;
			if (userMocks)
			{
				//Container.RegisterInstance<IAccountService>(Resolve<AccountService_mock>());
			}
			else
			{

			}

			Container.RegisterTypeForNavigation<SignInView, SignInViewModel>();
			Container.RegisterTypeForNavigation<AccountsView>("AccountsView");
			Container.RegisterTypeForNavigation<CustomerAccountView, CustomerAccountViewModel>();
			Container.RegisterTypeForNavigation<DashBoard2View>("DashBoard2View");
			Container.RegisterTypeForNavigation<DashBoardView>("DashBoardView");
			Container.RegisterTypeForNavigation<InventoryView, InventoryViewModel>();
			Container.RegisterTypeForNavigation<PinLocationView, PinLocationViewModel>();
			Container.RegisterTypeForNavigation<VisitView, VisitViewModel>();
			Container.RegisterTypeForNavigation<TaskView, TaskViewModel>();
			Container.RegisterTypeForNavigation<ItemView, ItemViewModel>();
			Container.RegisterTypeForNavigation<AccountView>("AccountView");
			Container.RegisterTypeForNavigation<LostSaleDetailsView>("LostSaleDetailsView");
			Container.RegisterTypeForNavigation<TrackMileageView>("TrackMileageView");
			Container.RegisterTypeForNavigation<OrdersView>("OrdersView");
			Container.RegisterTypeForNavigation<InventoryItemView, InventoryItemViewModel>();
			Container.RegisterTypeForNavigation<RootPage>("Root");
			Container.RegisterTypeForNavigation<SettingsView>("SettingsView");
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

		#endregion
	}
}
