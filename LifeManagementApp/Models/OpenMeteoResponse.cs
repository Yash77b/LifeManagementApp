using System.Text.Json.Serialization;

namespace LifeManagementApp.Models
{
    public class OpenMeteoResponse
    {
        [JsonPropertyName("current")]
        public WeatherInfo? Current { get; set; }
    }
}