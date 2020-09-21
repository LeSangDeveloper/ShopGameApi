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
    public class CategoryController : ControllerBase
    {

        private readonly ShopGameApiDBContext _context;
        private readonly IConfiguration _config;

        public CategoryController(ShopGameApiDBContext context, IConfiguration config)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("AddCategory")]
        public IActionResult PostAddCategory(Category category)
        {

            return Ok();
        }

    }
}