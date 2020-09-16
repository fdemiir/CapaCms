using CmsCapaMedikal.Helper;
using CmsCapaMedikal.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
            var model = new List<Categories>();
            model = db.GetAllCategories();
            ViewBag.CategoryList = model;
            return View();
        }

        [HttpPost]
        public ActionResult SaveProducts(Products product)
        {
            try
            {
                foreach (var file in Request.Form.Files)
                {
                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    product.Photo = ms.ToArray();

                    ms.Close();
                    ms.Dispose();

                    //product.Photo = db.Images.Add(img);
                    //db.SaveChanges();
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
