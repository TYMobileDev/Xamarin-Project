using PacificCoral.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using PacificCoral.ViewModels;
using PacificCoral.Extensions;
using Prism.Navigation;
using Prism.Unity;
using Xamarin.Forms;

namespace PacificCoral
{
    public class App : PrismApplication
    {
        public static App Instance { get; set; }
        public INavigationService Navigation => NavigationService;

		protected override void OnInitialized()
        {
            Instance = this;
            NavigateToUnAuthorizedArea();
        }

        protected override void RegisterTypes()
        {
			Container.RegisterInstance<INavigationService>(Container.Resolve<Prism.Unity.Navigation.UnityPageNavigationService>());
            Container.RegisterTypeForNavigation<SignInView, SignInViewModel>();
            Container.RegisterTypeForNavigation<AccountsView, AccountsViewModel>();
            Container.RegisterTypeForNavigation<CustomerAccountView, CustomerAccountViewModel>();
            Container.RegisterTypeForNavigation<DashBoard2View, DashBoard2ViewModel>();
            Container.RegisterTypeForNavigation<DashBoardView, DashBoardViewModel>();
        }

        public async Task NavigateToUnAuthorizedArea()
        {
            await NavigationService.NavigateAsync<SignInView>();
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
    }
}
