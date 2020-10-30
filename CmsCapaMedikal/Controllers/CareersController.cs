using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmsCapaMedikal.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CmsCapaMedikal.Controllers
{
    public class CareersController : Controller
    {
        private readonly CapaMedikalContext _context;
        private readonly IHostingEnvironment _environment;

        // Constructor
        public CareersController(CapaMedikalContext context, IHostingEnvironment IHostingEnvironment)
        {
            _context = context;
            _environment = IHostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            return View(_context.Careers);
        }
        public IActionResult Delete(string id)
        {
            var prod = _context.Careers.Where(x => x.Id == id).SingleOrDefault();
            return View(prod);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            var prod = _context.Careers.Where(x => x.Id == id).SingleOrDefault();
            _context.Careers.Remove(prod);
            _context.SaveChanges();

            return RedirectToAction("List");
        }
    }
}
