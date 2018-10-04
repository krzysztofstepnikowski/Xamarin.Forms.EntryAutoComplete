using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using EntryAutoComplete.Annotations;

namespace EntryAutoComplete.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string _searchCountry = string.Empty;

        public string SearchCountry
        {
            get { return _searchCountry; }
            set
            {
                _searchCountry = value;
                OnPropertyChanged();
                FilterCountries();
            }
        }

        private List<string> _countries;

        public List<string> Countries
        {
            get { return _countries; }
            set
            {
                _countries = value;
                OnPropertyChanged();
            }
        }

        private List<string> _countriesFilter;

        public List<string> CountriesFilter
        {
            get { return _countriesFilter; }
            set
            {
                _countriesFilter = value; 
                OnPropertyChanged();
            }
        }

        public MainPageViewModel()
        {
            Countries = new List<string>()
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
                "Germana",
                "Germanu1",
                "Germanu2",
                "Germanu3",
                "Germanu4",
                "Germanu5",
                "Germanu6",
                "Germanu7",
                "Germanu8",
                "Germanu9",
                "Germanu10",
                "Germanu11",
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

            CountriesFilter = new List<string>(Countries);
        }

        private void FilterCountries()
        {
            if (Countries != null)
            {
                if (string.IsNullOrEmpty(_searchCountry))
                {
                    CountriesFilter = new List<string>(Countries);
                }

                else
                {
                    var filterCountries = Countries.Where(x =>
                        x.ToLower().Equals(_searchCountry.ToLower()) || x.ToLower().Contains(_searchCountry.ToLower()));

                    CountriesFilter = new List<string>(filterCountries);
                } 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}