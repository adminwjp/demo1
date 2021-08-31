#if !( NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 ||  NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System;
using System.Data;
using System.Data.Common;

namespace Utility.Database.Driver
{
    #region NpgsqlDatabaseProvider

    /// <summary>Npgsql数据库对象反射抽象基类实现</summary>
    public class NpgsqlDbDriver : AbstractReflectDbDriver
    {
        public static readonly AbstractReflectDbDriver Empty = new NpgsqlDbDriver();

        protected NpgsqlDbDriver() : base("Npgsql", "Npgsql.NpgsqlConnection, Npgsql",
            "Npgsql.NpgsqlCommand, Npgsql", "Npgsql.NpgsqlDataAdapter, Npgsql",
           "Npgsql.NpgsqlDataReader, Npgsql")
        {
        }
    }

    #endregion NpgsqlDatabaseProvider


  

}
#endif