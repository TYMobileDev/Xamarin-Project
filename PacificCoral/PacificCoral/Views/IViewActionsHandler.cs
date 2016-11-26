using System;

namespace PacificCoral
{
	public interface IViewActionsHandler
	{
		bool OnBackButtonPressed();
		void OnAppearing();
		void OnDisappearing();
	}
}
