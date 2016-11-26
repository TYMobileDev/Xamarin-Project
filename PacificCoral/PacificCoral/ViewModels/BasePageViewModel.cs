using System;

namespace PacificCoral
{
	public class BasePageViewModel : BaseViewModel, IViewActionsHandler
	{
		#region -- Public properties --

		private bool _IsBusy;

		public bool IsBusy
		{
			get { return _IsBusy; }
			set { SetProperty(ref _IsBusy, value); }
		}

		#endregion


		#region -- IViewActionsHandler implementation --

		public virtual void OnAppearing()
		{

		}

		public virtual bool OnBackButtonPressed()
		{
			return false;
		}

		public virtual void OnDisappearing()
		{

		}

		#endregion
	}
}
