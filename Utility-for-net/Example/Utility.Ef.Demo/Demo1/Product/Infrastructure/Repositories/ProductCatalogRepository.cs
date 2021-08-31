#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1

using Product.Domain.Entities;
using Product.Domain.Repositories;
using System;
using System.Collections.Generic;
#if NET48
using System.Data.Entity;
#else
using Microsoft.EntityFrameworkCore;
#endif
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Ef.Repositories;
using Utility.Ef;
using Utility.Application.Services.Dtos;
using Autofac.Annotation;
using Utility.Interceptors;

namespace Product.Infrastructure.Repositories
{
    [Component(typeof(IProductCatagoryRepository), AutofacScope = AutofacScope.InstancePerLifetimeScope
          , Interceptor = typeof(IocTranstationAopInterceptor), EnableAspect = true,
        InterceptorType = InterceptorType.Interface
          )]
    public class ProductCatagoryRepository :BaseEfRepository<ProductDbContext, ProductCatagoryEntity, long>, IProductCatagoryRepository
       // where Context: DbContext
    {
        public ProductCatagoryRepository(DbContextProvider<ProductDbContext> context) : base(context)
        {
        }

        public IList<ProductCatagoryEntity> Find(ProductCatagoryEntity entity)
        {
            throw new NotImplementedException();
        }

        public IList<ProductCatagoryEntity> FindByPage(ProductCatagoryEntity entity, int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }

        public IList<ProductCatagoryEntity> FindCatagory()
        {
            throw new NotImplementedException();
        }

        public ResultDto<ProductCatagoryEntity> FindResultDtoByPage(ProductCatagoryEntity entity, int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }

        public virtual List<ProductCatagoryEntity> GetCatagory()
        {
            return Query().Where(it => !it.IsDeleted).ToList();
            // return GetAll().Where(it=>!it.IsDeleted&&!it.ParentId.HasValue).Include(it => it.Children).ToList();
        }

        public virtual List<ProductCatagoryEntity> GetParentCatagory()
        {
            return Query().Where(it => !it.IsDeleted && it.ParentId==0)
                .Select(it => new ProductCatagoryEntity() { Id = it.Id, Name = it.Name }).ToList();
        }
    }
}
#endif