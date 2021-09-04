using dotMorten.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace AutoCompleteEntry
{
    public partial class MainPage : ContentPage
    {
        private List<string> CountriesList;

        public MainPage()
        {
            InitializeComponent();
            GetCountriesFromFile();
        }

        private void GetCountriesFromFile()
        {
            using(var country = typeof(MainPage).Assembly.GetManifestResourceStream("AutoCompleteEntry.Data.Countries.txt"))
            {
                CountriesList = new StreamReader(country).ReadToEnd().Split('\n').Select(t => t.Trim()).ToList();
            }
        }

        private void contryEntry_TextChanged(object sender, AutoSuggestBoxTextChangedEventArgs e)
        {
            AutoSuggestBox input = (AutoSuggestBox)sender;

            input.ItemsSource = GetSuggestions(input.Text);
        }

        private List<string> GetSuggestions(string input)
        {
            return string.IsNullOrWhiteSpace(input) ? null : CountriesList.Where(c => c.StartsWith(input, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        private void contryEntry_QuerySubmitted(object sender, AutoSuggestBoxQuerySubmittedEventArgs e)
        {
            if (e.ChosenSuggestion == null)
                displayCountry.Text = String.Empty;
            else
                displayCountry.Text = e.ChosenSuggestion.ToString();
        }
    }
}
