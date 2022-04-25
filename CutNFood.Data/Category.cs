using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CutNFood.Data.Data;
using System.Data.Common;

namespace CutNFood.Data
{
    public class Category
    {
        public Category()
        {

        }

        //Add
        public void AddCategory(string categoryName)
        {
            DbConnection connection = null;
            try
            {
                connection = Helper.GetOpenGetConnection();
                using (var context = new DBCUTNFOODEntities(connection, false))
                {
                    var categoryToAdd = new tbl_category
                    {
                        categoryName = categoryName
                    };

                    context.tbl_category.Add(categoryToAdd);
                    context.SaveChanges();
                }
            }
            finally
            {
                if (connection != null) connection.Close();
            }

        }
        
        //All
        public List<tbl_category> GetAllFoodCategories()
        {
            DbConnection connection = null;
            List<tbl_category> categories = null;

            connection = Helper.GetOpenGetConnection();
            using (var context = new DBCUTNFOODEntities(connection, false))
            {
                categories = context.tbl_category.ToList<tbl_category>();
            }
            return categories;
        }

        //Single
        public tbl_category GetFoodCategory(string searchCategory)
        {
            DbConnection connection = null;
            tbl_category category = null;

            connection = Helper.GetOpenGetConnection();
            using (var context = new DBCUTNFOODEntities(connection, false))
            {
                category = context.tbl_category.FirstOrDefault(x => x.categoryName == searchCategory);
            }
            return category;
        }
       
    }
}
