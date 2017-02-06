﻿using System;
using Xamarin.Forms;

namespace PacificCoral.Control
{
	public class DeviationsView : Grid
	{
		public DeviationsView()
		{
			Margin = new Thickness(5);
			BackgroundColor = Color.White;
			ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
			ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
			RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

			var deviationsLabel = new Label()
			{
				Text = "Deviations",
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				TextColor = Color.FromHex("#8B572A"),
				FontAttributes = FontAttributes.Bold,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
			};

			var pendingAprovalLabel = new Label()
			{
				Text = "Pending Aproval",
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				HorizontalOptions = LayoutOptions.EndAndExpand,
			};

			var pendingAprovalValueLabel = new Label()
			{
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				HorizontalOptions = LayoutOptions.StartAndExpand,
				FontAttributes = FontAttributes.Bold,
			};
			pendingAprovalValueLabel.SetBinding(Label.TextProperty, "DeviationSummaryItems[0].Submitted");

			var activeLabel = new Label()
			{
				Text = "Active",
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				HorizontalOptions = LayoutOptions.EndAndExpand,
			};

			var activeValueLabel = new Label()
			{
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				HorizontalOptions = LayoutOptions.StartAndExpand,
				FontAttributes = FontAttributes.Bold,
			};
			activeValueLabel.SetBinding(Label.TextProperty, "DeviationSummaryItems[0].Active");

			var expiringLabel = new Label()
			{
				Text = "Expiring",
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				HorizontalOptions = LayoutOptions.EndAndExpand,
			};

			var expiringValueLabel = new Label()
			{
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				HorizontalOptions = LayoutOptions.StartAndExpand,
				FontAttributes = FontAttributes.Bold,
			};
			expiringValueLabel.SetBinding(Label.TextProperty, "DeviationSummaryItems[0].Expiring");

			var expiredLabel = new Label()
			{
				Text = "Expired",
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				HorizontalOptions = LayoutOptions.EndAndExpand,
			};

			var expiredValueLabel = new Label()
			{
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				HorizontalOptions = LayoutOptions.StartAndExpand,
				FontAttributes = FontAttributes.Bold,
			};
			expiredValueLabel.SetBinding(Label.TextProperty, new Binding("DeviationSummaryItems[0].Expired"));

			Children.Add(deviationsLabel, 0, 2, 0, 1);
			Children.Add(pendingAprovalLabel, 0, 1, 1, 2);
			Children.Add(pendingAprovalValueLabel, 1, 2, 1, 2);
			Children.Add(activeLabel, 0, 1, 2, 3);
			Children.Add(activeValueLabel, 1, 2, 2, 3);
			Children.Add(expiringLabel, 0, 1, 3, 4);
			Children.Add(expiringValueLabel, 1, 2, 3, 4);
			Children.Add(expiredLabel, 0, 1, 4, 5);
			Children.Add(expiredValueLabel, 1, 2, 4, 5);
		}
	}
}