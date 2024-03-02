using CoffeeMachineAPI.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachineAPI.Test
{
    public class WeatherCheckTest
    {
        WeatherChecker checker;
        public WeatherCheckTest()
        {
            // Arrange
            checker = new WeatherChecker();
        }
        [Fact]
        public async void Get_current_temperature()
        {
            // Act
            float temp = await checker.GetTempAsync() ?? 0;
            // Assert
            Assert.IsType<float>(temp);
        }
        [Fact]
        public void Retrieve_correct_api_env()
        {
            // Arrange
            string startupPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "CoffeeMachineAPI/Data/apienv.json");
            string? json = startupPath != null ? File.ReadAllText(startupPath) : "";
            dynamic expectedConfig = JsonConvert.DeserializeObject(json) ?? new { };
            // Act
            dynamic apiConfig = checker.ApiConfig;
            // Assert
            Assert.Equal(expectedConfig.Lat, apiConfig.Lat);
            Assert.Equal(expectedConfig.Lon, apiConfig.Lon);
            Assert.Equal(expectedConfig.ApiKey, apiConfig.ApiKey);
        }
        [Fact]
        public void Retrieve_correct_api_url()
        {
            // Arrange
            string startupPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "CoffeeMachineAPI/Data/apienv.json");
            string? json = startupPath != null ? File.ReadAllText(startupPath) : "";
            dynamic expectedConfig = JsonConvert.DeserializeObject(json) ?? new { };
            string expectedUrl = String.Format("https://api.openweathermap.org/data/3.0/onecall?lat={0}&lon={1}&exclude=minutely,hourly,daily,alerts&units=metric&appid={2}",
                        expectedConfig.Lat, expectedConfig.Lon, expectedConfig.ApiKey);
            // Act
            string apiUrl = checker.ApiUrl;
            // Assert
            Assert.Equal(expectedUrl, apiUrl);


        }
    }
}
