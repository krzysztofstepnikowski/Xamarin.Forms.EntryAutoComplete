using System;
using System.Collections;
using Xamarin.Forms;

namespace EntryAutoComplete.CustomControls
{
    public class EntryAutoComplete : View
    {
        public static readonly BindableProperty SearchTextProperty = BindableProperty.Create(nameof(SearchText),
            typeof(string), typeof(EntryAutoComplete), null, BindingMode.TwoWay, null, OnSearchTextPropertyChanged);

        public static readonly BindableProperty SearchTextColorProperty =
            BindableProperty.Create<EntryAutoComplete, Color>(p => p.SearchTextColor, Color.Black, BindingMode.OneWay,
                null, OnSearchTextColorPropertyChanged);

        public static readonly BindableProperty MaximumVisibleElementsProperty =
            BindableProperty.Create(nameof(MaximumVisibleElements), typeof(int), typeof(EntryAutoComplete), 4);

        public static readonly BindableProperty MinimumPrefixCharacterProperty =
            BindableProperty.Create(nameof(MinimumPrefixCharacter), typeof(int), typeof(EntryAutoComplete), 2);

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(PlaceholderProperty), typeof(string), typeof(EntryAutoComplete), null,
                BindingMode.OneWay, null, OnPlaceholderPropertyChanged);

        public static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create<EntryAutoComplete, Color>(p => p.PlaceholderColor, Color.DarkGray,
                BindingMode.OneWay, null, OnPlaceholderColorChanged);

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource),
            typeof(IEnumerable), typeof(EntryAutoComplete), null, BindingMode.OneWay, null,
            OnItemsSourcePropertyChanged);

        public string SearchText
        {
            get { return (string) GetValue(SearchTextProperty); }

            set { SetValue(SearchTextProperty, value); }
        }

        public Color SearchTextColor
        {
            get { return (Color) GetValue(SearchTextColorProperty); }
            set { SetValue(SearchTextColorProperty, value); }
        }

        public int MaximumVisibleElements
        {
            get { return (int) GetValue(MaximumVisibleElementsProperty); }
            set { SetValue(MaximumVisibleElementsProperty, value); }
        }

        public int MinimumPrefixCharacter
        {
            get { return (int) GetValue(MinimumPrefixCharacterProperty); }
            set { SetValue(MinimumPrefixCharacterProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public string Placeholder
        {
            get { return (string) GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public Color PlaceholderColor
        {
            get { return (Color) GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }

        private static void OnSearchTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            throw new NotImplementedException();
        }

        private static void OnSearchTextColorPropertyChanged(BindableObject bindable, Color oldValue, Color newValue)
        {
            if (bindable is EntryAutoComplete entryAutoComplete)
            {
                entryAutoComplete.SearchTextColor = newValue;
            }
        }


        private static void OnPlaceholderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is EntryAutoComplete entryAutoComplete)
            {
                entryAutoComplete.Placeholder = newValue.ToString();
            }
           
        }


        private static void OnPlaceholderColorChanged(BindableObject bindable, Color oldValue, Color newValue)
        {
            if (bindable is EntryAutoComplete entryAutoComplete)
            {
                entryAutoComplete.PlaceholderColor = newValue;
            }
        }


        private static void OnItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            throw new NotImplementedException();
        }
    }
}