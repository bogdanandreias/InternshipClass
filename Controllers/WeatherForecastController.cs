using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RazorMvc.Utilities;
using RestSharp;


namespace RazorMVC.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly string apiKey;
        private readonly double lat;
        private readonly double lon;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;

            this.lat = double.Parse(configuration["WeatherForecast:lat"], CultureInfo.InvariantCulture);
            this.lon = double.Parse(configuration["WeatherForecast:lon"], CultureInfo.InvariantCulture);
            this.apiKey = configuration["WeatherForecast:apiKey"];
        }

        /// <summary>
        /// Getting Weather forecast for five days for default location.
        /// </summary>
        /// <returns>Enumerable of weatherForecast objects.</returns>
        [HttpGet]
        public List<WeatherForecast> Get()
        {
            var weatherForecasts = Get(this.lat, this.lon);

            return weatherForecasts.GetRange(1, 5);
        }

        [HttpGet ("/forecast")]
        public List<WeatherForecast> Get(double lat, double lon)
        {
            

            var client = new RestClient($"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&exclude=hourly,minutely&appid={apiKey}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            return ConvertResponseContentToWeatherForecastList(response.Content);
        }

        [NonAction]
        public List<WeatherForecast> ConvertResponseContentToWeatherForecastList(string content)
        {

            var root = JObject.Parse(content);
            var testToken = root["daily"];
            if(testToken == null)
            {
                var codToken = root["cod"];
                var messageToken = root["message"];
                throw new Exception($"Weather API doesn't work. Please check if you can run Weather API. : {messageToken}({codToken})");
            }
            var forecasts = new List<WeatherForecast>();
            foreach (var token in testToken)
            {
                forecasts.Add(new WeatherForecast
                {
                    Date = DateTimeConverter.ConvertEpochToDateTime(long.Parse(token["dt"].ToString())),
                    TemperatureK = double.Parse(token["temp"]["day"].ToString()),
                    Summary = token["weather"][0]["description"].ToString(),
                });

            }

            return forecasts;
        }
    }
}
