using System.ComponentModel;
using EntryAutoComplete.CustomControl;
using EntryAutoComplete.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace EntryAutoComplete.iOS.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}