using System.ComponentModel;
using Android.Support.V4.View;
using EntryAutoComplete.CustomControl;
using EntryAutoComplete.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using FrameRenderer = Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer;

[assembly:ExportRenderer(typeof(ShadowedFrame), typeof(ShadowedFrameRenderer))]
namespace EntryAutoComplete.Droid.Renderers
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class ShadowedFrameRenderer : FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null)
            {
                return;;
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

            // we need to reset the StateListAnimator to override the setting of Elevation on touch down and release.
            Control.StateListAnimator = new Android.Animation.StateListAnimator();

            // set the elevation manually
            ViewCompat.SetElevation(this, materialFrame.ShadowRadius);
            ViewCompat.SetElevation(Control, materialFrame.ShadowRadius);
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}