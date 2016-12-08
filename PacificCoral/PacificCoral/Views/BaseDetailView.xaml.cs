using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PacificCoral
{
	public partial class BaseDetailView : BaseContentPage
	{
		public BaseDetailView()
		{
			InitializeComponent();
			UpdateViews();
		}

		#region -- Public properties --

		public View ViewView
		{
			set
			{
				_viewViewContainer.Content = value;
			}
		}

		public View EditView
		{
			set {
				_editViewContainer.Content = value;
			}
		}

		public static readonly BindableProperty ModeProperty =
			BindableProperty.Create(nameof(Mode), typeof(DetailsMode), typeof(BaseDetailView), default(DetailsMode));

		public DetailsMode Mode
		{
			get { return (DetailsMode)GetValue(ModeProperty); }
			set { SetValue(ModeProperty, value); }
		}

		#endregion

		#region -- Overrides --

		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName == nameof(Mode))
				UpdateViews();
		}

		#endregion

		#region -- Private helpers --

		private void UpdateViews()
		{
			if (Mode == DetailsMode.View)
				_contentContainer.RaiseChild(_viewViewContainer);
			else
				_contentContainer.RaiseChild(_editViewContainer);
		}

		#endregion
	}
}
