using System.Collections;
using System.Linq;
using Xamarin.Forms;

namespace EntryAutoComplete.CustomControl
{
    public enum Mode
    {
        StartsWith,
        Contains,
        EndsWith
    }

    public class EntryAutoComplete : ContentView
    {
        public static readonly BindableProperty SearchTextProperty =
            BindableProperty.Create(nameof(SearchText), typeof(string), typeof(EntryAutoComplete),
                defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnSearchTextChanged);

        public static readonly BindableProperty SearchTextColorProperty =
            BindableProperty.Create(nameof(SearchTextColor), typeof(Color), typeof(EntryAutoComplete), Color.Black,
                propertyChanged: OnSearchTextColorChanged);

        public static readonly BindableProperty MaximumVisibleElementsProperty =
            BindableProperty.Create(nameof(MaximumVisibleElements), typeof(int), typeof(EntryAutoComplete), 4);

        public static readonly BindableProperty MinimumPrefixCharacterProperty =
            BindableProperty.Create(nameof(MinimumPrefixCharacter), typeof(int), typeof(EntryAutoComplete), 1);

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(EntryAutoComplete),
                propertyChanged: OnPlaceholderChanged);

        public static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(EntryAutoComplete), Color.DarkGray,
                propertyChanged: OnPlaceholderColorChanged);

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(EntryAutoComplete));

        public static readonly BindableProperty IsClearButtonVisibleProperty =
            BindableProperty.Create(nameof(IsClearButtonVisible), typeof(bool), typeof(EntryAutoComplete), true,
                propertyChanged: OnIsClearImageVisibleChanged);

        public static readonly BindableProperty SearchTypeProperty = BindableProperty.Create(nameof(SearchType),
            typeof(Mode), typeof(Xamarin.Forms.PlatformConfiguration.AndroidSpecific.Entry),
            Mode.Contains, BindingMode.OneWay, null, propertyChanged: OnSearchTypeChanged);

        private static void OnSearchTextChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var autoCompleteView = bindable as EntryAutoComplete;
            var searchText = (string) newvalue;
            autoCompleteView.SearchText = searchText;
            autoCompleteView.SuggestionsListView.ItemsSource = autoCompleteView.ItemsSource;
            autoCompleteView._originSuggestions = autoCompleteView.ItemsSource;
        }

        private static void OnSearchTextColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var entryAutoComplete = bindable as EntryAutoComplete;
            var textColor = (Color) newvalue;
            entryAutoComplete.SearchEntry.TextColor = textColor;
        }

        private static void OnPlaceholderColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var entryAutoComplete = bindable as EntryAutoComplete;
            var placeholderColor = (Color) newvalue;
            entryAutoComplete.SearchEntry.PlaceholderColor = placeholderColor;
        }

        private static void OnIsClearImageVisibleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var entryAutoComplete = bindable as EntryAutoComplete;
            var isVisible = (bool) newValue;
            entryAutoComplete.ClearSearchEntryImage.IsVisible = isVisible;
        }

        private static void OnPlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var entryAutoComplete = bindable as EntryAutoComplete;
            var placeholder = (string) newValue;
            entryAutoComplete.SearchEntry.Placeholder = placeholder;
        }

        private static void OnSearchTypeChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var entryAutoComplete = bindable as EntryAutoComplete;
            var searchType = (Mode) newvalue;
            entryAutoComplete.SearchType = searchType;
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

        public bool IsClearButtonVisible
        {
            get { return (bool) GetValue(IsClearButtonVisibleProperty); }
            set { SetValue(IsClearButtonVisibleProperty, value); }
        }

        public Mode SearchType
        {
            get { return (Mode) GetValue(SearchTypeProperty); }

            set { SetValue(SearchTypeProperty, value); }
        }

        private Grid Container;
        private ScrollView SuggestionWrapper;
        private ListView SuggestionsListView;
        private Entry SearchEntry;
        private Image ClearSearchEntryImage;

        private IEnumerable _originSuggestions;

        public EntryAutoComplete()
        {
            InitGrid();
            InitSearchEntry();
            InitClearImage();
            InitSuggestionsListView();
            InitSuggestionsScrollView();

            PlaceControlsInGrid();

            Content = Container;
        }

        private void InitGrid()
        {
            Container = new Grid
            {
                RowSpacing = 0
            };
            Container.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
            Container.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });
            Container.RowDefinitions.Add(new RowDefinition() { Height = 50 });
        }

        private void InitSearchEntry()
        {
            SearchEntry = new Entry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };
            SearchEntry.TextChanged += SearchEntry_TextChanged;
        }

        private void InitClearImage()
        {
            ClearSearchEntryImage = new Image
            {
                Source = "baseline_search_black_24.png",
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 24,
                HeightRequest = 24
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (x, y) => SearchEntry.Text = "";
            ClearSearchEntryImage.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private void InitSuggestionsListView()
        {
            SuggestionsListView = new ListView
            {
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.End
            };

            SuggestionsListView.ItemSelected += (e, sender) => SuggestionsListView.SelectedItem = null;
        }

        private void InitSuggestionsScrollView()
        {
            SuggestionWrapper = new ScrollView
            {
                Orientation = ScrollOrientation.Vertical,
                BackgroundColor = Color.White,
                Content = SuggestionsListView,
                IsVisible = false
            };
        }

        private void PlaceControlsInGrid()
        {
            SearchEntry.HorizontalOptions = new LayoutOptions
            {
                Alignment = LayoutAlignment.Fill,
                Expands = true
            };

            var searchEntryLayout = new Grid();
            searchEntryLayout.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            });
            searchEntryLayout.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Auto)
            });
            searchEntryLayout.RowDefinitions.Add(new RowDefinition() {Height = 50});

            searchEntryLayout.Children.Add(SearchEntry, 0, 0);
            searchEntryLayout.Children.Add(ClearSearchEntryImage, 1, 0);

            Container.Children.Add(SuggestionWrapper);
            Container.Children.Add(searchEntryLayout, 0, 1);
        }

        private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            SuggestionsListView.ItemsSource = _originSuggestions;
            SearchEntry_IconChanged(e);


            if (e.NewTextValue.Length >= MinimumPrefixCharacter)
            {
                var suggestions = FilterSuggestions(SuggestionsListView.ItemsSource, e.NewTextValue);
                SuggestionsListView.ItemsSource = suggestions;
            }

            SuggestionWrapper.IsVisible = e.NewTextValue.Length != 0 &&
                                          e.NewTextValue.Length >= MinimumPrefixCharacter &&
                                          SuggestionsListView.ItemsSource.Cast<object>().Count() != 0;
            SuggestionWrapper.IsEnabled = SuggestionsListView.ItemsSource.Cast<object>().Count() >= MaximumVisibleElements;

            if (SuggestionWrapper.IsVisible)
            {
                UpdateLayout();
            }
        }

        private void SearchEntry_IconChanged(TextChangedEventArgs e)
        {
            ClearSearchEntryImage.Source = string.IsNullOrEmpty(e.NewTextValue)
                ? "baseline_search_black_24.png"
                : "baseline_close_black_24.png";
        }

        private IEnumerable FilterSuggestions(IEnumerable itemsSource, string searchText)
        {
            switch (SearchType)
            {
                case Mode.StartsWith:
                    return itemsSource.Cast<object>().Where(x => x.ToString().Equals(searchText) || 
                                                                 x.ToString().ToLower().StartsWith(searchText.ToLower()));
                case Mode.EndsWith:
                    return itemsSource.Cast<object>().Where(x => x.ToString().Equals(searchText) || 
                                                                 x.ToString().ToLower().EndsWith(searchText.ToLower()));
                case Mode.Contains:
                    return itemsSource.Cast<object>().Where(x => x.ToString().Equals(searchText) || 
                                                                 x.ToString().ToLower().Contains(searchText.ToLower()));
            }

            return itemsSource;
        }

        private int GetExpectedHeight()
        {
            var items = SuggestionsListView.ItemsSource.Cast<object>().ToList();
            var wrapperHeightRequest = (items.ToList().Count >= MaximumVisibleElements
                ? MaximumVisibleElements * 40 + 60
                : items.Count * 40 + 60);

            return wrapperHeightRequest;
        }

        private void UpdateLayout()
        {
            var expectedHeight = GetExpectedHeight();
            Container.HeightRequest = expectedHeight;
            Container.ForceLayout();
        }
    }
}