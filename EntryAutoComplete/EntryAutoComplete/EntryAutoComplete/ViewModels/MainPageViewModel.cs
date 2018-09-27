using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EntryAutoComplete.Annotations;

namespace EntryAutoComplete.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private List<string> _countries;

        public List<string> Countries
        {
            get { return _countries; }
            set
            {
                _countries = value;
                OnPropertyChanged(nameof(Countries));
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
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}