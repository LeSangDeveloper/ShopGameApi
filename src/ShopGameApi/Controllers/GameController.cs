using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopGameApi.Data;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
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

        public List<Game> GetGames() => _context.Games.ToList<Game>();

    }
}