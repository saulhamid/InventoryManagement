using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons
{
  public  class SqlQuery
    {
        //static void ExecuteSql(ObjectContext c, string sql)
        //{
        //    var entityConnection = (System.Data.EntityClient.EntityConnection)c.Connection;
        //    DbConnection conn = entityConnection.StoreConnection;
        //    ConnectionState initialState = conn.State;
        //    try
        //    {
        //        if (initialState != ConnectionState.Open)
        //            conn.Open();  // open connection if not already open
        //        using (DbCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = sql;
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //    finally
        //    {
        //        if (initialState != ConnectionState.Open)
        //            conn.Close(); // only close connection if not initially open
        //    }
        //}

    }
}
