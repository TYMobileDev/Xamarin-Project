﻿/*
 * BottomNavigationBar for Xamarin Forms
 * Copyright (c) 2016 Thrive GmbH and others (http://github.com/thrive-now).
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.ComponentModel;
using System.Linq;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using PacificCoral;
using PacificCoral.Droid;
using BottomNavigationBar;
using BottomNavigationBar.Listeners;
using Android.Support.V4.Content;
using PacificCoral.Droid.Renderers;

[assembly: ExportRenderer(typeof(RootPage), typeof(BottomBarPageRenderer))]
namespace PacificCoral.Droid.Renderers
{
	public class BottomBarPageRenderer : VisualElementRenderer<RootPage>, IOnTabClickListener
	{
		private bool _disposed;
		private BottomNavigationBar.BottomBar _bottomBar;
		private FrameLayout _frameLayout;
		private IPageController _pageController;

		public BottomBarPageRenderer()
		{
			AutoPackage = false;
		}

		#region -- IOnTabClickListener implementation --

		public void OnTabSelected(int position)
		{
			SwitchContent(Element.Children[position]);
		}

		public void OnTabReSelected(int position)
		{
		}

		#endregion

		#region -- Overrides --

		protected override void Dispose(bool disposing)
		{
			if (disposing && !_disposed)
			{
				_disposed = true;

				RemoveAllViews();

				foreach (Page pageToRemove in Element.Children)
				{
					IVisualElementRenderer pageRenderer = Platform.GetRenderer(pageToRemove);

					if (pageRenderer != null)
					{
						pageRenderer.ViewGroup.RemoveFromParent();
						pageRenderer.Dispose();
					}

					// pageToRemove.ClearValue (Platform.RendererProperty);
				}

				if (_bottomBar != null)
				{
					_bottomBar.SetOnTabClickListener(null);
					_bottomBar.Dispose();
					_bottomBar = null;
				}

				if (_frameLayout != null)
				{
					_frameLayout.Dispose();
					_frameLayout = null;
				}

				/*if (Element != null) {
					PageController.InternalChildren.CollectionChanged -= OnChildrenCollectionChanged;
				}*/
			}

			base.Dispose(disposing);
		}

		protected override void OnAttachedToWindow()
		{
			base.OnAttachedToWindow();
			_pageController.SendAppearing();
		}

		protected override void OnDetachedFromWindow()
		{
			base.OnDetachedFromWindow();
			_pageController.SendDisappearing();
		}

		protected override void OnElementChanged(ElementChangedEventArgs<RootPage> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				RootPage bottomBarPage = e.NewElement;

				if (_bottomBar == null)
				{
					_pageController = PageController.Create(bottomBarPage);

					// create a view which will act as container for Page's
					_frameLayout = new FrameLayout(Forms.Context);
					_frameLayout.LayoutParameters = new FrameLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent, GravityFlags.Fill);
					AddView(_frameLayout, 0);

					// create bottomBar control
					_bottomBar = BottomNavigationBar.BottomBar.Attach(_frameLayout, null);

					_bottomBar.MaxFixedTabCount = 2;

					_bottomBar.NoTabletGoodness();
					_bottomBar.LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
					_bottomBar.SetOnTabClickListener(this);

					UpdateTabs();
					UpdateBarBackgroundColor();
					UpdateBarTextColor();
				}

				if (bottomBarPage.CurrentPage != null)
				{
					SwitchContent(bottomBarPage.CurrentPage);
				}

			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == nameof(TabbedPage.CurrentPage))
			{
				SwitchContent(Element.CurrentPage);
			}
			else if (e.PropertyName == NavigationPage.BarBackgroundColorProperty.PropertyName)
			{
				UpdateBarBackgroundColor();
			}
			else if (e.PropertyName == NavigationPage.BarTextColorProperty.PropertyName)
			{
				UpdateBarTextColor();
			}
		}

		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			int width = r - l;
			int height = b - t;

			var context = Context;

			_bottomBar.Measure(MeasureSpecFactory.MakeMeasureSpec(width, MeasureSpecMode.Exactly), MeasureSpecFactory.MakeMeasureSpec(height, MeasureSpecMode.AtMost));
			int tabsHeight = Math.Min(height, Math.Max(_bottomBar.MeasuredHeight, _bottomBar.MinimumHeight));

			if (width > 0 && height > 0)
			{
				_pageController.ContainerArea = new Rectangle(0, 0, context.FromPixels(width), context.FromPixels(height - 108));

				_bottomBar.Measure(MeasureSpecFactory.MakeMeasureSpec(width, MeasureSpecMode.Exactly), MeasureSpecFactory.MakeMeasureSpec(tabsHeight, MeasureSpecMode.Exactly));
				_bottomBar.Layout(0, 0, width, tabsHeight);
			}

			base.OnLayout(changed, l, t, r, b);
		}

		#endregion

		protected virtual void SwitchContent(Page view)
		{
			Context.HideKeyboard(this);

			_frameLayout.RemoveAllViews();

			if (view == null)
			{
				return;
			}

			if (Platform.GetRenderer(view) == null)
			{
				Platform.SetRenderer(view, Platform.CreateRenderer(view));
			}

			_frameLayout.AddView(Platform.GetRenderer(view).ViewGroup);

			for (int i = 0; i < _bottomBar.Items.Count(); i++)
			{
				var item = _bottomBar.Items[i];
				var title = item.GetTitle(this.Context);
				if (title == view.Title && _bottomBar.CurrentTabPosition != i)
				{
					_bottomBar.SelectTabAtPosition(i, false);
				}
			}
		}

		#region -- Private helpers --

		private void UpdateBarBackgroundColor()
		{
			if (_disposed || _bottomBar == null)
			{
				return;
			}

			_bottomBar.SetBackgroundColor(Element.BarBackgroundColor.ToAndroid());
		}

		private void UpdateBarTextColor()
		{
			if (_disposed || _bottomBar == null)
			{
				return;
			}

			_bottomBar.SetActiveTabColor(Element.BarTextColor.ToAndroid());
			// haven't found yet how to set text color for tab items on_bottomBar, doesn't seem to have a direct way
		}

		private void UpdateTabs()
		{
			// create tab items
			SetTabItems();

			// set tab colors
			SetTabColors();
		}

		private void SetTabItems()
		{
			BottomBarTab[] tabs = Element.Children.Select(page =>
			{
				var tabIconId = ResourceManagerEx.IdFromTitle(page.Icon, ResourceManager.DrawableClass);
				return new BottomBarTab(tabIconId, "");
			}).ToArray();

			_bottomBar.SetItems(tabs);
		}

		private void SetTabColors()
		{
			for (int i = 0; i < Element.Children.Count; ++i)
			{
				_bottomBar.MapColorForTab(i, StyleManager.GetAppResource<Color>("DefaultMainColor").ToAndroid());
			}
		}

		#endregion
	}
}