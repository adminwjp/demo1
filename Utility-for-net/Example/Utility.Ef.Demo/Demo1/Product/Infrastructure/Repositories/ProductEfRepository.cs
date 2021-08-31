#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Autofac.Annotation;
using Product.Domain.Entities;
using Product.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utility.Ef;
using Utility.Ef.Repositories;
using Utility.Extensions;
using Utility.Interceptors;
using Z.EntityFramework.Plus;

namespace Product.Infrastructure.Repositories
{
	/// <summary>
	/// 产品 仓库 基于 ef 实现 
	/// </summary>
	[Component(typeof(IProductRepository), AutofacScope = AutofacScope.InstancePerLifetimeScope
		,Interceptor =typeof(IocTranstationAopInterceptor),EnableAspect =true, 
		InterceptorType = InterceptorType.Interface
		)]
	public class ProductEfRepository : BaseEfRepository<ProductEntity>, IProductRepository
    {
        public ProductEfRepository(DbContextProvider<ProductDbContext> dbContext ):base(dbContext)
        {

        }
        protected override Expression<Func<ProductEntity, bool>> GetWhere(ProductEntity entity)
        {
                Expression<Func<ProductEntity, bool>> where = null;
                if (entity.CatagoryId>0)
                {
					where = where.Or(it => it.CatagoryId == entity.CatagoryId);
				}
				if (!string.IsNullOrEmpty(entity.Keywords))
				{
					where = where.Or(it => it.Keywords.Contains(entity.Keywords));
				}
				if (!string.IsNullOrEmpty(entity.CreateAccount))
				{
					where = where.Or(it => it.CreateAccount == entity.CreateAccount);
				}
				if (!string.IsNullOrEmpty(entity.Title))
				{
					where = where.Or(it => it.Title.Contains(entity.Title));
				}
				if (!string.IsNullOrEmpty(entity.UpdateAccount))
				{
					where = where.Or(it => it.UpdateAccount == entity.UpdateAccount);
				}
				if (entity.ActivityId>0)
				{
					where = where.Or(it => it.ActivityId == entity.ActivityId);
				}
				if (entity.Status!=0)
				{
					where = where.Or(it => it.Status == entity.Status);
				}
				if (!string.IsNullOrEmpty(entity.SearchKey))
				{
					where = where.Or(it => it.SearchKey.Contains(entity.SearchKey));
				}
				if (!string.IsNullOrEmpty(entity.Name))
				{
					where = where.Or(it => it.Name.Contains(entity.Name));
				}
				if (entity.GiftID>0)
				{
					where = where.Or(it => it.GiftID == entity.GiftID);
				}
                return where;
        }

		public bool AddSocket(long id,int num)
        {
			base.Update(it => it.Id == id,it=> new ProductEntity() { Stock=it.Stock+num});
			return true;
        }

		public bool RemoveSocket(long id, int num)
		{
			base.Update(it => it.Id == id, it => new ProductEntity() { Stock = it.Stock - num });
			return true;
		}
		public bool UpdatePrice(long id, decimal newPrice)
		{
			base.Update(it => it.Id == id, it => new ProductEntity() { Price= newPrice });
			return true;
		}
	}
}
#endif