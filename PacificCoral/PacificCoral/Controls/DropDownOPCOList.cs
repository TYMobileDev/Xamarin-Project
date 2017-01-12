using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace PacificCoral
{
	public class DropDownOPCOList : Grid
	{
		private Label _label;
		private ListView _autoCompleteListView;
		private IList<string> _list;

		#region -- Public properties --

		public static readonly BindableProperty ItemSelectedCommandProperty =
			BindableProperty.Create(nameof(ItemSelectedCommand), typeof(ICommand), typeof(AutoCompleteList), default(ICommand));

		public ICommand ItemSelectedCommand
		{
			get { return (ICommand)GetValue(ItemSelectedCommandProperty); }
			set { SetValue(ItemSelectedCommandProperty, value); }
		}

		public static readonly BindableProperty SourceProperty =
			BindableProperty.Create(nameof(SourceItems), typeof(IList<string>), typeof(AutoCompleteList), default(string), defaultBindingMode: BindingMode.TwoWay);

		public IList<string> SourceItems
		{
			get { return (IList<string>)GetValue(SourceProperty); }
			set { SetValue(SourceProperty, value); }
		}

		#endregion

		public DropDownOPCOList()
		{
			this.RowDefinitions.Add(new RowDefinition() { Height = 30 });
			this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
			this.RowSpacing = 0;

			this.ColumnDefinitions.Add(new ColumnDefinition() { Width = 1 });
			this.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
			this.BackgroundColor = Color.Transparent;
			this.InputTransparent = false;

			_list = new List<string>
				{
					"PFG Atlanta",
					"PFG Columbia",
					"PFG Atlanta 1",
					"PFG Atlanta 2",
					"PFG Atlanta 3",
					"PFG Atlanta 4",
				};

			_autoCompleteListView = new ListView
			{
				IsVisible = false,
				RowHeight = 30,
				HeightRequest = 0,
				BackgroundColor = StyleManager.GetAppResource<Color>("DefaultMainColor"),
				VerticalOptions = LayoutOptions.Start,
				SeparatorVisibility = SeparatorVisibility.None,
				InputTransparent = false,
			};
			_autoCompleteListView.ItemTemplate = new DataTemplate(() =>
			{
				var label = new Label()
				{
					TextColor = Color.Black,
					HorizontalTextAlignment = TextAlignment.Center,
					FontSize = 12,
				};
				label.SetBinding(Label.TextProperty, ".");

				var separator = new BoxView()
				{
					HeightRequest = 1,
					BackgroundColor = StyleManager.GetAppResource<Color>("DefaultGreyColor"),
					HorizontalOptions = LayoutOptions.FillAndExpand,
				};

				var stack = new StackLayout()
				{
					HeightRequest = 20,
					Children = {
						label,
						separator,
					}
				};

				return new ViewCell { View = stack };
			});

			_label = new Label
			{
				FontSize = 13,
				Margin = new Thickness(0, 8, 0, 0),
				Text = _list[0],
				HorizontalOptions = LayoutOptions.CenterAndExpand,
			};

			var image = new Image()
			{
				Source = "arrow_down",
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Margin = new Thickness(0, 0, 0, 0),
				HeightRequest = 7,
			};

			var stackHrz = new StackLayout()
			{
				Orientation = StackOrientation.Horizontal,
				BackgroundColor = Color.Transparent,
				Children =
				{
					_label,
					image,
				},
			};

			var tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.Tapped += (s, e) =>
			{
				Show();
			};
			stackHrz.GestureRecognizers.Add(tapGestureRecognizer);

			this.Children.Add(_autoCompleteListView, 0, 2, 1, 2);
			this.Children.Add(stackHrz, 1, 0);

			_autoCompleteListView.ItemSelected += ItemSelected;
		}

		#region -- Private helpers --

		private async void Show()
		{
			try
			{
				if (_list != null)
				{
					_autoCompleteListView.HeightRequest = _list.Count * 15;
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
			var searchModel = (string)e.SelectedItem;

			HandleItemSelected(searchModel);
		}

		private void HandleItemSelected(string model)
		{
			if (model == null)
				return;

			_label.Text = model;

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
