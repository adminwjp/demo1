
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq.Expressions;
using Utility.IdentityServer.Models;
using Utility.IdentityServer4;
using Utility.IdentityServer4.EntityConfigurations;

namespace Utility.IdentityServer.Data
{
    public class IdentityDbContextHelper
    {
        private class PersonalDataConverter : ValueConverter<string, string>
        {
            public PersonalDataConverter(IPersonalDataProtector protector)
                : base((Expression<Func<string, string>>)((string s) => protector.Protect(s)), (Expression<Func<string, string>>)((string s) => protector.Unprotect(s)), (ConverterMappingHints)null)
            {
            }
        }

        public static void OnModelCreating(ModelBuilder builder, StoreOptions storeOptions, IPersonalDataProtector personalDataProtector)
        {
            //IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
          
            builder.ApplyConfiguration(new IdentityRoleEntityConfiguration());
            builder.ApplyConfiguration(new IdentityRoleClaimEntityConfiguration());
            builder.ApplyConfiguration(new IdentityUserRoleEntityConfiguration());

            //IdentityUserContext<TUser, TKey, TUserClaim, TUserLogin, TUserToken>
            int maxKeyLength = storeOptions?.MaxLengthForKeys ?? 0;
            if (maxKeyLength != IdentityServer4Config.KeyMaxLength)
            {
                maxKeyLength = IdentityServer4Config.KeyMaxLength;
            }
            builder.ApplyConfiguration(new IdentityUserClaimEntityConfiguration());
            builder.ApplyConfiguration(new IdentityUserLoginEntityConfiguration());
            builder.Entity<IdentityUserLogin<string>>(it => {
                // it.HasNoKey();
            });

            builder.ApplyConfiguration(new IdentityUserTokenEntityConfiguration());

            bool encryptPersonalData = storeOptions?.ProtectPersonalData ?? false;
            encryptPersonalData = false;//不执行
            PersonalDataConverter converter = null;
            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration<ApplicationUser>());

          }
    }
}
