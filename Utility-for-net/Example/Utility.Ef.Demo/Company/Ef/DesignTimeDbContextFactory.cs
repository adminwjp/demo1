using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utility;
using Utility.Ef;
using Utility.Infrastructure;

namespace Company.Ef
{
    /// <summary>
    /// vs 升级后造成 的 环境 变动 影响
    /// System.Reflection.AmbiguousMatchException: Ambiguous match found. 什么 ef 迁移不过去 单元测试可以  不能放到 一起 要单独放
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CompanyDbContext>
    {
        public CompanyDbContext CreateDbContext(string[] args)
        {
            //Method not found: 'System.Collections.Generic.Dictionary`2<System.String,System.Object> Microsoft.Extensions.Configuration.IConfigurationBuilder.get_Properties()'.
            ConfigHelper.DbFlag = DbFlag.Sqlite;

            string product = "Data Source=E:/work/utility/Utility-for-net/Example/Example.Web/demo.db;";
           // ConfigHelper.ConnectionString =ConfigManager.GetByConsul($"ShopProduct/{ConfigHelper.DbFlag}ConnectionString");
            ConfigHelper.ConnectionString = product;
            var bulder = AbstractDesignTimeDbContextFactory<CompanyDbContext>.Parse();
            return new CompanyDbContext(bulder.Options,new NoMediator());
        }
        //public static DbContextOptionsBuilder<CompanyDbContext> Parse(DbFlag flag, string connectionString, DbContextOptionsBuilder<CompanyDbContext> bulder) => flag switch
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
