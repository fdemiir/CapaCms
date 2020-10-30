using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmsCapaMedikalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CmsCapaMedikalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareersController : ControllerBase
    {
        private readonly CapamedikalApiContext _context;
        public CareersController(CapamedikalApiContext context)
        {
            _context = context;
        }

        [Route("InsertCareer")]
        public async Task<IActionResult> Create(Careers career)
        {
            var careerId = Guid.NewGuid();
            career.Id = careerId.ToString().Replace("-","");
            _context.Careers.Add(career);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
