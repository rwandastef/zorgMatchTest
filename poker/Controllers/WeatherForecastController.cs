using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using poker.Game;
using poker.Model;


namespace poker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
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
        public IActionResult Get()
        {
            var cards = DealCards.Deal(2);
            var result = Compare.Wins(cards[0], cards[1]);

            var wins = result == 0 ? "player 1 " : "player 2 ";
            return Ok(new { result = wins, kind1 = Rules.WhatKindOfHand(cards[0]).ToString(),
                kind2 = Rules.WhatKindOfHand(cards[1]).ToString(),
                player1 = cards[0],
                player2 = cards[1]
            });
        }
    }
}
