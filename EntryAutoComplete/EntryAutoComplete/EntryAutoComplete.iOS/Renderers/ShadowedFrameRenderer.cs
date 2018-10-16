using System.ComponentModel;
using CoreGraphics;
using EntryAutoComplete.CustomControl;
using EntryAutoComplete.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(ShadowedFrame), typeof(ShadowedFrameRenderer))]
namespace EntryAutoComplete.iOS.Renderers
{
    public class ShadowedFrameRenderer : FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null)
            {
                return;
            }

            UpdateShadow();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals(nameof(ShadowedFrame.ShadowRadius)))
            {
                UpdateShadow();
            }
        }

        private void UpdateShadow()
        {
            var materialFrame = (ShadowedFrame)Element;

            Layer.ShadowRadius = materialFrame.ShadowRadius;
            Layer.ShadowColor = UIColor.Gray.CGColor;
            Layer.ShadowOffset = new CGSize(2, 2);
            Layer.ShadowOpacity = 0.80f;
            Layer.ShadowPath = UIBezierPath.FromRect(Layer.Bounds).CGPath;
            Layer.MasksToBounds = false;
        }
    }
}