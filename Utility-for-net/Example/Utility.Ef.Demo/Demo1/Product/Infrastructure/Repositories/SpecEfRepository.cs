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
	/// 产品 规格 仓库 基于 ef 实现 
	/// </summary>
	[Component(typeof(ISpecRepository), AutofacScope = AutofacScope.InstancePerLifetimeScope
		,Interceptor =typeof(IocTranstationAopInterceptor),EnableAspect =true,
		InterceptorType = InterceptorType.Interface
		)]
	public class SpecEfRepository : BaseEfRepository<SpecEntity>, ISpecRepository
    {
        public SpecEfRepository(DbContextProvider<ProductDbContext> dbContext) : base(dbContext)
        {

        }
		protected override Expression<Func<SpecEntity, bool>> GetWhere(SpecEntity entity)
		{
			Expression<Func<SpecEntity, bool>> where = null;
			if (entity.ProductId.HasValue&& entity.ProductId.Value>0)
			{
				where = where.Or(it => it.ProductId == entity.ProductId);
			}
			if (!string.IsNullOrEmpty(entity.Size))
			{
				where = where.Or(it => it.Size.Contains(entity.Size));
			}
			if (!string.IsNullOrEmpty(entity.Color))
			{
				where = where.Or(it => it.Color.Contains(entity.Color));
			}
			return where;
		}

		public bool AddSocket(long id, int num)
		{
			base.Update(it => it.Id == id, it => new SpecEntity() { Stock = it.Stock + num });
			return true;
		}

		public bool RemoveSocket(long id, int num)
		{
			base.Update(it => it.Id == id, it => new SpecEntity() { Stock = it.Stock - num });
			return true;
		}
		public bool UpdatePrice(long id, decimal newPrice)
		{
			base.Update(it => it.Id == id, it => new SpecEntity() { Price = newPrice });
			return true;
		}
	}
}
#endif