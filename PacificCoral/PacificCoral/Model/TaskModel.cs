using System;
namespace PacificCoral
{
	public class TaskModel : ObservableObject
	{
		#region -- Public properties --

		public string Title { get; set; }

		public string Description { get; set; }

		public string TimeStr { get; set; }

		public string DateStr { get; set; }

		private bool _IsDone;

		public bool IsDone
		{
			get { return _IsDone; }
			set { SetProperty(ref _IsDone, value); }
		}

		#endregion
	}
}
