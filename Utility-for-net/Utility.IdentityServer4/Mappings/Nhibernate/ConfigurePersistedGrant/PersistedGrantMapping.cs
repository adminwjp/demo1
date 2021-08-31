using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Utility.IdentityServer4.Mappings
{
    public class PersistedGrantMapping : IEntityTypeConfiguration<PersistedGrant>
    {
        public void Configure(EntityTypeBuilder<PersistedGrant> grant)
        {
            IdentityServer4Config.ToTable(grant, IdentityServer4Config.OperationalStore.PersistedGrants);
            grant.Property((Expression<Func<PersistedGrant, string>>)((PersistedGrant x) => x.Key))
           // .HasMaxLength(200)
           .HasMaxLength(IdentityServer4Config.KeyMaxLength)//mysql Specified key was too long; max key length is 767 bytes
            ;//.ValueGeneratedNever();
            grant.Property((Expression<Func<PersistedGrant, string>>)((PersistedGrant x) => x.Type)).HasMaxLength(50).IsRequired();
            grant.Property((Expression<Func<PersistedGrant, string>>)((PersistedGrant x) => x.SubjectId)).HasMaxLength(200);
            grant.Property((Expression<Func<PersistedGrant, string>>)((PersistedGrant x) => x.SessionId)).HasMaxLength(100);
            grant.Property((Expression<Func<PersistedGrant, string>>)((PersistedGrant x) => x.ClientId)).HasMaxLength(200).IsRequired();
            grant.Property((Expression<Func<PersistedGrant, string>>)((PersistedGrant x) => x.Description)).HasMaxLength(200);
            // MySql || SqlServer
            if(IdentityServer4Config.Driver== Driver.MySql5_5)
            {
                grant.Property((Expression<Func<PersistedGrant, DateTime>>)((PersistedGrant x) => x.CreationTime))
          .HasColumnType("datetime")//mysql
          ;
                grant.Property((Expression<Func<PersistedGrant, DateTime?>>)((PersistedGrant x) => x.Expiration))
            .HasColumnType("datetime")//mysql
            ;
                grant.Property((Expression<Func<PersistedGrant, DateTime?>>)((PersistedGrant x) => x.ConsumedTime))
            .HasColumnType("datetime")//mysql
          ;
            }

            grant.Property((Expression<Func<PersistedGrant, string>>)((PersistedGrant x) => x.Data)).HasMaxLength(50000).IsRequired();
            grant.HasKey((Expression<Func<PersistedGrant, object>>)((PersistedGrant x) => x.Key));
            grant.HasIndex((Expression<Func<PersistedGrant, object>>)((PersistedGrant x) => new
            {
                x.SubjectId,
                x.ClientId,
                x.Type
            }));
            grant.HasIndex((Expression<Func<PersistedGrant, object>>)((PersistedGrant x) => new
            {
                x.SubjectId,
                x.SessionId,
                x.Type
            }));
            grant.HasIndex((Expression<Func<PersistedGrant, object>>)((PersistedGrant x) => x.Expiration));
        }

    }
}
