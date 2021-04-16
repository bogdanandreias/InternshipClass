using System;
using Xunit;
using RazorMvc.Utilities;
using RazorMVC.WebAPI;
using RazorMVC.WebAPI.Controllers;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

namespace RazorMVC.Tests
{
    public class DayTests
    {
        private IConfigurationRoot configuration;

        public DayTests()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }
        [Fact]
        public void CheckEpochConversion()
        {
            //Assume
            long ticks = 1617184800;

            //Act
            DateTime dateTime = DateTimeConverter.ConvertEpochToDateTime(ticks);

            //Assert
            Assert.Equal(31, dateTime.Day);
            Assert.Equal(03, dateTime.Month);
            Assert.Equal(2021, dateTime.Year);
        }

        [Fact]
        public void ConvertOutputOfWeatherAPIToWeatherForecast()
        {
            //Assume
            //https://api.openweathermap.org/data/2.5/onecall?lat=45.75&lon=25.3333&exclude=hourly,minutely&appid=7447d7b593ced104ad765a48307b6771
            var lat = 45.75;
            var lon = 25.3333;
            var APIKey = "7447d7b593ced104ad765a48307b6771";
            WeatherForecastController weatherForecastController = InstantiateWeatherForecastController();

            //Act
            var weatherForecasts = weatherForecastController.Get();

            //Assert
            Assert.Equal(5, weatherForecasts.Count);
            
        }

        [Fact]
        public void ConvertWeatherJsonToWeatherForecast()
        {
            //Assume
            string content = GetStreamLines();
            WeatherForecastController weatherForecastController = InstantiateWeatherForecastController();
            //Act
            var weatherForecasts = weatherForecastController.ConvertResponseContentToWeatherForecastList(content);
            WeatherForecast weatherForecastForTomorrow = weatherForecasts[1];

            //Assert
            Assert.Equal(285.39, weatherForecastForTomorrow.TemperatureK);
        }

        private string GetStreamLines()
        {
            var assembly = this.GetType().Assembly;
            using var stream = assembly.GetManifestResourceStream("RazorMVC.Tests.weatherForecast.json");
            StreamReader streamReader = new StreamReader(stream);
            var streamLineReader = "";

            while (!streamReader.EndOfStream)
            {
                streamLineReader += streamReader.ReadLine();

            }
            return streamLineReader;
        }

        private WeatherForecastController InstantiateWeatherForecastController()
        {
            Microsoft.Extensions.Logging.ILogger<WeatherForecastController> nullLogger = new NullLogger<WeatherForecastController>();
            var weatherForecastController = new WeatherForecastController(nullLogger, configuration);
            return weatherForecastController;
        }
    }
}
