
using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Utility.IdentityServer4.EntityConfigurations
{
    public class IdentityUserTokenEntityConfiguration : IEntityTypeConfiguration<IdentityUserToken<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<string>> b)
        {
            StoreOptions storeOptions = null;
            bool encryptPersonalData = storeOptions?.ProtectPersonalData ?? false;
            encryptPersonalData = false;//不执行
            PersonalDataConverter converter = null;
            IPersonalDataProtector personalDataProtector = null;
            b.HasKey(t => new
            {
                t.UserId,
                t.LoginProvider,
                t.Name
            });
            if (IdentityServer4Config.KeyMaxLength > 0)
            {
                b.Property( t => t.UserId).HasMaxLength(IdentityServer4Config.KeyMaxLength);
                b.Property(t => t.LoginProvider).HasMaxLength(IdentityServer4Config.KeyMaxLength);
                b.Property(t => t.Name).HasMaxLength(IdentityServer4Config.KeyMaxLength);
            }

            if (encryptPersonalData)
            {
                foreach (PropertyInfo item2 in ((IEnumerable<PropertyInfo>)typeof(IdentityUserToken<string>).GetProperties()).Where((Func<PropertyInfo, bool>)((PropertyInfo prop) => Attribute.IsDefined(prop, typeof(ProtectedPersonalDataAttribute)))))
                {
                    if (item2.PropertyType != typeof(string))
                    {
                        //throw new InvalidOperationException(Resources.CanOnlyProtectStrings);
                        throw new InvalidOperationException("CanOnlyProtectStrings");
                    }

                    b.Property(typeof(string), item2.Name).HasConversion(converter);
                }
            }

            b.ToTable("AspNeUserTokens");
        }

    }
}
