using EntryAutoComplete.iOS.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(EntryAutoComplete.CustomControl.EntryAutoComplete),typeof(EntryAutoCompleteRenderer))]
namespace EntryAutoComplete.iOS.CustomRenderer
{
    public class EntryAutoCompleteRenderer : ViewRenderer
    {
       
    }
}