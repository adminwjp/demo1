using Autofac.Annotation;
using Product.Domain.Entities;
using Product.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain.Repositories;
using Utility.Ef;
using Utility.Interceptors;

namespace Product.Infrastructure.Repositories
{
    [Component(typeof(IBrandRepository), "BrandRepository", AutofacScope = AutofacScope.InstancePerLifetimeScope
       , Interceptor = typeof(IocTranstationAopInterceptor), EnableAspect = true, InterceptorType = InterceptorType.Interface
       )]

    public class BrandRepository : Utility.Ef.Repositories.BaseEfRepository<BrandEntity,long>, IBrandRepository
    {
        public BrandRepository(DbContextProvider<ProductDbContext> dbContext) : base(dbContext)
        {

        }
    }
}
