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
			set 
			{ 
				SetProperty(ref _Date, value); 
				UpdateDateTime();
			}
		}

		private TimeSpan _Time;

		public TimeSpan Time
		{
			get { return _Time; }
			set
			{
				SetProperty(ref _Time, value);
				UpdateDateTime();
			}
		}

		private DateTime _DateTimeFull;

		public DateTime DateTimeFull
		{
			get { return _DateTimeFull; }
			set { SetProperty(ref _DateTimeFull, value); }
		}

		#endregion

		#region -- Overrides --

		protected override void Init()
		{
			base.Init();
			var dateFromModel = Model.Date;
			Date = dateFromModel.Date;
			Time = dateFromModel.TimeOfDay;
		}

		#endregion

		#region -- Private helpers --

		private void UpdateDateTime()
		{
			DateTimeFull = Date.Date + Time;
			if (Model != null)
			{
				Model.Date = DateTimeFull;
			}
		}

		#endregion
	}
}
