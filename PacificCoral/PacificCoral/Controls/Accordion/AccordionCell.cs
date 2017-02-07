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
			Margin = new Thickness(5, 5, 5, 5);
			HeightRequest = 50;
			BindingContext = this;

			var titleLabel = new Label()
			{
				HorizontalOptions = LayoutOptions.Start,
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				VerticalOptions = LayoutOptions.Center,
				TextColor = Color.Black,
				Margin = new Thickness(20, 0, 0, 0),
			};
			titleLabel.SetBinding(Label.TextProperty, "TitleCell");
			var icon = new Image()
			{
				Source = "open_cell.png",
				Margin = new Thickness(10),
				HorizontalOptions = LayoutOptions.EndAndExpand,
			};

			Children.Add(titleLabel);
			Children.Add(icon);
		}

		#region -- Public properties --

		public string TitleCell { get; set;}

		#endregion
	}
}
