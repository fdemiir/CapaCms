using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmsCapaMedikalAPI.Helper;
using CmsCapaMedikalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CmsCapaMedikalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        DbManager db = new DbManager();

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
