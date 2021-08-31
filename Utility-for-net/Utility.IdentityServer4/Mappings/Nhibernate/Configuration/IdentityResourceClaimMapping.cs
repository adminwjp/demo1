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
    public class IdentityResourceClaimMapping : IEntityTypeConfiguration<IdentityResourceClaim>
    {
        public void Configure(EntityTypeBuilder<IdentityResourceClaim> claim)
        {
            claim.HasKey(it => it.Id);
            IdentityServer4Config.ToTable(claim,IdentityServer4Config.ConfigurationStore.IdentityResourceClaim).HasKey((Expression<Func<IdentityResourceClaim, object>>)((IdentityResourceClaim x) => x.Id));
            claim.Property((Expression<Func<IdentityResourceClaim, string>>)((IdentityResourceClaim x) => x.Type)).HasMaxLength(200).IsRequired();
        }
    }
}