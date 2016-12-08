using System;
using Xamarin.Forms;
using System.Windows.Input;
namespace PacificCoral
{
	public class ExtendedListView : ListView
	{
		public ExtendedListView()
		{
			this.ItemTapped += (sender, e) => {

				if (TappedCommand != null)
				{
					TappedCommand?.Execute(e.Item);
					SelectedItem = null;
				}
			};
		}

		#region -- Public properties --

		public static readonly BindableProperty TappedCommandProperty =
			BindableProperty.Create(nameof(TappedCommand), typeof(ICommand), typeof(ExtendedListView), default(ICommand));

		public ICommand TappedCommand
		{
			get { return (ICommand)GetValue(TappedCommandProperty); }
			set { SetValue(TappedCommandProperty, value); }
		}

		#endregion
	}
}
