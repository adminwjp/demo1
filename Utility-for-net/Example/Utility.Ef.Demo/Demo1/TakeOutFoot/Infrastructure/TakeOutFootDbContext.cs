#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using MediatR;
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
#endif
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System.Data.Entity;
#endif
using TakeOutFoot.Gifts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TakeOutFoot.Activties;
using TakeOutFoot.Domain.Entities;
using TakeOutFoot.Prizes;
using TakeOutFoot.Signs;
using Utility;
using Utility.Ef;
//using Utility.Domain.Entities;
using Utility.Infrastructure;


namespace TakeOutFoot.Infrastructure
{
    /// <summary>
    /// vs 升级后造成 的 环境 变动 影响
    /// System.Reflection.AmbiguousMatchException: Ambiguous match found. 什么 ef 迁移不过去 单元测试可以  不能放到 一起 要单独放
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TakeOutFootDbContext>
    {
        public TakeOutFootDbContext CreateDbContext(string[] args)
        {
            //Method not found: 'System.Collections.Generic.Dictionary`2<System.String,System.Object> Microsoft.Extensions.Configuration.IConfigurationBuilder.get_Properties()'.
            ConfigHelper.DbFlag = DbFlag.Sqlite;

            string product = "Data Source=E:/work/utility/Utility-for-net/Example/Example.Web/demo.db;";
            // ConfigHelper.ConnectionString =ConfigManager.GetByConsul($"ShopProduct/{ConfigHelper.DbFlag}ConnectionString");
            ConfigHelper.ConnectionString = product;
            var bulder = AbstractDesignTimeDbContextFactory<TakeOutFootDbContext>.Parse();
            return new TakeOutFootDbContext(bulder.Options, new NoMediator());
        }
    }
    /// <summary>
    /// An error occurred while accessing the Microsoft.Extensions.Hosting services. Continuing without
    /// the application service provider. Error: ConfigureServices returning an System.IServiceProvider isn't supported.
    //Unable to create an object of type 'TakeOutFootDbContext'. For the different patterns supported at design time, see https://go.microsoft.com/fwlink/?linkid=851728
    /// </summary>
    //public class TakeOutFootDbContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<TakeOutFootDbContext>
    //{
    //    public TakeOutFootDbContext CreateDbContext(string[] args)
    //    {

    //        var optionsBuilder = new DbContextOptionsBuilder<TakeOutFootDbContext>();
    //        optionsBuilder.UseSqlite("Data Source=TakeOutFoot.db;");

    //        return new TakeOutFootDbContext(optionsBuilder.Options,new NoMediator());
    //    }
    //}
    /// <summary>
    /// 产品 数据库 上下文
    /// </summary>
    public class TakeOutFootDbContext:BaseDbContext<TakeOutFootDbContext,BaseEntity>
    {
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
        public TakeOutFootDbContext(DbContextOptions<TakeOutFootDbContext> options, IMediator mediator) : base(options, mediator)
        {
            //this.Database.EnsureDeleted();
            //this.Database.EnsureCreated();
        }
#endif
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
         public TakeOutFootDbContext(string connectionString, IMediator mediator) : base(connectionString, mediator)
        {
            //this.Database.EnsureDeleted();
            //this.Database.EnsureCreated();
        }
#endif

        public DbSet<Activty> Activties { get; set; }
        public DbSet<ActivtySetting> ActivtySettings { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Prize> Prizes { get; set; }
        public DbSet<Sign> Signs { get; set; }
        public DbSet<SignConfig> SignRecords { get; set; }
        public DbSet<SignRecord> SignConfigs { get; set; }
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            //optionsBuilder.UseLoggerFactory(new LoggerFactory());
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Activty>(entity =>
            {
                BindModel(entity);
                if (ConfigHelper.DbFlag == DbFlag.MySql)
                {
                    entity.Property(it => it.StartDate).HasColumnName("start_date").HasColumnType("datetime");
                    entity.Property(it => it.EndDate).HasColumnName("end_date").HasColumnType("datetime");
                }
            });
            modelBuilder.Entity<ActivtySetting>(entity =>
            {
                BindModel(entity);
            });
            modelBuilder.Entity<Gift>(entity =>
            {
                BindModel(entity);
            });
            modelBuilder.Entity<Prize>(entity =>
            {
                BindModel(entity);
            });
            modelBuilder.Entity<Sign>(entity =>
            {
                BindModel(entity);
                if (ConfigHelper.DbFlag == DbFlag.MySql)
                {
                    entity.Property(it => it.StartDate).HasColumnName("start_date").HasColumnType("datetime");
                    entity.Property(it => it.EndDate).HasColumnName("end_date").HasColumnType("datetime");
                }
            });
            modelBuilder.Entity<SignConfig>(entity =>
            {
                BindModel(entity);
            });
            modelBuilder.Entity<SignRecord>(entity =>
            {
                BindModel(entity);
                if (ConfigHelper.DbFlag == DbFlag.MySql)
                {
                    entity.Property(it => it.SignDate).HasColumnName("sign_date").HasColumnType("datetime");
                }
            });
            base.OnModelCreating(modelBuilder);
        }

        public static void BindModel<TEntity>(EntityTypeBuilder<TEntity> entity)
            where TEntity :BaseEntity
        {
            if (ConfigHelper.DbFlag == DbFlag.MySql)
            {
                entity.Property(it => it.CreationTime).HasColumnName("creation_time").HasColumnType("datetime");
                entity.Property(it => it.LastModificationTime).HasColumnName("last_modification_time").HasColumnType("datetime");
                entity.Property(it => it.DeletionTime).HasColumnName("deletion_time").HasColumnType("datetime");
            }
        }
#endif
    }
}
#endif