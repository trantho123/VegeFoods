using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VEGEFOODS.Models;
using VEGEFOODS.Models.ClientModel;

namespace VEGEFOODS.Controllers
{
    public class ClientLoginController : Controller
    {
       private readonly VegeFoodsDbContext dbContext;
        public ClientLoginController()
        {
            dbContext = new VegeFoodsDbContext();
        }
        // GET: ClientLogin
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginModel)
        {
            
            if(ModelState.IsValid)
            {
                var user = dbContext.users.FirstOrDefault(u => u.email.Equals(loginModel.email) &&  u.password.Equals(loginModel.password));
                if(user != null)
                {
                    Session["User"] = user;
                    Session["IdUser"] = user.id.ToString();
                    Session["UserName"] = user.name.ToString();
                    return RedirectToAction("Index", "ClientHome");
                }
                else
                {
                    ViewBag.Notification = "Wrong user name of password.";
                }

                
            }
            return View(loginModel);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
      
        [HttpPost]
        public ActionResult SignUp(SignUpViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = dbContext.users.FirstOrDefault(u => u.email.Equals(model.email));
                if(user != null)
                {
                    ViewBag.Notification = "This email has already exitsted";
                }
                else
                {
                    try
                    {
                        user newUser = new user();
                        newUser.name = model.userName;
                        newUser.email = model.email;
                        newUser.password = model.password;
                        newUser.role_id = 1;
                        dbContext.users.Add(newUser);
                        dbContext.SaveChanges();
                        return RedirectToAction("Login", "ClientLogin");
                    }
                    catch
                    {
                        ViewBag.Notification = "Error";
                    }
                }
            }
            return View(model);
        }
    }
}