using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.IdentityServer4;

namespace Utility.IdentityServer4.EntityConfigurations
{
   
    public class IdentityUserClaimEntityConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
        {
            builder.HasKey( uc => uc.Id);
            builder.Property(u => u.UserId).HasMaxLength(IdentityServer4Config.KeyMaxLength);
            builder.ToTable("AspNetUserClaims");
           
        }

    }
}
