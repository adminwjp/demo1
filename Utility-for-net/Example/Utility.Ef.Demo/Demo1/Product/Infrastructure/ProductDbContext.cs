#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1

using MediatR;
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
#endif
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System.Data.Entity;
#endif
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Utility;
using Utility.Ef;
//using Utility.Domain.Entities;
using Utility.Infrastructure;

namespace Product.Infrastructure
{
    /// <summary>
 /// vs 升级后造成 的 环境 变动 影响
 /// System.Reflection.AmbiguousMatchException: Ambiguous match found. 什么 ef 迁移不过去 单元测试可以  不能放到 一起 要单独放
 /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProductDbContext>
    {
        public ProductDbContext CreateDbContext(string[] args)
        {
            //Method not found: 'System.Collections.Generic.Dictionary`2<System.String,System.Object> Microsoft.Extensions.Configuration.IConfigurationBuilder.get_Properties()'.
            ConfigHelper.DbFlag = DbFlag.Sqlite;

            string product = "Data Source=E:/work/utility/Utility-for-net/Example/Example.Web/demo.db;";
            // ConfigHelper.ConnectionString =ConfigManager.GetByConsul($"ShopProduct/{ConfigHelper.DbFlag}ConnectionString");
            ConfigHelper.ConnectionString = product;
            var bulder = AbstractDesignTimeDbContextFactory<ProductDbContext>.Parse();
            return new ProductDbContext(bulder.Options,new NoMediator());
        }
    }
    /// <summary>
    /// 产品 数据库 上下文
    /// </summary>
    public class ProductDbContext:BaseDbContext<ProductDbContext,BaseEntity>
    {

#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
 public ProductDbContext(DbContextOptions<ProductDbContext> options, IMediator mediator) :base(options, mediator)
        {
            //this.Database.EnsureDeleted();
            //this.Database.EnsureCreated();
        }
#endif
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
        public ProductDbContext(string nameOrConnectionString, IMediator mediator) : base(nameOrConnectionString, mediator)
        {
        }
#endif

        public DbSet<BrandEntity> Brnands { get; set; }
        
        public DbSet<ProductCatagoryEntity> ProductCatagories { get; set; }
        public DbSet<ProductCatagoryAttribueEntity> CatagoryAttribueEntities { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<SpecEntity> Specs { get; set; }
        public DbSet<ProductAttribueEntity> ProductAttribues { get; set; }
        public DbSet<CartEntity> Carts { get; set; }
        public DbSet<CartDetailEntity> CartDetails { get; set; }


#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            //optionsBuilder.UseLoggerFactory(new LoggerFactory());
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            Set<ProductCatagoryEntity>(modelBuilder);
            Set<ProductCatagoryAttribueEntity>(modelBuilder);
            Set<ProductEntity>(modelBuilder);
            Set<SpecEntity>(modelBuilder);
            Set<ProductAttribueEntity>(modelBuilder);
            Set<CartEntity>(modelBuilder);
            Set<CartDetailEntity>(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

       protected void Set<T>(ModelBuilder modelBuilder) where T:BaseEntity
        {
            //modelBuilder.Entity<T>(entity =>
            //{
            //    entity.HasKey(it => it.Id);
            //    entity.Property(it => it.Id).HasColumnName("id").HasMaxLength(36);
            //    if (ConfigHelper.DbFlag == DbFlag.MySql)
            //    {
            //        entity.Property(it => it.CreationTime).HasColumnName("creation_time").HasColumnType("datetime");
            //        entity.Property(it => it.LastModificationTime).HasColumnName("last_modification_time").HasColumnType("datetime");
            //        entity.Property(it => it.DeletionTime).HasColumnName("deletion_time").HasColumnType("datetime");
            //    }
            //    entity.Property(it => it.IsDeleted).HasColumnName("is_deleted");
            //});
        }
#endif
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add();
            base.OnModelCreating(modelBuilder);
        }
#endif


 
    }
}
#endif