namespace PacificCoral.Controls
{
	using System;
	using System.Collections;
	using System.Collections.Specialized;
	using System.Reflection;
	using System.Windows.Input;
	using Xamarin.Forms;

	public class BindablePicker_1 : ContentView
	{
		Boolean _disableNestedCalls;

		private ExtendedPicker _picker;

		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(BindablePicker_1),
				null, propertyChanged: OnItemsSourceChanged);

		public static readonly BindableProperty SelectedItemProperty =
			BindableProperty.Create("SelectedItem", typeof(Object), typeof(BindablePicker_1),
				null, BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);

		public static readonly BindableProperty SelectedValueProperty =
			BindableProperty.Create("SelectedValue", typeof(Object), typeof(BindablePicker_1),
				null, BindingMode.TwoWay, propertyChanged: OnSelectedValueChanged);

		public String DisplayMemberPath { get; set; }

		public IEnumerable ItemsSource
		{
			get { return (IEnumerable)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		public Object SelectedItem
		{
			get { return GetValue(SelectedItemProperty); }
			set
			{
				if (this.SelectedItem != value)
				{
					SetValue(SelectedItemProperty, value);
					InternalSelectedItemChanged();
				}
			}
		}

		public Object SelectedValue
		{
			get { return GetValue(SelectedValueProperty); }
			set
			{
				SetValue(SelectedValueProperty, value);
				InternalSelectedValueChanged();
			}
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

		public String SelectedValuePath { get; set; }

		public BindablePicker_1()
		{
			BackgroundColor = Color.Transparent;
			var tap = new TapGestureRecognizer();
			tap.BindingContext = this;
			tap.SetBinding(TapGestureRecognizer.CommandProperty, CommandProperty.PropertyName);

			var displayedValue = new Label
			{
				BindingContext = this,
				//TextColor = StyleManager.GetAppResource<Color>("cloudyBlue"),
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				//FontSize = StyleManager.GetAppResource<Double>("FontSize_15"),
				InputTransparent = true
			};
			displayedValue.SetBinding(Label.TextProperty, SelectedValueProperty.PropertyName);

			var picker = new ExtendedPicker
			{
				BindingContext = this,
				//TextColor = StyleManager.GetAppResource<Color>("darktwo"),
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Opacity = Device.OnPlatform(0.1, 0, 0),
				BackgroundColor = Color.Transparent,
			};
			picker.SetBinding(Picker.SelectedIndexProperty, SelectedItemProperty.PropertyName, BindingMode.TwoWay);
			//picker.

			_picker = picker;
			_picker.SelectedIndexChanged += OnSelectedIndexChanged;

			var grid = new Grid();

			var stack = new StackLayout
			{
				BackgroundColor = StyleManager.GetAppResource<Color>("DefaultMainColor"),
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

		public event EventHandler<SelectedItemChangedEventArgs> ItemSelected;

		private void InstanceOnItemsSourceChanged(Object oldValue, Object newValue)
		{
			_disableNestedCalls = true;
			_picker.Items.Clear();

			var oldCollectionINotifyCollectionChanged = oldValue as INotifyCollectionChanged;
			if (oldCollectionINotifyCollectionChanged != null)
			{
				oldCollectionINotifyCollectionChanged.CollectionChanged -= ItemsSource_CollectionChanged;
			}

			var newCollectionINotifyCollectionChanged = newValue as INotifyCollectionChanged;
			if (newCollectionINotifyCollectionChanged != null)
			{
				newCollectionINotifyCollectionChanged.CollectionChanged += ItemsSource_CollectionChanged;
			}

			if (!Equals(newValue, null))
			{
				var hasDisplayMemberPath = !String.IsNullOrWhiteSpace(this.DisplayMemberPath);

				foreach (var item in (IEnumerable)newValue)
				{
					if (hasDisplayMemberPath)
					{
						var type = item.GetType();
						var prop = type.GetRuntimeProperty(this.DisplayMemberPath);
						_picker.Items.Add(prop.GetValue(item).ToString());
					}
					else
					{
						_picker.Items.Add(item.ToString());
					}
				}

				_picker.SelectedIndex = -1;
				this._disableNestedCalls = false;

				if (this.SelectedItem != null)
				{
					this.InternalSelectedItemChanged();
				}
				else if (hasDisplayMemberPath && this.SelectedValue != null)
				{
					this.InternalSelectedValueChanged();
				}
			}
			else
			{
				_disableNestedCalls = true;
				_picker.SelectedIndex = -1;
				this.SelectedItem = null;
				this.SelectedValue = null;
				_disableNestedCalls = false;
			}
		}

		void InternalSelectedItemChanged()
		{
			if (_disableNestedCalls)
			{
				return;
			}

			var selectedIndex = -1;
			Object selectedValue = null;
			if (this.ItemsSource != null)
			{
				var index = 0;
				var hasSelectedValuePath = !String.IsNullOrWhiteSpace(this.SelectedValuePath);
				foreach (var item in this.ItemsSource)
				{
					if (item != null && item.Equals(this.SelectedItem))
					{
						selectedIndex = index;
						if (hasSelectedValuePath)
						{
							var type = item.GetType();
							var prop = type.GetRuntimeProperty(this.SelectedValuePath);
							selectedValue = prop.GetValue(item);
						}
						break;
					}
					index++;
				}
			}
			_disableNestedCalls = true;
			this.SelectedValue = selectedValue;
			_picker.SelectedIndex = selectedIndex;
			_disableNestedCalls = false;
		}

		void InternalSelectedValueChanged()
		{
			if (_disableNestedCalls)
			{
				return;
			}

			if (String.IsNullOrWhiteSpace(this.SelectedValuePath))
			{
				return;
			}
			var selectedIndex = -1;
			Object selectedItem = null;
			var hasSelectedValuePath = !String.IsNullOrWhiteSpace(this.SelectedValuePath);
			if (this.ItemsSource != null && hasSelectedValuePath)
			{
				var index = 0;
				foreach (var item in this.ItemsSource)
				{
					if (item != null)
					{
						var type = item.GetType();
						var prop = type.GetRuntimeProperty(this.SelectedValuePath);
						if (Object.Equals(prop.GetValue(item), this.SelectedValue))
						{
							selectedIndex = index;
							selectedItem = item;
							break;
						}
					}

					index++;
				}
			}
			_disableNestedCalls = true;
			this.SelectedItem = selectedItem;
			_picker.SelectedIndex = selectedIndex;
			_disableNestedCalls = false;
		}

		void ItemsSource_CollectionChanged(Object sender, NotifyCollectionChangedEventArgs e)
		{
			var hasDisplayMemberPath = !String.IsNullOrWhiteSpace(this.DisplayMemberPath);
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				foreach (var item in e.NewItems)
				{
					if (hasDisplayMemberPath)
					{
						var type = item.GetType();
						var prop = type.GetRuntimeProperty(this.DisplayMemberPath);
						_picker.Items.Add(prop.GetValue(item).ToString());
					}
					else
					{
						_picker.Items.Add(item.ToString());
					}
				}
			}
			else if (e.Action == NotifyCollectionChangedAction.Remove)
			{
				foreach (var item in e.NewItems)
				{
					if (hasDisplayMemberPath)
					{
						var type = item.GetType();
						var prop = type.GetRuntimeProperty(this.DisplayMemberPath);
						_picker.Items.Remove(prop.GetValue(item).ToString());
					}
					else
					{
						_picker.Items.Remove(item.ToString());
					}
				}
			}
			else if (e.Action == NotifyCollectionChangedAction.Replace)
			{
				foreach (var item in e.NewItems)
				{
					if (hasDisplayMemberPath)
					{
						var type = item.GetType();
						var prop = type.GetRuntimeProperty(this.DisplayMemberPath);
						_picker.Items.Remove(prop.GetValue(item).ToString());
					}
					else
					{
						var index = _picker.Items.IndexOf(item.ToString());
						if (index > -1)
						{
							_picker.Items[index] = item.ToString();
						}
					}
				}
			}
			else if (e.Action == NotifyCollectionChangedAction.Reset)
			{
				_picker.Items.Clear();
				if (e.NewItems != null)
				{
					foreach (var item in e.NewItems)
					{
						if (hasDisplayMemberPath)
						{
							var type = item.GetType();
							var prop = type.GetRuntimeProperty(this.DisplayMemberPath);
							_picker.Items.Remove(prop.GetValue(item).ToString());
						}
						else
						{
							var index = _picker.Items.IndexOf(item.ToString());
							if (index > -1)
							{
								_picker.Items[index] = item.ToString();
							}
						}
					}
				}
				else
				{
					_disableNestedCalls = true;
					this.SelectedItem = null;
					_picker.SelectedIndex = -1;
					this.SelectedValue = null;
					_disableNestedCalls = false;
				}
			}
		}

		static void OnItemsSourceChanged(BindableObject bindable, Object oldValue, Object newValue)
		{
			if (Equals(newValue, null) && Equals(oldValue, null))
			{
				return;
			}

			var picker = (BindablePicker_1)bindable;
			picker.InstanceOnItemsSourceChanged(oldValue, newValue);
		}

		void OnSelectedIndexChanged(Object sender, EventArgs e)
		{
			if (_disableNestedCalls)
			{
				return;
			}

			if (_picker.SelectedIndex < 0 || this.ItemsSource == null || !this.ItemsSource.GetEnumerator().MoveNext())
			{
				_disableNestedCalls = true;
				if (_picker.SelectedIndex != -1)
				{
					_picker.SelectedIndex = -1;
				}
				this.SelectedItem = null;
				this.SelectedValue = null;
				_disableNestedCalls = false;
				return;
			}

			_disableNestedCalls = true;

			var index = 0;
			var hasSelectedValuePath = !String.IsNullOrWhiteSpace(this.SelectedValuePath);
			foreach (var item in this.ItemsSource)
			{
				if (index == _picker.SelectedIndex)
				{
					this.SelectedItem = item;
					if (hasSelectedValuePath)
					{
						var type = item.GetType();
						var prop = type.GetRuntimeProperty(this.SelectedValuePath);
						this.SelectedValue = prop.GetValue(item);
					}

					break;
				}
				index++;
			}

			_disableNestedCalls = false;
		}

		static void OnSelectedItemChanged(BindableObject bindable, Object oldValue, Object newValue)
		{
			var boundPicker = (BindablePicker_1)bindable;
			boundPicker.ItemSelected?.Invoke(boundPicker, new SelectedItemChangedEventArgs(newValue));
			boundPicker.InternalSelectedItemChanged();
		}

		static void OnSelectedValueChanged(BindableObject bindable, Object oldValue, Object newValue)
		{
			var boundPicker = (BindablePicker_1)bindable;
			boundPicker.InternalSelectedValueChanged();
		}

	}
}
