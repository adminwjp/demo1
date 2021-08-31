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
    public class ClientScopeMapping : IEntityTypeConfiguration<ClientScope>
    {
        public void Configure(EntityTypeBuilder<ClientScope> scope)
        {
            IdentityServer4Config.ToTable(scope,IdentityServer4Config.ConfigurationStore.ClientScopes);
            scope.HasKey(it => it.Id);
            scope.Property((Expression<Func<ClientScope, string>>)((ClientScope x) => x.Scope)).HasMaxLength(200).IsRequired();
        }
    }
}
