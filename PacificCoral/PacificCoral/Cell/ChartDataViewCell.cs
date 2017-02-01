using System;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
namespace PacificCoral
{
	public class ChartDataViewCell : ViewCell
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
			//TODO: lineSeries.SetBinding(ChartSeries.ItemsSourceProperty, "OpcoSalesChartItems");
			lineSeries.XBindingPath = "Period";
			lineSeries.YBindingPath = "LBS";
			lineSeries.Color = Color.FromHex("#bababa");
			chart.Series.Add(lineSeries);

			//Initializing area series
			AreaSeries areaSeries = new AreaSeries();
			//TODO: lineSeries.SetBinding(ChartSeries.ItemsSourceProperty, "OpcoSalesChartItems");
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

			//Content = chart;

			View = chart;
		}
	}
}
