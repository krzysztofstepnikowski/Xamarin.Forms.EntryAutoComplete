using System.Collections;
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

            var suggestions = autoCompleteView.ItemsSource;
            if (newvalue != null)
            {
                autoCompleteView.SearchEntry.Text = (string) newvalue;
                suggestions = autoCompleteView.FilterSuggestions(suggestions, autoCompleteView.SearchEntry.Text);
            }

            autoCompleteView.SuggestionsListView.ItemsSource = suggestions;
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
            BindableProperty.Create(nameof(MinimumPrefixCharacter), typeof(int), typeof(EntryAutoComplete), 2);

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
            typeof(IEnumerable), typeof(EntryAutoComplete),null,BindingMode.OneWay,null,OnItemsSourceChanged);

        private static void OnItemsSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var entryAutoComplete = bindable as EntryAutoComplete;

            var itemsSource = (IEnumerable) newvalue;
            entryAutoComplete.ItemsSource = itemsSource;
            itemsSource = entryAutoComplete.FilterSuggestions(itemsSource, entryAutoComplete.SearchText);
            entryAutoComplete.SuggestionsListView.ItemsSource = itemsSource;

        }

        private IEnumerable FilterSuggestions(IEnumerable itemsSource, string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return itemsSource;
            }

            else
            {
                itemsSource = itemsSource.Cast<object>().Where(x =>
                    x.ToString().Equals(searchText) || x.ToString().ToLower().Contains(searchText.ToLower()));
            }

            return itemsSource;
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

        private Grid _container { get; }
        private ScrollView _suggestionWrapper { get; }
        private ListView SuggestionsListView { get; }
        private Entry SearchEntry { get; set; }

        public EntryAutoComplete()
        {
            _container = new Grid();
            SearchEntry = new Entry();
            SuggestionsListView = new ListView();
            _suggestionWrapper = new ScrollView();

            // init Grid Layout
            _container.RowSpacing = 0;
            _container.ColumnDefinitions.Add(new ColumnDefinition() {Width = GridLength.Star});
            _container.RowDefinitions.Add(new RowDefinition() {Height = GridLength.Star});
            _container.RowDefinitions.Add(new RowDefinition() {Height = 50});


            //Init Search Entry
            SearchEntry.HorizontalOptions = LayoutOptions.Fill;
            SearchEntry.VerticalOptions = LayoutOptions.Fill;

            // init Suggestions ListView
            SuggestionsListView.BackgroundColor = Color.White;
            SuggestionsListView.VerticalOptions = LayoutOptions.End;

            // ScrollView for ListView
            _suggestionWrapper.VerticalOptions = LayoutOptions.Fill;
            _suggestionWrapper.Orientation = ScrollOrientation.Vertical;
            _suggestionWrapper.BackgroundColor = Color.White;
            _suggestionWrapper.Content = SuggestionsListView;

            _container.Children.Add(_suggestionWrapper);
            _container.Children.Add(SearchEntry, 0, 1);

            Content = _container;
        }
    }
}