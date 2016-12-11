using System;
using NControl.Abstractions;
using NControl.Controls;
using Xamarin.Forms;

namespace PacificCoral
{
	public class Checkbox : RoundCornerView
	{
		private Image _checked;

		public Checkbox()
		{
			_checked = new Image
			{
				Source = "checked",
				//HeightRequest = 15,
				//WidthRequest = 15,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				InputTransparent = true,
				Opacity = 0
			};
			Padding = new Thickness(3);
			this.BorderColor = Color.FromHex("#979797");
			this.BorderWidth = 1;
			this.CornerRadius = 4;
			this.IsClippedToBounds = true;
			HeightRequest = 15;
			MinimumHeightRequest = 15;
			WidthRequest = 15;
			MinimumWidthRequest = 15;
			VerticalOptions = LayoutOptions.Center;
			HorizontalOptions = LayoutOptions.Center;

			Content = _checked;
		}

		#region -- Public properties --

		public static readonly BindableProperty IsCheckedProperty =
			BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(Checkbox), default(bool), BindingMode.TwoWay, propertyChanged: OnCheckedChanged);

		public bool IsChecked
		{
			get { return (bool)GetValue(IsCheckedProperty); }
			set { SetValue(IsCheckedProperty, value); }
		}

		#endregion

		#region -- Overrides --

		protected override void LayoutChildren(double x, double y, double width, double height)
		{
			base.LayoutChildren(x, y, width, height);
			if (Device.OS == TargetPlatform.Android)
			{
				_checked.Layout(Bounds);
			}
		}

		public override bool TouchesBegan(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
		{
			return true;
		}

		public override bool TouchesEnded(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
		{
			base.TouchesEnded(points);
			var isTouchEndedInside = false;
			foreach (var item in points)
			{
				if ((this.Bounds.Width >= item.X && item.X >= 0) &&
					(this.Bounds.Height >= item.Y && item.Y >= 0))
					isTouchEndedInside = true;
			}
			if (isTouchEndedInside)
			{
				OnClicked();
			}
			return true;
		}

		public override bool TouchesCancelled(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
		{
			return base.TouchesCancelled(points);
		}

		#endregion

		#region -- Private helpers --

		private static void OnCheckedChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var _this = (Checkbox)bindable;
			_this.UpdateCheckedState();
		}

		private void UpdateCheckedState()
		{
			uint animationTime = 150;
			if (IsChecked)
				_checked.FadeTo(1, animationTime);
			else
				_checked.FadeTo(0, animationTime);
		}

		private void OnClicked()
		{
			IsChecked = !IsChecked;
		}

		#endregion
	}
}
