using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ShopGameApi.Data;
using Microsoft.Extensions.Configuration;
using ShopGameApi.Models;
using ShopGameApi.Objects;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopGameApi.Controllers
{
    [ApiController]
    [Route("api/company")]
    public class CompanyController : ControllerBase
    {

        private readonly ShopGameApiDBContext _context;
        private readonly IConfiguration _config;

        public CompanyController(ShopGameApiDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet]
        public List<Company> GetCompanies() 
        {
            List<Company> companies =  _context.Companies.ToList<Company>();
    
            return companies;
        }

        [HttpGet("{id}")]
        public List<GameObjectJson> GetGameOfCompany(int id)
        {
            List<Game> games = _context.Games
            .Include(g => g.Company)
            .Include(g => g.Rating)
            .Include(g => g.CategoryGame)
            .ThenInclude(cg => cg.Category)
            .Where<Game>(g => g.Company.CompanyId == id)
            .ToList();

            List<GameObjectJson> gameObjects = new List<GameObjectJson>();

            foreach (Game game in games)
            {
                gameObjects.Add(game.Covert());
            }

            return gameObjects;
        }

        [HttpPost("AddCompany")]
        public async Task<IActionResult> PostAddCompany(Company company)
        {
            Company result = _context.Companies.FirstOrDefault<Company>(c => c.Name.ToLower() == company.Name.ToLower());
            if (result == null)
            {
                await _context.Companies.AddAsync(company);
                await _context.SaveChangesAsync();
                return Ok(company);
            }
            return BadRequest(new { error = "Cannot create new company" });
        }

    }
}