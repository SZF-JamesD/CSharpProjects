using System;

namespace Ex0901.Models
{
    public class ForecastItem
    {
        public DateTime Date { get; set; }
        public double FutureTemperature { get; set; }
        public string FutureWeatherDescription { get; set; }
        public string Icon { get; set; }
        public string TimeString => Date.ToString("HH:mm");
        public string TemperatureDisplay => $"{FutureTemperature:0} C";
        public string IconUrl => $"https://openweathermap.org/img/wn/{Icon}@2x.png";
    }
}
