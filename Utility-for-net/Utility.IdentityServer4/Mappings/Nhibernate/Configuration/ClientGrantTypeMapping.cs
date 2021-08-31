﻿using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Utility.IdentityServer4.Mappings
{
    public class ClientGrantTypeMapping : IEntityTypeConfiguration<ClientGrantType>
    {
        public void Configure(EntityTypeBuilder<ClientGrantType> grantType)
        {
            IdentityServer4Config.ToTable(grantType, IdentityServer4Config.ConfigurationStore.ClientGrantType);
            grantType.HasKey(it => it.Id);
            grantType.Property((Expression<Func<ClientGrantType, string>>)((ClientGrantType x) => x.GrantType)).HasMaxLength(250).IsRequired();
        }
    }
}
