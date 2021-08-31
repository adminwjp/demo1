using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Utility.IdentityServer4.Mappings
{
    public class ApiResourceMapping : IEntityTypeConfiguration<ApiResource>
    {
        public void Configure(EntityTypeBuilder<ApiResource> apiResource)
        {
            apiResource.HasKey(it => it.Id);
            //MySql || SqlServer
            if (IdentityServer4Config.Driver== Driver.MySql5_5)
            {
                //mysql
                apiResource.Property((Expression<Func<ApiResource, DateTime>>)((ApiResource x) => x.Created)).HasColumnType("datetime");
                apiResource.Property((Expression<Func<ApiResource, DateTime?>>)((ApiResource x) => x.Updated)).HasColumnType("datetime");
                apiResource.Property((Expression<Func<ApiResource, DateTime?>>)((ApiResource x) => x.LastAccessed)).HasColumnType("datetime");
            }


            IdentityServer4Config.ToTable(apiResource,IdentityServer4Config.ConfigurationStore.ApiResource).HasKey((Expression<Func<ApiResource, object>>)((ApiResource x) => x.Id));
            apiResource.Property((Expression<Func<ApiResource, string>>)((ApiResource x) => x.Name)).HasMaxLength(200).IsRequired();
            apiResource.Property((Expression<Func<ApiResource, string>>)((ApiResource x) => x.DisplayName)).HasMaxLength(200);
            apiResource.Property((Expression<Func<ApiResource, string>>)((ApiResource x) => x.Description)).HasMaxLength(1000);
            if(IdentityServer4Config.Driver== Driver.Oracle)
            {
                apiResource.Property((Expression<Func<ApiResource, string>>)((ApiResource x) => x.AllowedAccessTokenSigningAlgorithms)).HasColumnName("AATSA").HasMaxLength(100);//列过长会报错 
            }
            else
            {
                apiResource.Property((Expression<Func<ApiResource, string>>)((ApiResource x) => x.AllowedAccessTokenSigningAlgorithms)).HasMaxLength(100);
            }


            apiResource.HasIndex((Expression<Func<ApiResource, object>>)((ApiResource x) => x.Name)).IsUnique();
            apiResource.HasMany((Expression<Func<ApiResource, IEnumerable<ApiResourceSecret>>>)((ApiResource x) => x.Secrets)).WithOne((Expression<Func<ApiResourceSecret, ApiResource>>)((ApiResourceSecret x) => x.ApiResource)).HasForeignKey((Expression<Func<ApiResourceSecret, object>>)((ApiResourceSecret x) => x.ApiResourceId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            apiResource.HasMany((Expression<Func<ApiResource, IEnumerable<ApiResourceScope>>>)((ApiResource x) => x.Scopes)).WithOne((Expression<Func<ApiResourceScope, ApiResource>>)((ApiResourceScope x) => x.ApiResource)).HasForeignKey((Expression<Func<ApiResourceScope, object>>)((ApiResourceScope x) => x.ApiResourceId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            apiResource.HasMany((Expression<Func<ApiResource, IEnumerable<ApiResourceClaim>>>)((ApiResource x) => x.UserClaims)).WithOne((Expression<Func<ApiResourceClaim, ApiResource>>)((ApiResourceClaim x) => x.ApiResource)).HasForeignKey((Expression<Func<ApiResourceClaim, object>>)((ApiResourceClaim x) => x.ApiResourceId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            apiResource.HasMany((Expression<Func<ApiResource, IEnumerable<ApiResourceProperty>>>)((ApiResource x) => x.Properties)).WithOne((Expression<Func<ApiResourceProperty, ApiResource>>)((ApiResourceProperty x) => x.ApiResource)).HasForeignKey((Expression<Func<ApiResourceProperty, object>>)((ApiResourceProperty x) => x.ApiResourceId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
