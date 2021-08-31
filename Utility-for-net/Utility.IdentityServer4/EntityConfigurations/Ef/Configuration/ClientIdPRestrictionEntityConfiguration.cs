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
    public class ClientIdPRestrictionEntityConfiguration : IEntityTypeConfiguration<ClientIdPRestriction>
    {
        public void Configure(EntityTypeBuilder<ClientIdPRestriction> idPRestriction)
        {
            IdentityServer4Config.ToTable(idPRestriction,IdentityServer4Config.ConfigurationStore.ClientIdPRestriction);
            idPRestriction.HasKey(it => it.Id);
            idPRestriction.Property( x => x.Provider).HasMaxLength(200).IsRequired();
        }
    }
}
