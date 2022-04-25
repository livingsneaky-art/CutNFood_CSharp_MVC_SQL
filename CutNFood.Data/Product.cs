using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CutNFood.Data.Data;
using System.Data.Common;
//using System.Data.Entity.Core.EntityClient;
//using System.Data.SqlClient;

namespace CutNFood.Data
{
    public class Product
    {
        public Product()
        {
            
        }
        //----CRUD-----------
        //Create
        //Add
        public void AddProduct(int categoryId, string productName, decimal price, bool isAvailable, byte[] prodImage = null)
        {
            DbConnection connection = null;
            try
            {
                connection = Helper.GetOpenGetConnection();
                using (var context = new DBCUTNFOODEntities(connection, false))
                {
                    var category = context.tbl_category.Find(categoryId);
                    
                    var productToAdd = new tbl_products
                    {
                        productName = productName,
                        productPrice = price,
                        tbl_category = category,
                        isAvailable = isAvailable
                    };
                    if(prodImage != null)
                    {
                        productToAdd.productImage = prodImage;
                    }

                    context.tbl_products.Add(productToAdd);
                    context.SaveChanges();
                }
            }
            finally
            {
                if (connection != null) connection.Close();
            }

        }

        //Update
        //Edit
        public void EditProduct(int productId, string updatedProductName = "", decimal? updatedProductPrice = null, bool isAvailable = false, byte[] prodImage = null)
        {
            DbConnection connection = null;
            try
            {
                connection = Helper.GetOpenGetConnection();
                using (var context = new DBCUTNFOODEntities(connection, false))
                {
                    var prod = context.tbl_products.Find(productId);
                    if(prod != null)
                    {
                        if (!string.IsNullOrWhiteSpace(updatedProductName) && updatedProductName != prod.productName)
                        {
                            prod.productName = updatedProductName;
                        }
                        if (updatedProductPrice != null && updatedProductPrice != prod.productPrice)
                        {
                            prod.productPrice = updatedProductPrice ?? (decimal)0.0;
                        }
                        if (isAvailable != prod.isAvailable)
                            prod.isAvailable = isAvailable;
                        if (prodImage != prod.productImage)
                            prod.productImage = prodImage;
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
        public void DeleteProduct(int productId)
        {
            DbConnection connection = null;
            try
            {
                connection = Helper.GetOpenGetConnection();
                using (var context = new DBCUTNFOODEntities(connection, false))
                {
                    var prod = context.tbl_products.Find(productId);
                    context.tbl_products.Remove(prod);
                    context.SaveChanges();
                }
            }
            finally
            {
                if (connection != null) connection.Close();
            }

        }

        //Retrieve
        //Get All
        public List<tbl_products> GetAllProducts()
        {
            DbConnection connection = null;
            List<tbl_products> products = null;

            connection = Helper.GetOpenGetConnection();
            using (var context = new DBCUTNFOODEntities(connection, false))
            {
                products = context.tbl_products.ToList<tbl_products>();
                foreach(var prod in products)
                {
                    prod.tbl_category = context.tbl_category.Find(prod.category_id);
                }
            }
            return products;
        }

        //Get Single
        public tbl_products GetProductBySearchName(string searchProductName)
        {
            DbConnection connection = null;
            tbl_products product = null;

            connection = Helper.GetOpenGetConnection();
            using (var context = new DBCUTNFOODEntities(connection, false))
            {
                product = context.tbl_products.FirstOrDefault(x => x.productName == searchProductName);
            }
            return product;
        }

        public tbl_products GetProductById(int productId)
        {
            DbConnection connection = null;
            tbl_products product = null;

            connection = Helper.GetOpenGetConnection();
            using (var context = new DBCUTNFOODEntities(connection, false))
            {
                product = context.tbl_products.Find(productId);
                if(product != null)
                product.tbl_category = context.tbl_category.Find(product.category_id);
            }
            return product;
        }

    }
}
