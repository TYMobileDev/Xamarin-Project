﻿using System;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
namespace PacificCoral
{
	public class ChartDataViewCell : StackLayout
	{
		public ChartDataViewCell()
		{
			SfChart chart = new SfChart();

			//Initializing Primary Axis   
			CategoryAxis primaryAxis = new CategoryAxis();
			primaryAxis.Title = new ChartAxisTitle(){ Text = "Running 30 Day Period"};
			primaryAxis.MaximumLabels = 12;
			chart.PrimaryAxis = primaryAxis;

			//Initializing Secondary Axis
			NumericalAxis secondaryAxis = new NumericalAxis();
			secondaryAxis.Title = new ChartAxisTitle(){ Text = "LBS" };
			secondaryAxis.RangePadding = NumericalPadding.Additional;
			chart.SecondaryAxis = secondaryAxis;

			//Initializing line series
			LineSeries lineSeries = new LineSeries();
			lineSeries.SetBinding(ChartSeries.ItemsSourceProperty, "OpcoSalesChartItems[0]");
			lineSeries.XBindingPath = "Period";
			lineSeries.YBindingPath = "LBS";
			lineSeries.Color = Color.FromHex("#bababa");
			chart.Series.Add(lineSeries);

			//Initializing area series
			AreaSeries areaSeries = new AreaSeries();
			lineSeries.SetBinding(ChartSeries.ItemsSourceProperty, "OpcoSalesChartItems[0]");
			areaSeries.XBindingPath = "Period";
			areaSeries.YBindingPath = "LBS";
			areaSeries.Color = Color.FromHex("#ebebeb");

			areaSeries.DataMarker = new ChartDataMarker()
			{
				MarkerColor = Color.Black,
				MarkerHeight = 10,
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

			//this.Content = chart;

			//View = chart;

			var label = new Label()
			{
				Text = "dfdfdfdfdfdfd",
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),

			};

			Children.Add(chart);
			Children.Add(label);
		}
	}
}
