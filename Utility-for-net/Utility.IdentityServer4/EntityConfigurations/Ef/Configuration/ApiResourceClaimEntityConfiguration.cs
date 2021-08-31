using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Utility.IdentityServer4.EntityConfigurations
{
    public class ApiResourceClaimEntityConfiguration : IEntityTypeConfiguration<ApiResourceClaim>
    {
        public void Configure(EntityTypeBuilder<ApiResourceClaim> apiClaim)
        {
            apiClaim.HasKey(it => it.Id);
            IdentityServer4Config.ToTable(apiClaim,IdentityServer4Config.ConfigurationStore.ApiResourceClaim).HasKey(x => x.Id);
            apiClaim.Property(x => x.Type).HasMaxLength(200).IsRequired();
        }

    }
}
