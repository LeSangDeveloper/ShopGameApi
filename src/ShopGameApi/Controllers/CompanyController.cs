using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ShopGameApi.Data;
using Microsoft.Extensions.Configuration;
using ShopGameApi.Models;
using System.Threading.Tasks;

namespace ShopGameApi.Controllers
{
    [ApiController]
    [Route("{controller}")]
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
        public List<Company> GetCompanies() => _context.Companies.ToList<Company>();

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