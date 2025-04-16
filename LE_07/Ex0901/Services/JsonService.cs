using Ex0901.Interfaces;
using Ex0901.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;

namespace Ex0901.Services
{
    public class JsonService : IJsonService
    {
        private JObject _data;
        public JObject Data
        {
            get => _data; 
            set => _data = value;
        }

        public WeatherInfo ParseCurrentWeather()
        {
            var info = new WeatherInfo
            {
                CityName = _data["name"]?.ToString(),
                Longitude = (double)_data["coord"]?["lon"],
                Latitude = (double)_data["coord"]?["lat"],
                Temperature = (double?)_data["main"]?["temp"] ?? 0,
                WeatherDescription = _data["weather"]?[0]?["description"]?.ToString(),
                Icon = _data["weather"]?[0]?["icon"]?.ToString()
            };
            return info;
        }

        public ObservableCollection<ForecastItem> ParseForecastData()
        {
            var items = new ObservableCollection<ForecastItem>();
            var list = _data["list"];

            foreach (var entry in list)
            {
                items.Add(new ForecastItem
                {
                    Date = DateTime.Parse(entry["dt_txt"]?.ToString() ?? string.Empty),
                    FutureTemperature = (double)(entry["main"]?["temp"] ?? 0),
                    FutureWeatherDescription = entry["weather"]?[0]?["description"]?.ToString() ?? "No Description",
                    Icon = entry["weather"]?[0]?["icon"]?.ToString() ?? "default-icon"
                });
            }
            return items;
        }

        public AirQualityInfo ParseAirQualityInfo()
        {
            var aqi = (int)_data["list"]?[0]?["main"]?["aqi"];
            var components = _data["list"]?[0]?["components"];

            return new AirQualityInfo
            {
                AQI = aqi,
                CO = (double)components?["co"],
                NO2 = (double)components?["no2"],
                O3 = (double)components?["o3"],
                PM25 = (double)components?["pm2_5"],
                PM10 = (double)components?["pm10"],
                NH3 = (double)components?["nh3"]
            };
        }

        
    }
}
