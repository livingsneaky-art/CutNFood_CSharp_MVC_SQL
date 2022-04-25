using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using CutNFood.Data.Data;

namespace CutNFood.Data
{
    public class Cart
    {
        //Add
        public void AddCart(int userId, decimal totalPrice, out int cartId)
        {
            cartId = 0;
            DbConnection connection = null;
            try
            {
                connection = Helper.GetOpenGetConnection();
                using (var context = new DBCUTNFOODEntities(connection, false))
                {
                    
                    var cartToAdd = new tbl_cart
                    {
                        user_id = userId,
                        totalPrice = totalPrice
                    };

                    context.tbl_cart.Add(cartToAdd);
                    context.SaveChanges();
                    cartId = cartToAdd.cart_id;
                }
            }
            finally
            {
                if (connection != null) connection.Close();
            }

        }

        //Edit
        public void EditCart(int cartId, bool? isCheckOut = null, decimal? updatedTotalPrice = null)
        {
            DbConnection connection = null;
            try
            {
                connection = Helper.GetOpenGetConnection();
                using (var context = new DBCUTNFOODEntities(connection, false))
                {
                    var cart = context.tbl_cart.Find(cartId);
                    if(isCheckOut != null && cart.isCheckOut != isCheckOut)
                    {
                        cart.isCheckOut = isCheckOut ?? false;
                    }
                    
                    if (updatedTotalPrice != null && updatedTotalPrice != cart.totalPrice)
                    {
                        cart.totalPrice = updatedTotalPrice ?? (decimal)0.0;
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
        public void DeleteCart(int cartId)
        {
            DbConnection connection = null;
            try
            {
                connection = Helper.GetOpenGetConnection();
                using (var context = new DBCUTNFOODEntities(connection, false))
                {
                    var cart = context.tbl_cart.Find(cartId);
                    context.tbl_cart.Remove(cart);
                    context.SaveChanges();
                }
            }
            finally
            {
                if (connection != null) connection.Close();
            }

        }

        //View cart total price
        public tbl_cart GetCartByCartId(int cartId)
        {
            DbConnection connection = null;
            tbl_cart cart = null;

            connection = Helper.GetOpenGetConnection();
            using (var context = new DBCUTNFOODEntities(connection, false))
            {
                cart = context.tbl_cart.Find(cartId);
            }
            return cart;
        }

        //Get user's current cart
        public tbl_cart GetCartByUserId(int userId)
        {
            DbConnection connection = null;
            tbl_cart cart = null;

            connection = Helper.GetOpenGetConnection();
            using (var context = new DBCUTNFOODEntities(connection, false))
            {
                cart = context.tbl_cart.FirstOrDefault(x => x.user_id == userId && x.isCheckOut == false);
            }
            return cart;
        }

        //Get all pending carts, GetAllUnPaidCarts
        public List<tbl_cart> GetAllUnPaidCarts()
        {
            DbConnection connection = null;
            List<tbl_cart> carts = null;

            connection = Helper.GetOpenGetConnection();
            using (var context = new DBCUTNFOODEntities(connection, false))
            {
                carts = context.tbl_cart.Where(x => x.isCheckOut == false && x.totalPrice > (decimal)0.0).ToList<tbl_cart>();
                foreach (var cart in carts)
                {
                    cart.tbl_account = context.tbl_account.Find(cart.user_id);
                }
            }
            return carts;
        }
    }
}
