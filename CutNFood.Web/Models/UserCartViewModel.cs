using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CutNFood.Data.Data;

namespace CutNFood.Web.Models
{
    public class UserCartViewModel
    {
        public tbl_cart Cart { get; set; }
        public List<CartItem> CartItems { get; set; }

    }

    public class CartItem
    {
        public int? cartItem_id { get; set; }
        public Nullable<int> product_id { get; set; }
        public Nullable<int> no_of_items { get; set; }
        public Nullable<decimal> item_totalPrice { get; set; }
        public Nullable<int> cart_id { get; set; }

        public tbl_products product { get; set; }
        public tbl_category category { get; set; }
    }

}