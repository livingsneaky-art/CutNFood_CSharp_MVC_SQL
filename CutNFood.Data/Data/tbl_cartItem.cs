//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CutNFood.Data.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_cartItem
    {
        public int cartItem_id { get; set; }
        public Nullable<int> product_id { get; set; }
        public Nullable<int> no_of_items { get; set; }
        public Nullable<decimal> item_totalPrice { get; set; }
        public Nullable<int> cart_id { get; set; }
    
        public virtual tbl_cart tbl_cart { get; set; }
        public virtual tbl_products tbl_products { get; set; }
        public virtual tbl_products tbl_products1 { get; set; }
    }
}