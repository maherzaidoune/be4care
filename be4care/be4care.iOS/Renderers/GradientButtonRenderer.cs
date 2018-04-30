using be4care.iOS.Renderers;
using CoreAnimation;
using CoreGraphics;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(be4care.GradientButton), typeof(GradientButtonRenderer))]
namespace be4care.iOS.Renderers
{
    class GradientButtonRenderer : VisualElementRenderer<StackLayout>
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            GradientButton stack = (GradientButton)this.Element;
            CGColor startColor = stack.StartColor.ToCGColor();

            CGColor endColor = stack.EndColor.ToCGColor();
    
            var gradientLayer = new CAGradientLayer()
            {
                StartPoint = new CGPoint(0, 0.5),
                EndPoint = new CGPoint(1, 0.5)
            };

            gradientLayer.Frame = rect;
            gradientLayer.Colors = new CGColor[] { startColor, endColor };

            NativeView.Layer.InsertSublayer(gradientLayer, 0);
        }
    }
}
