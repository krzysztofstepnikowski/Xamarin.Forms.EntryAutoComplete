using EntryAutoComplete;
using EntryAutoComplete.Sample.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace EntryAutoComplete.Sample.Droid.Renderers
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class BorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
            }
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}