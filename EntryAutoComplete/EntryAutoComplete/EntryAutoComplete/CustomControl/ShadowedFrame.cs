using Xamarin.Forms;

namespace EntryAutoComplete.CustomControl
{
    public class ShadowedFrame : Frame
    {
        public static BindableProperty ShadowRadiusProperty = BindableProperty.Create(nameof(ShadowRadius), typeof(float), typeof(ShadowedFrame), 4.0f);

        public float ShadowRadius
        {
            get
            {
                return (float)GetValue(ShadowRadiusProperty);
            }
            set
            {
                SetValue(ShadowRadiusProperty, value);
            }
        }
    }
}
