using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEGEFOODS.Models.ClientModel
{
   public class ShoppingCart
    {
        public List<ShoppingCartItem> Items;
        public ShoppingCart()
        {
            Items = new List<ShoppingCartItem>();
        }
        public void AddToCart(ShoppingCartItem item, int quantity)
        {
            var checkExits = Items.FirstOrDefault(x => x.Product.id == item.Product.id);
            if (checkExits != null)
            {
                checkExits.Quantity += quantity;
                checkExits.Total = (double)checkExits.Product.price*checkExits.Quantity;
            }
            else { Items.Add(item); }
        }
        public void RemoveFromCart(int id)
        {
            var checkExits = Items.SingleOrDefault(x => x.Product.id == id);
            if(checkExits != null)
            {
                Items.Remove(checkExits);
            }
        }
        public void UpdateQuantity(int id, int quantity)
        {
            var checkExits= Items.SingleOrDefault(x => x.Product.id == id); 
            if(checkExits != null)
            {
                checkExits.Quantity = quantity;
                checkExits.Total = (double)checkExits.Product.price * checkExits.Quantity;
            }
        }
        public double GetTotal()
        {
            double total = 0;

            foreach (var item in Items)
            {
                total = total + item.Total;
            }

            return total;
        }
        public void ClearCart()
        {
            Items.Clear();
        }

    }
    public class ShoppingCartItem
    {
        public product Product { get; set; }
        public int Quantity {  get; set; }
        public double Total {  get; set; }
    }
}