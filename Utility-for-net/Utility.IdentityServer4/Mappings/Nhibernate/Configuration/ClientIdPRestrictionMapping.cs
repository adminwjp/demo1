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
    public class ClientIdPRestrictionMapping : IEntityTypeConfiguration<ClientIdPRestriction>
    {
        public void Configure(EntityTypeBuilder<ClientIdPRestriction> idPRestriction)
        {
            IdentityServer4Config.ToTable(idPRestriction,IdentityServer4Config.ConfigurationStore.ClientIdPRestriction);
            idPRestriction.HasKey(it => it.Id);
            idPRestriction.Property((Expression<Func<ClientIdPRestriction, string>>)((ClientIdPRestriction x) => x.Provider)).HasMaxLength(200).IsRequired();
        }
    }
}
