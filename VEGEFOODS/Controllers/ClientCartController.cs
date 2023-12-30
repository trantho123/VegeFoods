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
    public class ClientCartController : Controller
    {
        private readonly VegeFoodsDbContext dbContext;
        public ClientCartController()
        {
            dbContext = new VegeFoodsDbContext();
        }
        // GET: ClientCart
        [HttpGet]
        public ActionResult Cart()
        {
            var userid = Session["IdUser"];
            if (userid != null)
            {
                ViewBag.CheckLogin = userid;
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart != null)
                {
                    return View(cart.Items);
                }
                //   return RedirectToAction("Login", "ClientLogin");
                return View();
            }
            return View(); 
        }
        [HttpGet]
        public ActionResult CheckOut()
        {
            /*var userid = Session["IdUser"];
            if(userid == null)
            {
                return RedirectToAction("Login", "ClientLogin");
            }*/

            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                ViewBag.CheckCart = cart;
                ViewBag.CheckItemCart = cart.Items.Count();
            }
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(OrderViewModel order)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                ViewBag.CheckCart = cart;
                ViewBag.CheckItemCart = cart.Items.Count();
            }
            if (ModelState.IsValid)
            {
                user user = (user)Session["User"];
                order checkOut = new order
                {
                    user_id = user.id,
                    total_prices = cart.GetTotal(),
                    created_date = DateTime.Now,
                    receiver = order.Name,
                    receiverPhone = order.Phone,
                    address = order.Address,
                    order_details = new List<order_details>()
                };
                foreach (var item in cart.Items)
                {
                    var orderDetail = new order_details
                    {
                        product_id = item.Product.id,
                        quantity = item.Quantity,
                        price = item.Total,
                    };

                    checkOut.order_details.Add(orderDetail);
                }
                dbContext.orders.Add(checkOut);
                dbContext.SaveChanges();
                Session.Remove("Cart");
                return View("SuccessCheckOut");
            }
           
            return View(order);

        }
        [HttpGet]
        public ActionResult Partial_Item_Cart()
        {
            /*var userid = Session["IdUser"];
            if (userid == null)
            {
                return RedirectToAction("Login", "ClientLogin");
            }*/
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return PartialView(cart.Items);
            }
            return PartialView();

        }

        public ActionResult Partial_Item_Order()
        {
            /*var userid = Session["IdUser"];
            if (userid == null)
            {
                return RedirectToAction("Login", "ClientLogin");
            }*/
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return PartialView(cart.Items);
            }
            return PartialView();

        }
        public ActionResult WrongCart()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var userid = Session["IdUser"];
          
            var code = new
            {
                Success = false,
                msg = "Them san pham that bai",
                code = -1,
                Count = 0
            };
            if (userid == null)
            {
                code = new
                {
                    Success = false,
                    msg = "Vui long dang nhap truoc khi them vao gio hang",
                    code = -1,
                    Count = 0
                };
                return Json(code);
            }
            var checkProduct = dbContext.products.FirstOrDefault(x => x.id == id);
            if(checkProduct != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if(cart == null)
                {
                    cart = new ShoppingCart();
                }
                ShoppingCartItem item = new ShoppingCartItem();
                item.Product = checkProduct;
                item.Quantity = quantity;
                item.Total = (double)item.Product.price * quantity;
                cart.AddToCart(item, quantity);
                Session["Cart"] = cart;
                code = new
                {
                    Success = true,
                    msg = "Them san pham vao gio thanh cong",
                    code = 1,
                    Count = cart.Items.Count
                };

            }
            return Json(code);
        }
        [HttpGet]
        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if(cart != null)
            {
                return Json(new { Count = cart.Items.Count },JsonRequestBehavior.AllowGet);
            }
            return Json(new { Count = 0 }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            var code = new
            {
                Success = false,
                msg = "",
                code = -1
            };
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                var checkProduct = cart.Items.FirstOrDefault(x => x.Product.id == id);
                if (checkProduct != null)
                {
                    cart.UpdateQuantity(id, quantity);
                    code = new
                    {
                        Success = true,
                        msg = "Thanh cong",
                        code = 1
                    };
                }
            }
            return Json(code);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new
            {
                Success = false,
                msf = "",
                code = -1,
                Count = 0
            };

            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                var checkProduct = cart.Items.FirstOrDefault(x => x.Product.id == id);
                if(checkProduct != null)
                {
                    cart.RemoveFromCart(id);
                    code = new
                    {
                        Success = true,
                        msf = "",
                        code = 1,
                        Count = cart.Items.Count
                    };
                }
  
            }
            return Json(code);
        }

    }
}