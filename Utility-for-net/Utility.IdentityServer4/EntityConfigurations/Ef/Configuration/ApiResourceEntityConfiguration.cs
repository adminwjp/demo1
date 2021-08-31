using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Utility.IdentityServer4.EntityConfigurations
{
    public class ApiResourceEntityConfiguration : IEntityTypeConfiguration<ApiResource>
    {
        public void Configure(EntityTypeBuilder<ApiResource> apiResource)
        {
            apiResource.HasKey(it => it.Id);
            //MySql || SqlServer
            if (IdentityServer4Config.Driver== Driver.MySql5_5)
            {
                //mysql
                apiResource.Property(x => x.Created).HasColumnType("datetime");
                apiResource.Property(x => x.Updated).HasColumnType("datetime");
                apiResource.Property(x => x.LastAccessed).HasColumnType("datetime");
            }


            IdentityServer4Config.ToTable(apiResource,IdentityServer4Config.ConfigurationStore.ApiResource).HasKey(x => x.Id);
            apiResource.Property(x => x.Name).HasMaxLength(200).IsRequired();
            apiResource.Property(x => x.DisplayName).HasMaxLength(200);
            apiResource.Property(x => x.Description).HasMaxLength(1000);
            if(IdentityServer4Config.Driver== Driver.Oracle)
            {
                apiResource.Property(x => x.AllowedAccessTokenSigningAlgorithms).HasColumnName("AATSA").HasMaxLength(100);//列过长会报错 
            }
            else
            {
                apiResource.Property(x => x.AllowedAccessTokenSigningAlgorithms).HasMaxLength(100);
            }


            apiResource.HasIndex(x => x.Name).IsUnique();
            apiResource.HasMany(x => x.Secrets).WithOne(x => x.ApiResource).HasForeignKey(x => x.ApiResourceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            apiResource.HasMany(x => x.Scopes).WithOne(x => x.ApiResource).HasForeignKey( x => x.ApiResourceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            apiResource.HasMany( x => x.UserClaims).WithOne(x=> x.ApiResource).HasForeignKey(x => x.ApiResourceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            apiResource.HasMany(x => x.Properties).WithOne(x => x.ApiResource).HasForeignKey( x => x.ApiResourceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
