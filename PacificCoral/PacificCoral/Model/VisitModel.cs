using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PacificCoral
{
	public class VisitModel : INotifyPropertyChanged
	{
		#region -- Public properties --

		public string Title { get; set; }

		public string Description { get; set; }

		public string TimeStr { get; set; }

		public string DateStr { get; set; }

		public DateTime Date { get; set; }

		#endregion

		public event PropertyChangedEventHandler PropertyChanged;

		//[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
