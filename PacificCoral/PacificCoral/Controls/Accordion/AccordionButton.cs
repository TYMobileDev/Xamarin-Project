using System;
using Xamarin.Forms;

namespace PacificCoral.Controls
{
	public class AccordionButton : Button
	{
		private bool mExpand = false;

		public AccordionButton()
		{
			HorizontalOptions = LayoutOptions.FillAndExpand;
			BorderColor = Color.Black;
			BorderRadius = 5;
			BorderWidth = 0;
		}

		#region -- Public properties --

		public bool Expand
		{
			get { return mExpand; }
			set { mExpand = value; }
		}
		public ContentView AssosiatedContent { get; set; }

		#endregion

	}
}
