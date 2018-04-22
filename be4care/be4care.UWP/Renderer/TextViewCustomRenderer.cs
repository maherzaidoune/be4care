﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

namespace be4care.UWP.Renderer
{
    class TextViewCustomRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Windows.UI.Xaml.ResourceDictionary dic = new Windows.UI.Xaml.ResourceDictionary();
                dic.Source = new Uri("ms-appx:///rounded_corners.xaml");
                Control.Style = dic["RoundedEditorStyle"] as Windows.UI.Xaml.Style;
            }
        }
    }
}
