#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using Autofac.Annotation;
using Product.Domain.Entities;
using Product.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utility.Ef;
using Utility.Interceptors;

namespace Product.Infrastructure.Repositories
{
    [Component(typeof(IProductCatagoryRepository), AutofacScope = AutofacScope.InstancePerLifetimeScope
        ,Interceptor =typeof(IocTranstationAopInterceptor),EnableAspect =true,
        InterceptorType = InterceptorType.Interface
        )]
    public class CatagoryEfRepository : BaseEfRepository<ProductCatagoryEntity>, IProductCatagoryRepository
    {
        public CatagoryEfRepository(DbContextProvider<ProductDbContext> dbContext) : base(dbContext)
        {

        }

        public IList<ProductCatagoryEntity> FindCatagory()
        {
            return base.Query((Expression<Func<ProductCatagoryEntity,bool>>)null).Select(it=>new ProductCatagoryEntity {Id=it.Id,Name=it.Name, ParentId = it.ParentId }).ToList();
        }

        public List<ProductCatagoryEntity> GetCatagory()
        {
            throw new NotImplementedException();
        }

        public List<ProductCatagoryEntity> GetParentCatagory()
        {
            throw new NotImplementedException();
        }
    }
}
#endif