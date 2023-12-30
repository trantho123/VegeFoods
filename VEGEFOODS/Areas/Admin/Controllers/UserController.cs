using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VEGEFOODS.Models;

namespace VEGEFOODS.Areas.Admin.Controllers
{
    public class UserController : Controller
    {

        private VegeFoodsDbContext vegeFoodsDbContext = new VegeFoodsDbContext();

        // GET: Admin/User
        public ActionResult Index(String searchString)
        {

            var users = String.IsNullOrEmpty(searchString)
                ? vegeFoodsDbContext.users.ToList()
                : vegeFoodsDbContext.users
                .Where(u => u.id.ToString().Contains(searchString) || u.name.Contains(searchString)).ToList();

            return View(users);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Role = new SelectList(vegeFoodsDbContext.roles.ToList(), "id", "role_name");
            var user = vegeFoodsDbContext.users.Find(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(user user)
        {
            vegeFoodsDbContext.users.Attach(user);
            vegeFoodsDbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
            vegeFoodsDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var user = vegeFoodsDbContext.users.Find(id);
            if (user != null)
            {
                vegeFoodsDbContext.users.Remove(user);
                vegeFoodsDbContext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public ActionResult Add()
        {
            ViewBag.Role = new SelectList(vegeFoodsDbContext.roles.ToList(), "id", "role_name");
            return View();
        }

        [HttpPost]
        public ActionResult Add(user user)
        {
            vegeFoodsDbContext.users.Add(user);
            vegeFoodsDbContext.SaveChanges();
            return RedirectToAction("Index");
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
                        var user = vegeFoodsDbContext.users.Find(Convert.ToInt32(item));
                        vegeFoodsDbContext.users.Remove(user);
                        vegeFoodsDbContext.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpGet]
        public ActionResult Search(String searchString)
        {
            var searchResults = vegeFoodsDbContext.users
                .Where(u => u.id.ToString().Contains(searchString) || u.name.Contains(searchString))
                .ToList();

            return View("Index", searchResults);
        }
    }
}