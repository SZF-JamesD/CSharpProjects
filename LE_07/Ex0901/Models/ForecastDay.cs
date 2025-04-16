using System;
using System.Collections.ObjectModel;

namespace Ex0901.Models
{
    public class ForecastDay
    {
        public DateTime Date { get; set; }
        public ObservableCollection<ForecastItem> ForecastsCollection { get; set; }

        public string DateString => Date.ToString(("dddd"));
    }
}
