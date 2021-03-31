using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Getting Weather forecast for five days.
        /// </summary>
        /// <returns>Enumerable of weatherForecast objects.</returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var client = new RestClient("https://api.openweathermap.org/data/2.5/weather?q=Brasov&appid=7447d7b593ced104ad765a48307b6771");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureK = rng.Next(250, 320),
                Summary = Summaries[rng.Next(Summaries.Length)],
            })
            .ToArray();
        }

        public IList<WeatherForecast> FetchWeatherForecasts(double lat, double lon, string apiKey)
        {
            var client = new RestClient($"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&exclude=hourly,minutely&appid={apiKey}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            return ConvertResponseContentToWeatherForecastList(response.Content);
        }

        public IList<WeatherForecast> ConvertResponseContentToWeatherForecastList(string content)
        {

            JToken root = JObject.Parse(content);
            JToken testToken = root["daily"];
            IList<WeatherForecast> forecasts = new List<WeatherForecast>();
            foreach (var token in testToken)
            {
                var forecast = new WeatherForecast();
                forecast.Date = DateTimeConverter.ConvertEpochToDateTime(long.Parse(token["dt"].ToString()));
                forecast.TemperatureK = double.Parse(token["temp"]["day"].ToString());
                forecast.Summary = token["weather"][0]["description"].ToString();
                forecasts.Add(forecast);
            }

            return forecasts;
        }
    }
}
