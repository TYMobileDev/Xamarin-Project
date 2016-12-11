using System;

namespace PacificCoral
{
	public class TrackItemModel
	{
		#region -- Public properties --

		public ETrackItemType Type { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public TimeSpan Duration { get; set; }
		public double Lenght { get; set; } 

		#endregion
	}
}
