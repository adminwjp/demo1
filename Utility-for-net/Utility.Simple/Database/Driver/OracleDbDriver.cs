#if !( NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 ||  NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System;
using System.Data;
using System.Data.Common;

namespace Utility.Database.Driver
{

    #region OracleDatabaseProvider

    /// <summary>Oracle数据库对象反射抽象基类实现</summary>
    public class OracleDbDriver : AbstractReflectDbDriver
    {
        public static readonly AbstractReflectDbDriver Empty = new OracleDbDriver();

        protected OracleDbDriver() : base("Oracle.ManagedDataAccess", "Oracle.ManagedDataAccess.Client.OracleConnection, Oracle.ManagedDataAccess",
            "Oracle.ManagedDataAccess.Client.OracleCommand, Oracle.ManagedDataAccess", "Oracle.ManagedDataAccess.Client.OracleDataAdapter, Oracle.ManagedDataAccess",
           "Oracle.ManagedDataAccess.Client.OracleDataReader, Oracle.ManagedDataAccess")
        {
        }
    }

    #endregion OracleDatabaseProvider


  

}
#endif