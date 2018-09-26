using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using EntryAutoComplete.Droid.CustomRenderer;

[assembly:ExportRenderer(typeof(EntryAutoComplete.CustomControl.EntryAutoComplete),typeof(EntryAutoCompleteRenderer))]
namespace EntryAutoComplete.Droid.CustomRenderer
{
    public class EntryAutoCompleteRenderer : ViewRenderer
    {
        public EntryAutoCompleteRenderer(Context context) : base(context)
        {
            
        }
    }
}