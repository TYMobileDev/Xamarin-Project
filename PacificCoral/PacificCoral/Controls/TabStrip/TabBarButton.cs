using System;
using NControl.Abstractions;
using Xamarin.Forms;

namespace PacificCoral
{
	public class TabBarButton : NControlView
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DesaCo.TabBarButton"/> class.
		/// </summary>
		public TabBarButton()
		{
			Padding = new Thickness(5);
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is selected.
		/// </summary>
		/// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
		public bool IsSelected
		{
			get
			{
				return Content.Opacity == 1;
			}
			set
			{
				if (value)
					Content.Opacity = 1;
				else
					Content.Opacity = 0.5;
			}
		}

		/// <summary>
		/// Toucheses the began.
		/// </summary>
		/// <param name="points">Points.</param>
		public override bool TouchesBegan(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
		{
			base.TouchesBegan(points);
			//TODO: fix it
			IsSelected = true;

			return false;
		}
	}
}
