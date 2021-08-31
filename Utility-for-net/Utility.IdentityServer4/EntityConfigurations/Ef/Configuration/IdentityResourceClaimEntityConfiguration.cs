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
    public class IdentityResourceClaimEntityConfiguration : IEntityTypeConfiguration<IdentityResourceClaim>
    {
        public void Configure(EntityTypeBuilder<IdentityResourceClaim> claim)
        {
            claim.HasKey(it => it.Id);
            IdentityServer4Config.ToTable(claim,IdentityServer4Config.ConfigurationStore.IdentityResourceClaim).HasKey(x => x.Id);
            claim.Property(x => x.Type).HasMaxLength(200).IsRequired();
        }
    }
}