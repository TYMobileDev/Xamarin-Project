using System;
using System.Collections.Generic;
using PacificCoral.Controls;
using Xamarin.Forms;

namespace PacificCoral.Controls
{
	public class Accordion : ContentView
	{
		//private List<AccordionSource> ItemsSource;
		private bool mFirstExpaned = false;
		private StackLayout mMainLayout;

		public Accordion()
		{
			//var mMainLayout = new StackLayout();
			//Content = mMainLayout;
			SetItems();
		}

		//public Accordion(List<AccordionSource> aSource)
		//{
		//	//ItemsSource = aSource;
		//	DataBind();
		//}

		#region -- Public properties --

		//public List<AccordionSource> DataSource
		//{
		//	get { return ItemsSource; }
		//	set { ItemsSource = value; }
		//}
		public bool FirstExpaned
		{
			get { return mFirstExpaned; }
			set { mFirstExpaned = value; }
		}

		public static readonly BindableProperty ItemsSourceProperty =
   BindableProperty.Create(nameof(ItemsSource), typeof(IList<AccordionSource>), typeof(Accordion), default(IList<AccordionSource>),BindingMode.TwoWay, null, ItemsSourceChanged);

		public IList<AccordionSource> ItemsSource
		{
			get { return (IList<AccordionSource>)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		#endregion


		#region -- Private helpers --

		private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (Accordion)bindable;
			control.SetItems();
		}

		private void SetItems()
		{
			var vMainLayout = new StackLayout();
			var vFirst = true;
			if (ItemsSource != null)
			{
				foreach (var vSingleItem in ItemsSource)
				{
					var vHeaderButton = new AccordionButton()
					{
						Text = vSingleItem.HeaderText,
						TextColor = vSingleItem.HeaderTextColor,
						BackgroundColor = vSingleItem.HeaderBackGroundColor
					};

					var vAccordionContent = new ContentView()
					{
						Content = vSingleItem.ContentItems,
						IsVisible = false
					};
					if (vFirst)
					{
						vHeaderButton.Expand = mFirstExpaned;
						vAccordionContent.IsVisible = mFirstExpaned;
						vFirst = false;
					}
					vHeaderButton.AssosiatedContent = vAccordionContent;
					vHeaderButton.Clicked += OnAccordionButtonClicked;
					vMainLayout.Children.Add(vHeaderButton);
					vMainLayout.Children.Add(vAccordionContent);
				}
			}
			mMainLayout = vMainLayout;
			Content = mMainLayout;
		}

		private void OnAccordionButtonClicked(object sender, EventArgs args)
		{
			foreach (var vChildItem in mMainLayout.Children)
			{
				if (vChildItem.GetType() == typeof(ContentView)) vChildItem.IsVisible = false;
				if (vChildItem.GetType() == typeof(AccordionButton))
				{
					var vButton = (AccordionButton)vChildItem;
					vButton.Expand = false;
				}
			}
			var vSenderButton = (AccordionButton)sender;

			if (vSenderButton.Expand)
			{
				vSenderButton.Expand = false;
			}
			else vSenderButton.Expand = true;
			vSenderButton.AssosiatedContent.IsVisible = vSenderButton.Expand;
		}

		#endregion
	}
}
