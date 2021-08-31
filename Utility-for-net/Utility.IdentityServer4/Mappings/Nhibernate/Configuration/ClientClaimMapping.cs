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
    public class ClientClaimMapping : IEntityTypeConfiguration<ClientClaim>
    {
        public void Configure(EntityTypeBuilder<ClientClaim> claim)
        {
            IdentityServer4Config.ToTable(claim,IdentityServer4Config.ConfigurationStore.ClientClaim);
            claim.HasKey(it => it.Id);
            claim.Property((Expression<Func<ClientClaim, string>>)((ClientClaim x) => x.Type)).HasMaxLength(250).IsRequired();
            claim.Property((Expression<Func<ClientClaim, string>>)((ClientClaim x) => x.Value)).HasMaxLength(250).IsRequired();
        }
    }
}
