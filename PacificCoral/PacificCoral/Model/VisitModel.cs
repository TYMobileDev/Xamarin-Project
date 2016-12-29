using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PacificCoral
{
	public class VisitModel
	{
		#region -- Public properties --

		public string Title { get; set; }

		public string Description { get; set; }

		public string TimeStr { get; set; }

		public string DateStr { get; set; }

		public DateTime Date { get; set; }

		#endregion
	}
}
