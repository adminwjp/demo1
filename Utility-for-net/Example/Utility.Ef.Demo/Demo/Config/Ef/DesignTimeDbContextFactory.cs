using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utility;
using Utility.Ef;
using Utility.Infrastructure;

namespace Config.Ef
{
    /// <summary>
    /// System.Reflection.AmbiguousMatchException: Ambiguous match found. 什么 ef 迁移不过去 单元测试可以  不能放到 一起 要单独放
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ConfigDbContext>
    {
        public ConfigDbContext CreateDbContext(string[] args)
        {
            ConfigHelper.DbFlag = DbFlag.Sqlite;
            string config = "Data Source=E:/work/utility/Utility-for-net/Example/Example.Web/config.db;";
           // ConfigHelper.ConnectionString = Utility.ConnectionHelper.ConnectionString;
            ConfigHelper.ConnectionString = config;
             var bulder = AbstractDesignTimeDbContextFactory<ConfigDbContext>.Parse();
            return new ConfigDbContext(bulder.Options);
        }
        //public static DbContextOptionsBuilder<ProductDbContext> Parse(DbFlag flag, string connectionString, DbContextOptionsBuilder<ProductDbContext> bulder) => flag switch
        //{
        //    DbFlag.MySql => bulder.UseMySql(connectionString),
        //    DbFlag.SqlServer => bulder.UseSqlServer(connectionString),
        //    DbFlag.Oracle => bulder.UseOracle(connectionString),
        //    DbFlag.Postgre => bulder.UseNpgsql(connectionString),
        //    DbFlag.Sqlite => bulder.UseSqlite(connectionString),
        //    _ => bulder
        //};
    }
}
