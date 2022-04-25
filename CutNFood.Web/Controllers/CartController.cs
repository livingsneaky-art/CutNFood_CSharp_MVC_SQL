using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CutNFood.Data.Data;
using CutNFood.Web.Models;

namespace CutNFood.Web.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ViewUserCart(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) RedirectToAction("Menu", "Product");

            ViewBag.Message = "Display my cart items here.";
            var user = new CutNFood.Data.Account();
            var cart = new CutNFood.Data.Cart();
            var cartItem = new CutNFood.Data.CartItem();

            int userId = string.IsNullOrWhiteSpace(username) ? 0 : user.GetUser(username).user_id;
            var userCartModel = new UserCartViewModel();
            var cartItems = new List<CartItem>();

            var userCart = cart.GetCartByUserId(userId);
            if(userCart != null)
            {
                var userCartItems = cartItem.GetCartItemsByCart(userCart.cart_id);
                foreach (var item in userCartItems)
                {
                    var cItem = new CartItem
                    {
                        cartItem_id = item.cartItem_id,
                        product_id = item.product_id,
                        no_of_items = item.no_of_items,
                        item_totalPrice = item.item_totalPrice,
                        cart_id = item.cart_id
                    };

                    var product = new CutNFood.Data.Product();
                    var cProduct = product.GetProductById(item.product_id ?? 0);
                    cItem.product = cProduct;
                    cItem.category = cProduct.tbl_category;

                    cartItems.Add(cItem);
                }

                userCartModel.Cart = userCart;
                userCartModel.CartItems = cartItems;
            }
            
            return View(userCartModel);
        }

        [AllowAnonymous]
        public ActionResult DeleteCartItem(string username, int cartId, int cartItemId)
        {
            var cartItem = new CutNFood.Data.CartItem();
            var itemToDelete = cartItem.GetCartItemById(cartItemId);

            if (itemToDelete != null)
            {
                cartItem.DeleteCartItem(cartId, cartItemId);
            }
            return RedirectToAction("ViewUserCart", "Cart", new { username = username });
        }

        //View Carts Summary
        [AllowAnonymous]
        public ActionResult ViewCarts()
        {
            var cartViewModel = new CartViewModel();
            var list = new List<CustomerCart>();

            var cart = new CutNFood.Data.Cart();
            var carts = cart.GetAllUnPaidCarts();

            foreach(var item in carts)
            {
                var customerCart = new CustomerCart
                {
                    Cart = item,
                    Customer = item.tbl_account
                };
                list.Add(customerCart);
            }
            cartViewModel.Carts = list;
            return View(cartViewModel);
        }

        //CheckOut / Paid
        [AllowAnonymous]
        public ActionResult CheckOut(int cartId)
        {
            var cart = new CutNFood.Data.Cart();
            cart.EditCart(cartId, true);
            return RedirectToAction("ViewCarts", "Cart");
        }

    }
}