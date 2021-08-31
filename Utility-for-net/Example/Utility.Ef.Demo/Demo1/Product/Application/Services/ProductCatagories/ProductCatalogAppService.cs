#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using Autofac.Annotation;
using Product.Application.Services.ProductCatagories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using Utility.Domain.Uow;
using Product.Domain.Entities;
using System.Linq.Expressions;
using Utility.Extensions;
using Product.Infrastructure;
using Utility.Ef.Uow;
using Utility.Ef;
using Utility.Application.Services;
using Product.Domain.Repositories;
using Product.Application.Services.Dtos;
using Utility.Interceptors;

namespace Product.Application.Services.ProductCatagories
{
    /// <summary>
    ///前端  的 菜单 信息
    /// </summary>
    [Component(typeof(IProductCatagoryAppService), AutofacScope = AutofacScope.InstancePerDependency
        , Interceptor = typeof(IocTranstationAopInterceptor), EnableAspect = true, 
        InterceptorType = InterceptorType.Interface
        )]

    public class ProductCatagoryAppService: CrudAppService<IProductCatagoryRepository,CreateProductCatagoryInput, UpdateProductCatagoryInput, GetProductCatagoryInput, GetProductCatagoryOutput, ProductCatagoryEntity,long>
        , IProductCatagoryAppService
    {
        //[Autowired]
        protected readonly ILogger<ProductCatagoryAppService> logger;

        public ProductCatagoryAppService(IProductCatagoryRepository repository,ILogger<ProductCatagoryAppService> logger):base(repository)
        {
            this.logger = logger;
           
        }

        //[Autowired] 
        //private readonly ProductDbContext productDbContext;
        /**
         -- 查询 分类 、子分类 及 父分类 商品(数据量 大话 没必要),再过滤
       SELECT
		c.id,
		c.NAME name,
		c.pid,
		p.NAME product_name,
		p.id product_id 
	FROM
		t_Catagory c
		LEFT JOIN (
		SELECT
			p.Catagory_id,
			p.id,
			p.NAME,
			p.creation_time 
		FROM
			t_product p 
		WHERE
			Catagory_id IN ( SELECT id FROM t_Catagory WHERE pid IS NOT NULL OR pid != '0' ) 
		ORDER BY
			hit DESC,
			creation_time 
		) p ON c.id = p.Catagory_id 
	ORDER BY
		c.pid,
		c.creation_time DESC,
	p.creation_time DESC 
         */
        /// <summary>
        /// 菜单 分类  
        /// 查询 分类 、子分类  及 父分类 前3 热销商品
        /// 一条  sql 查询 嵌套太多 重复 查询 sql 太多(效率低 sql 还复杂  不好维护);
        /// 查询 分类 、子分类  及 父分类 商品(数据量 大话 没必要),再过滤 ;
        /// 自定义  实现(存储过程 自定义 函数 实现 麻烦 数据库不同 语法也不同)
        /// 单表查询 再组合
        /// </summary>
        /// <returns></returns>
        public virtual IList<ProductCatagoryOutput> Catagories()
        {
            string sql = $"select Id,Name,Code,Pid,Flag from {ProductCatagoryEntity.TableName} where flag={(int)ProductCatagoryFlag.Nav} or flag={(int)ProductCatagoryFlag.ChildrenNav} order by order1,creation_time";
            List<ProductCatagoryOutput> Catagorys = FindCatagorys<ProductCatagoryOutput>(sql);
            if (Catagorys == null)
            {
                return Catagorys;
            }
            foreach (var item in Catagorys)
            {
                if(item.Cids!=null)
                {
                    //"An exception was thrown while attempting to evaluate a LINQ query parameter expression. 
                    //To show additional information call EnableSensitiveDataLogging() when overriding DbContext.OnConfiguring
                    Expression<Func<ProductEntity, bool>> where=null;//=it=>it.CatagoryID==item.Pids[0];
                    //for (int i = 1; i < item.Pids.Count; i++)
                    //{
                    //    where = where.Or(it => it.CatagoryID == item.Pids[i]);
                    //}      
                    foreach (var id in item.Cids)
                    {
                        where = where.Or(it => it.CatagoryId == id);
                    }
                    var productEntities = UnitWork.Query(where).OrderByDescending(it => it.Hit)
                        .OrderByDescending(it=>it.CreationTime).Select(it => new ProductOutput { Name= it.Name, Id= it.Id }).Take(3).ToList();
                    if (productEntities != null && productEntities.Count > 0)
                    {
                        item.Products = item.Products ?? new List<ProductOutput>();
                        item.Products.AddRange(productEntities);
                    }
                }
            }
            return Catagorys;
        }

        /// <summary>
        ///  导航 菜单 分类  
        /// </summary>
        /// <returns></returns>
        public virtual List<NavgationOutput> NavgationCatagories()
        {
            //这样 查询绑定数据为 null
            List<NavgationOutput> CatagoryEntities = UnitWork.Connection.Query<NavgationOutput>($"select Id,Name,Code from {ProductCatagoryEntity.TableName}  where flag={(int)ProductCatagoryFlag.Nav}   order by order1,creation_time").ToList();
            return CatagoryEntities;
        }

        /// <summary>
        /// 底部导航菜单
        /// </summary>
        /// <returns></returns>
        public virtual IList<BottomOutput> Bottom()
        {
            string sql = $"select Id,Name,Code,Pid,Flag from {ProductCatagoryEntity.TableName} where flag={(int)ProductCatagoryFlag.BottomNav}  order by order1,creation_time";
            List<BottomOutput> Catagorys = FindCatagorys<BottomOutput>(sql);
            return Catagorys;
        }
        /// <summary>
        /// 底部导航菜单 链接
        /// </summary>
        /// <returns></returns>
        public virtual IList<BottomNavgationOutput> BottomLink()
        {
            string sql = $"select Id,Name,Http,Target from {ProductCatagoryEntity.TableName} where flag={(int)ProductCatagoryFlag.BottomNavLink}  order by order1,creation_time";
            List<BottomNavgationOutput> bottoms = UnitWork.Connection.Query<BottomNavgationOutput>(sql).ToList();
            return bottoms;
        }
        /// <summary>
        /// 整理 菜单 分类  
        /// </summary>
        /// <returns></returns>
        public virtual List<T> FindCatagorys<T>(string sql)where T:BottomOutput<T>,new()
        {
            //这样 查询绑定数据为 null
            List<ProductCatagoryEntity> CatagoryEntities = UnitWork.Connection.Query<ProductCatagoryEntity>(sql).ToList();
            //List<CatagoryEntity> CatagoryEntities = unitWork.Find<CatagoryEntity>().OrderByDescending(it => it.Order1)
            //    .OrderByDescending(it => it.CreationTime).Select(it => new CatagoryEntity() { Id = it.Id, Name = it.Name, Code = it.Code, Pid = it.Pid }).ToList();

            if (CatagoryEntities == null || CatagoryEntities.Count == 0)
            {
                logger.LogInformation(" find  Catagory success,not have data ");
                return null;
            }
            List<T> Catagorys = new List<T>();
            List<ProductCatagoryEntity> notFounds = new List<ProductCatagoryEntity>();
            foreach (var item in CatagoryEntities)
            {
                T CatagoryOutput = new T();
                Set(CatagoryOutput, item);
                if (item.ParentId > 0 &&item.Flag== ProductCatagoryFlag.Nav)
                {
                    Catagorys.Add(CatagoryOutput);
                }
                else
                {
                    T parent = Catagorys.Find(it => it.Id== item.ParentId);
                    if (parent == null)
                    {
                        logger.LogWarning(" find parent Catagory fail,add notFounds list, {Pid} ", item.ParentId);
                        notFounds.Add(item);
                    }
                    else
                    {
                        if(item.Flag== ProductCatagoryFlag.ChildrenNav)
                        {
                            SetParent(parent, CatagoryOutput, item);
                        }
                        else
                        {
                            logger.LogWarning(" find parent Catagory success, {Pid} children {Id} not match ", item.ParentId, item.Id);
                        }
                    }
                }
            }
            if (notFounds.Any())
            {
                foreach (var item in notFounds)
                {
                    T CatagoryOutput = new T();
                    Set(CatagoryOutput, item);
                    T parent = Catagorys.Find(it => it.Id==item.ParentId);
                    if (parent == null)
                    {
                        logger.LogError("notFounds list find parent Catagory fail,{Pid} ", item.ParentId);
                        //throw new Exception($"notFounds list find parent Catagory fail,{item.Pid} ");
                    }
                    else
                    {
                        SetParent(parent,CatagoryOutput,item);
                    }
                }
            }
            return Catagorys;
        }

        protected void SetParent<T>(T parent, T CatagoryOutput, ProductCatagoryEntity item) where T : BottomOutput<T>, new()
        {
            parent.Cids = parent.Cids ?? new List<long>();
            parent.Children = parent.Children ?? new List<T>();
            parent.Children.Add(CatagoryOutput);
            parent.Cids.Add(item.Id);
        }

        protected void Set<T>(T CatagoryOutput, ProductCatagoryEntity item) where T : BottomOutput<T>, new()
        {
           
            CatagoryOutput.Id = item.Id;
            CatagoryOutput.Name = item.Name;
            CatagoryOutput.Code = item.Code;
        }

        public virtual IList<ProductCatagoryOutput> DapperCatagorys()
        {
            string sql = @"";
            var data = Repository.Connection.Query(sql);
            List<ProductCatagoryOutput> Catagorys = new List<ProductCatagoryOutput>();
            foreach (var item in data)
            {
                ProductCatagoryOutput CatagoryOutput = new ProductCatagoryOutput();
                CatagoryOutput.Id = item.id;
                CatagoryOutput.Name = item.name;
                if (item.pid == null || item.pid == "0")
                {
                    Catagorys.Add(CatagoryOutput);
                }
                else
                {
                    string pid = item.pid;
                    var parent = Catagorys.Find(it => it.Id.Equals(pid));
                    if (parent == null)
                    {
                        logger.LogWarning("Catagory id {CatagoryId} not found", pid);
                    }
                    else
                    {
                        //parent.Children ??= new List<CatagoryOutput>();
                        parent.Children = parent.Children ?? new List<ProductCatagoryOutput>();
                        parent.Children.Add(CatagoryOutput);
                        if (item.product_id != null)
                        {
                            CatagoryOutput.Products = CatagoryOutput.Products ?? new List<ProductOutput>();
                            ProductOutput productOutput = new ProductOutput();
                            CatagoryOutput.Products.Add(productOutput);
                            productOutput.Id = item.product_id;
                            productOutput.Name = item.product_name;
                        }
                    }
                }
            }
            return Catagorys;
        }


        public virtual List<CatagoryDto> FindCatagory()
        {
            var datas = Repository.FindCatagory();
            if (datas?.Count == 0)
            {
                return null;
            }
            List<CatagoryDto> CatagoryDtos = new List<CatagoryDto>();
            Dictionary<long, CatagoryDto> caches = new Dictionary<long, CatagoryDto>();
            foreach (var item in datas)
            {
                CatagoryDto temp = new CatagoryDto() { Label = item.Name, Value = item.Id };
                CatagoryDto Catagory = null;
                if (item.ParentId == 0)
                {
                    if (caches.ContainsKey(item.ParentId))
                    {
                        Catagory = caches[item.ParentId];
                        Catagory.Label = item.Name;
                    }
                    else
                    {
                        CatagoryDtos.Add(temp);
                        caches[item.ParentId] = temp;
                    }
                }
                else
                {
                    if (caches.ContainsKey(item.ParentId))
                    {
                        Catagory = caches[item.ParentId];
                    }
                    else
                    {
                        Catagory = new CatagoryDto() { Value = item.ParentId };
                        CatagoryDtos.Add(Catagory);
                        caches[item.ParentId] = Catagory;
                    }
                    Catagory.Children = Catagory.Children ?? new List<CatagoryDto>();
                    Catagory.Children.Add(temp);
                }
            }
            return CatagoryDtos;
        }
    }
}
#endif