using System;
using Xamarin.Forms;

namespace PacificCoral.Controls
{
	public class AccordionButton : Button
	{
		private bool mExpand = false;

		public AccordionButton()
		{
			//HorizontalOptions = LayoutOptions.FillAndExpand;
			//BorderColor = Color.Black;
			//BorderRadius = 5;
			//BorderWidth = 0;

			//var stack = new StackLayout()
			//{
			//	Orientation = StackOrientation.Horizontal,
			//};

			//var nameLabel = new Label()
			//{
			//	HorizontalOptions = LayoutOptions.Start,
			//	FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
			//};
			//nameLabel.SetBinding(Label.TextProperty, NameCellProperty.PropertyName);
			//nameLabel.SetBinding(Label.TextColorProperty, TextColorProperty.PropertyName);

			//stack.Children.Add(nameLabel);

			//Content = stack;
		}

		#region -- Public properties --

		//public static readonly BindableProperty NameCellProperty =
		//	BindableProperty.Create(nameof(NameCell), typeof(string), typeof(AccordionCell), default(string));
		//public string NameCell
		//{
		//	get
		//	{
		//		return (string)GetValue(NameCellProperty);
		//	}
		//	set
		//	{
		//		SetValue(NameCellProperty, value);
		//	}
		//}

		//public static readonly BindableProperty TextColorProperty =
		//	BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(AccordionCell), default(Color));
		//public Color TextColor
		//{
		//	get
		//	{
		//		return (Color)GetValue(TextColorProperty);
		//	}
		//	set
		//	{
		//		SetValue(TextColorProperty, value);
		//	}
		//}

		public bool Expand
		{
			get { return mExpand; }
			set { mExpand = value; }
		}
		public ContentView AssosiatedContent { get; set; }

		#endregion
	}
}
