using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;

namespace CutNFood.Data
{
    public class Helper
    {
        public static DbConnection GetOpenGetConnection()
        {
            try
            {
                var conn = System.Configuration.ConfigurationManager.ConnectionStrings["DBCUTNFOODEntities"];
                var connectionstring = conn.ConnectionString;
                DbConnection connection = new EntityConnection(connectionstring);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
