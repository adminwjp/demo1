#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using System;
using System.Collections.Generic;
using System.Text;
using Utility.Pool;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
//using System.Data.SqlClient; //ex 
using Microsoft.Data.SqlClient;
//netstandard2.1; not support 
//using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using Npgsql;
using Microsoft.Data.Sqlite;
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
//using Microsoft.Extensions.Configuration;
#endif
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System.Configuration;
#endif

namespace Utility.Ef
{
    /// <summary>
    /// 连接 帮助类
    /// </summary>
    public class EfConnectionHelper
    {
        //private static string connectionString;

        /// <summary>
        /// 连接地址
        /// </summary>
        //public static string ConnectionString
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(connectionString))
        //        {
        //            connectionString = GetConnectionString();
        //        }
        //        return connectionString;
        //    }
        //    set { connectionString = value; }
        //}

        /// <summary>
        /// 获取连接地址
        /// </summary>
        /// <returns></returns>
//        public static string GetConnectionString()
//        {
//            string name = $"{ConfigHelper.DbFlag}ConnectionString";
          
//            string sqlConnectionString = string.Empty;
//#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
//            //if (string.IsNullOrEmpty(sqlConnectionString))
//            //{
//            //    sqlConnectionString = ConfigurationManager.ConnectionStrings[name]?.ConnectionString;
//            //    Console.WriteLine($"app.config or web.config read {name} value {sqlConnectionString}");
//            //}
//#endif
//#if  NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1

//            //if (string.IsNullOrEmpty(sqlConnectionString))
//            //{
//            //    string text = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
//            //    if (string.IsNullOrEmpty(text))
//            //    {
//            //        text = "Development";
//            //    }
//            //    IConfigurationBuilder configurationBuilder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
//            //    .SetBasePath(Environment.CurrentDirectory)
//            //    .AddJsonFile("appsettings." + text + ".json", optional: false, reloadOnChange: true);
//            //    //.AddEnvironmentVariables();
//            //    IConfigurationRoot configuration = configurationBuilder.Build();
//            //    sqlConnectionString = configuration.GetConnectionString(name);
//            //}
//#endif
//            connectionString = sqlConnectionString;
//            return sqlConnectionString;

//        }

        /// <summary>
        /// 手动 设置 ef 上下文 池
        /// </summary>
        /// <typeparam name="Context"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static IObjectPool<Context> GetDbContexts<Context>(string connectionString,DbFlag flag) where Context : DbContext
        {
            var options = GetDbContextOptions<Context>(connectionString,flag);
            Func<Context> create = () => {
                Context context = (Context)Activator.CreateInstance(typeof(Context), new object[] { options });
                return context;
            };
            var pool = new ObjectPool<Context>(create);
            return pool;
        }

        /// <summary>
        /// 手动 设置 ef 上下文 池
        /// </summary>
        /// <typeparam name="Context"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="flag"></param>
        /// <param name="version">ef core net standard 2.1 mysql</param>
        /// <returns></returns>
        public static DbContextOptions<Context> GetDbContextOptions<Context>(string connectionString, DbFlag flag, string version = "") where Context : DbContext
        {
            DbContextOptionsBuilder<Context> optionsBuilder = new DbContextOptionsBuilder<Context>();
            switch (flag)
            {
               
                case DbFlag.SqlServer:
                    optionsBuilder.UseSqlServer(connectionString);
                    break;
                case DbFlag.MySql:
                    optionsBuilder.UseMySql(connectionString
#if NETSTANDARD2_1
                            , ServerVersion.Parse(version)
#endif
                        );
                    break;
                case DbFlag.Sqlite:
                    optionsBuilder.UseSqlite(connectionString);
                    break;
                case DbFlag.Oracle:
                    optionsBuilder.UseOracle(connectionString);
                    break;
                case DbFlag.Postgre:
                    optionsBuilder.UseNpgsql(connectionString);
                    break;
                case DbFlag.None:
                default:
                    break;
            }
            return optionsBuilder==null?null:optionsBuilder.Options;
        }


