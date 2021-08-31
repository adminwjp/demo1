#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using Autofac.Annotation;
using Product.Application.Services.Specs;
using Product.Domain.Entities;
using Product.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Application.Services;
using Utility.Cache;
using Utility.Mappers;
using Utility.Helpers;
using Utility.Interceptors;

namespace Product.Application.Services.Specs
{
    public interface ISpecAppService: ICrudAppService<ISpecRepository, CreateSpecInput, UpdateSpecInput, GetSpecInput, GetSpecOutput, SpecEntity, long>
    {

    }
    /// <summary>
    /// 产品 规格 服务
    /// </summary>
    [Component(typeof(ISpecAppService), AutofacScope = AutofacScope.InstancePerLifetimeScope
        , Interceptor = typeof(IocTranstationAopInterceptor), EnableAspect = true,
        InterceptorType = InterceptorType.Interface
        )]
 
    public class SpecAppService : CrudAppService<ISpecRepository, CreateSpecInput,UpdateSpecInput,
        GetSpecInput,GetSpecOutput, SpecEntity,long>,
        ISpecAppService
    {
     
        public SpecAppService(ISpecRepository repository, IMapper objectMapper, ICacheContent cache)
            : base(repository)
        {
            this.Mapper = objectMapper;
            this.Cache = cache;
        }
        

        /// <summary>
        /// 根据商品 id 查询商品规格 任何用户可以查询 展示 页面显示
        /// 1.放人缓存里面  缓存查询; 内存 还是 分布式 缓存 查询, 内存 查询所有 再次条件查询; 分布式缓存 需要什么查询什么 
        /// 2.不放人缓存 直接库里 查
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>

        public IList<GetSpecOutput> FindByPage(long productID, int page = 1, int size = 10)
        {
            var res = Repository.QueryByPage(it=>it.ProductId==productID&&!it.IsDeleted, page, size);
            var result = Mapper.Map<IList<GetSpecOutput>>(res);
            return result;
        }
        public void Products(string productId)
        {

        }
    }
}
#endif