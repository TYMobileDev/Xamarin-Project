using System;
using Xamarin.Forms;
using PacificCoral.Controls;
using System.ServiceModel.Channels;

namespace PacificCoral.Controls
{
	public class DashboardControl : ContentView
	{
		private AccordionControl accordionView;
		private StackLayout header;

		public DashboardControl()
		{
			CreateHeader();

			accordionView = new AccordionControl(header);
			accordionView.SetBinding(AccordionControl.ItemsSourceProperty, "AccordionSource");

			Margin = new Thickness(0, 0, 0, Device.OnPlatform(0, 50, 0));

			Content = accordionView;
		}

		#region -- Private helpers --

		private void CreateHeader()
		{
			header = new StackLayout()
			{
				Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0),
				BackgroundColor = StyleManager.GetAppResource<Color>("DefaultLightColor"),
			};

			var banner = new Image()
			{
				Source = "title.png",
				Margin = new Thickness(20, 5),
			};

			var trackMileageItem = new ClickableContentView()
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Content = new StackLayout()
				{
					Children = {
						new Image()
						{
							Source = "car",
							Style = StyleManager.GetAppResource<Style>("tabButtonImageStyle"),
						},
						new Label()
						{
							Text= "Track mileage",
							Style = StyleManager.GetAppResource<Style>("tabButtonLabelStyle"),
						}
					}
				}
			};
			trackMileageItem.SetBinding(ClickableContentView.CommandProperty, "ViewTrackMileageCommand");

			var viewAccountsItem = new ClickableContentView()
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Content = new StackLayout()
				{
					Children = {
						new Image()
						{
							Source = "account",
							Style = StyleManager.GetAppResource<Style>("tabButtonImageStyle"),
						},
						new Label()
						{
							Text= "View accounts",
							Style = StyleManager.GetAppResource<Style>("tabButtonLabelStyle"),
						}
					}
				}
			};
			viewAccountsItem.SetBinding(ClickableContentView.CommandProperty, "ViewAccountsCommand");

			var itemsItem = new ClickableContentView()
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Content = new StackLayout()
				{
					Children = {
						new Image()
						{
							Source = "items",
							Style = StyleManager.GetAppResource<Style>("tabButtonImageStyle"),
						},
						new Label()
						{
							Text= "Items",
							Style = StyleManager.GetAppResource<Style>("tabButtonLabelStyle"),
						}
					}
				}
			};
			itemsItem.SetBinding(ClickableContentView.CommandProperty, "ViewOrdersCommand");

			var settingsItem = new ClickableContentView()
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Content = new StackLayout()
				{
					Children = {
						new Image()
						{
							Source = "settings",
							Style = StyleManager.GetAppResource<Style>("tabButtonImageStyle"),
						},
						new Label()
						{
							Text= "Settings",
							Style = StyleManager.GetAppResource<Style>("tabButtonLabelStyle"),
						}
					}
				}
			};
			settingsItem.SetBinding(ClickableContentView.CommandProperty, "SettingsCommand");

			var tabStack = new StackLayout()
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = {
					trackMileageItem,
					viewAccountsItem,
					itemsItem,
					settingsItem,
				},
				Margin = new Thickness(0, 0, 0, 5),
			};

			var separator = new BoxView()
			{
				Style = StyleManager.GetAppResource<Style>("HorizontalSeparatorStyle"),
			};

			var picker = new BindablePicker()
			{
				HeightRequest = 50,
				BackgroundColor = StyleManager.GetAppResource<Color>("DefaultMainColor"),
				SelectedValuePath = "OPCO",
				DisplayMemberPath = "OPCO",
				Margin = new Thickness(5),
				Title = "Select Opco...",
			};
			picker.SetBinding(BindablePicker.ItemsSourceProperty, new Xamarin.Forms.Binding("Opcos"));
			picker.SetBinding(BindablePicker.SelectedValueProperty, new Xamarin.Forms.Binding("CurrentOpco"));


			header.Children.Add(banner);
			header.Children.Add(tabStack);
			header.Children.Add(separator);
			header.Children.Add(picker);

		}

		#endregion
	}
}