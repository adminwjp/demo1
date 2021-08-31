using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.IdentityServer4;

namespace Utility.EntityConfigurations
{

    public class ApiResourceSecretEntityConfiguration : IEntityTypeConfiguration<ApiResourceSecret>
    {
        public void Configure(EntityTypeBuilder<ApiResourceSecret> apiClaim)
        {
            IdentityServer4Config.ToTable(apiClaim, IdentityServer4Config.ConfigurationStore.ApiResourceSecret).HasKey(x => x.Id);
            apiClaim.HasKey(it => it.Id);

if (IdentityServer4Config.Driver== Driver.MySql5_5|| IdentityServer4Config.Driver == Driver.SqlServer)
            {
                //mysql
                apiClaim.Property(x => x.Created).HasColumnType("datetime");
                apiClaim.Property(x => x.Expiration).HasColumnType("datetime");
            }
              


            apiClaim.Property(x => x.Description).HasMaxLength(1000);
            apiClaim.Property(x => x.Value).HasMaxLength(4000).IsRequired();
            apiClaim.Property(x => x.Type).HasMaxLength(250).IsRequired();
        }

    }
}
