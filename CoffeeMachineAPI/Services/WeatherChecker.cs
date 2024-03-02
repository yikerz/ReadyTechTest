using CoffeeMachineAPI.Services.IServices;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CoffeeMachineAPI.Services
{
    public class WeatherChecker : IWeatherChecker
    {
        private dynamic _apiConfig;
        private string _apiUrl;
        // Property to retrieve API configuration from JSON file
        public dynamic ApiConfig
        {
            get
            {
                if (_apiConfig == null)
                {
                    string? assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    string jsonPath = Path.Combine(assemblyDir??"../", "Data", "apienv.json");
                    string json = File.ReadAllText(jsonPath);
                    _apiConfig = JsonConvert.DeserializeObject(json) ?? new { } ;
                }
                return _apiConfig;
            }
        }
        // Property to construct API URL using configuration
        public string ApiUrl
        {
            get
            {
                if (_apiUrl == null)
                {
                    _apiUrl = String.Format("https://api.openweathermap.org/data/3.0/onecall?lat={0}&lon={1}&exclude=minutely,hourly,daily,alerts&units=metric&appid={2}", 
                        ApiConfig.Lat, ApiConfig.Lon, ApiConfig.ApiKey);
                }
                return _apiUrl;
            }
        }

        // Method to asynchronously fetch temperature
        public async Task<float?> GetTempAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(ApiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    JsonNode? weatherNode = JsonNode.Parse(responseBody);
                    if (weatherNode != null)
                    {
                        float temp = (float)weatherNode["current"]!["temp"]!;
                        return temp;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching temperature: {ex.Message}");
            }
            return null;
        }

    }
}
