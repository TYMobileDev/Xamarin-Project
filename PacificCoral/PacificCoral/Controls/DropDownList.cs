using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using PacificCoral.Model;

namespace PacificCoral
{
	public class DropDownList : Grid
	{
		private ListView _autoCompleteListView;
		private IList<CustomerModel> _list;

		#region -- Public properties --

		public static readonly BindableProperty ItemSelectedCommandProperty =
			BindableProperty.Create(nameof(ItemSelectedCommand), typeof(ICommand), typeof(AutoCompleteList), default(ICommand));

		public ICommand ItemSelectedCommand
		{
			get { return (ICommand)GetValue(ItemSelectedCommandProperty); }
			set { SetValue(ItemSelectedCommandProperty, value); }
		}

		public static readonly BindableProperty SourceProperty =
			BindableProperty.Create("SourceItems", typeof(IList<CustomerModel>), typeof(AutoCompleteList), default(string), defaultBindingMode: BindingMode.TwoWay);

		public IList<CustomerModel> SourceItems
		{
			get { return (IList<CustomerModel>)GetValue(SourceProperty); }
			set { SetValue(SourceProperty, value); }
		}

		#endregion

		public DropDownList()
		{

			this.RowDefinitions.Add(new RowDefinition() { Height = 35 });
			this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
			this.RowSpacing = 0;

			this.ColumnDefinitions.Add(new ColumnDefinition() { Width = 1 });
			this.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
			this.BackgroundColor = Color.White;
			this.InputTransparent = false;

			_list = new List<CustomerModel>
				{
					new CustomerModel()
					{
						UserName="John Smith",
						PhoneNumber = "+39876534677",
						Address = "Miami, the USA",
						Mail = "johnsmith@gmail.com",
					}
				};

			_autoCompleteListView = new ListView
			{
				IsVisible = false,
				RowHeight = 100,
				HeightRequest = 0,
				BackgroundColor = StyleManager.GetAppResource<Color>("DefaultMainColor"),
				VerticalOptions = LayoutOptions.Start,
				SeparatorVisibility = SeparatorVisibility.None,
				InputTransparent = false,
			};
			_autoCompleteListView.ItemTemplate = new DataTemplate(() =>
			{
				
				var nameLabel = new Label()
				{
					FontAttributes = FontAttributes.Bold,
					FontSize = 12,
				};
				nameLabel.SetBinding(Label.TextProperty, "UserName");

				var phoneLabel = new Label()
				{
					FontSize = 12,
				};
				phoneLabel.SetBinding(Label.TextProperty, "PhoneNumber");

				var mailLabel = new Label()
				{
					FontAttributes = FontAttributes.Bold,
					FontSize = 12,
				};
				mailLabel.SetBinding(Label.TextProperty, "Mail");


				var adressLabel = new Label()
				{
					FontSize = 12,
				};
				adressLabel.SetBinding(Label.TextProperty, "Address");

				var stackAddress = new StackLayout()
				{
					Orientation = StackOrientation.Horizontal,
					Children = {
						new Label(){ Text = "Address: ", FontSize= 12, FontAttributes = FontAttributes.Bold },
						adressLabel,
					},
				};

				var stack = new StackLayout()
				{
					Margin = new Thickness(10, 10, 0, 0),
					Children =
					{
						nameLabel,
						phoneLabel,
						mailLabel,
						stackAddress
					},
				}; 

				return new ViewCell { View = stack };

			});

			var label = new Label()
			{
				Margin = new Thickness(0, 15, 0, 0),
				Text = "Operating Partner",
				FontSize = 13,
			};

			var tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.Tapped += (s, e) =>
			{
				Search();
			};
			label.GestureRecognizers.Add(tapGestureRecognizer);

			var image = new Image()
			{
				Source = "plus",
				Margin = new Thickness(0, 15, 0, 0),
				HorizontalOptions = LayoutOptions.Start,
				HeightRequest = 10,
			};

			var stackHrz = new StackLayout()
			{
				Orientation = StackOrientation.Horizontal,
				Children =
				{
					label,
					image,
				},
			};

			this.Children.Add(stackHrz, 1, 0);
			this.Children.Add(_autoCompleteListView, 0, 2, 1, 2);

			_autoCompleteListView.ItemSelected += ItemSelected;

		}

		#region -- Private helpers --

		private void Search()
		{
			try
			{
				if (_list != null)
				{
					_autoCompleteListView.HeightRequest = _list.Count * 100;
					_autoCompleteListView.IsVisible = true;
					_autoCompleteListView.ItemsSource = _list;
				}
				else
				{
					_autoCompleteListView.HeightRequest = 0;
					_autoCompleteListView.IsVisible = false;
				}
			}
			catch (Exception ex)
			{
				// TODO
			}
		}

		private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
				return;
			
			_autoCompleteListView.SelectedItem = null;
			Reset();
		}

		private void Reset()
		{
			_autoCompleteListView.ItemsSource = null;
			_autoCompleteListView.IsVisible = false;
			_autoCompleteListView.HeightRequest = 0;
		}

		#endregion
	}
}
