using System;
using Xamarin.Forms;

namespace PacificCoral.Controls
{
	public class AccordionSource
	{
		public string HeaderText { get; set; }
		public Color HeaderTextColor { get; set; }
		public Color HeaderBackGroundColor { get; set; }
		public View ContentItems { get; set; }
	}
}