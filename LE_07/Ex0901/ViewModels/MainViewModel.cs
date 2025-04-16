using Ex0901.Helpers;
using Ex0901.Interfaces;
using Ex0901.Models;
using MvvmUtilities;
using MvvmUtilities.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Ex0901.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public string Title => "Home";

        private static readonly Regex _coordinatesRegex = new Regex
        (
            @"^\s*(-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?)\s*$",
            RegexOptions.Compiled
        );

        public ICommand SearchCommand { get; }

        private readonly IApiService _apiService;
        private readonly IJsonService _jsonService;
        private readonly IDialogService _dialogService;

        private ObservableCollection<TileOverlay> _tileGrid;
        public ObservableCollection<TileOverlay> TileGrid
        {
            get => _tileGrid;
            set
            {
                SetProperty(ref _tileGrid, value);
            }
        }

        private ObservableCollection<ForecastItem> _forecasts;
        public ObservableCollection<ForecastItem> Forecasts
        {
            get => _forecasts;
            set => SetProperty(ref _forecasts, value);
        }

        private List<BitmapImage> _weatherTileImages;
        public List<BitmapImage> WeatherTileImages
        {
            get => _weatherTileImages;
            set
            {
                SetProperty(ref _weatherTileImages, value);
            }
        }

        private List<BitmapImage> _mapTileImages;
        public List<BitmapImage> MapTileImages
        {
            get => _mapTileImages;
            set
            {
                SetProperty(ref _mapTileImages, value); 
            }
        }

        private string _searchCriteria;
        public string SearchCriteria
        {
            get => _searchCriteria;
            set
            {
                SetProperty(ref _searchCriteria, value);
                ((AsyncRelayCommand)SearchCommand).RaiseCanExecuteChanged();
            }
        }

        private string _airQuality;
        public string AirQuality
        {
            get => _airQuality;
            set => SetProperty(ref _airQuality, value);
        }

        public string Location
        {
            get => _searchCriteria;
            set
            {
                SetProperty(ref _searchCriteria, value);
            }
        }

        private string _temperature;
        public string Temperature
        {
            get => _temperature;
            set => SetProperty(ref _temperature, value);
        }

        private string _weatherIcon;
        public string WeatherIcon
        {
            get => _weatherIcon;
            set => SetProperty(ref _weatherIcon, value);
        }

        private string _weatherDescription;
        public string WeatherDescription
        {
            get => _weatherDescription; 
            set => SetProperty(ref _weatherDescription, value);
        }

        private string _aqi;
        public string Aqi
        {
            get => _aqi; 
            set => SetProperty(ref _aqi, value);
        }

        private string _day;
        public string Day
        {
            get => _day; 
            set => SetProperty(ref _day, value);
        }

        private string _futureTemperature;
        public string FutureTemperature
        {
            get => _futureTemperature; 
            set => SetProperty(ref _futureTemperature, value);
        }

        private string _futureWeatherDescription;
        public string FutureWeatherDescription
        {
            get => _futureWeatherDescription; 
            set => SetProperty(ref _futureWeatherDescription, value);
        }

        private WeatherInfo _currentWeather;
        public WeatherInfo CurrentWeather
        {
            get => _currentWeather;
            set => SetProperty(ref _currentWeather, value);
        }
        
        
        private ObservableCollection<ForecastDay> _groupedForecasts = new ObservableCollection<ForecastDay>();

        public ObservableCollection<ForecastDay> GroupedForecasts
        {
            get => _groupedForecasts;
            set => SetProperty(ref _groupedForecasts, value);
        }

        public MainViewModel(IApiService apiService, IJsonService jsonService)
        {
            _apiService = apiService;
            _jsonService = jsonService;
            SearchCommand = new AsyncRelayCommand(SearchWeather);

        }

        

        private async Task SearchWeather()
        {
            try
            {
                if (string.IsNullOrEmpty(SearchCriteria))
                    return;

                await  FetchAndSetWeatherData();
                var airQualityTask = FetchAndSetAirQuality();
                var forecastTask = FetchAndSetForecast();
                var loadTilesTask = LoadAndDisplayTiles();

                await Task.WhenAll(airQualityTask, forecastTask, loadTilesTask);
            }
            catch (RegexMatchTimeoutException ex)
            {
                _dialogService.ShowError("Input Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                _dialogService.ShowError("An error occurred: " + ex.Message);
            }
        }


        private async Task FetchAndSetWeatherData()
        {
            JObject json;
            if (_coordinatesRegex.IsMatch(SearchCriteria))
            {
                var parts = SearchCriteria.Split(',');
                var latitude = parts[0].Trim();
                var longitude = parts[1].Trim();

                json = await _apiService.FetchWeatherByCordsAsync(latitude, longitude);
            }
            else
            {
                json = await _apiService.FetchWeatherByCityAsync(SearchCriteria);
            }
            _jsonService.Data = json;
            CurrentWeather = _jsonService.ParseCurrentWeather();
            Location = "Location: " + CurrentWeather.CityName + " Lat: " + CurrentWeather.Latitude + " Long: " + CurrentWeather.Longitude;
            Temperature = "Temp: " + CurrentWeather.Temperature.ToString() + "C ";
            WeatherDescription = "Description: " + CurrentWeather.WeatherDescription;
        }


        private async Task FetchAndSetAirQuality()
        {
            JObject airQualityData = await _apiService.FetchAirPollutionAqiAsync(CurrentWeather.Latitude, CurrentWeather.Longitude);
            _jsonService.Data = airQualityData;
            AirQuality = "Air Quality: " + _jsonService.ParseAirQualityInfo().AQI.ToString();
        }


        private async Task FetchAndSetForecast()
        {
            var ForecastData = await _apiService.FetchForecastDataAsync(CurrentWeather.Latitude, CurrentWeather.Longitude);
            _jsonService.Data = ForecastData;
            Forecasts = _jsonService.ParseForecastData();
            var grouped = Forecasts.GroupBy(f => f.Date.Date).Select(g => new ForecastDay
            {
                Date = g.Key,
                ForecastsCollection = new ObservableCollection<ForecastItem>(g)
            });
            GroupedForecasts = new ObservableCollection<ForecastDay>(grouped);
        }

        private async Task LoadAndDisplayTiles()
        {
            var (MapTileImages, WeatherTileImages) = await LoadMapTiles(CurrentWeather.Latitude, CurrentWeather.Longitude);
            var tiles = new List<TileOverlay>();
            int count = WeatherTileImages?.Count ?? 0;
            for (int i = 0; i < count; i++)
            {
                tiles.Add(new TileOverlay
                {
                    MapImage = MapTileImages[i],
                    WeatherImage = WeatherTileImages[i]
                });
            }
            TileGrid = new ObservableCollection<TileOverlay>(tiles);
        }
    

        private async Task<(List<BitmapImage> MapTileImages, List<BitmapImage> WeatherTileImages)> LoadMapTiles(double lat, double lon)
        {
            var (x, y) = MapTileHelper.LatLonToTile(lat, lon, 10);

            var MapTileImages = await _apiService.FetchMapTilesAsync("map", 10, x, y);
            var WeatherTileImages = await _apiService.FetchMapTilesAsync("temp_new",10, x, y);

            return (MapTileImages, WeatherTileImages);
        }
    }
}
