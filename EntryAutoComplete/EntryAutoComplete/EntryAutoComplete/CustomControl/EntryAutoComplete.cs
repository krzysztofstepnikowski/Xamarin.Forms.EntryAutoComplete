using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace EntryAutoComplete.CustomControl
{
    public class EntryAutoComplete : ContentView
    {
        public static readonly BindableProperty SearchTextProperty = BindableProperty.Create(nameof(SearchText),
            typeof(string), typeof(EntryAutoComplete), null, BindingMode.TwoWay, null, OnSearchTextChanged);

        private static void OnSearchTextChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var autoCompleteView = bindable as EntryAutoComplete;
            autoCompleteView.SearchText = (string)newvalue;
            autoCompleteView.SuggestionsListView.ItemsSource = autoCompleteView.ItemsSource;
            autoCompleteView._originSuggestions = autoCompleteView.ItemsSource;
        }

        public static readonly BindableProperty SearchTextColorProperty = BindableProperty.Create(nameof(SearchTextColor), typeof(Color), typeof(EntryAutoComplete), Color.Black,
            BindingMode.OneWay, null, OnSearchTextColorChanged);

        private static void OnSearchTextColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var entryAutoComplete = bindable as EntryAutoComplete;
            entryAutoComplete.SearchEntry.TextColor = (Color) newvalue;
        }

        public static readonly BindableProperty MaximumVisibleElementsProperty =
            BindableProperty.Create(nameof(MaximumVisibleElements), typeof(int), typeof(EntryAutoComplete), 4);

        public static readonly BindableProperty MinimumPrefixCharacterProperty =
            BindableProperty.Create(nameof(MinimumPrefixCharacter), typeof(int), typeof(EntryAutoComplete), 1);

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(EntryAutoComplete), null,
                BindingMode.OneWay, null, OnPlaceholderChanged);


        public static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(EntryAutoComplete), Color.DarkGray,
                BindingMode.OneWay, null, OnPlaceholderColorChanged);

        private static void OnPlaceholderColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var entryAutoComplete = bindable as EntryAutoComplete;
            entryAutoComplete.SearchEntry.PlaceholderColor = (Color) newvalue;
        }

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource),
            typeof(IEnumerable), typeof(EntryAutoComplete));

 

        private IEnumerable FilterSuggestions(IEnumerable itemsSource, string searchText)
        {
            var suggestions = itemsSource.Cast<object>();

            if (string.IsNullOrEmpty(searchText))
            {
                return itemsSource;
            }

            else
            {
                suggestions = itemsSource.Cast<object>().Where(x =>
                    x.ToString().Equals(searchText) || x.ToString().ToLower().Contains(searchText.ToLower()));
            }

            return suggestions;
        }

        private static void OnPlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var entryAutoComplete = bindable as EntryAutoComplete;
            entryAutoComplete.SearchEntry.Placeholder = newValue?.ToString();
        }

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

        private Grid Container { get; }
        private ScrollView SuggestionWrapper { get; }
        private ListView SuggestionsListView { get; }
        private Entry SearchEntry { get; set; }
        private IEnumerable _originSuggestions { get; set; }

        public EntryAutoComplete()
        {
            Container = new Grid();
            SearchEntry = new Entry();
            SuggestionsListView = new ListView();
            SuggestionWrapper = new ScrollView();

            // init Grid Layout
            Container.RowSpacing = 0;
            Container.ColumnDefinitions.Add(new ColumnDefinition() {Width = GridLength.Star});
            Container.RowDefinitions.Add(new RowDefinition() {Height = GridLength.Star});
            Container.RowDefinitions.Add(new RowDefinition() {Height = 50});


            //Init Search Entry
            SearchEntry.HorizontalOptions = LayoutOptions.Fill;
            SearchEntry.VerticalOptions = LayoutOptions.Fill;
            SearchEntry.TextChanged += SearchEntry_TextChanged;

            // init Suggestions ListView
            SuggestionsListView.BackgroundColor = Color.White;
            SuggestionsListView.VerticalOptions = LayoutOptions.End;

            // ScrollView for ListView
            SuggestionWrapper.VerticalOptions = LayoutOptions.Fill;
            SuggestionWrapper.Orientation = ScrollOrientation.Vertical;
            SuggestionWrapper.BackgroundColor = Color.White;
            SuggestionWrapper.Content = SuggestionsListView;
            SuggestionWrapper.IsVisible = false;

            Container.Children.Add(SuggestionWrapper);
            Container.Children.Add(SearchEntry, 0, 1);

            Content = Container;
        }

        private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            SuggestionsListView.ItemsSource = _originSuggestions;

            if (e.NewTextValue.Length >= MinimumPrefixCharacter)
            {
                var suggestions = FilterSuggestions(SuggestionsListView.ItemsSource, e.NewTextValue);
                SuggestionsListView.ItemsSource = suggestions;
                SuggestionWrapper.IsVisible = e.NewTextValue.Length!=0 && e.NewTextValue.Length >= MinimumPrefixCharacter;
            }

            else
            {
                SuggestionWrapper.IsVisible = false;
            }
        }
    }
}