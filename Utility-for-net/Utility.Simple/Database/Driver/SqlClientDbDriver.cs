#if !( NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 ||  NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System;
using System.Data;
using System.Data.Common;

namespace Utility.Database.Driver
{


    #region SqlClientDatabaseProvider

    /// <summary>SqlServer数据库对象反射抽象基类实现 System.Data.SqlClient</summary>
    public class SqlClientDbDriver : AbstractReflectDbDriver
    {
        public static readonly AbstractReflectDbDriver Empty = new SqlClientDbDriver();

        protected SqlClientDbDriver() : base("System.Data.SqlClient", "System.Data.SqlClient.SqlConnection, System.Data.SqlClient",
            "System.Data.SqlClient.SqlCommand, System.Data.SqlClient", "System.Data.SqlClient.SqlDataAdapter, System.Data.SqlClient", "System.Data.SqlClient.SqlDataReader, System.Data.SqlClient")
        {
        }
    }



    #endregion SqlClientDatabaseProvider

    #region SqlClientDatabaseProvider


    /// <summary>SqlServer数据库对象反射抽象基类实现</summary>
    public class SqlServerDbDriver : AbstractReflectDbDriver
    {
        public static readonly AbstractReflectDbDriver Empty = new SqlServerDbDriver();

        protected SqlServerDbDriver() : base("System.Data", "System.Data.SqlClient.SqlConnection, System.Data",
            "System.Data.SqlClient.SqlCommand, System.Data", "System.Data.SqlClient.SqlDataAdapter, System.Data", "System.Data.SqlClient.SqlDataReader, System.Data")
        {
        }
    }

    /// <summary>SqlServer数据库对象反射抽象基类实现</summary>
    public class SqlServerDbDriverV1 : AbstractReflectDbDriver
    {
        public static readonly AbstractReflectDbDriver Empty = new SqlServerDbDriverV1();

        protected SqlServerDbDriverV1() : base("System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
            "System.Data.SqlClient.SqlConnection, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
            "System.Data.SqlClient.SqlCommand, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
            "System.Data.SqlClient.SqlDataAdapter, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
            "System.Data.SqlClient.SqlDataReader, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")
        {
            IsSupportVersion = false;
        }
    }

    #endregion SqlClientDatabaseProvider



}
#endif