using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Company.Domain.Entities;
using Utility.Infrastructure;
using Company.Ef.EntityMappings;
using Utility.Demo.Domain.Entities;
using Utility.Demo.Ef.EntityMappings;
using Microsoft.Extensions.Logging;
using Utility.Attributes;

namespace Company.Ef
{
    [Transtation(UseTranstation =false )]
    public class CompanyDbContext: BaseDbContext<CompanyDbContext, BaseEntity>
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options, IMediator mediator) : base(options,mediator)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            //optionsBuilder.UseLoggerFactory(new LoggerFactory());
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MenuMap());
            modelBuilder.Entity<MenuEntity>(it=> {
                it.ToTable("t_c_menu");
            });
            //modelBuilder.ApplyConfiguration(new UserMap());
            //modelBuilder.ApplyConfiguration(new UserLogMap());
            // modelBuilder.ApplyConfiguration(new AdminMap());
            // modelBuilder.ApplyConfiguration(new TokenMap());
            var t = typeof(RelationMap);
            modelBuilder.ApplyConfigurationsFromAssembly(t.Assembly,it=>it.Namespace==t.Namespace);
            base.OnModelCreating(modelBuilder);
        }
        //public DbSet<UserEntity> Users { get; set; }
        //public DbSet<UserLogEntity> UserLogs { get; set; }

       // public DbSet<AdminEntity> Admins { get; set; }
        //public DbSet<TokenEntity> Tokens { get; set; }
        public DbSet<MenuEntity> Menus { get; set; }
     
        public DbSet<CompanyCatagoryEntity> Catagories { get; set; }
        public DbSet<RelationEntity> Relations { get; set; }
        public DbSet<LangeEntity> Langes { get; set; }
    }
}
