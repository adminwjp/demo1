#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using Autofac.Annotation;
using Product.Application.Services.Products;
using Product.Application.Services.Specs;
using Product.Domain.Entities;
using Product.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utility.Application.Services;
using Utility.Cache;
using Utility.Ioc;
using Utility.Mappers;
using Utility.Helpers;
using Utility.Interceptors;

namespace Product.Application.Services.Products
{
   
    /// <summary>
    /// 产品 服务 
    /// </summary>
    [Component(typeof(IProductAppService), AutofacScope = AutofacScope.InstancePerDependency
        , Interceptor = typeof(IocTranstationAopInterceptor), EnableAspect = true, 
        InterceptorType = InterceptorType.Interface
        )]
    public class ProductAppService: CrudAppService<IProductRepository, CreateProductInput,UpdateProductInput,GetProductInput,GetProductOutput, ProductEntity,long>,
        IProductAppService
    {
        private IProductRepository repository;
        public ProductAppService(IProductRepository repository,IMapper objectMapper,IIocManager iocManager, ICacheContent cache) 
            :base(repository)
        {
            this.Mapper = objectMapper;
            this.IocManager = iocManager;
            this.Cache = cache;
        }
        
       

        /// <summary>
        /// 根据分类 查询商品 任何用户可以查询 展示 页面显示
        /// 1.放人缓存里面  缓存查询; 内存 还是 分布式 缓存 查询, 内存 查询所有 再次条件查询; 分布式缓存 需要什么查询什么 
        /// 2.不放人缓存 直接库里 查
        /// </summary>
        /// <param name="CatagoryId"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>

        public  IList<GetProductOutput> FindByPage(long CatagoryId,int page = 1, int size = 10)
        {
            var res = repository.QueryByPage(it=>!it.IsDeleted&&it.CatagoryId==CatagoryId, page, size);
            var result = Mapper.Map<IList<GetProductOutput>>(res);
            return result;
        }

        /// <summary>
        /// 根据分类 查询商品 任何用户可以查询 展示 页面显示
        /// 1.放人缓存里面  缓存查询; 内存 还是 分布式 缓存 查询, 内存 查询所有 再次条件查询; 分布式缓存 需要什么查询什么 
        /// 2.不放人缓存 直接库里 查
        /// </summary>
        /// <param name="CatagoryId"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>

        public IList<ProductAndSpecDto> FindToGetAllProductAndSpecDtoByPage(long CatagoryId, int page = 1, int size = 10)
        {
            var res = repository.QueryByPage(it => !it.IsDeleted && it.CatagoryId == CatagoryId, page, size);
            var result = Mapper.Map<IList<ProductAndSpecDto>>(res);
            var spce = IocManager.Get<SpecAppService>();
            foreach (var item in result)
            {
                var specDtos = spce.FindByPage(item.Id, page, size);
                item.Specs = specDtos;
            }
            return result;
        }

        /// <summary>
        /// 添加库存
        /// 1.如果放入缓存 也要 更新 分类服务 的商品信息
        /// 2.不放人缓存 直接数据库操作 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool AddSocket(long id, int num)
        {
            repository.AddSocket(id, num);
            return true;
        }

        /// <summary>
        /// 移除库存 修改库存
        /// 1.如果放入缓存 也要 更新 分类服务 的商品信息
        /// 2.不放人缓存 直接数据库操作 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool RemoveSocket(long id, int num)
        {
            repository.RemoveSocket(id, num);
            return true;
        }

        /// <summary>
        /// 价格变动
        /// 1.如果放入缓存 也要 更新 分类服务 的商品信息
        /// 2.不放人缓存 直接数据库操作 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newPrice"></param>
        /// <returns></returns>
        public bool UpdatePrice(long id, decimal newPrice)
        {
            repository.UpdatePrice(id, newPrice);
            return true;
        }/// <summary>
         /// 获取 左侧 热门推荐商品
         /// </summary>
         /// <returns></returns>
        public virtual IList<LeftHotProductOutput> LeftHotProducts(int size = 10)
        {
            //string sql = $"select Price ,now_Price NowPrice,Picture,Name from {ProductEntity.TableName} order by sell_count desc,hit desc,creation_time ";
            //List<LeftHotProductOutput> leftHotProductOutputs = unitWork.Connection.Query<LeftHotProductOutput>(sql).ToList();
            List<LeftHotProductOutput> leftHotProductOutputs = UnitWork.Query<ProductEntity>().OrderByDescending(it => it.Sales)
                .OrderByDescending(it => it.Hit).OrderByDescending(it => it.CreationTime).Select(it => new LeftHotProductOutput()
                { Id = it.Id, Price = it.Price, Picture = it.Picture, NowPrice = it.Price, Name = it.Name }).Take(size).ToList();
            return leftHotProductOutputs;
        }
        /// <summary>
        /// 获取  热门商品
        /// </summary>
        /// <returns></returns>
        public virtual IList<LeftHotProductOutput> HotProducts(int size = 10)
        {
            //string sql = $"select Price ,now_Price NowPrice,Picture,Name from {ProductEntity.TableName} order by sell_count desc,hit desc,creation_time ";
            //List<LeftHotProductOutput> leftHotProductOutputs = unitWork.Connection.Query<LeftHotProductOutput>(sql).ToList();
            List<LeftHotProductOutput> leftHotProductOutputs = UnitWork.Query<ProductEntity>().OrderByDescending(it => it.Sales)
                .OrderByDescending(it => it.Hit).OrderByDescending(it => it.CreationTime).Select(it => new LeftHotProductOutput()
                { Id = it.Id, Price = it.Price, Picture = it.Picture, NowPrice = it.Price, Name = it.Name }).Take(size).ToList();
            return leftHotProductOutputs;
        }
        /// <summary>
        /// 获取  最新商品
        /// </summary>
        /// <returns></returns>
        public virtual IList<LeftHotProductOutput> NewProducts(int size = 10)
        {
            //string sql = $"select Price ,now_Price NowPrice,Picture,Name from {ProductEntity.TableName} order by sell_count desc,hit desc,creation_time ";
            //List<LeftHotProductOutput> leftHotProductOutputs = unitWork.Connection.Query<LeftHotProductOutput>(sql).ToList();
            List<LeftHotProductOutput> leftHotProductOutputs = UnitWork.Query<ProductEntity>(it => it.IsNew )
                .OrderByDescending(it => it.CreationTime).Select(it => new LeftHotProductOutput()
                { Id = it.Id, Price = it.Price, Picture = it.Picture, NowPrice = it.Price, Name = it.Name }).Take(size).ToList();
            return leftHotProductOutputs;
        }
        /// <summary>
        /// 获取  特价 商品
        /// </summary>
        /// <returns></returns>
        public virtual IList<LeftHotProductOutput> SpecialPriceProducts(int size = 10)
        {
            //string sql = $"select Price ,now_Price NowPrice,Picture,Name from {ProductEntity.TableName} order by sell_count desc,hit desc,creation_time ";
            //List<LeftHotProductOutput> leftHotProductOutputs = unitWork.Connection.Query<LeftHotProductOutput>(sql).ToList();
            List<LeftHotProductOutput> leftHotProductOutputs = UnitWork.Query<ProductEntity>(it => it.Sale)
                .OrderByDescending(it => it.CreationTime).Select(it => new LeftHotProductOutput()
                { Id = it.Id, Price = it.Price, Picture = it.Picture, NowPrice = it.Price, Name = it.Name }).Take(size).ToList();
            return leftHotProductOutputs;
        }
        public void Products(string CatagoryId)
        {

        }

        public void Product(string productId)
        {

        }
    }
}
#endif