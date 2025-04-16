namespace Ex0901.Models
{
    public class WeatherInfo
    {
        private string _cityName;
        private double _latitude;
        private double _longitude;
        private double _temperature;
        private string _weatherDescription;
        private string _icon;
        private int _aqi;

        public string CityName
        {
            get => _cityName;
            set => _cityName = value;
        }

        public double Latitude
        {
            get => _latitude;
            set => _latitude = value;
        }

        public double Longitude
        {
            get => _longitude;
            set => _longitude = value;
        }

        public double? Temperature
        {
            get => _temperature;
            set => _temperature = (double)value;
        }

        public string WeatherDescription
        {
            get => _weatherDescription;
            set => _weatherDescription = value;
        }

        public string Icon
        {
            get => _icon;
            set => _icon = value;
        }

        public int Aqi
        {
            get => _aqi;
            set => _aqi = value;
        }
    }
}
