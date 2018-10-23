using System;
using System.Collections;
using System.Linq;
using Xamarin.Forms;

namespace EntryAutoComplete
{
    public class EntryAutoComplete : ContentView
    {
        private const int RowHeight = 60;

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
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(EntryAutoComplete), propertyChanged: OnItemSourceChanged);

        public static readonly BindableProperty IsClearButtonVisibleProperty =
            BindableProperty.Create(nameof(IsClearButtonVisible), typeof(bool), typeof(EntryAutoComplete), true,
                propertyChanged: OnIsClearImageVisibleChanged);

        public static readonly BindableProperty SearchModeProperty = BindableProperty.Create(nameof(SearchMode),
            typeof(SearchMode), typeof(EntryAutoComplete), SearchMode.Contains, BindingMode.TwoWay, propertyChanged: OnSearchModeChanged);

        private static void OnSearchTextChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var autoCompleteView = bindable as EntryAutoComplete;
            var searchText = (string)newvalue;
            autoCompleteView.UpdateSuggestions(searchText);
        }

        private static void OnItemSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var autoCompleteView = bindable as EntryAutoComplete;
            var items = (IEnumerable)newvalue;
            autoCompleteView._originSuggestions = items;
        }

        private static void OnSearchTextColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var entryAutoComplete = bindable as EntryAutoComplete;
            var textColor = (Color)newvalue;
            entryAutoComplete.SearchEntry.TextColor = textColor;
        }

        private static void OnPlaceholderColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var entryAutoComplete = bindable as EntryAutoComplete;
            var placeholderColor = (Color)newvalue;
            entryAutoComplete.SearchEntry.PlaceholderColor = placeholderColor;
        }

        private static void OnIsClearImageVisibleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var entryAutoComplete = bindable as EntryAutoComplete;
            var isVisible = (bool)newValue;
            entryAutoComplete.ClearSearchEntryImage.IsVisible = isVisible;
        }

        private static void OnPlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var entryAutoComplete = bindable as EntryAutoComplete;
            var placeholder = (string)newValue;
            entryAutoComplete.SearchEntry.Placeholder = placeholder;
        }

        private static void OnSearchModeChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var entryAutoComplete = bindable as EntryAutoComplete;
            var searchType = (SearchMode)newvalue;
            entryAutoComplete.SearchMode = searchType;
        }

        public string SearchText
        {
            get => (string)GetValue(SearchTextProperty);
            set => SetValue(SearchTextProperty, value);
        }

        public Color SearchTextColor
        {
            get => (Color)GetValue(SearchTextColorProperty);
            set => SetValue(SearchTextColorProperty, value);
        }

        public int MaximumVisibleElements
        {
            get => (int)GetValue(MaximumVisibleElementsProperty);
            set => SetValue(MaximumVisibleElementsProperty, value);
        }

        public int MinimumPrefixCharacter
        {
            get => (int)GetValue(MinimumPrefixCharacterProperty);
            set => SetValue(MinimumPrefixCharacterProperty, value);
        }

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }

        public bool IsClearButtonVisible
        {
            get => (bool)GetValue(IsClearButtonVisibleProperty);
            set => SetValue(IsClearButtonVisibleProperty, value);
        }

        public SearchMode SearchMode
        {
            get => (SearchMode)GetValue(SearchModeProperty);
            set
            {
                SetValue(SearchModeProperty, value);
                UpdateSuggestions(SearchText);
            }
        }

        private Grid Container;
        private ShadowedFrame ShadowedFrame;
        private Grid SearchEntryLayout;
        private ScrollView SuggestionWrapper;
        private StackLayout SuggestionsStackLayout;
        private BorderlessEntry SearchEntry;
        private Image ClearSearchEntryImage;

        private IEnumerable _originSuggestions = Array.Empty<object>();

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
            Container.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            Container.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            Container.RowDefinitions.Add(new RowDefinition { Height = 50 });
        }

        private void InitSearchEntry()
        {
            SearchEntry = new BorderlessEntry
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
                HorizontalOptions = LayoutOptions.End,
                WidthRequest = 24,
                HeightRequest = 24,
                Margin = Device.RuntimePlatform != Device.UWP ? new Thickness(0, 0, 5, 0) : new Thickness(0, 0, 10, 15)
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (x, y) => SearchEntry.Text = "";
            ClearSearchEntryImage.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private void InitSuggestionsListView()
        {
            SuggestionsStackLayout = new StackLayout
            {
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.End,
                Spacing = 0,
                Padding = new Thickness(20,0)
            };
        }

        private void InitSuggestionsScrollView()
        {
            SuggestionWrapper = new ScrollView
            {
                Orientation = ScrollOrientation.Vertical,
                BackgroundColor = Color.White,
                Content = new ShadowedFrame
                {
                    Content = SuggestionsStackLayout
                },
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

            ShadowedFrame = new ShadowedFrame
            {
                Content = InitSearchEntryLayout()
            };

            SearchEntryLayout = new Grid();
            SearchEntryLayout.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            });
            SearchEntryLayout.RowDefinitions.Add(new RowDefinition { Height = 80 });
            SearchEntryLayout.Children.Add(ShadowedFrame, 0, 0);


            Container.Children.Add(SuggestionWrapper);
            Container.Children.Add(SearchEntryLayout, 0, 1);
        }

        private Grid InitSearchEntryLayout()
        {
            var searchEntryLayout = new Grid();
            searchEntryLayout.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            });
            searchEntryLayout.RowDefinitions.Add(new RowDefinition {Height = 50});

            searchEntryLayout.Children.Add(SearchEntry, 0, 0);
            searchEntryLayout.Children.Add(ClearSearchEntryImage, 0, 0);

            return searchEntryLayout;
        }

        private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchText = e.NewTextValue;
        }

        private void SearchEntry_IconChanged(string searchText)
        {
            ClearSearchEntryImage.Source = string.IsNullOrEmpty(searchText)
                ? "baseline_search_black_24.png"
                : "baseline_close_black_24.png";
        }

        private void UpdateSuggestions(string newSearchText)
        {
            var newSuggestions = _originSuggestions;
            SearchEntry_IconChanged(newSearchText);


            if (newSearchText.Length >= MinimumPrefixCharacter)
            {
                newSuggestions = FilterSuggestions(newSuggestions, newSearchText);
            }

            SuggestionWrapper.IsVisible = newSearchText.Length != 0 &&
                                          newSearchText.Length >= MinimumPrefixCharacter &&
                                          newSuggestions.Cast<object>().Count() != 0;

            SuggestionsStackLayout.Children.Clear();
            foreach (var item in newSuggestions)
            {
                SuggestionsStackLayout.Children.Add(new Label
                {
                    Text = item.ToString(),
                    TextColor = System.Drawing.Color.Black,
                    FontSize = 16,
                    HeightRequest = RowHeight,
                    VerticalTextAlignment = TextAlignment.Center,
                    GestureRecognizers =
                    {
                        new TapGestureRecognizer
                        {
                            Command = new Command(() =>
                            {
                                SearchEntry.Text = item.ToString();
                                SuggestionWrapper.IsVisible = false;
                            })
                        }
                    },

                });

                SuggestionsStackLayout.Children.Add(new BoxView
                {
                    HeightRequest = 1,
                    BackgroundColor = Color.LightGray,
                    HorizontalOptions = LayoutOptions.Fill
                });                
            }

            if (SuggestionWrapper.IsVisible)
            {
                UpdateLayout();
            }
        }

        private IEnumerable FilterSuggestions(IEnumerable itemsSource, string searchText)
        {
            return itemsSource
                .Cast<object>()
                .Where(obj => SearchMode.Filter(searchText, obj))
                .ToArray();
        }

        private int GetSuggestionsListHeight()
        {
            var items = SuggestionsStackLayout.Children.Cast<object>().ToList();
            return items.ToList().Count >= MaximumVisibleElements
                ? MaximumVisibleElements * RowHeight
                : items.Count * RowHeight;
        }

        private void UpdateLayout()
        {
            var listHeight = GetSuggestionsListHeight();
            SuggestionWrapper.HeightRequest = listHeight;
            Container.HeightRequest = SuggestionsStackLayout.Children.Count>4 ? listHeight + SearchEntryLayout.Height : listHeight;
            Container.ForceLayout();
        }
    }
}