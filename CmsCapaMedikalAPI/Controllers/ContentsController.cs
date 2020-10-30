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
    public class ContentsController : ControllerBase
    {
        private readonly CapamedikalApiContext _context;

        // Constructor
        public ContentsController(CapamedikalApiContext context)
        {
            _context = context;
        }

        [Route("GetAllContents")]
        public async Task<ActionResult> GetAllContents()
        {
            return Ok(_context.Contents);
        }
    }
}
