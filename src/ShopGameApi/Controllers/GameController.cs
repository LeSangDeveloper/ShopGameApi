using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ShopGameApi.Data;
using Microsoft.Extensions.Configuration;
using ShopGameApi.Models;

namespace ShopGameApi.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class GameController : ControllerBase
    {
        private readonly ShopGameApiDBContext _context;
        private readonly IConfiguration _config;

        public GameController(ShopGameApiDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet]
        public List<Game> GetListGames()
        {
            List<Game> games = _context.Games.ToList<Game>();

            return games;
        }

        [HttpPost]
        public IActionResult PostAddGame(Game game, Company company)
        {

            return Ok();
        }

    }
}