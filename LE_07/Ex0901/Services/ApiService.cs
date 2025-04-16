using Ex0901.Interfaces;
using MvvmUtilities.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Ex0901.Services
{
    public class ApiService : IApiService
    {
        private readonly IDialogService _dialogService;
        private readonly string _apiKey = "38ecdfc00a06ba828f61e264db280dd3";

        public async Task<JObject> ResponseHandler(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            var data = JObject.Parse(responseBody);

            Console.WriteLine(data);

            return data;
        }
        public async Task<JObject> FetchWeatherByCityAsync(string city, string units = "metric")
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units={units}");

                    return await ResponseHandler(response);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Get Weather By City Error " + e.Message);
                    _dialogService.ShowError("Http Error: " + e);
                    return null;
                }
            }
        }

        public async Task<JObject> FetchWeatherByCordsAsync(string latitude, string longitude, string units = "metric")
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&long={longitude}&appid={_apiKey}&units={units}");

                    return await ResponseHandler(response);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Get Weather By Cords Error " + e.Message);
                    _dialogService.ShowError("Http Error: " + e);
                    return null;
                }
            }
        }

        public async Task<List<BitmapImage>> FetchMapTilesAsync(string layer, int zoom, int x, int y) //rate limits are a problem for the 9 map tiles. I can only do one per second
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    List<BitmapImage> tileImages = new List<BitmapImage>();

                    for (int yStart = y - 1; yStart <= y + 1; yStart++)
                    {
                        for (int xStart = x - 1; xStart <= x + 1; xStart++)
                        {
                            string url;
                            
                            if (layer == "map")
                            {
                                client.DefaultRequestHeaders.UserAgent.ParseAdd("MyWeatherMapApp (052277@edu.szf.at)");
                                await Task.Delay(1100);
                                url = $"https://tile.openstreetmap.org/{zoom}/{xStart}/{yStart}.png";
                            }
                            else
                            {
                                url = $"https://tile.openweathermap.org/map/{layer}/{zoom}/{xStart}/{yStart}.png?appid={_apiKey}";
                            }
                            var response = await client.GetAsync(url);
                            if (response.IsSuccessStatusCode)
                            {
                                var imageBytes = await response.Content.ReadAsByteArrayAsync();

                                BitmapImage bitmapImage = new BitmapImage();
                                using (var stream = new MemoryStream(imageBytes))
                                {
                                    bitmapImage.BeginInit();
                                    bitmapImage.StreamSource = stream;
                                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                                    bitmapImage.EndInit();
                                    bitmapImage.Freeze();
                                }
                                tileImages.Add(bitmapImage);
                            }
                            else
                            {
                                Console.WriteLine($"Failed to load tile at {url}");
                                return null;
                            }
                        }
                    }
                    return tileImages;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Get Weather By Cords Error " + e.Message);
                    _dialogService.ShowError("Http Error: " + e);
                    return null;
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e.Message);
                    _dialogService.ShowError("Null Exception: " + e);
                    return null;
                }
            }
        }

        
        public async Task<JObject> FetchAirPollutionAqiAsync(double lat, double lon)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url = $"http://api.openweathermap.org/data/2.5/air_pollution?lat={lat}&lon={lon}&appid={_apiKey}";

                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var data = JObject.Parse(json);

                    return data;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Fetch AQI Error: " + e.Message);
                    _dialogService.ShowError("HTTP Error while fetching air pollution data: " + e.Message);
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected error: " + ex.Message);
                    _dialogService.ShowError("Unexpected error: " + ex.Message);
                    return null;
                }
            }
        }

        public async Task<JObject> FetchForecastDataAsync(double lat, double lon)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url = $"https://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&units=metric&appid={_apiKey}";

                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var data = JObject.Parse(json);
                    return data;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Fetch AQI Error: " + e.Message);
                    _dialogService.ShowError("HTTP Error while fetching air pollution data: " + e.Message);
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected error: " + ex.Message);
                    _dialogService.ShowError("Unexpected error: " + ex.Message);
                    return null;
                }

            }
        }
    }
}
        
