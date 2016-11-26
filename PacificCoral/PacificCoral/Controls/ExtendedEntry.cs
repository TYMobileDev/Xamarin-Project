using System;
using Xamarin.Forms;

namespace PacificCoral
{
	public class ExtendedEntry : Entry
	{
		#region -- Public properties --

		public static readonly BindableProperty BorderWidthProperty =
			BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(ExtendedEntry), default(double));

		public double BorderWidth
		{
			get { return (double)GetValue(BorderWidthProperty); }
			set { SetValue(BorderWidthProperty, value); }
		}

		#endregion
	}
}