        /// <summary>
        /// 手动 设置 ef 上下文 池
        /// </summary>
        /// <typeparam name="Context"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="flag"></param>
        /// <param name="version">ef core net standard 2.1 mysql</param>
        /// <returns></returns>
        public static void GetDbContextOptions(DbContextOptionsBuilder optionsBuilder,string connectionString, DbFlag flag, string version = "") 
        {
            switch (flag)
            {
                case DbFlag.SqlServer:
                    optionsBuilder.UseSqlServer(connectionString);
                    break;
                case DbFlag.MySql:
                    optionsBuilder.UseMySql(connectionString
#if NETSTANDARD2_1
                            , ServerVersion.AutoDetect(connectionString)
#endif
                        );
                    break;
                case DbFlag.Sqlite:
                    optionsBuilder.UseSqlite(connectionString);
                    break;
                case DbFlag.Oracle:
                    optionsBuilder.UseOracle(connectionString);
                    break;
                case DbFlag.Postgre:
                    optionsBuilder.UseNpgsql(connectionString);
                    break;
                case DbFlag.None:
                default:
                    break;
            }
        }
        /// <summary>
        /// handler get db connection
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static DbConnection GetConnection(string connectionString, DbFlag flag = DbFlag.None)
        {
            switch (flag)
            {
                case DbFlag.SqlServer:
                    return new SqlConnection(connectionString);
                case DbFlag.MySql:
                    //return new MySqlConnection(connectionString);
                    break;
                case DbFlag.Sqlite:
                    return new SqliteConnection(connectionString);
                case DbFlag.Oracle:
                    return new OracleConnection(connectionString);
                case DbFlag.Postgre:
                    return new NpgsqlConnection(connectionString);
                case DbFlag.None:
                default:
                    break;
            }
            return null;
        }


        /// <summary>
        /// handler get db command
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static DbCommand GetCommand(DbFlag flag = DbFlag.None)
        {
            switch (flag)
            {
                case DbFlag.SqlServer:
                    return new SqlCommand();
                case DbFlag.MySql:
                    //return new MySqlCommand();
                    break;
                case DbFlag.Sqlite:
                    return new SqliteCommand();
                case DbFlag.Oracle:
                    return new OracleCommand();
                case DbFlag.Postgre:
                    return new NpgsqlCommand();
                case DbFlag.None:
                default:
                    break;
            }
            return null;
        }

        /// <summary>
        /// handler get db dataAdapter
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static DbDataAdapter GetDataAdapter(DbFlag flag = DbFlag.None)
        {
            switch (flag)
            {
                case DbFlag.SqlServer:
                    return new SqlDataAdapter();
                case DbFlag.MySql:
                    // return new MySqlDataAdapter();
                    break;
                case DbFlag.Sqlite:
                    return null;
                case DbFlag.Oracle:
                    return new OracleDataAdapter();
                case DbFlag.Postgre:
                    return new NpgsqlDataAdapter();
                case DbFlag.None:
                default:
                    break;
            }
            return null;
        }

        /// <summary>
        /// handler get db dataAdapter
        /// </summary>
        /// <param name="command"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static DbDataAdapter GetDataAdapter(DbCommand command,DbFlag flag = DbFlag.None)
        {
            switch (flag)
            {
                case DbFlag.SqlServer:
                    return new SqlDataAdapter((SqlCommand)command);
                case DbFlag.MySql:
                    //return new MySqlDataAdapter((MySqlCommand)command);
                    break;
                case DbFlag.Sqlite:
                    return null;
                case DbFlag.Oracle:
                    return new OracleDataAdapter((OracleCommand)command);
                case DbFlag.Postgre:
                    return new NpgsqlDataAdapter((NpgsqlCommand)command);
                case DbFlag.None:
                default:
                    break;
            }
            return null;
        }
    }
}
#endif