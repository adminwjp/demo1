#if !( NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 ||  NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System;
using System.Data;
using System.Data.Common;

namespace Utility.Database.Driver
{
  
    #region MySqlDatabaseProvider

    /// <summary>MySql数据库对象反射抽象基类实现 MySql.Data</summary>
    public class MySqlDbDriver : AbstractReflectDbDriver
    {
        public static readonly AbstractReflectDbDriver Empty = new MySqlDbDriver();

        protected MySqlDbDriver() : base("MySql.Data", "MySql.Data.MySqlClient.MySqlConnection,MySql.Data",
            "MySql.Data.MySqlClient.MySqlCommand, MySql.Data", "MySql.Data.MySqlClient.MySqlDataAdapter, MySql.Data", "MySql.Data.MySqlClient.MySqlDataReader, MySql.Data")
        {
        }
    }


    /// <summary>MySql数据库对象反射抽象基类实现参数化无效 即NAME=@NAME 只能这样写NAME=`AA` 具体原因未查 MySqlConnector </summary>
    public class MySqlConnectorDbDriver : AbstractReflectDbDriver
    {
        public static readonly AbstractReflectDbDriver Empty = new MySqlConnectorDbDriver();

        protected MySqlConnectorDbDriver() : base("MySqlConnector", "MySql.Data.MySqlClient.MySqlConnection, MySqlConnector",
            "MySql.Data.MySqlClient.MySqlCommand,MySqlConnector", "MySql.Data.MySqlClient.MySqlDataAdapter, MySqlConnector", "MySql.Data.MySqlClient.MySqlDataReader, MySqlConnector")
        {
            this.DataParamterTypeName = "MySql.Data.MySqlClient.MySqlParameter,MySqlConnector";
        }
    }

    /// <summary>MySql数据库对象反射抽象基类实现参数化无效 即NAME=@NAME 只能这样写NAME=`AA` 具体原因未查 MySqlConnector </summary>
    public class MySqlConnectorDbDriverV1 : AbstractReflectDbDriver
    {
        public static readonly AbstractReflectDbDriver Empty = new MySqlConnectorDbDriverV1();

        protected MySqlConnectorDbDriverV1() : base("MySqlConnector", "MySqlConnector.MySqlConnection, MySqlConnector",
            "MySqlConnector.MySqlCommand,MySqlConnector", "MySqlConnector.MySqlDataAdapter, MySqlConnector", "MySqlConnector.MySqlDataReader, MySqlConnector")
        {
            this.DataParamterTypeName = "MySqlConnector.MySqlParameter,MySqlConnector";
        }
    }
    #endregion MySqlDatabaseProvider




}
#endif