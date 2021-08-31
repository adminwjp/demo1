#if !( NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 ||  NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System;
using System.Data;
using System.Data.Common;

namespace Utility.Database.Driver
{

    #region SQLiteDatabaseProvider

    /// <summary>SQLite数据库对象反射抽象基类实现</summary>
    public class SQLiteDbDriver : AbstractReflectDbDriver
    {
        public static readonly AbstractReflectDbDriver Empty = new SQLiteDbDriver();

        protected SQLiteDbDriver() : base("System.Data.SQLite", "System.Data.SQLite.SQLiteConnection, System.Data.SQLite",
            "System.Data.SQLite.SQLiteCommand, System.Data.SQLite", "System.Data.SQLite.SQLiteDataAdapter, System.Data.SQLite",
           "System.Data.SQLite.SQLiteDataReader, System.Data.SQLite")
        {
        }
    }

    #endregion SQLiteDatabaseProvider


}
#endif