using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutNFood.Data.Data
{
    public partial class DBCUTNFOODEntities
    {
        public DBCUTNFOODEntities(string connectionString)
            : base(connectionString)
        {
        }

        public DBCUTNFOODEntities(System.Data.Common.DbConnection connection, bool contextOwnsConnection) : base(connection, contextOwnsConnection)
        {
        }
    }
}
