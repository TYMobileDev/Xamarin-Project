using System;
using Prism.Navigation;

namespace PacificCoral
{
	public class VisitViewModel : BaseDetailPageViewModel<VisitModel>
	{
		public VisitViewModel(INavigationService navigationService) : base(navigationService)
		{
			//TODO: fix that
			Title = "Visit";
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

		private DateTime _DateTimeFull;

		public DateTime DateTimeFull
		{
			get { return _DateTimeFull; }
			set { SetProperty(ref _DateTimeFull, value); }
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

		private bool _Cutting;

		public bool Cutting
		{
			get { return _Cutting; }
			set { SetProperty(ref _Cutting, value); }
		}

		private bool _IsCustomTitle;

		public bool IsCustomTitle
		{
			get { return _IsCustomTitle; }
			set { SetProperty(ref _IsCustomTitle, value); }
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