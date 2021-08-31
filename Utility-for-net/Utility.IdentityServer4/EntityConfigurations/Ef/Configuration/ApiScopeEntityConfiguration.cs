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
    public class ApiScopeEntityConfiguration : IEntityTypeConfiguration<ApiScope>
    {
        public void Configure(EntityTypeBuilder<ApiScope> scope)
        {
            scope.HasKey(it => it.Id);
            IdentityServer4Config.ToTable(scope, IdentityServer4Config.ConfigurationStore.ApiScope).HasKey(x => x.Id);
            scope.Property(x=> x.Name).HasMaxLength(200).IsRequired();
            scope.Property(x => x.DisplayName).HasMaxLength(200);
            scope.Property(x => x.Description).HasMaxLength(1000);
            scope.HasIndex(x => x.Name).IsUnique();
            scope.HasMany(x => x.UserClaims).WithOne(x => x.Scope).HasForeignKey(x=> x.ScopeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
