using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace PacificCoral
{
	public class AutoCompleteList : Grid
	{
		private bool _textChangeItemSelected;
		private ExtendedEntry _entry;
		private ListView _autoCompleteListView;

		#region -- Public properties --

		public static readonly BindableProperty ItemSelectedCommandProperty =
			BindableProperty.Create(nameof(ItemSelectedCommand), typeof(ICommand), typeof(AutoCompleteList), default(ICommand));

		public ICommand ItemSelectedCommand
		{
			get { return (ICommand)GetValue(ItemSelectedCommandProperty); }
			set { SetValue(ItemSelectedCommandProperty, value); }
		}

		public static readonly BindableProperty SourceProperty = 
			BindableProperty.Create("SourceItems", typeof(IList<string>), typeof(AutoCompleteList), default(string), defaultBindingMode:BindingMode.TwoWay);
		
		public IList<string> SourceItems
		{
			get { return (IList<string>)GetValue(SourceProperty); }
			set { SetValue(SourceProperty, value); }
		}

		#endregion

		public AutoCompleteList()
		{
			this.RowDefinitions.Add(new RowDefinition() { Height = 35 });
			this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
			this.RowSpacing = 0;

			this.ColumnDefinitions.Add(new ColumnDefinition() { Width = 1 });
			this.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
			this.BackgroundColor = Color.White;

			_autoCompleteListView = new ListView
			{
				IsVisible = false,
				RowHeight = 30,
				HeightRequest = 0,
				BackgroundColor = StyleManager.GetAppResource<Color>("DefaultMainColor"),
				VerticalOptions = LayoutOptions.Start,
			};
			_autoCompleteListView.ItemTemplate = new DataTemplate(() =>
			{
				var cell = new TextCell();
				cell.SetBinding(TextCell.TextProperty, ".");
				cell.TextColor = Color.Black;
				return cell;
			});

			_entry = new ExtendedEntry
			{
				FontSize = 13,
				Placeholder = "PrimaryID",
				BackgroundColor = Color.White
			};
			_entry.Focused += EntryUnfocused;
			_entry.TextChanged += SearchTextChanged;
			_entry.Unfocused += EntryUnfocused;


			var image = new Image()
			{
				Source = "plus",
				HorizontalOptions = LayoutOptions.Start,
				HeightRequest=10,
			};

			var stackHrz = new StackLayout()
			{
				Orientation = StackOrientation.Horizontal,
				Children =
				{
					_entry,
					image,
				},
			};

			this.Children.Add(stackHrz, 1, 0);
			this.Children.Add(_autoCompleteListView, 0, 2, 1, 2);

			_autoCompleteListView.ItemSelected += ItemSelected;

			_textChangeItemSelected = false;
		}

		#region -- Private helpers --

		private void SearchTextChanged(object sender, TextChangedEventArgs e)
		{
			if (_textChangeItemSelected)
			{
				_textChangeItemSelected = false;
				return;
			}
			Search();
		}

		private async void Search()
		{
			try
			{
				if (string.IsNullOrEmpty(_entry.Text))
				{
					_autoCompleteListView.ItemsSource = null;
					_autoCompleteListView.IsVisible = false;
					_autoCompleteListView.HeightRequest = 0;
					return;
				}

				var list = new List<string>
				{
					"1233455",
					"3293920",
					"5784758",
					"1237777",
					"4343437",
					"3232327",
				};

				if (list != null)
				{
					_autoCompleteListView.HeightRequest = list.Count * 15;
					_autoCompleteListView.IsVisible = true;
					_autoCompleteListView.ItemsSource = list;
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

		private async void HandleItemSelected(string model)
		{
			if (model == null)
				return;

			//var filterLocation = new FilterLocation();

			//if (model.Geometry != null)
			//{

			//	var coordinate = new CoordinateDto()
			//	{
			//		Lat = (decimal)model.Geometry.Location.Lat,
			//		Lng = (decimal)model.Geometry.Location.Lng
			//	};

			//	filterLocation.Description = model.Formatted_address;
			//	filterLocation.Coordinate = coordinate;
			//}

			//if (PlaceSelectedCommand != null && PlaceSelectedCommand.CanExecute(this))
			//	PlaceSelectedCommand.Execute(filterLocation);

			_entry.Text = model;

			_textChangeItemSelected = true;
			_autoCompleteListView.SelectedItem = null;
			Reset();
		}

		private void EntryUnfocused(object sender, FocusEventArgs e)
		{
			if (e.IsFocused)
				Search();
		}

		private void Reset()
		{
			_autoCompleteListView.ItemsSource = null;
			_autoCompleteListView.IsVisible = false;
			_autoCompleteListView.HeightRequest = 0;

			_entry.Unfocus();
		}

		#endregion
	}
}
