using Autofac;
using Autofac.Extras.DynamicProxy;
using SocialContact.Application.Services;
using SocialContact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Application.Services;
using Utility.Demo;
using Utility.Demo.Domain.Entities;
using Utility.Domain.Repositories;
using Utility.Extensions;
using Utility.Interceptors;
using Utility.Ioc;

namespace SocialContact
{
    public class SocialContactModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            //asp net core 
            //Cannot resolve parameter 'SocialContact.Application.Services.AdminAppService adminAppService
            var ts = new Type[] { typeof(CatagoryAppService), typeof(EdutionAppService),
                typeof(IconAppService), typeof(RelationAppService), typeof(UserMenuAppService)
                , typeof(WorkAppService)};
            foreach (var t in ts)
            {
                builder.RegisterGeneric(
                    //t
                    (it, types1) =>
                    {
                        var ts1 = t.BaseType.GenericTypeArguments;
                        var temps = new Type[] { ts1[1], ts1[2] };
                        var r = typeof(IRepository<,>).MakeGenericType(temps);
                        return Activator.CreateInstance(t, new object[] { it.ResolveNamed("SocialContactRepository", r) });
                    }
                )
                .As(t).Named(t.Name,t)
                .InstancePerDependency()
                .InterceptedBy(typeof(IocTranstationAopInterceptor));
                //.EnableClassInterceptorsByGeneric();
            }
        }
    }
}
