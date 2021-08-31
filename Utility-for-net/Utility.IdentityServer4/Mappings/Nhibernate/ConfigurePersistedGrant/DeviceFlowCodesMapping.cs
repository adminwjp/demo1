using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Utility.IdentityServer4.Mappings
{
    public class DeviceFlowCodesMapping : ClassMapping<DeviceFlowCodes>
    {
        public DeviceFlowCodesMapping()
        {
            Lazy(false);
            Table(IdentityServer4Config.OperationalStore.DeviceFlowCodes.Name);
            Property((Expression<Func<DeviceFlowCodes, string>>)((DeviceFlowCodes x) => x.DeviceCode)
                ,x=> { x.Length(200);x.NotNullable(true);x.Index("DeviceCode");x.Unique(true); });
            Property((Expression<Func<DeviceFlowCodes, string>>)((DeviceFlowCodes x) => x.UserCode)
                , x => { x.Length(IdentityServer4Config.KeyMaxLength); x.NotNullable(true); })
           // .HasMaxLength(200)
           //mysql Specified key was too long; max key length is 767 bytes
            ;
            Property((Expression<Func<DeviceFlowCodes, string>>)((DeviceFlowCodes x) => x.SubjectId), x => { x.Length(200); });
            Property((Expression<Func<DeviceFlowCodes, string>>)((DeviceFlowCodes x) => x.SessionId), x => { x.Length(100); });
            Property((Expression<Func<DeviceFlowCodes, string>>)((DeviceFlowCodes x) => x.ClientId), x => { x.Length(200); x.NotNullable(true); });
            Property((Expression<Func<DeviceFlowCodes, string>>)((DeviceFlowCodes x) => x.Description), x => { x.Length(200);  });
            // MySql || SqlServer
            //if (IdentityServer4Config.Driver == Driver.MySql5_5)
            {
                Property((Expression<Func<DeviceFlowCodes, DateTime>>)((DeviceFlowCodes x) => x.CreationTime))
               //mysql
              ;
                Property((Expression<Func<DeviceFlowCodes, DateTime?>>)((DeviceFlowCodes x) => x.Expiration),x=> { x.Index("Expiration"); })
                 //mysql
                 ;
            }


            Property((Expression<Func<DeviceFlowCodes, string>>)((DeviceFlowCodes x) => x.Data), x => { x.Length(50000); x.NotNullable(true); });
            Id((Expression<Func<DeviceFlowCodes, object>>)((DeviceFlowCodes x) => x.UserCode));

        }
    }
}
