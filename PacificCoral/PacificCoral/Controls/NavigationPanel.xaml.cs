
using Microsoft.Practices.Unity;
using PacificCoral.Views;
using PacificCoral.Extensions;
using Prism.Navigation;
using Xamarin.Forms;

namespace PacificCoral.Controls
{
    public partial class NavigationPanel : ContentView
    {
        private readonly INavigationService navigationService;
        public static readonly BindableProperty ActivePageProperty = BindableProperty.Create("ActivePage", typeof(ActivePage), typeof(NavigationPanel), ActivePage.Page3, BindingMode.TwoWay, propertyChanging: ActivePageChanging);

        public ActivePage ActivePage
        {
            get { return (ActivePage)GetValue(ActivePageProperty); }
            set { SetValue(ActivePageProperty, value); }
        }

        public NavigationPanel()
        {
            InitializeComponent();
            navigationService = App.Instance?.Navigation;//Container?.Resolve<INavigationService>();

            if(navigationService == null) return;

            var tapGestureRecognizer1 = new TapGestureRecognizer();
            tapGestureRecognizer1.Tapped += async (s, e) => {
                await navigationService.NavigateAsync<AccountsView>();
            };
            image1.GestureRecognizers.Add(tapGestureRecognizer1);
            image1Active.GestureRecognizers.Add(tapGestureRecognizer1);
            
            var tapGestureRecognizer2 = new TapGestureRecognizer();
            tapGestureRecognizer2.Tapped += async (s, e) =>
            {
                await navigationService.NavigateAsync<DashBoardView>();
                //(App.Current.MainPage as NavigationPage).Navigation.PushAsync(new DashBoardView());
            };
            image2.GestureRecognizers.Add(tapGestureRecognizer2);
            image2Active.GestureRecognizers.Add(tapGestureRecognizer2);

            var tapGestureRecognizer3 = new TapGestureRecognizer();
            tapGestureRecognizer3.Tapped += async (s, e) => {
				await navigationService.NavigateAsync<InventoryView>();
                //(App.Current.MainPage as NavigationPage).Navigation.PushAsync(new DashBoard2View());
            };
            image3.GestureRecognizers.Add(tapGestureRecognizer3);
            image3Active.GestureRecognizers.Add(tapGestureRecognizer3);
        }

        private static void ActivePageChanging(object bindable, object oldValue, object newValue)
        {
            var navPanel = bindable as NavigationPanel;
            var activePage = (ActivePage)newValue;

            navPanel.image1.IsVisible = true;
            navPanel.image1Active.IsVisible = false;
            navPanel.image2.IsVisible = true;
            navPanel.image2Active.IsVisible = false;
            navPanel.image3.IsVisible = true;
            navPanel.image3Active.IsVisible = false;

            switch (activePage)
            {
                case ActivePage.Page1:                    
                    navPanel.image1Active.IsVisible = true;
                    break;
                case ActivePage.Page2:
                    navPanel.image2Active.IsVisible = true;
                    break;
                case ActivePage.Page3:
                    navPanel.image3Active.IsVisible = true;
                    break;
                default:
                    break;
            }
        }
    }

    public enum ActivePage
    { 
        Page1,
        Page2,
        Page3
    }
}
