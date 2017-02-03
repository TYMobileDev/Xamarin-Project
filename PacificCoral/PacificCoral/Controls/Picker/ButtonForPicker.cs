using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PacificCoral.Controls
{
	public class ButtonForPicker : ContentView
	{
		private ExtendedPicker _picker;

		public ButtonForPicker()
		{
			BackgroundColor = Color.Transparent;
			var tap = new TapGestureRecognizer();
			tap.BindingContext = this;
			tap.SetBinding(TapGestureRecognizer.CommandProperty, CommandProperty.PropertyName);

			var title = new Label
			{
				BindingContext = this,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				TextColor = Color.Black,
				InputTransparent = true
			};
			title.SetBinding(Label.TextProperty, TitleProperty.PropertyName);

			var displayedValue = new Label
			{
				BindingContext = this,
				//TextColor = StyleManager.GetAppResource<Color>("cloudyBlue"),
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				//FontSize = StyleManager.GetAppResource<Double>("FontSize_15"),
				InputTransparent = true
			};
			displayedValue.SetBinding(Label.TextProperty, DisplayedValueProperty.PropertyName);

			var picker = new ExtendedPicker
			{
				BindingContext = this,
				//TextColor = StyleManager.GetAppResource<Color>("darktwo"),
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Opacity = Device.OnPlatform(0.1, 0, 0),
				BackgroundColor = Color.Transparent,
			};
			picker.SetBinding(Picker.IsEnabledProperty, EnablePickerProperty.PropertyName);
			picker.SetBinding(Picker.SelectedIndexProperty, SelectedIndexProperty.PropertyName, BindingMode.TwoWay);

			_picker = picker;

			var grid = new Grid();

			var stack = new StackLayout
			{
				BackgroundColor = Color.Red,//StyleManager.GetAppResource<Color>("darktwo"),
				Padding = new Thickness(5),
				VerticalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Horizontal,
				Children = {
					displayedValue,
				},
				InputTransparent = true
			};
			grid.Children.Add(stack, 0, 0);

			this.GestureRecognizers.Add(tap);
			Content = grid;
		}

		#region -- Public properties --

		public static readonly BindableProperty SelectedIndexProperty =
					   BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(View), default(int));

		public int SelectedIndex
		{
			get { return (int)GetValue(SelectedIndexProperty); }
			set { SetValue(SelectedIndexProperty, value); }
		}

		public static readonly BindableProperty CommandProperty =
			BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ButtonForPicker), default(ICommand));

		public ICommand Command
		{
			get
			{
				return (ICommand)GetValue(CommandProperty);
			}
			set
			{
				SetValue(CommandProperty, value);
			}
		}

		public static readonly BindableProperty TitleProperty =
			BindableProperty.Create(nameof(Title), typeof(string), typeof(ButtonForPicker), default(string));

		public string Title
		{
			get
			{
				return (string)GetValue(TitleProperty);
			}
			set
			{
				SetValue(TitleProperty, value);
			}
		}

		public static readonly BindableProperty DisplayedValueProperty =
			BindableProperty.Create(nameof(DisplayedValue), typeof(string), typeof(ButtonForPicker), default(string), BindingMode.TwoWay);

		public string DisplayedValue
		{
			get
			{
				return (string)GetValue(DisplayedValueProperty);
			}
			set
			{
				SetValue(DisplayedValueProperty, value);
			}
		}

		public static readonly BindableProperty EnablePickerProperty =
			BindableProperty.Create(nameof(EnablePicker), typeof(bool), typeof(ButtonForPicker), false);

		public bool EnablePicker
		{
			get
			{
				return (bool)GetValue(EnablePickerProperty);
			}
			set
			{
				SetValue(EnablePickerProperty, value);
			}
		}

		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<string>), typeof(ButtonForPicker), default(IEnumerable<string>));

		public IEnumerable<string> ItemsSource
		{
			get
			{
				return (IEnumerable<string>)GetValue(ItemsSourceProperty);
			}
			set
			{
				SetValue(ItemsSourceProperty, value);
			}
		}

		#endregion

		#region -- Overrides -- 

		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			switch (propertyName)
			{
				case nameof(ItemsSource):
					UpdateItems();
					break;
				case nameof(DisplayedValue):
					UpdatePickerFromSourceValue();
					break;
				case nameof(SelectedIndex):
					UpdateSourceFromPicker();
					break;
				case nameof(EnablePicker):
					{
						var grid = Content as Grid;
						grid.Children.Remove(_picker);
						if (EnablePicker)
						{
							grid.Children.Add(_picker, 0, 0);
							UpdatePickerFromSourceValue();
						}
					}
					break;
				default:
					break;
			}
		}

		#endregion

		#region -- Private helpers --

		private void UpdateSourceFromPicker()
		{
			if (IsPickerSynchronized())
				return;
			if (SelectedIndex >= 0 && ItemsSource != null && ItemsSource.Count() > SelectedIndex)
			{
				DisplayedValue = ItemsSource.ElementAt(SelectedIndex);
			}
			else {
				DisplayedValue = string.Empty;
			}
		}

		private void UpdateItems()
		{
			_picker.Items.Clear();
			if (ItemsSource != null)
				foreach (var item in ItemsSource)
				{
					_picker.Items.Add(item);
				}
		}

		private void UpdatePickerFromSourceValue()
		{
			if (!IsPickerSynchronized())
				SelectedIndex = GetIndexInSource(DisplayedValue);
		}

		private bool IsPickerSynchronized()
		{
			return SelectedIndex == GetIndexInSource(DisplayedValue);
		}

		private int GetIndexInSource(string val)
		{
			var index = -1;
			if (ItemsSource != null)
			{
				for (int i = 0; i < ItemsSource.Count(); i++)
				{
					if (ItemsSource.ElementAtOrDefault(i) == DisplayedValue)
						index = i;
				}
			}
			return index;
		}

		#endregion
	}
}
