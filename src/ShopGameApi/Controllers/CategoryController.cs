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
    public class CategoryController : ControllerBase
    {

        private readonly ShopGameApiDBContext _context;
        private readonly IConfiguration _config;

        public CategoryController(ShopGameApiDBContext context, IConfiguration config)
        {
            _config = config;
            _context = context;
        }

        [HttpGet]
        public List<Category> GetCategories() => _context.Categories.ToList<Category>();

        [HttpPost("AddCategory")]
        public async Task<IActionResult> PostAddCategory(Category category)
        {
            Category result = _context.Categories.FirstOrDefault<Category>( s => s.Name == category.Name.ToLower() );
            if (result == null)
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return Ok(category);
            }
            return BadRequest(new { error = "Cannot Create Category" });
        }

    }
}