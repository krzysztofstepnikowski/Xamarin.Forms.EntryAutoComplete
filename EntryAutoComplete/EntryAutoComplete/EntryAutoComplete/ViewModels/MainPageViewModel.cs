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
                "Argentina",
                "Armenia",
                "England",
                "Brazil",
                "Germany",
                "France",
                "Finland",
                "Poland",
                "Portugal",
                "Sweden",
                "Switzerland",
                "USA"
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