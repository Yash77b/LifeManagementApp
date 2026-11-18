using System.Net.Http.Json;
using LifeManagementApp.Models;

namespace LifeManagementApp.Services
{
    public class OpenMeteoWeatherService : IWeatherService
    {
        private const string Url =
            "https://api.open-meteo.com/v1/forecast?latitude=63.10&longitude=21.62" +
            "&current=temperature_2m,wind_speed_10m&wind_speed_unit=ms";

        private readonly HttpClient httpClient;

        public OpenMeteoWeatherService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<WeatherInfo> GetCurrentWeatherAsync()
        {
            OpenMeteoResponse? response =
                await httpClient.GetFromJsonAsync<OpenMeteoResponse>(Url);

            if (response == null || response.Current == null)
                throw new Exception("No weather data received.");

            return response.Current;
        }
    }
}