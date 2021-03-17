using byin_netcore.Attributes;
using byin_netcore.RequestModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using byin_netcore_transver.Exception;

namespace byin_netcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : Controller
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            Random rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("GetException")]
        [Route("/api/WeatherForecast/GetException")]
        public IEnumerable<WeatherForecast> GetException()
        {
            throw new HttpTestException();
        }

        [HttpPost("PostTest")]
        [ValidateModel]
        //[Authorize(Roles = "admin")]
        public IActionResult PostTest([FromBody] PostOrderRequest postOrder)
        {
            return null;
        }
    }
}
