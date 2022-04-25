using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CutNFood.Data.Data;

namespace CutNFood.Web.Models
{
    public class CartViewModel
    {
        public List<CustomerCart> Carts { get; set; }
    }

    public class CustomerCart
    {
        public tbl_cart Cart { get; set; }
        public tbl_account Customer { get; set; }

    }

}