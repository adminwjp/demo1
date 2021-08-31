#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace Utility.Nhibernate
{
    /// <summary>
    /// Nhibernate 帮助类 支持 sqlite mysql sqlserver postgre oracle
    /// </summary>
    public static class NhibernateHelper
    {
        /// <summary>
        /// 支持 sqlite mysql sqlserver postgre oracle
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="flag">database flag</param>
        /// <param name="connectionString">database connectionString</param>
        public static FluentConfiguration UseNhibernate(this FluentConfiguration configuration, DbFlag flag,string connectionString)
        {
            switch (flag)
            {
                case DbFlag.MySql:
                   return configuration.Database(MySQLConfiguration.Standard.ConnectionString(connectionString)
                        .ShowSql().FormatSql().Raw("hbm2ddl.auto", "update"));
                    //break;
                case DbFlag.SqlServer:
                    return configuration.Database(MsSqlCeConfiguration.Standard.ConnectionString(connectionString)
                         .ShowSql().FormatSql().Raw("hbm2ddl.auto", "update"));
                    //break;
                case DbFlag.Oracle:
                    return configuration.Database(OracleManagedDataClientConfiguration.Oracle10.ConnectionString(connectionString)
                        .ShowSql().FormatSql().Raw("hbm2ddl.auto", "update"));
                   // break;
                case DbFlag.Postgre:
                    return configuration.Database(PostgreSQLConfiguration.Standard.ConnectionString(connectionString)
                        .ShowSql().FormatSql().Raw("hbm2ddl.auto", "update"));
                    //break;
                case DbFlag.Sqlite:
                    return configuration.Database(SQLiteConfiguration.Standard.ConnectionString(connectionString)
                         .ShowSql().FormatSql().Raw("hbm2ddl.auto", "update"));
                   // break;
                default:
                    throw new System.NotSupportedException("not support database");
            }
            
        }
    }
}
#endif