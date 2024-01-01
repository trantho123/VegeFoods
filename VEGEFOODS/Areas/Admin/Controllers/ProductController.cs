using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VEGEFOODS.Models;

namespace VEGEFOODS.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {

        private VegeFoodsDbContext vegeFoodsDbContext = new VegeFoodsDbContext();

        public ActionResult Index(string searchString)
        {
            var productItems = string.IsNullOrEmpty(searchString)
                ? vegeFoodsDbContext.products.ToList()
                : vegeFoodsDbContext.products
                    .Where(p => p.name.Contains(searchString) || p.id.ToString().Contains(searchString))
                    .ToList();

            return View(productItems);
        }


        public ActionResult Add()
        {
            ViewBag.Category = new SelectList(vegeFoodsDbContext.categories.ToList(), "id", "category_name");
            return View();
        }
        [HttpPost]
        public ActionResult Add(product product)
        {
            vegeFoodsDbContext.products.Add(product);
            vegeFoodsDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Category = new SelectList(vegeFoodsDbContext.categories.ToList(), "id", "category_name");
            var productItem = vegeFoodsDbContext.products.Find(id);
            return View(productItem);
        }

        [HttpPost]
        public ActionResult Edit(product product)
        {
            vegeFoodsDbContext.products.Attach(product);
            vegeFoodsDbContext.Entry(product).State = System.Data.Entity.EntityState.Modified;
            vegeFoodsDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var product = vegeFoodsDbContext.products.Find(id);
            if (product != null)
            {
                vegeFoodsDbContext.products.Remove(product);
                vegeFoodsDbContext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult DeleteAll(String ids)
        {
            if (!String.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if(items!=null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var product = vegeFoodsDbContext.products.Find(Convert.ToInt32(item));
                        vegeFoodsDbContext.products.Remove(product);
                        vegeFoodsDbContext.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpGet]
        public ActionResult Search(string searchString)
        {
            var searchResults = vegeFoodsDbContext.products
                .Where(p => p.name.Contains(searchString) || p.id.ToString().Contains(searchString))
                .ToList();

            return View("Index", searchResults);
        }

    }
}