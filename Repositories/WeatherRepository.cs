using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Grupo1NotasMVVM.Models.WeatherModel;

namespace Grupo1NotasMVVM.Repositories
{
    internal class WeatherRepository
    {
        public async Task<WeatherData> GetWeatherCurrentLocationAsync()
        {

            GeolocationRepository geolocationRepository = new GeolocationRepository();
            var location = await geolocationRepository.GetCurrentLocation();
            return await GetWeatherDataAsync(location.Latitude, location.Longitude);
        }
        public async Task<WeatherData> GetWeatherDataAsync(double latitude, double longitude)
        {
            HttpClient httpClient = new HttpClient();
            string latitude_f = latitude.ToString().Replace(",", ".");
            string longitude_f = longitude.ToString().Replace(",", ".");
            string url = $"https://api.open-meteo.com/v1/forecast?latitude=" + latitude_f + "&longitude=" + longitude_f + "&current=temperature_2m,relative_humidity_2m,rain";
            var response = await httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            WeatherData data = JsonConvert.DeserializeObject<WeatherData>(result);
            return data;
        }
    }
}
