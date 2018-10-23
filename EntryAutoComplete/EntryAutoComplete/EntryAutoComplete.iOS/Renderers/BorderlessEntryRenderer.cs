using System.ComponentModel;
using EntryAutoComplete;
using EntryAutoComplete.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace EntryAutoComplete.iOS.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}