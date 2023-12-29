using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VEGEFOODS.Models;

namespace VEGEFOODS.Controllers
{
    public class ClientHomeController : Controller
    {
        private readonly VegeFoodsDbContext dbContext;
        public ClientHomeController()
        {
            dbContext = new VegeFoodsDbContext();
        }
        // GET: ClientHome
        public ActionResult Index()
        {
            
            var products = dbContext.products.OrderBy(u => u.id).Take(8).ToList();
            return View(products);
        }
    }
}