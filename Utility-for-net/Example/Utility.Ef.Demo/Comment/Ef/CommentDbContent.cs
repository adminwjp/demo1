#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
#endif
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System.Data.Entity;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Comment.Domain.Entities;
using Microsoft.EntityFrameworkCore.Design;
using Utility.Ef;

namespace Comment.Ef
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CommentDbContent>
    {
        public CommentDbContent CreateDbContext(string[] args)
        {
            //Method not found: 'System.Collections.Generic.Dictionary`2<System.String,System.Object> Microsoft.Extensions.Configuration.IConfigurationBuilder.get_Properties()'.
            ConfigHelper.DbFlag = DbFlag.Sqlite;

             ConfigHelper.ConnectionString = CommentDbContent.ConnectionString;
            var bulder = AbstractDesignTimeDbContextFactory<CommentDbContent>.Parse();
            return new CommentDbContent(bulder.Options);
        }
    }
    public class CommentDbContent: DbContext
    {
        public static string ConnectionString { get; set; } =
           "Data Source=E:/work/utility/Utility-for-net/Example/Example.Web/demo.db;";
        public CommentDbContent(DbContextOptions<CommentDbContent> options):base(options)
        {

        }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<CommentType> CommentTypes { get; set; }
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Set<Comments>(modelBuilder);
            Set<CommentType>(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            //optionsBuilder.UseLoggerFactory(new LoggerFactory());
            base.OnConfiguring(optionsBuilder);
        }
        protected virtual void Set<T>(ModelBuilder modelBuilder)
            where T:BaseEntity
        {
            modelBuilder.Entity<T>(entity =>
            {
                entity.HasKey(it => it.Id);
                if (ConfigHelper.DbFlag == DbFlag.MySql)
                {
                    entity.Property(it => it.CreationTime).HasColumnName("creation_time").HasColumnType("datetime");
                    entity.Property(it => it.LastModificationTime).HasColumnName("last_modification_time").HasColumnType("datetime");
                    entity.Property(it => it.DeletionTime).HasColumnName("deletion_time").HasColumnType("datetime");
                }
                entity.Property(it => it.IsDeleted).HasColumnName("is_deleted");
            });
        }

#endif
    }
}
