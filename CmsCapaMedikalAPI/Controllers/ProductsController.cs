using System;
using System.Collections.Generic;
using CmsCapaMedikal.Helper;
using CmsCapaMedikal.Models;
using Microsoft.AspNetCore.Mvc;

namespace CmsCapaMedikalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        SqlManagerHelper db = new SqlManagerHelper();

        public ActionResult<List<Products>> GetAllProducts()
        {
            try
            {
                return db.GetAllProducts();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
