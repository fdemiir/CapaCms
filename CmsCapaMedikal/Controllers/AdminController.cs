using CmsCapaMedikal.Helper;
using CmsCapaMedikal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCapaMedikal.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EditProducts()
        {
            var db = new SqlManagerHelper();
            var model = new List<Products>();
            model = db.GetProducts();
            ViewBag.CategoryList = model;
            return View();
        }

        [HttpPost]
        public ActionResult SaveProducts(Products product)
        {
            try
            {
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
