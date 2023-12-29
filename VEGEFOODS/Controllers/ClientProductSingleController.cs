using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VEGEFOODS.Models;

namespace VEGEFOODS.Controllers
{
    public class ClientProductSingleController : Controller
    {
        private readonly VegeFoodsDbContext dbContext;
        public ClientProductSingleController()
        {
            dbContext = new VegeFoodsDbContext();
        }
        // GET: ClientProductSingle
        public ActionResult Product(int? ID)
        {
            if(ID  == null) { 
                return RedirectToAction("Index", "Home");
            }
            var product = dbContext.products.FirstOrDefault(u => u.id == ID);
           
            return View(product);
        }
    }
}