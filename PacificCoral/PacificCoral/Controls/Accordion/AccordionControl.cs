using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace PacificCoral.Controls
{
	/// <summary>
	/// Area devices list page.
	/// </summary>
	public class AccordionControl : Grid
	{
		readonly List<AccordionEntry> m_entries = new List<AccordionEntry>();
		private ScrollView m_scrollView;
		private StackLayout m_cellStackLayout;
		private Image m_shadowImage;

		#region -- Public properties --

		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList<AccordionModel>), typeof(AccordionControl), default(IList<AccordionModel>), BindingMode.TwoWay, null, ItemsSourceChanged);
		public IList<AccordionModel> ItemsSource
		{
			get { return (IList<AccordionModel>)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		/// <summary>
		/// Gets or sets the default color of the button background.
		/// </summary>
		/// <value>The default color of the button background.</value>
		public Color DefaultButtonBackgroundColor { get; set; } = StyleManager.GetAppResource<Color>("MainBgColor");

		/// <summary>
		/// Gets or sets the default color of the button text.
		/// </summary>
		/// <value>The default color of the button text.</value>
		public Color DefaultButtonTextColor { get; set; }

		/// <summary>
		/// Gets or sets the duration of the animation.
		/// </summary>
		/// <value>The duration of the animation.</value>
		public uint AnimationDuration { get; set; }

		/// <summary>
		/// Occurs when cell touched.
		/// </summary>
		public event Action<int> CellTouched;

		#endregion

		//public AccordionControl()
		public AccordionControl(View header)
		{
			AnimationDuration = 400;
			BackgroundColor = DefaultButtonBackgroundColor;
			DefaultButtonTextColor =  Color.White;
			Padding = new Thickness(0, 0, 0, 0);

			RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

			RowSpacing = 0;
			ColumnSpacing = 0;

			CreateStacklayout();

			m_scrollView = new ScrollView()
			{
				BackgroundColor = DefaultButtonBackgroundColor,
				Content = m_cellStackLayout,
				Orientation = ScrollOrientation.Vertical,
				VerticalOptions = LayoutOptions.FillAndExpand,
			};

			Children.Add(m_scrollView, 0, 1);

			if (header != null)
			{
				Children.Add(header, 0, 0);
			}
		}

		#region -- Private helpers --

		private void CreateStacklayout()
		{
			m_cellStackLayout = new StackLayout()
			{
				BackgroundColor = DefaultButtonBackgroundColor,
				Orientation = StackOrientation.Vertical,
				Spacing = 0
			};
		}

		/// <summary>
		/// Add the specified cell and view.
		private void BuildContent()
		{
			if (ItemsSource != null)
			{
				foreach( var item in ItemsSource)
				{
					var entry = new AccordionEntry()
					{
						Cell = item.CellAccordion,
						View = new ScrollView()
						{
							Content = item.ViewAccordion
						},
						OriginalSize = new Size(item.ViewAccordion.WidthRequest, item.ViewAccordion.HeightRequest)
					};

					m_entries.Add(entry);

					m_cellStackLayout.Children.Add(entry.Cell);
					m_cellStackLayout.Children.Add(entry.View);

					var cellIndex = m_entries.Count - 1;

					var tapGestureRecognizer = new TapGestureRecognizer();

					if (CellTouched == null)
						tapGestureRecognizer.Tapped += (object sender, EventArgs e) => OnCellTouchUpInside(cellIndex);
					else
						tapGestureRecognizer.Tapped += (object sender, EventArgs e) => CellTouched(cellIndex);

					item.CellAccordion.GestureRecognizers.Add(tapGestureRecognizer);
				};
				CloseAllEntries();
			}
		}

		private void Clear()
		{
			m_entries.Clear();
			m_cellStackLayout.Children.Clear();
		}

		/// <summary>
		/// Closes all entries.
		/// </summary>
		private void CloseAllEntries()
		{
			foreach (var entry in m_entries)
			{
				entry.View.HeightRequest = 0;
				entry.IsOpen = false;
				entry.View.IsVisible = false;
			}
		}

		private void OpenAccordion(AccordionEntry entry)
		{
			// Get the element (cell) touched
			var element = m_cellStackLayout.Children.FirstOrDefault(x => x == entry.Cell);
			if (element == null)
				return;
			double position = 0;
			entry.View.Animate("expand",
				x =>
				{
					entry.View.IsVisible = true;
					entry.View.HeightRequest = entry.OriginalSize.Height * x;

					// calculate te position to be scrolled based on the X from Animate and element Y position
					position = element.Y * x;


				}, 16, AnimationDuration, Easing.SpringOut, (d, b) =>
				{
					m_scrollView.ScrollToAsync(0, position, true);
					entry.View.IsVisible = true;
				});
		}

		private void CloseAccordion(AccordionEntry entry)
		{
			entry.View.Animate("colapse",
				x =>
				{
					var change = entry.OriginalSize.Height * x;
					entry.View.HeightRequest = entry.OriginalSize.Height - change;
				}, 0, AnimationDuration, Easing.SpringIn, (d, b) =>
				{
					entry.View.IsVisible = false;
				});
		}

		/// <summary>
		/// Raises the cell touch up inside event.
		/// </summary>
		/// <param name="cellIndex">cell index.</param>
		private void OnCellTouchUpInside(int cellIndex)
		{
			var touchedEntry = m_entries[cellIndex];
			bool isTouchingToClose = touchedEntry.IsOpen;

			var entriesToClose = m_entries.Where(e => e.IsOpen);
			foreach (var entry in entriesToClose)
			{
				CloseAccordion(entry);
				entry.IsOpen = false;
			}

			if (!isTouchingToClose)
			{
				OpenAccordion(touchedEntry);
				touchedEntry.IsOpen = true;
			}
		}

		private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (AccordionControl)bindable;
			control.BuildContent();
		}

		#endregion
	}
}
