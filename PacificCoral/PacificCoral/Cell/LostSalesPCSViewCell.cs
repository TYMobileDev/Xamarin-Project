using System;
using Xamarin.Forms;
namespace PacificCoral
{
	public class LostSalesPCSViewCell : ViewCell
	{
		public LostSalesPCSViewCell()
		{
			var mainStack = new StackLayout()
			{

			};

			var grid = new Grid()
			{
				Margin = new Thickness(10),
				RowDefinitions = {
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Auto },
				},
				ColumnDefinitions = {
					new ColumnDefinition { Width = GridLength.Star },
					new ColumnDefinition { Width = GridLength.Star },
					new ColumnDefinition { Width = GridLength.Star },
				},
			};

			var itemCodeLabel = new Label()
			{
				FontAttributes = FontAttributes.Bold,
			};
			itemCodeLabel.SetBinding(Label.TextProperty, "ItemCode");

			var descriptionLabel = new Label()
			{
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
			};
			descriptionLabel.SetBinding(Label.TextProperty, "Description");

			var period1StartDateLabel = new Label()
			{
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
			};
			period1StartDateLabel.SetBinding(Label.TextProperty, new Binding("Period1StartDate",stringFormat:"{0:MMM/yy}"));

			var period2EndDateLabel = new Label()
			{
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
			};
			period2EndDateLabel.SetBinding(Label.TextProperty, new Binding("Period2EndDate", stringFormat: "{0:MMM/yy}"));

			var gainLossLabel = new Label()
			{
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
			};
			gainLossLabel.SetBinding(Label.TextProperty, new Binding("GainLoss"));


			grid.Children.Add(itemCodeLabel, 0, 1, 0, 1);
			grid.Children.Add(descriptionLabel, 1, 2, 0, 1);
			grid.Children.Add(period1StartDateLabel, 0, 1, 1, 2);
			grid.Children.Add(period2EndDateLabel, 1, 2, 1, 2);
			grid.Children.Add(gainLossLabel, 2, 3, 1, 2);

			var separator = new BoxView()
			{
				Style = StyleManager.GetAppResource<Style>("HorizontalSeparatorStyle"),
			};

			mainStack.Children.Add(grid);
			mainStack.Children.Add(separator);

			View = mainStack;
		}
	}
}