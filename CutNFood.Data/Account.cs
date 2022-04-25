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
    public class Account
    {
        public Account()
        {

        }

        //Add
        public void AddUser(int userTypeid, string username, string password, string email)
        {
            DbConnection connection = null;
            try
            {
                connection = Helper.GetOpenGetConnection();
                using (var context = new DBCUTNFOODEntities(connection, false))
                {
                    var userToAdd = new tbl_account
                    {
                        username = username,
                        password = password,
                        email = email,
                        userType_id = userTypeid
                    };

                    context.tbl_account.Add(userToAdd);
                    context.SaveChanges();
                }
            }
            finally
            {
                if (connection != null) connection.Close();
            }

        }
        
        public DbSet<tbl_account> GetUsers()
        {
            DbConnection connection = null;
            DbSet<tbl_account> users = null;

            try
            {
                connection = Helper.GetOpenGetConnection();
                using (var context = new DBCUTNFOODEntities(connection, false))
                {
                    users = context.tbl_account;
                }
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return users;
        }

        public tbl_account GetUser(string searchUsername)
        {
            DbConnection connection = null;
            tbl_account user = null;
            try
            {
                connection = Helper.GetOpenGetConnection();
                using (var context = new DBCUTNFOODEntities(connection, false))
                {
                    user = context.tbl_account.FirstOrDefault(x => x.username == searchUsername);
                }
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return user;
        }

        public bool AuthenticateUser(string username, string password)
        {
            bool isValidUser = false;
            DbConnection connection = null;
            tbl_account user = null;

            connection = Helper.GetOpenGetConnection();
            using (var context = new DBCUTNFOODEntities(connection, false))
            {
                user = context.tbl_account.Where(x => x.username == username).FirstOrDefault();
                if(user != null)
                {
                    if (user.password == password)
                        isValidUser = true;
                }
            }
            return isValidUser;
        }

    }
}
