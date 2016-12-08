using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;

namespace PacificCoral
{
	public class TaskViewModel : BaseDetailPageViewModel<TaskModel>
	{
		public TaskViewModel(INavigationService navigationService) : base(navigationService)
		{
			//TODO: fix that
			Title = "Task";
		}

		#region -- Public properties --

		private DateTime _Date;

		public DateTime Date
		{
			get { return _Date; }
			set { SetProperty(ref _Date, value); }
		}

		private TimeSpan _Time;

		public TimeSpan Time
		{
			get { return _Time; }
			set { SetProperty(ref _Time, value); }
		}

		#endregion

		#region -- Overrides --

		#endregion

		#region -- Private helpers --

		#endregion
	}
}
