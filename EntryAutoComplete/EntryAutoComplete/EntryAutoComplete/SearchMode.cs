using System;

namespace EntryAutoComplete
{
    public class SearchMode
    {
        private readonly Func<string, object, bool> _filter;
        private SearchMode(Func<string, object, bool> filter)
        {
            _filter = filter;
        }
        public bool Filter(string entry, object obj) => _filter(entry, obj);
        public static SearchMode StartsWith { get; } = new SearchMode((entry, obj) => obj.ToString().ToLower().StartsWith(entry.ToLower()));
        public static SearchMode Contains { get; } = new SearchMode((entry, obj) => obj.ToString().ToLower().Contains(entry.ToLower()));
        public static SearchMode EndsWith { get; } = new SearchMode((entry, obj) => obj.ToString().ToLower().EndsWith(entry.ToLower()));
        public static SearchMode Using(Func<string, object, bool> filter)
        {
            return new SearchMode(filter);
        }
    }
}