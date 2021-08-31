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
    public class DeviceFlowCodesEntityConfiguration : IEntityTypeConfiguration<DeviceFlowCodes>
    {
        public void Configure(EntityTypeBuilder<DeviceFlowCodes> codes)
        {
            IdentityServer4Config.ToTable(codes, IdentityServer4Config.OperationalStore.DeviceFlowCodes);
            codes.Property(x => x.DeviceCode).HasMaxLength(200).IsRequired();
            codes.Property(x => x.UserCode)
           // .HasMaxLength(200)
           .HasMaxLength(IdentityServer4Config.KeyMaxLength)//mysql Specified key was too long; max key length is 767 bytes
            .IsRequired();
            codes.Property(x => x.SubjectId).HasMaxLength(200);
            codes.Property(x => x.SessionId).HasMaxLength(100);
            codes.Property(x=> x.ClientId).HasMaxLength(200).IsRequired();
            codes.Property(x=> x.Description).HasMaxLength(200);
            // MySql || SqlServer
            if(IdentityServer4Config.Driver== Driver.MySql5_5)
            {
                codes.Property(x => x.CreationTime)
               .HasColumnType("datetime")//mysql
              ;
                codes.Property(x => x.Expiration)
                 .HasColumnType("datetime")//mysql
                 ;
            }
                

            codes.Property(x => x.Data).HasMaxLength(50000).IsRequired();
            codes.HasKey(x => new
            {
                x.UserCode
            });
            codes.HasIndex(x => x.DeviceCode).IsUnique();
            codes.HasIndex(x => x.Expiration);
        }

    }
}
