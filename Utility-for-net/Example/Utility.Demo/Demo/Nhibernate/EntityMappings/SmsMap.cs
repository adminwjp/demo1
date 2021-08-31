namespace Utility.Demo.Nhibernate.EntityMappings
{
    using FluentNHibernate.Mapping;
    using NHibernate.Mapping.ByCode;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utility.Demo.Domain.Entities;
    using Utility.Nhibernate.EntityMappings;

    public class SmsMap: BaseNhibernateMapp<SmsEntity,long>
    {
        public SmsMap():base("t_sms"){
          
        }
        protected override void Set()
        {
            Id(x => x.AppId, x => { x.Column("id");x.Generator(Generators.Identity); });
            Property(x => x.AppId, x => { x.Column("app_id"); x.Length(50); });
            Property(x => x.Secrpt, x => { x.Column("secrt"); x.Length(50); });

        }
    }
}