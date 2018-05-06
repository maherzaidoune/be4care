using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using be4care.Droid.Renderers;
using BottomBar.Droid.Renderers;
using BottomBar.XamarinForms;
using BottomNavigationBar;
using BottomNavigationBar.Listeners;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BottomBarPage), typeof(BottomBarPageRenderer))]

namespace BottomBar.Droid.Renderers
{
    public class BottomBarPageRenderer : VisualElementRenderer<BottomBarPage>, IOnTabClickListener
    {
        bool _disposed;
        BottomNavigationBar.BottomBar _bottomBar;
        FrameLayout _frameLayout;
        IPageController _pageController;

        public BottomNavigationBar.BottomBar BottomBar => _bottomBar;

        public BottomBarPageRenderer(Android.Content.Context context) : base(context)
        {
            AutoPackage = false;
        }

    #region IOnTabClickListener
    public void OnTabSelected(int position)
        {
            //Do we need this call? It's also done in OnElementPropertyChanged
            SwitchContent(Element.Children[position]);
            var bottomBarPage = Element as BottomBarPage;
            bottomBarPage.CurrentPage = Element.Children[position];
            //bottomBarPage.RaiseCurrentPageChanged();
        }

        public void OnTabReSelected(int position)
        {
        }

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


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(TabbedPage.CurrentPage))
            {
                SwitchContent(Element.CurrentPage);
                UpdateSelectedTabIndex(Element.CurrentPage);
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
        }


        void UpdateSelectedTabIndex(Page page)
        {
            var index = Element.Children.IndexOf(page);
            _bottomBar.SelectTabAtPosition(index, true);
        }

        void UpdateBarBackgroundColor()
        {
            if (_disposed || _bottomBar == null)
            {
                return;
            }

            _bottomBar.SetBackgroundColor(Element.BarBackgroundColor.ToAndroid());
        }

        void UpdateBarTextColor()
        {
            if (_disposed || _bottomBar == null)
            {
                return;
            }

            _bottomBar.SetActiveTabColor(Element.BarTextColor.ToAndroid());
            // The problem SetActiveTabColor does only work in fiexed mode // haven't found yet how to set text color for tab items on_bottomBar, doesn't seem to have a direct way
        }

    }

}