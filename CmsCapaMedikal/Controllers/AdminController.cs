using CmsCapaMedikal.Helper;
using CmsCapaMedikal.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;

namespace CmsCapaMedikal.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHostingEnvironment _environment;

        // Constructor
        public AdminController(IHostingEnvironment IHostingEnvironment)
        {
            _environment = IHostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EditProducts()
        {
            var db = new SqlManagerHelper();
            var model = new List<Categories>();
            model = db.GetAllCategories();
            ViewBag.CategoryList = model;
            var prodmodel = new List<Products>();
            prodmodel = db.GetAllProducts();
            ViewBag.ProductList = prodmodel;
            return View();
        }
        public IActionResult ListProducts()
        {
            var db = new SqlManagerHelper();
            var prodmodel = new List<Products>();
            prodmodel = db.GetAllProducts();
            ViewBag.ProductList = prodmodel;
            return View();
        }
        [HttpPost]
        public ActionResult SaveProducts(Products product)
        {
            try
            {
                var newFileName = string.Empty;
                if (HttpContext.Request.Form.Files != null)
                {
                    var fileName = string.Empty;
                    string PathDB = string.Empty;
                    var files = HttpContext.Request.Form.Files;
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                            var FileExtension = Path.GetExtension(fileName);
                            //newFileName = myUniqueFileName + FileExtension;
                            newFileName = product.Code + FileExtension;
                            fileName = Path.Combine(_environment.WebRootPath, "Images") + $@"\{newFileName}";
                            PathDB = "Images/" + newFileName;
                            product.Image = PathDB;
                            using (FileStream fs = System.IO.File.Create(fileName))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                        }
                    }
                }
                var db = new SqlManagerHelper();
                db.InsertProducts(product);
                return RedirectToAction("EditProducts");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
