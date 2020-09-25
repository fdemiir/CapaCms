using System;
using System.Collections.Generic;
using CmsCapaMedikal.Helper;
using CmsCapaMedikal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CmsCapaMedikalAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        SqlManagerHelper db = new SqlManagerHelper();

        public ActionResult<List<Categories>> GetAllProducts()
        {
            try
            {
                return db.GetAllCategories();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
