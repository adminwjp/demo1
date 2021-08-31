#if  NET40 ||NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System;
using System.Data.Entity;
namespace Config.Ef
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public const string ConnectionString= "Database = Config; Data Source = localhost; User Id = root; Password = wjp930514.; Old Guids = True; charset = utf8;";
      /// <summary>
      /// 
      /// </summary>
        public ConfigDbContext() : base("Config")
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        public ConfigDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Config.Model.ServiceModel> ServiceModels { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<Config.Model.ConfigModel> ConfigModels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Config.Ef.DAL.Mapp.ServiceEfMapp());
            modelBuilder.Configurations.Add(new Config.Ef.DAL.Mapp.ConfigEfMapp());
            // modelBuilder.Configurations.AddFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }

    }


}
#endif

#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Config.Domain.Entities;
using Config.Ef.EntityMappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Utility;

namespace Config.Ef
{
     
    /// <summary>
    /// 配置 ef 上下文
    /// </summary>
    public class ConfigDbContext : DbContext
    {
        //public static readonly ILoggerFactory MyLoggerFactory  = LoggerFactory.Create(builder =>
        //{
        //   // builder.AddConsole();
        //});
        public ConfigDbContext(DbContextOptions<ConfigDbContext> options):base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public ConfigDbContext() : base()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        /// <summary>
        /// 服务 
        /// </summary>
        public DbSet<ServiceEntity> Services { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        public DbSet<ConfigEntity> Configs { get; set; }
        /// <summary>
        /// 数据库驱动
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            //optionsBuilder.UseMySql(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
        /// <summary>
        /// model 映射
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ServiceEfMapp());
            modelBuilder.ApplyConfiguration(new ConfigEfMapp());


            //modelBuilder.Entity<Config.Model.ServiceModel>(entity =>
            //{
            //    if (ConfigHelper.DbFlag == DbFlag.MySql)
            //    {
            //        entity.Property(it => it.CreateDate).HasColumnType("datetime");
            //        entity.Property(it => it.LastDate).HasColumnType("datetime");
            //    }
            //});
            //modelBuilder.Entity<Config.Model.ConfigModel>(entity =>
            //{
            //    if (ConfigHelper.DbFlag == DbFlag.MySql)
            //    {
            //        entity.Property(it => it.CreateDate).HasColumnType("datetime");
            //        entity.Property(it => it.LastDate).HasColumnType("datetime");
            //    }
            //});
            // modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly, it => it.Name.EndsWith("EfMapp")&&it.Name!= "BasEfMapp");
            base.OnModelCreating(modelBuilder);
        }

    }


}
#endif