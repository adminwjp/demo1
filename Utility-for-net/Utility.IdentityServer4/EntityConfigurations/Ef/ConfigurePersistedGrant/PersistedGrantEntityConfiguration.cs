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
    public class PersistedGrantEntityConfiguration : IEntityTypeConfiguration<PersistedGrant>
    {
        public void Configure(EntityTypeBuilder<PersistedGrant> grant)
        {
            IdentityServer4Config.ToTable(grant, IdentityServer4Config.OperationalStore.PersistedGrants);
            grant.Property(x => x.Key)
           // .HasMaxLength(200)
           .HasMaxLength(IdentityServer4Config.KeyMaxLength)//mysql Specified key was too long; max key length is 767 bytes
            ;//.ValueGeneratedNever();
            grant.Property(x => x.Type).HasMaxLength(50).IsRequired();
            grant.Property(x=> x.SubjectId).HasMaxLength(200);
            grant.Property(x => x.SessionId).HasMaxLength(100);
            grant.Property(x => x.ClientId).HasMaxLength(200).IsRequired();
            grant.Property(x => x.Description).HasMaxLength(200);
            // MySql || SqlServer
            if(IdentityServer4Config.Driver== Driver.MySql5_5)
            {
                grant.Property(x => x.CreationTime)
          .HasColumnType("datetime")//mysql
          ;
                grant.Property(x => x.Expiration)
            .HasColumnType("datetime")//mysql
            ;
                grant.Property(x=> x.ConsumedTime)
            .HasColumnType("datetime")//mysql
          ;
            }

            grant.Property(x => x.Data).HasMaxLength(50000).IsRequired();
            grant.HasKey(x => x.Key);
            grant.HasIndex(x => new
            {
                x.SubjectId,
                x.ClientId,
                x.Type
            });
            grant.HasIndex(x=> new
            {
                x.SubjectId,
                x.SessionId,
                x.Type
            });
            grant.HasIndex(x => x.Expiration);
        }

    }
}
