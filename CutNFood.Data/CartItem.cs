using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using CutNFood.Data.Data;

namespace CutNFood.Data
{
    public class CartItem
    {
        //Add product to cart
        public void AddCartItem(int cartId, int productId, int noOfItems)
        {
            DbConnection connection = null;
            try
            {
                connection = Helper.GetOpenGetConnection();
                using (var context = new DBCUTNFOODEntities(connection, false))
                {
                    var cart = context.tbl_cart.Find(cartId);
                    var prod = context.tbl_products.Find(productId);

                    var cartItemToAdd = new tbl_cartItem
                    {
                        cart_id = cartId,
                        product_id = productId,
                        no_of_items =noOfItems,
                        item_totalPrice = noOfItems * prod.productPrice
                    };
                    
                    context.tbl_cartItem.Add(cartItemToAdd);
                    context.SaveChanges();

                    //calculate cart total price
                    decimal updatedTotalPrice = (decimal)0.0;
                    var items = context.tbl_cartItem.Where(x => x.cart_id == cartId).ToList<tbl_cartItem>();
                    foreach(var item in items)
                    {
                        updatedTotalPrice = updatedTotalPrice + item.item_totalPrice??(decimal)0.0;
                    }
                    if(updatedTotalPrice != cart.totalPrice)
                    {
                        cart.totalPrice = updatedTotalPrice;
                    }
                    context.SaveChanges();

                }
            }
            finally
            {
                if (connection != null) connection.Close();
            }

        }

        //Edit
        public void EditCartItem(int cartId, int cartItemId, int updatedNoOfItems)
        {
            DbConnection connection = null;
            try
            {
                connection = Helper.GetOpenGetConnection();
                using (var context = new DBCUTNFOODEntities(connection, false))
                {
                    var cart = context.tbl_cart.Find(cartId);
                    var cartItem = context.tbl_cartItem.Find(cartItemId);
                    var prod = context.tbl_products.Find(cartItem.product_id);

                    if (updatedNoOfItems != cartItem.no_of_items)
                    {
                        cartItem.no_of_items = updatedNoOfItems;
                        cartItem.item_totalPrice = updatedNoOfItems * prod.productPrice;
                    }
                    context.SaveChanges();

                    //calculate cart total price
                    decimal updatedTotalPrice = (decimal)0.0;
                    var items = context.tbl_cartItem.Where(x => x.cart_id == cartId).ToList<tbl_cartItem>();
                    foreach (var item in items)
                    {
                        updatedTotalPrice = updatedTotalPrice + item.item_totalPrice ?? (decimal)0.0;
                    }
                    if (updatedTotalPrice != cart.totalPrice)
                    {
                        cart.totalPrice = updatedTotalPrice;
                    }
                    context.SaveChanges();
                }
            }
            finally
            {
                if (connection != null) connection.Close();
            }

        }

        //Delete
        public void DeleteCartItem(int cartId, int cartItemId)
        {
            DbConnection connection = null;
            try
            {
                connection = Helper.GetOpenGetConnection();
                using (var context = new DBCUTNFOODEntities(connection, false))
                {
                    var cart = context.tbl_cart.Find(cartId);
                    var cartItem = context.tbl_cartItem.Find(cartItemId);

                    context.tbl_cartItem.Remove(cartItem);
                    context.SaveChanges();

                    //calculate cart total price
                    decimal updatedTotalPrice = (decimal)0.0;
                    var items = context.tbl_cartItem.Where(x => x.cart_id == cartId).ToList<tbl_cartItem>();
                    foreach (var item in items)
                    {
                        updatedTotalPrice = updatedTotalPrice + item.item_totalPrice ?? (decimal)0.0;
                    }
                    if (updatedTotalPrice != cart.totalPrice)
                    {
                        cart.totalPrice = updatedTotalPrice;
                    }
                    context.SaveChanges();
                }
            }
            finally
            {
                if (connection != null) connection.Close();
            }

        }

        //View cart items by cart id, by user id
        public List<tbl_cartItem> GetCartItemsByCart(int cartId)
        {
            DbConnection connection = null;
            List<tbl_cartItem> listCartItems = null;

            connection = Helper.GetOpenGetConnection();
            using (var context = new DBCUTNFOODEntities(connection, false))
            {
                listCartItems = context.tbl_cartItem.Where(x => x.cart_id == cartId).ToList<tbl_cartItem>();
            }
            return listCartItems;
        }

        public tbl_cartItem GetCartItemById(int cartItemId)
        {
            DbConnection connection = null;
            tbl_cartItem cartItem = null;

            connection = Helper.GetOpenGetConnection();
            using (var context = new DBCUTNFOODEntities(connection, false))
            {
                cartItem = context.tbl_cartItem.Find(cartItemId);
            }
            return cartItem;
        }
    }
}
