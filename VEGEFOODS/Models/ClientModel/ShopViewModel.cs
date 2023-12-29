using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEGEFOODS.Models.ClientModel
{
    public class ShopViewModel
    {
        public List<category> categories ;
        public List<product> products ;
        public int page { get; set; }
        public int maxPage { get; set; }
        public int categoryNow {  get; set; }

        public ShopViewModel(List<category> categories, List<product> products, int page, int maxPage, int categoryNow)
        {
            this.categories = categories;
            this.products = products;
            this.page = page;
            this.maxPage =maxPage;
            this.categoryNow = categoryNow;
        }
    }
}