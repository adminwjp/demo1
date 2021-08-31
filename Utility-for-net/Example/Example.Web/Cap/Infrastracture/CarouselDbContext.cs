using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Shop.Cap.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.Ef;

namespace Shop.Cap.Api.Infrastracture
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CarouselDbContext>
    {
        public CarouselDbContext CreateDbContext(string[] args)
        {
            //Method not found: 'System.Collections.Generic.Dictionary`2<System.String,System.Object> Microsoft.Extensions.Configuration.IConfigurationBuilder.get_Properties()'.
            ConfigHelper.DbFlag = DbFlag.Sqlite;

            // ConfigHelper.ConnectionString =ConfigManager.GetByConsul($"ShopProduct/{ConfigHelper.DbFlag}ConnectionString");
            ConfigHelper.ConnectionString = CarouselDbContext.ConnectionString;
            var bulder = AbstractDesignTimeDbContextFactory<CarouselDbContext>.Parse();
            return new CarouselDbContext(bulder.Options);
        }
    }
    public class CarouselDbContext: DbContext
    {
        public CarouselDbContext(DbContextOptions<CarouselDbContext> options):base(options)
        {

        }
        public static string ConnectionString { get; set; } =
            "Data Source=E:/work/utility/Utility-for-net/Example/Example.Web/demo.db;"
            ;//"Database=Shop;Data Source=192.168.1.3;User Id=root;Password=wjp930514.;Old Guids=True;charset=utf8;";

        public DbSet<CarouselModel> Carousels { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            //optionsBuilder.UseLoggerFactory(new LoggerFactory());
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarouselModel>(entity =>
            {
                entity.HasKey(it => it.Id);
                entity.Property(it => it.Id).HasColumnName("id").HasMaxLength(36);
                if (ConfigHelper.DbFlag == DbFlag.MySql)
                {
                    entity.Property(it => it.CreationTime).HasColumnName("creation_time").HasColumnType("datetime");
                    entity.Property(it => it.LastModificationTime).HasColumnName("last_modification_time").HasColumnType("datetime");
                    entity.Property(it => it.DeletionTime).HasColumnName("deletion_time").HasColumnType("datetime");
                }
                entity.Property(it => it.IsDeleted).HasColumnName("is_deleted");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
