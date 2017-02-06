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
		SfChart _chart;

		#region -- Public properties --

		//public static readonly BindableProperty OpcoSalesChartItemsProperty =
		//	BindableProperty.Create(nameof(OpcoSalesChartItems), typeof(IList<OpcoSalesSummaries>), typeof(LostSalesPCSViewCell), default(IList<OpcoSalesSummaries>),BindingMode.TwoWay); //, propertyChanged: OnOpcoSalesChartItemsPropertyChange
		//public IList<OpcoSalesSummaries> OpcoSalesChartItems
		//{
		//	get
		//	{
		//		return (IList<OpcoSalesSummaries>)GetValue(OpcoSalesChartItemsProperty);
		//	}
		//	set
		//	{
		//		SetValue(OpcoSalesChartItemsProperty, value);
		//	}
		//}

		#endregion

		public ChartDataView()
		{
			RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

			HeightRequest = 250;

			BackgroundColor = StyleManager.GetAppResource<Color>("DefaultLightColor");
			Margin = new Thickness(5, 10, 5, 10);

			_chart = new SfChart()
			{
				Margin = new Thickness(5),
			};
			//Initializing Primary Axis   
			CategoryAxis primaryAxis = new CategoryAxis();
			primaryAxis.Title = new ChartAxisTitle() { Text = "Running 30 Day Period" };
			primaryAxis.MaximumLabels = 12;
			_chart.PrimaryAxis = primaryAxis;

			//Initializing Secondary Axis
			NumericalAxis secondaryAxis = new NumericalAxis();
			secondaryAxis.Title = new ChartAxisTitle() { Text = "LBS" };
			secondaryAxis.RangePadding = NumericalPadding.Additional;
			_chart.SecondaryAxis = secondaryAxis;

			//Initializing line series
			LineSeries lineSeries = new LineSeries();
			lineSeries.SetBinding(ChartSeries.ItemsSourceProperty, new Binding("OpcoSalesChartItems", BindingMode.TwoWay));
			//lineSeries.SetBinding(LineSeries.ItemsSourceProperty, OpcoSalesChartItemsProperty.PropertyName);
			lineSeries.XBindingPath = "Period";
			lineSeries.YBindingPath = "LBS";
			lineSeries.Color = Color.FromHex("#bababa");
			_chart.Series.Add(lineSeries);

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
			_chart.Series.Add(areaSeries);
				
			//this.Content = chart;

			//View = chart;

			var label = new Label()
			{
				TextColor = StyleManager.GetAppResource<Color>("DefaultDarkColor"),
				FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
				HorizontalOptions = LayoutOptions.End,
				Margin = new Thickness(5),
			};
			label.SetBinding(Label.TextProperty, "Revenue");

			Children.Add(_chart, 0, 2);
			Children.Add(label, 0, 1);
		}

		private void BuildChartDataView()
		{
			

			//var OpcoSalesChartItems = new ObservableCollection<OpcoSalesSummaries>()
			//{
			//		new OpcoSalesSummaries()
			//		{
			//			LBS = 200,
			//			Period = 2,
			//		},
			//		new OpcoSalesSummaries()
			//		{
			//			LBS = 400,
			//			Period = 4,
			//		},
			//		new OpcoSalesSummaries()
			//		{
			//			LBS = 600,
			//			Period = 6,
			//		},
			//		new OpcoSalesSummaries()
			//		{
			//			LBS = 800,
			//			Period = 8,
			//		},
			//};
		}

		#region -- Private helpers --

		//private static void OnOpcoSalesChartItemsPropertyChanged(BindableObject bindable, Object oldValue, Object newValue)
		//{
		//	var control = (ChartDataView)bindable;
		//	control.BuildChartDataView();
		//}

		#endregion
	}
}
