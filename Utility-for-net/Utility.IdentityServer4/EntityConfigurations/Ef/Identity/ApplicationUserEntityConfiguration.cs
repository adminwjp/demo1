
using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Utility.IdentityServer4.EntityConfigurations
{
    internal class PersonalDataConverter : ValueConverter<string, string>
    {
        public PersonalDataConverter(IPersonalDataProtector protector)
            : base((Expression<Func<string, string>>)((string s) => protector.Protect(s)), (Expression<Func<string, string>>)((string s) => protector.Unprotect(s)), (ConverterMappingHints)null)
        {
        }
    }
    public class ApplicationUserEntityConfiguration<User> : IEntityTypeConfiguration<User> where User: IdentityUser
    {
  
        public void Configure(EntityTypeBuilder<User> builder)
        {
            StoreOptions storeOptions =null;
            bool encryptPersonalData = storeOptions?.ProtectPersonalData ?? false;
            encryptPersonalData = false;//不执行
            PersonalDataConverter converter = null;
            IPersonalDataProtector personalDataProtector = null;
            //sqlserver The property 'ApplicationUser.LockoutEnd' is of type 'Nullable<DateTimeOffset>' which is not supported by current database provider. Either change the property CLR type or ignore 
            //the property using the '[NotMapped]' attribute or by using 'EntityTypeBuilder.Ignore' in 'OnModelCreating'.
            if (IdentityServer4Config.Driver== Driver.MySql5_5)
            {
                builder.Property(r => r.LockoutEnd)
              .HasColumnType("datetime")//mysql
              ;
            }
            else if(IdentityServer4Config.Driver== Driver.SqlServer)
            {
                builder.Ignore((r => r.LockoutEnd));
            }

            builder.HasMany<IdentityUserRole<string>>().WithOne().HasForeignKey((Expression<Func<IdentityUserRole<string>, object>>)((IdentityUserRole<string> ur) => ur.UserId))
                .IsRequired();

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasMaxLength(IdentityServer4Config.KeyMaxLength);
            builder.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");
            builder.ToTable("AspNetUsers");
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(u => u.UserName).HasMaxLength(256);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(256);
            if (encryptPersonalData)
            {
                converter = new PersonalDataConverter(personalDataProtector);
                foreach (PropertyInfo item in ((IEnumerable<PropertyInfo>)typeof(User).GetProperties()).Where((Func<PropertyInfo, bool>)((PropertyInfo prop) => Attribute.IsDefined(prop, typeof(ProtectedPersonalDataAttribute)))))
                {
                    if (item.PropertyType != typeof(string))
                    {
                        throw new InvalidOperationException("CanOnlyProtectStrings");
                    }

                    builder.Property(typeof(string), item.Name).HasConversion(converter);
                }
            }

            builder.HasMany<IdentityUserClaim<string>>().WithOne().HasForeignKey( uc => uc.UserId)
                .IsRequired();
            builder.HasMany<IdentityUserLogin<string>>().WithOne().HasForeignKey( ul => ul.UserId)
                .IsRequired();
            builder.HasMany<IdentityUserToken<string>>().WithOne().HasForeignKey( ut => ut.UserId)
                .IsRequired();
        }

    }
}
