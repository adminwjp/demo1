#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utility.Ef
{
    /// <summary>
    /// System.Reflection.AmbiguousMatchException: Ambiguous match found. 什么 ef 迁移不过去 单元测试可以  不能放到 一起 要单独放
    /// </summary>
    public  class AbstractDesignTimeDbContextFactory<Db> : IDesignTimeDbContextFactory<Db>where Db : DbContext
    {

        /// <summary>
        /// 创建数据库 默认不创建
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual Db CreateDbContext(string[] args)
        {
          return null;
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <returns></returns>
        public static DbContextOptionsBuilder<Db> Parse()
        {
            var builder = new DbContextOptionsBuilder<Db>();
            builder = Parse(ConfigHelper.DbFlag, ConfigHelper.ConnectionString, builder);
            return builder;
        }

        /// <summary>
        /// 根据标识使用对应数据库
        /// </summary>
        /// <param name="flag">标识</param>
        /// <param name="connectionString">数据库连接地址</param>
        /// <param name="bulder">使用数据库驱动</param>
        /// <param name="version">ef core net standard 2.1 mysql</param>
        /// <returns></returns>
        public static DbContextOptionsBuilder<Db> Parse(DbFlag flag, string connectionString, DbContextOptionsBuilder<Db> bulder, string version = "")
        {
            switch (flag)
            {
                case DbFlag.MySql:
                    return bulder.UseMySql(connectionString
#if NETSTANDARD2_1
                            ,ServerVersion.Parse(version)
#endif
                        , options => { options.MigrationsAssembly(typeof(Db).Assembly.GetName().Name); });
                case DbFlag.SqlServer:
                    return bulder.UseSqlServer(connectionString, options => { options.MigrationsAssembly(typeof(Db).Assembly.GetName().Name); });
                case DbFlag.Oracle:
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
                    return bulder;
#else
                    return bulder.UseOracle(connectionString, options => { options.MigrationsAssembly(typeof(Db).Assembly.GetName().Name); });
#endif
                case DbFlag.Postgre:
                    return bulder.UseNpgsql(connectionString, options => { options.MigrationsAssembly(typeof(Db).Assembly.GetName().Name); });
                case DbFlag.Sqlite:
                    return bulder.UseSqlite(connectionString, options => { options.MigrationsAssembly(typeof(Db).Assembly.GetName().Name); });
                default:
                    return bulder;
            }
        }
        //public static DbContextOptionsBuilder<Db> Parse(DbFlag flag, string connectionString, DbContextOptionsBuilder<Db> bulder)
        //    => flag switch
        //    {
        //        DbFlag.MySql => bulder.UseMySql(connectionString),
        //        DbFlag.SqlServer => bulder.UseSqlServer(connectionString),
        //        DbFlag.Oracle => bulder.UseOracle(connectionString),
        //        DbFlag.Postgre => bulder.UseNpgsql(connectionString),
        //        DbFlag.Sqlite => bulder.UseSqlite(connectionString),
        //        _ => bulder
        //    };

    }
}
#endif