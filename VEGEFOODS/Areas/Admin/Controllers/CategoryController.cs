using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VEGEFOODS.Models;

namespace VEGEFOODS.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {

        private VegeFoodsDbContext vegeFoodsDbContext =new VegeFoodsDbContext();

        // GET: Admin/Category
        public ActionResult Index(String searchString)
        {
            var categoryItems = String.IsNullOrEmpty(searchString)
                ? vegeFoodsDbContext.categories.ToList()
                : vegeFoodsDbContext.categories
                .Where(c => c.id.ToString().Contains(searchString) || c.category_name.Contains(searchString)).ToList();

            return View(categoryItems);
        }

        public ActionResult Add()
        {
            return View();
        }

       [HttpPost]
        public ActionResult Add(category category)
        {

            vegeFoodsDbContext.categories.Add(category);
            vegeFoodsDbContext.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            var category = vegeFoodsDbContext.categories.Find(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(category category)
        {
            vegeFoodsDbContext.categories.Attach(category);
            //vegeFoodsDbContext.Entry(category).Property(x => x.id).IsModified = true;
            vegeFoodsDbContext.Entry(category).Property(x => x.category_name).IsModified = true;
            vegeFoodsDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var category = vegeFoodsDbContext.categories.Find(id);
            if (category != null)
            {
                vegeFoodsDbContext.categories.Remove(category);
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
                    foreach(var item in items)
                    {
                        var category= vegeFoodsDbContext.categories.Find(Convert.ToInt32(item));
                        vegeFoodsDbContext.categories.Remove(category);
                        vegeFoodsDbContext.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpGet]
        public ActionResult search(String searchString)
        {
            var searchResults = vegeFoodsDbContext.categories
                .Where(c => c.id.ToString().Contains(searchString) || c.category_name.Contains(searchString)).ToList();

            return View("Index", searchResults);
        }
    }
}