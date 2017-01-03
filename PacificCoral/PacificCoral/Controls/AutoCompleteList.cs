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

		public static readonly BindableProperty PlaceSelectedCommandProperty =
			BindableProperty.Create(nameof(PlaceSelectedCommand), typeof(ICommand), typeof(AutoCompleteList), default(ICommand));

		public ICommand PlaceSelectedCommand
		{
			get { return (ICommand)GetValue(PlaceSelectedCommandProperty); }
			set { SetValue(PlaceSelectedCommandProperty, value); }
		}

		public static readonly BindableProperty BackCommandProperty =
			BindableProperty.Create(nameof(BackCommand), typeof(ICommand), typeof(AutoCompleteList), default(ICommand));

		public ICommand BackCommand
		{
			get { return (ICommand)GetValue(BackCommandProperty); }
			set { SetValue(BackCommandProperty, value); }
		}

		public static readonly BindableProperty UseMyCurrentLocationCommandProperty =
			BindableProperty.Create(nameof(UseMyCurrentLocationCommand), typeof(ICommand), typeof(AutoCompleteList), default(ICommand));

		public ICommand UseMyCurrentLocationCommand
		{
			get { return (ICommand)GetValue(UseMyCurrentLocationCommandProperty); }
			set { SetValue(UseMyCurrentLocationCommandProperty, value); }
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
			this.RowDefinitions.Add(new RowDefinition() { Height = 44 });
			//this.RowDefinitions.Add(new RowDefinition() { Height = 52 });
			this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
			this.RowSpacing = 0;

			this.ColumnDefinitions.Add(new ColumnDefinition() { Width = 1 });
			this.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

			this.Padding = new Thickness(0, Device.OnPlatform(0, 0, 0), 0, 0);
			this.BackgroundColor = Color.Red;

			_autoCompleteListView = new ListView
			{
				IsVisible = false,
				RowHeight = 25,
				HeightRequest = 0,
				BackgroundColor = StyleManager.GetAppResource<Color>("DefaultMainColor"),
				VerticalOptions = LayoutOptions.Start
			};
			//_autoCompleteListView.ItemTemplate = new DataTemplate(() =>
			//{
			//	var cell = new TextCell();
			//	cell.SetBinding(ImageCell.TextProperty, "Formatted_address");

			//	return cell;
			//});

			_entry = new ExtendedEntry
			{
				Placeholder = "PrimaryID",
				VerticalOptions = LayoutOptions.Center,
				BackgroundColor = Color.White
			};
			_entry.TextChanged += SearchTextChanged;
			_entry.Unfocused += EntryUnfocused;
			_entry.Focused += EntryUnfocused;

			var entryContentView = new RoundedContentView()
			{
				//Margin = new Thickness(5, 0),
				//Padding = new Thickness(15, 5),
				Content = _entry,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.Blue,
				Corners = 10
			};

			//var locationImage = new Image()
			//{
			//	//Source = "current_location",
			//	VerticalOptions = LayoutOptions.CenterAndExpand,
			//};

			//var locationLabel = new Label()
			//{
			//	Text = "UseMyCurrentLocation",
			//	VerticalOptions = LayoutOptions.CenterAndExpand,
			//	FontSize = 16,
			//	TextColor = Color.Red,
			//	LineBreakMode = LineBreakMode.TailTruncation
			//};

			//var location = new StackLayout()
			//{
			//	BackgroundColor = StyleManager.GetAppResource<Color>("DefaultDarkColor"),
			//	Orientation = StackOrientation.Horizontal,
			//	Padding = new Thickness(15, 0),
			//	InputTransparent = true,
			//	Spacing = 15
			//};
			//location.Children.Add(locationImage);
			//location.Children.Add(locationLabel);

			//_clickableLocationLabel = new ClickableContentView()
			//{
			//	Content = location,
			//	VerticalOptions = LayoutOptions.FillAndExpand,
			//};
			//_clickableLocationLabel.BindingContext = this;
			//_clickableLocationLabel.SetBinding(ClickableContentView.CommandProperty, nameof(UseMyCurrentLocationCommand));

			//this.Children.Add(boxListView, 0, 2, 2, 3);
			//this.Children.Add(new BoxView() { BackgroundColor = StyleManager.GetAppResource<Color>("DefaultDarkColor") }, 0, 2, 1, 2);
			//this.Children.Add(_clickableLocationLabel, 0, 2, 1, 2);

			this.Children.Add(_entry, 1, 0);
			this.Children.Add(_autoCompleteListView, 0, 2, 1, 2);
			//this.Children.Add(_autoCompleteListView, 0, 2, 2, 3);

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
			SearchPlaces();
		}

		private async void SearchPlaces()
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
					"32939203902",
					"5784758478",
					"123",
					"434343",
					"323232",
				};

				if (list != null)
				{
					_autoCompleteListView.HeightRequest = list.Count * 20;
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
				SearchPlaces();
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
