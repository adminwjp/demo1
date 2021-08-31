
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Linq.Expressions;

namespace Utility.IdentityServer4.Mappings
{
    internal class PersonalDataConverter : ValueConverter<string, string>
    {
        public PersonalDataConverter(IPersonalDataProtector protector)
            : base((Expression<Func<string, string>>)((string s) => protector.Protect(s)), (Expression<Func<string, string>>)((string s) => protector.Unprotect(s)), (ConverterMappingHints)null)
        {
        }
    }
    /// <summary>
    /// xml mapp 必须虚方法(属性)不然错误 ,注解可以需要 虚方法(属性)
    /// </summary>
    public class ApplicationUserMapping <User>: ClassMapping<User> where User: IdentityUser
    {
        public ApplicationUserMapping()
        {
            Lazy(false);
            //sqlserver The property 'ApplicationUser.LockoutEnd' is of type 'Nullable<DateTimeOffset>' which is not supported by current database provider. Either change the property CLR type or ignore 
            //the property using the '[NotMapped]' attribute or by using 'EntityTypeBuilder.Ignore' in 'OnModelCreating'.
            if (IdentityServer4Config.Driver != Driver.SqlServer)
            {
                Property(x => x.LockoutEnd);// mysql datetime
            }

            Id(u => u.Id);
            Property(u => u.Id, x=> { x.Length(IdentityServer4Config.KeyMaxLength); });
            Property(u => u.NormalizedUserName, x => { x.Index("UserNameIndex"); x.Length(256); x.Unique(true); });
            Property(u => u.NormalizedEmail, x => { x.Index("EmailIndex"); x.Length(256); });

            Table("AspNetUsers");
            //乐观锁
            Version(u => u.ConcurrencyStamp, x=> { 
                    
                });
            Property(u => u.UserName, x => { x.Length(256); });
            Property(u => u.Email, x => { x.Length(256); });

        }
    }
}
