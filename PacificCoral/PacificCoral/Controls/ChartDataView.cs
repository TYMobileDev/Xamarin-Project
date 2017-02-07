using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PacificCoral.Model;
using Syncfusion.SfChart.XForms;

using Xamarin.Forms;

namespace PacificCoral
{
	public class ChartDataView : Grid
	{
		public ChartDataView()
		{
			RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

			HeightRequest = 250;

			BackgroundColor = StyleManager.GetAppResource<Color>("DefaultLightColor");
			Margin = new Thickness(5, 5, 5, 10);

			SfChart chart = new SfChart()
			{
				Margin = new Thickness(10),
			};

			//Initializing Primary Axis   
			CategoryAxis primaryAxis = new CategoryAxis();
			primaryAxis.Title = new ChartAxisTitle() { Text = "Running 30 Day Period" };
			primaryAxis.MaximumLabels = 12;
			chart.PrimaryAxis = primaryAxis;

			//Initializing Secondary Axis
			NumericalAxis secondaryAxis = new NumericalAxis();
			secondaryAxis.Title = new ChartAxisTitle() { Text = "LBS" };
			secondaryAxis.RangePadding = NumericalPadding.Additional;
			chart.SecondaryAxis = secondaryAxis;

			//Initializing line series
			LineSeries lineSeries = new LineSeries();
			lineSeries.SetBinding(ChartSeries.ItemsSourceProperty, new Binding("OpcoSalesChartItems", BindingMode.TwoWay));
			lineSeries.XBindingPath = "Period";
			lineSeries.YBindingPath = "LBS";
			lineSeries.Color = Color.FromHex("#bababa");
			chart.Series.Add(lineSeries);

			//Initializing area series
			AreaSeries areaSeries = new AreaSeries();
			areaSeries.SetBinding(ChartSeries.ItemsSourceProperty, "OpcoSalesChartItems");
			areaSeries.XBindingPath = "Period";
			areaSeries.YBindingPath = "LBS";
			areaSeries.Color = Color.FromHex("#ebebeb");

			areaSeries.DataMarker = new ChartDataMarker()
			{
				MarkerColor = Color.Black,
				MarkerHeight = 10,
				MarkerWidth = 10,
				LabelContent = LabelContent.YValue,
				ShowLabel = false,
				ShowMarker = true,
				LabelStyle = new DataMarkerLabelStyle()
				{
					TextColor = Color.White,
					BackgroundColor = Color.Black,
					LabelPosition = DataMarkerLabelPosition.Center,
				},
			};
			chart.Series.Add(areaSeries);

			var label = new Label()
			{
				TextColor = StyleManager.GetAppResource<Color>("DefaultDarkColor"),
				FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
				HorizontalOptions = LayoutOptions.End,
				Margin = new Thickness(5),
			};
			label.SetBinding(Label.TextProperty, "Revenue");

			Children.Add(chart, 0, 2);
			Children.Add(label, 0, 1);
		}
	}
}
