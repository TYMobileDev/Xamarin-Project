using System;
using Xamarin.Forms;

namespace PacificCoral
{
	public class AccordionCell : StackLayout
	{
		public AccordionCell()
		{
			Orientation = StackOrientation.Horizontal;
			BackgroundColor = Color.White;
			Margin = new Thickness(0, 0, 0, 10);
			HeightRequest = 50;

			var nameLabel = new Label()
			{
				HorizontalOptions = LayoutOptions.Start,
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				VerticalOptions = LayoutOptions.Center,
				TextColor = Color.Yellow,
				//Text = Name,
			};
			//nameLabel.SetBinding(Label.TextProperty, NameCellProperty.PropertyName);

			nameLabel.Text = Name;

			Children.Add(nameLabel);
		}

		#region -- Public properties --

		public string Name { get; set; }

		public static readonly BindableProperty NameCellProperty =
			BindableProperty.Create(nameof(NameCell), typeof(string), typeof(AccordionCell), default(string));
		public string NameCell
		{
			get
			{
				return (string)GetValue(NameCellProperty);
			}
			set
			{
				SetValue(NameCellProperty, value);
			}
		}

		#endregion
	}
}
