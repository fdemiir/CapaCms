using CmsCapaMedikal.Helper;
using CmsCapaMedikal.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CmsCapaMedikal.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductsContext _context;
        private readonly IHostingEnvironment _environment;

        // Constructor
        public ProductsController(ProductsContext context, IHostingEnvironment IHostingEnvironment)
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

            return View(_context.Products);

            //var db = new SqlManagerHelper();
            //var prodmodel = new List<Products>();
            //prodmodel = db.GetAllProducts();
            //ViewBag.ProductList = prodmodel;
            //return View();
        }
        public IActionResult Details(int id)
        {
            var prod = _context.Products.Where(x => x.Id == id).SingleOrDefault();
            return View(prod);
        }

        public IActionResult Delete(int id)
        {
            var prod = _context.Products.Where(x => x.Id == id).SingleOrDefault();
            return View(prod);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var prod = _context.Products.Where(x => x.Id == id).SingleOrDefault();
            _context.Products.Remove(prod);
            _context.SaveChanges();

            return RedirectToAction("List");
        }
        public IActionResult Create()
        {
            var cat = _context.Categories;
            ViewBag.CategoryList = cat;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Products prod)
        {
            var filePath = Path.Combine(_environment.WebRootPath, "Images");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            var fullPath = Path.Combine(filePath, prod.ProductsFile.FileName);
            using (var fileFlood = new FileStream(fullPath, FileMode.Create)) 
            {
                await prod.ProductsFile.CopyToAsync(fileFlood);
            }
            prod.ProductImage = prod.ProductsFile.FileName;
            _context.Products.Add(prod);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }
        public IActionResult Edit(int id)
        {
            var prod = _context.Products.SingleOrDefault(x => x.Id == id);
            var cat = _context.Categories;
            ViewBag.CategoryList = cat;
            return View(prod);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Products prod)
        {
            var newProd = _context.Products.SingleOrDefault(x => x.Id == prod.Id);
            await TryUpdateModelAsync(newProd);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

    }
}
