using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CutNFood.Data.Data;
using CutNFood.Web.Models;
using System.IO;

namespace CutNFood.Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menu()
        {
            ViewBag.Message = "Display products here.";

            var prod = new CutNFood.Data.Product();
            List<tbl_products> products = prod.GetAllProducts();
            return View(products);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            var category = new CutNFood.Data.Category();
            var selectListCategories = new List<SelectListItem>();
            selectListCategories.Add(new SelectListItem
            {
                Text = "-Please select-",
                Value = "Select an item"
            });
            List<tbl_category> categories = category.GetAllFoodCategories();
            foreach(var cat in categories)
            {
                selectListCategories.Add(new SelectListItem
                {
                    Text = cat.categoryName,
                    Value = cat.category_id.ToString()
                });
            }

            var model = new ProductViewModel()
            {
                categories = selectListCategories
            };
            return View(model);
        }

        //
        // POST: /Product/Register
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(ProductViewModel model)
        {
            byte[] bytes = null;
            var product = new CutNFood.Data.Product();
            if(model.postedFile != null && model.postedFile.ContentLength > 0)
            {
                using (BinaryReader br = new BinaryReader(model.postedFile.InputStream))
                {
                    bytes = br.ReadBytes(model.postedFile.ContentLength);
                }
            }
            
            product.AddProduct(model.product.category_id, model.product.productName, model.product.productPrice, model.product.isAvailable, bytes);
            return RedirectToAction("Menu", "Product");
        }

        [AllowAnonymous]
        public ActionResult EditProduct(int productId=0)
        {
            var category = new CutNFood.Data.Category();
            var product = new CutNFood.Data.Product();
            var selectListCategories = new List<SelectListItem>();
            var productToEdit = product.GetProductById(productId);

            if(productToEdit != null)
            {
                selectListCategories.Add(new SelectListItem
                {
                    Text = "-Please select-",
                    Value = "Select an item"
                });
                List<tbl_category> categories = category.GetAllFoodCategories();
                foreach (var cat in categories)
                {
                    selectListCategories.Add(new SelectListItem
                    {
                        Text = cat.categoryName,
                        Value = cat.category_id.ToString()
                    });
                }
            }
            
            var model = new ProductViewModel()
            {
                categories = selectListCategories,
                product = productToEdit
            };
            return View(model);
        }

        //
        // POST: /Product/EditProduct
        [HttpPost]
        [AllowAnonymous]
        public ActionResult EditProduct(ProductViewModel model)
        {
            byte[] bytes = null;
            var product = new CutNFood.Data.Product();

            if (model.postedFile != null && model.postedFile.ContentLength > 0)
            {
                using (BinaryReader br = new BinaryReader(model.postedFile.InputStream))
                {
                    bytes = br.ReadBytes(model.postedFile.ContentLength);
                }
            }
            bytes = (bytes != null && bytes != model.product.productImage) ? bytes : model.product.productImage;
            
            product.EditProduct(model.product.product_id, model.product.productName, model.product.productPrice, model.product.isAvailable, bytes);
            return RedirectToAction("Menu", "Product");
        }

        [AllowAnonymous]
        public ActionResult Delete(int productId = 0)
        {
            var product = new CutNFood.Data.Product();
            var productToEdit = product.GetProductById(productId);

            if (productToEdit != null)
            {
                product.DeleteProduct(productId);
            }
            return RedirectToAction("Menu", "Product");
        }

        [AllowAnonymous]
        public ActionResult AddToCart(int productId = 0, string username = "")
        {
            int cartId = 0;
            var product = new CutNFood.Data.Product();
            var user = new CutNFood.Data.Account();
            var cart = new CutNFood.Data.Cart();
            var cartItem = new CutNFood.Data.CartItem();

            int userId = string.IsNullOrWhiteSpace(username) ? 0 : user.GetUser(username).user_id;
            var userCart = cart.GetCartByUserId(userId);

            if (userId > 0 && userCart == null)
            {
                //add cart
                cart.AddCart(userId, (decimal)0.0, out cartId);
            }
            else if (userId > 0 && userCart != null)
            {
                cartId = userCart.cart_id;
            }
            
            var productToCart = product.GetProductById(productId);
            if (productToCart != null && cartId > 0)
            {
                var userCartItems = cartItem.GetCartItemsByCart(cartId);
                var existedItem = userCartItems.FirstOrDefault(x => x.product_id == productId);

                //add product to cartitem tbl
                if (existedItem != null)
                {
                    int updatedNoOfItem = (existedItem.no_of_items ?? 0) + 1;
                    cartItem.EditCartItem(cartId, existedItem.cartItem_id, updatedNoOfItem);
                }
                else
                {
                    cartItem.AddCartItem(cartId, productId, 1);
                }
                
            }

            return RedirectToAction("ViewUserCart", "Cart", new { username = username });
        }

        
    }
}