using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EntryAutoComplete;

namespace EntryAutoComplete.Sample.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string _searchCountry = string.Empty;
        private bool _customSearchFunctionSwitchIsToggled;
        private SearchMode _searchMode = SearchMode.Contains;

        public string SearchCountry
        {
            get => _searchCountry;
            set
            {
                _searchCountry = value;
                OnPropertyChanged();
            }
        }

        public List<string> Countries { get; } = new List<string>
        {
            "African Union",
            "Andorra",
            "Armenia",
            "Austria",
            "Bahamas",
            "Barbados",
            "Belarus",
            "Belgium",
            "Benin",
            "Bolivia",
            "Bosnia and Herzegovina",
            "Botswana",
            "Brazil",
            "Bulgaria",
            "Burkina Faso",
            "Cameroon",
            "Canada",
            "Chad",
            "Chile",
            "China",
            "Colombia",
            "Congo",
            "Czech Republic",
            "Denmark",
            "Egypt",
            "England",
            "Estonia",
            "Finland",
            "Flag Of Europe",
            "France",
            "Gabon",
            "Germany",
            "Great Britain",
            "Hungary",
            "Iceland",
            "Iran",
            "Ireland",
            "Italy",
            "Jamaica",
            "Kuwait",
            "Latvia",
            "Liberia",
            "Lithuania",
            "Macedonia",
            "Mali",
            "Netherlands",
            "New Zealand",
            "Norway",
            "Philippines",
            "Poland",
            "Portugal",
            "Romania",
            "Russian Federation",
            "Slovakia",
            "Slovenia",
            "Spain",
            "Sweden",
            "Switzerland",
            "Togo",
            "Turkey",
            "Ukraine",
            "USA",
            "Wales"
        };

        public SearchMode SearchMode
        {
            get => _searchMode;
            set
            {
                _searchMode = value;
                OnPropertyChanged();
            }
        }

        public bool CustomSearchFunctionSwitchIsToggled
        {
            get => _customSearchFunctionSwitchIsToggled;
            set
            {
                _customSearchFunctionSwitchIsToggled = value;
                OnPropertyChanged();
                UpdateCustomSearchFunction();
            }
        }

        private void UpdateCustomSearchFunction()
        {
            SearchMode = CustomSearchFunctionSwitchIsToggled 
                ? SearchMode.Using((text, obj) => obj.ToString().Length % 2 == 0 && obj.ToString().ToLower().Contains(text.ToLower()))
                : SearchMode.Contains;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}