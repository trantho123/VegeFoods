using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using VEGEFOODS.Models;
using VEGEFOODS.Models.ClientModel;

namespace VEGEFOODS.Controllers
{
    public class ClientShopController : Controller
    {
        private readonly VegeFoodsDbContext dbContext;
        public ClientShopController()
        {
            dbContext = new VegeFoodsDbContext();
        }
  
        public ActionResult Shop(int? categoryId, int? page)
        {

            List<product> res;
            List<product> products;
            int pageSize = 8;
            var category = dbContext.categories.ToList();
            if (page == null)
            {
                page = 1;
            }
            if(categoryId == 0 || categoryId == null)
            {
                products = dbContext.products.ToList();
               
                categoryId = 0;
            }
            else
            {
                products = dbContext.products.Where(u => u.category_id == categoryId).ToList();
            }
            int maxPage = products.Count() / 8 + 1;
            res = products.Skip(((int)page - 1) * pageSize).Take(pageSize).ToList();
            ShopViewModel shopViewModel = new ShopViewModel(category, res, (int)page, maxPage, (int)categoryId);
            return View(shopViewModel);
        }
       
    }
}