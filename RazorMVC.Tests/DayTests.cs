using System;
using Xunit;
using RazorMvc.Utilities;
using RazorMVC.WebAPI;
using RazorMVC.WebAPI.Controllers;
using Microsoft.Extensions.Logging.Abstractions;

namespace RazorMVC.Tests
{
    public class DayTests
    {
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
            Microsoft.Extensions.Logging.ILogger<WeatherForecastController> nullLogger = new NullLogger<WeatherForecastController>();
            var weatherForecastController = new WeatherForecastController(nullLogger);

            //Act
            var weatherForecasts = weatherForecastController.FetchWeatherForecasts(lat, lon, APIKey);
            WeatherForecast weatherForecastForTomorrow =  weatherForecasts[1];

            //Assert
            Assert.Equal(284.25, weatherForecastForTomorrow.TemperatureK);

        }

    }
}
