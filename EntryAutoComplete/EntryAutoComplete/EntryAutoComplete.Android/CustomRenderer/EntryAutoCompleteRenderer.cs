using System.Collections;
using Android.Content;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using EntryAutoComplete.Droid.CustomRenderer;

[assembly:ExportRenderer(typeof(EntryAutoComplete.CustomControl.EntryAutoComplete),typeof(EntryAutoCompleteRenderer))]
namespace EntryAutoComplete.Droid.CustomRenderer
{
    public class EntryAutoCompleteRenderer : ViewRenderer<CustomControl.EntryAutoComplete,AutoCompleteTextView>
    {
        public EntryAutoCompleteRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CustomControl.EntryAutoComplete> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {

                var control = new AutoCompleteTextView(context: Forms.Context);

                if (!string.IsNullOrEmpty(e.NewElement.Placeholder))
                {
                    control.Hint = e.NewElement.Placeholder;
                    SetNativeControl(control);
                }

                if (e.NewElement.MinimumPrefixCharacter != 0)
                {
                    control.Threshold = e.NewElement.MinimumPrefixCharacter;
                    SetNativeControl(control);
                }
                
                UpdateAdapter(Element);
            }
        }

        private void UpdateAdapter(CustomControl.EntryAutoComplete element)
        {
            var items = (IList)element.ItemsSource; 
            var autoCompleteAdapter = new ArrayAdapter(Forms.Context, Resource.Layout.listview_custom_layout, Resource.Id.countryView, items);
            Control.Adapter = autoCompleteAdapter;
        }
    }
}