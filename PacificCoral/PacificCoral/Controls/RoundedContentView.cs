using System;
using Xamarin.Forms;

namespace PacificCoral
{
	public class RoundedContentView : ContentView
	{
		public static readonly BindableProperty CornersProperty =
			BindableProperty.Create(nameof(Corners), typeof(double), typeof(RoundedContentView), default(double));

		public double Corners
		{
			get { return (double)GetValue(CornersProperty); }
			set { SetValue(CornersProperty, value); }
		}
	}
}
