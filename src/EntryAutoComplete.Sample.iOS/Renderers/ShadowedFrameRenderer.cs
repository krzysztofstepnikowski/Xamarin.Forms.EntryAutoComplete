using CoreGraphics;
using EntryAutoComplete;
using EntryAutoComplete.Sample.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ShadowedFrame), typeof(ShadowedFrameRenderer))]

namespace EntryAutoComplete.Sample.iOS.Renderers
{
    public class ShadowedFrameRenderer : FrameRenderer
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            UpdateShadow();
        }

        private void UpdateShadow()
        {
            Layer.ShadowRadius = 4.0f;
            Layer.ShadowColor = UIColor.Gray.CGColor;
            Layer.ShadowOffset = new CGSize(2, 2);
            Layer.ShadowOpacity = 0.80f;
            Layer.ShadowPath = UIBezierPath.FromRect(Layer.Bounds).CGPath;
            Layer.MasksToBounds = false;
        }
    }
}