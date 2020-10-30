using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CmsCapaMedikal.Helper;
using CmsCapaMedikal.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CmsCapaMedikal.Controllers
{
    public class ContentsController : Controller
    {
        private readonly CapaMedikalContext _context;
        private readonly IHostingEnvironment _environment;

        // Constructor
        public ContentsController(CapaMedikalContext context, IHostingEnvironment IHostingEnvironment)
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

            return View(_context.Contents);
        }
        public IActionResult Details(string id)
        {
            var prod = _context.Contents.Where(x => x.Id == id).SingleOrDefault();
            return View(prod);
        }

        public IActionResult Delete(string id)
        {
            var prod = _context.Contents.Where(x => x.Id == id).SingleOrDefault();
            return View(prod);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            var prod = _context.Contents.Where(x => x.Id == id).SingleOrDefault();
            _context.Contents.Remove(prod);
            _context.SaveChanges();

            return RedirectToAction("List");
        }
        public IActionResult Create()
        {
            var cat = _context.Contents;
            ViewBag.CategoryList = cat;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contents content)
        {
            var filePath = Path.Combine(_environment.WebRootPath, "Images");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            var fullPath = Path.Combine(filePath, content.ContentFile.FileName);
            using (var fileFlood = new FileStream(fullPath, FileMode.Create))
            {
                await content.ContentFile.CopyToAsync(fileFlood);
            }
            var contentId = Guid.NewGuid();
            content.Id = contentId.ToString().Replace("-", "");
            content.ContentImage = content.ContentFile.FileName;
            _context.Contents.Add(content);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }
        public IActionResult Edit(string id)
        {
            var prod = _context.Contents.SingleOrDefault(x => x.Id == id);
            return View(prod);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Contents content)
        {
            var newProd = _context.Contents.SingleOrDefault(x => x.Id == content.Id);
            await TryUpdateModelAsync(newProd);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

    }
}
