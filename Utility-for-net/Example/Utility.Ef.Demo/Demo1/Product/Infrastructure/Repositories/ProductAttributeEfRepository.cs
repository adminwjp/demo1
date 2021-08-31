#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using Autofac.Annotation;
using Product.Domain.Entities;
using Product.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Ef;
using Utility.Interceptors;

namespace Product.Infrastructure.Repositories
{
    [Component(typeof(IProductAttributeRepository), AutofacScope = AutofacScope.InstancePerLifetimeScope
        ,Interceptor =typeof(IocTranstationAopInterceptor),EnableAspect =true,
        InterceptorType = InterceptorType.Interface
        )]

    public class ProductAttributeEfRepository: BaseEfRepository<ProductAttribueEntity>, IProductAttributeRepository
    {
        public ProductAttributeEfRepository(DbContextProvider<ProductDbContext> dbContext) : base(dbContext)
        {

        }
    }
}
#endif