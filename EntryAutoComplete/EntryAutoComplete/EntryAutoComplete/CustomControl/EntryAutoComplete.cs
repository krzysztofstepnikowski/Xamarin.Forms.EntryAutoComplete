using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace EntryAutoComplete.CustomControl
{
    public class EntryAutoComplete : ContentView
    {
        public static readonly BindableProperty SearchTextProperty = 
            BindableProperty.Create(nameof(SearchText), typeof(string), typeof(EntryAutoComplete), defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnSearchTextChanged);        

        public static readonly BindableProperty SearchTextColorProperty = 
            BindableProperty.Create(nameof(SearchTextColor), typeof(Color), typeof(EntryAutoComplete), Color.Black, propertyChanged: OnSearchTextColorChanged);

        public static readonly BindableProperty MaximumVisibleElementsProperty =
            BindableProperty.Create(nameof(MaximumVisibleElements), typeof(int), typeof(EntryAutoComplete), 4);

        public static readonly BindableProperty MinimumPrefixCharacterProperty =
            BindableProperty.Create(nameof(MinimumPrefixCharacter), typeof(int), typeof(EntryAutoComplete), 2);

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(EntryAutoComplete), propertyChanged: OnPlaceholderChanged);

        public static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(EntryAutoComplete), Color.DarkGray, propertyChanged: OnPlaceholderColorChanged);

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(EntryAutoComplete));

        public static readonly BindableProperty IsClearButtonVisibleProperty =
            BindableProperty.Create(nameof(IsClearButtonVisible), typeof(bool), typeof(EntryAutoComplete), true, propertyChanged: OnIsClearButtonVisibleChanged);

        private static void OnSearchTextChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var autoCompleteView = bindable as EntryAutoComplete;
            autoCompleteView.SearchText = (string)newvalue;
            autoCompleteView.SuggestionsListView.ItemsSource = autoCompleteView.ItemsSource;
            autoCompleteView._originSuggestions = autoCompleteView.ItemsSource;
        }

        private static void OnSearchTextColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var entryAutoComplete = bindable as EntryAutoComplete;
            entryAutoComplete.SearchEntry.TextColor = (Color)newvalue;
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
            entryAutoComplete.SearchEntry.PlaceholderColor = (Color)newvalue;
        }

        private static void OnIsClearButtonVisibleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var entryAutoComplete = bindable as EntryAutoComplete;
            var isVisible = (bool)newValue;
            entryAutoComplete.ClearSearchEntryButton.IsVisible = isVisible;
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

        public bool IsClearButtonVisible
        {
            get { return (bool)GetValue(IsClearButtonVisibleProperty); }
            set { SetValue(IsClearButtonVisibleProperty, value); }
        }

        private Grid Container;
        private ScrollView SuggestionWrapper;
        private ListView SuggestionsListView;
        private Entry SearchEntry;
        private Button ClearSearchEntryButton;

        private IEnumerable _originSuggestions;

        public EntryAutoComplete()
        {
            InitGrid();
            InitClearSearchEntryButton();
            InitSearchEntry();
            InitSuggestionsListView();
            InitSuggestionsScrollView();

            PlaceControlsInGrid();

            Content = Container;
        }

        private void PlaceControlsInGrid()
        {
            SearchEntry.HorizontalOptions = new LayoutOptions
            {
                Alignment = LayoutAlignment.Fill,
                Expands = true
            };
            ClearSearchEntryButton.HorizontalOptions = new LayoutOptions
            {
                Alignment = LayoutAlignment.End,
            };
            ClearSearchEntryButton.VerticalOptions = new LayoutOptions
            {
                Alignment = LayoutAlignment.Center
            };
            var searchEntryLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    SearchEntry,
                    ClearSearchEntryButton
                }
            };

            Container.Children.Add(SuggestionWrapper);
            Container.Children.Add(searchEntryLayout, 0, 1);
        }

        private void InitSuggestionsScrollView()
        {
            SuggestionWrapper = new ScrollView
            {
                VerticalOptions = LayoutOptions.Fill,
                Orientation = ScrollOrientation.Vertical,
                BackgroundColor = Color.White,
                Content = SuggestionsListView,
                IsVisible = false
            };
        }

        private void InitSuggestionsListView()
        {
            SuggestionsListView = new ListView
            {
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.End
            };
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

        private void InitClearSearchEntryButton()
        {
            ClearSearchEntryButton = new Button { Text = "✖", WidthRequest = 30, HeightRequest = 30, CornerRadius = 15, FontSize = 8 };
            ClearSearchEntryButton.Clicked += (e, sender) => SearchEntry.Text = "";
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
    }
}