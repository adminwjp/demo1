#if !( NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 ||  NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System;
using System.Data;
using System.Data.Common;

namespace Utility.Database.Driver
{

    /// <summary>Access数据库对象反射抽象基类实现 无法 使用,提示 没 安装 驱动 像 ef(mysql net 环境下 一样的提示)</summary>
    public class AccessDbDriver : AbstractReflectDbDriver
    {
        public static readonly AbstractReflectDbDriver Empty = new AccessDbDriver();

        protected AccessDbDriver() : base("System.Data",
            "System.Data.OleDb.OleDbConnection, System.Data",
            "System.Data.OleDb.OleDbCommand, System.Data",
            "System.Data.OleDb.OleDbDataAdapter, System.Data",
            "System.Data.OleDb.OleDbDataReader, System.Data")
        {
        }
    }

    /// <summary>Access数据库对象反射抽象基类实现 无法 使用,提示 没 安装 驱动 像 ef(mysql net 环境下 一样的提示)</summary>
    public class AccessV1DatabaseDriver : AbstractReflectDbDriver
    {
        public static readonly AbstractReflectDbDriver Empty = new AccessV1DatabaseDriver();

        protected AccessV1DatabaseDriver() : base("System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
            "System.Data.OleDb.OleDbConnection, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
            "System.Data.OleDb.OleDbCommand, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
            "System.Data.OleDb.OleDbDataAdapter, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
            "System.Data.OleDb.OleDbDataReader, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")
        {
            IsSupportVersion = false;
        }
    }
}
#endif