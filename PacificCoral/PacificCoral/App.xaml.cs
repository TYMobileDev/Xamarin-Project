﻿using Prism.Unity;
using Xamarin.Forms;
using Microsoft.Practices.Unity;
using Acr.UserDialogs;
using Xamarin.Forms.Xaml;
using Prism.Navigation;
using PacificCoral.Views;
using PacificCoral.ViewModels;
using Plugin.Media.Abstractions;
using Plugin.Media;

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

			MainPage = new SignInView()
			{
				BindingContext = Resolve<SignInViewModel>()
			};
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
			Container.RegisterInstance<IMedia>(CrossMedia.Current);

			var userMocks = true;
			if (userMocks)
			{
				//Container.RegisterInstance<IAccountService>(Resolve<AccountService_mock>());
			}
			else
			{

			}

			//Container.RegisterTypeForNavigation<LoginView>("Login");
			Container.RegisterTypeForNavigation<SignInView, SignInViewModel>();
			Container.RegisterTypeForNavigation<AccountsView, AccountsViewModel>();
			Container.RegisterTypeForNavigation<CustomerAccountView, CustomerAccountViewModel>();
			Container.RegisterTypeForNavigation<DashBoard2View>("DashBoard2View");
			Container.RegisterTypeForNavigation<DashBoardView, DashBoardViewModel>();
			Container.RegisterTypeForNavigation<InventoryView, InventoryViewModel>();
			Container.RegisterTypeForNavigation<PinLocationView, PinLocationViewModel>();
			Container.RegisterTypeForNavigation<VisitView, VisitViewModel>();
			Container.RegisterTypeForNavigation<TaskView, TaskViewModel>();
			Container.RegisterTypeForNavigation<ItemView, ItemViewModel>();
			Container.RegisterTypeForNavigation<AccountView, AccountViewModel>();
			Container.RegisterTypeForNavigation<LostSaleDetailsView>("LostSaleDetailsView");
			Container.RegisterTypeForNavigation<TrackMileageView, TrackMileageViewModel>();
			Container.RegisterTypeForNavigation<OrdersView, OrdersViewModel>();
			Container.RegisterTypeForNavigation<InventoryItemView, InventoryItemViewModel>();
			Container.RegisterTypeForNavigation<RootPage>("Root");
			Container.RegisterTypeForNavigation<NavigationRoot>("NavigationRoot");
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
