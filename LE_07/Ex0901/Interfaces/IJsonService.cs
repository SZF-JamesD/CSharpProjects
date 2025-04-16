using Ex0901.Models;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace Ex0901.Interfaces
{
    public interface IJsonService
    {
        JObject Data { get; set; }
        WeatherInfo ParseCurrentWeather();
        ObservableCollection<ForecastItem> ParseForecastData();
        AirQualityInfo ParseAirQualityInfo();
    }
}
