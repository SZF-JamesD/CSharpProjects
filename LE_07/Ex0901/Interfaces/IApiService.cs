using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Ex0901.Interfaces
{
    public interface IApiService
    {
        Task<JObject> ResponseHandler(HttpResponseMessage response);
        Task<JObject> FetchWeatherByCityAsync(string city, string units="metric");
        Task<JObject> FetchWeatherByCordsAsync(string latitude, string longitude, string units="metric");
        Task<List<BitmapImage>> FetchMapTilesAsync(string layer, int zoom, int x, int y);

        Task<JObject> FetchForecastDataAsync(double lat, double lon);
        Task<JObject> FetchAirPollutionAqiAsync(double lat, double lon);
    }
}
