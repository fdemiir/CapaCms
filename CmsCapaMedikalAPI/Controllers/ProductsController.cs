using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmsCapaMedikal.Models;
using CmsCapaMedikalAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CmsCapaMedikalAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly CapamedikalApiContext _context;

        // Constructor
        public ProductsController(CapamedikalApiContext context)
        {
            _context = context;
        }

        [Route("GetAllProducts")]
        public async Task<ActionResult> GetAllProducts()
        {
            var categories = await _context.Categories.ToListAsync();
            var products = await _context.Products.Where(x => categories.Select(a => a.CategoryName).Contains(x.ProductCategoryName)).ToListAsync();
            categories.ForEach(x => x.Items = products.FindAll(a => a.ProductCategoryName == x.CategoryName));

            return Ok(categories);
        }
     }
}
