#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0
using System;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Utility.Application.Services;
using Utility.Application.Services.Dtos;
using Utility.Attributes;
using Utility.Cache;
using Utility.Domain.Entities;
using Utility.Domain.Repositories;
using Utility.Domain.Services;
using Utility.Domain.Uow;
using Utility.Json;
using Microsoft.Extensions.DependencyInjection;
using Utility.Ioc;
using Microsoft.AspNetCore.Http;

namespace Utility.AspNetCore.Controllers
{
    [Transtation]
    public abstract class BaseController : ControllerBase
    {
        private ICacheContent cache;
        private IIocManager iocManager;
        public virtual IHttpContextAccessor ContextAccessor { get; set; }
        public virtual ICacheContent Cache
        {
            get
            {
                if (cache == null)
                {
                    //HttpContext null
                    //HttpContext.RequestServices null
                    //HttpContext.RequestServices.CreateScope().ServiceProvider null
                    //Request.HttpContext.RequestServices null
                    // HttpContext.RequestServices.GetRequiredService
                    cache = ContextAccessor?.HttpContext.RequestServices.GetRequiredService<ICacheContent>();
                }
                return cache;
            }
            protected set => cache = value;
        }
        public virtual DomainService Service { get; protected set; }
        public virtual IRepository Repository { get; protected set; }
        public virtual IUnitWork UnitWork { get; protected set; }
        public virtual IIocManager IocManager { 
            get {
                if (iocManager == null)
                {
                    iocManager = ContextAccessor?.HttpContext.RequestServices.GetRequiredService<IIocManager>();
                }
                return iocManager;
            } 
            set => iocManager = value; }
        protected virtual Language GetLanguage()
        {
            return Language.Chinese;
        }
    }
    /// <summary>
    /// [FromForm,FromBody] 只支持 FromForm (支持 普通 类型,form body from-data xml都支持)
    ///Microsoft.AspNetCore.Routing.Matching.AmbiguousMatchException: The request matched multiple endpoints. Matches:
    ///async	 not access  访问不了 咋回事  asp.net core 5.0 
    ///InsertAsync not access, Insert access
    ///async FromBody 支持  FromForm 一直请求中
    /// </summary>
    /// <typeparam name="ResponseApiService"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Service"></typeparam>
    [Route("api/[controller]")]
#if !NETCOREAPP2_0
    [ApiController]
#endif
    [ProducesResponseType(typeof(IResponseApi),200)]

    public class BaseController<ResponseApiService, Service, T, Key> : BaseController<ResponseApiService, Service, IRepository<T, Key>, T, T, T, T, T, Key>
         where ResponseApiService : ResponseApiService<Service, T, Key>
         where Service : CrudAppService<T, Key>, new()
         where T : class,IEntity<Key>
    {
     
    }

    [Route("api/[controller]")]
#if !NETCOREAPP2_0
    [ApiController]
#endif
    [ProducesResponseType(typeof(IResponseApi), 200)]

    public class BaseController<Service, T, Key> : BaseController< Service, IRepository<T, Key>, T, T, T, T, T, Key>
         
 where Service : ICrudAppService<T, Key>
        
 where T : class, IEntity<Key>
    {

    }

    [Route("api/[controller]")]
#if !NETCOREAPP2_0
    [ApiController]
#endif
    public class BaseControllerByRepository<RepositoryImpl, T, Key> : BaseController
 where RepositoryImpl : IRepository<T, Key>
 where T : class, IEntity<Key>
    {
        private RepositoryImpl repository;

        protected RepositoryImpl Repository { get => repository; set { repository = value; base.Repository = value; } }
        public BaseControllerByRepository()
        {
           // Cache = HttpContext.RequestServices.GetRequiredService<ICacheContent>();
        }
        public BaseControllerByRepository(RepositoryImpl repository):this()
        {
            this.Repository = repository;
        }

        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        [HttpPost("Add")]
        public virtual async Task<ResponseApi> Add([FromForm, FromBody] T obj)
        {
            var res = await Repository.InsertAsync(obj);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }

        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        [HttpPost("Insert")]
        public virtual ResponseApi Insert([FromForm, FromBody] T obj)
        {
            var res = Repository.Insert(obj);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }



        /// <summary>修改实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        [HttpPost("Modify")]
        public virtual async System.Threading.Tasks.Task<ResponseApi> Modify([FromForm, FromBody] T obj)
        {
            var res = await Repository.UpdateAsync(obj);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }

        /// <summary>修改实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        [HttpPost("Update")]
        public virtual ResponseApi Update([FromForm, FromBody] T obj)
        {
            var res = Repository.Update(obj);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }

        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        [HttpGet("Remove")]
        public virtual async System.Threading.Tasks.Task<ResponseApi> Remove(Key id)
        {
            var res = await Repository.DeleteAsync(id);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }

        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        [HttpGet("Delete")]
        public virtual ResponseApi Delete(Key id)
        {
            var res = Repository.Delete(id);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        [HttpPost("RemoveList")]
        public virtual async System.Threading.Tasks.Task<ResponseApi> RemoveList([FromForm, FromBody] DeleteEntity<Key> ids)
        {
            var res = await Repository.DeleteListAsync(ids.Ids);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        [HttpPost("DeleteList")]
        public virtual ResponseApi DeleteList([FromForm, FromBody] DeleteEntity<Key> ids)
        {
            var res = Repository.DeleteList(ids.Ids);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }

        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集信息 </return>
        [HttpPost("GetList")]
        public virtual async System.Threading.Tasks.Task<ResponseApi<System.Collections.Generic.List<T>>> GetList([FromForm, FromBody] T obj)
        {
            var res = await Repository.FindListByEntityAsync(obj);
            return ResponseApi<System.Collections.Generic.List<T>>.CreateSuccess().SetData(res);
        }

        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集信息 </return>
        [HttpPost("FindList")]
        public virtual ResponseApi<System.Collections.Generic.List<T>> FindList([FromForm, FromBody] T obj)
        {
            var res = Repository.FindListByEntity(obj);
            return ResponseApi<System.Collections.Generic.List<T>>.CreateSuccess().SetData(res);
        }


        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集数量信息</return>
        [HttpPost("GetCount")]
        public virtual async System.Threading.Tasks.Task<ResponseApi<long>> GetCount([FromForm, FromBody] T obj)
        {
            var res = await Repository.CountByEntityAsync(obj);
            return ResponseApi<long>.CreateSuccess().SetData(res);
        }

        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集数量信息</return>
        [HttpPost("Count")]
        public virtual ResponseApi<long> Count([FromForm, FromBody] T obj)
        {
            var res = Repository.CountByEntity(obj);
            return ResponseApi<long>.CreateSuccess().SetData(res);
        }

        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息</return>
        [HttpPost("GetListByPage")]
        public virtual async System.Threading.Tasks.Task<ResponseApi<System.Collections.Generic.List<T>>> GetListByPage([FromForm, FromBody] T obj, int page, int size)
        {
            var res = await Repository.FindListByEntityAndPageAsync(obj, page, size);
            return ResponseApi<System.Collections.Generic.List<T>>.CreateSuccess().SetData(res);
        }

        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息</return>
        [HttpPost("FindListByPage")]
        public virtual ResponseApi<System.Collections.Generic.List<T>> FindListByPage([FromForm, FromBody] T obj, int page, int size)
        {
            var res = Repository.FindListByEntityAndPage(obj, page, size);
            return ResponseApi<System.Collections.Generic.List<T>>.CreateSuccess().SetData(res);
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        [HttpPost("GetResultByPage")]
        public virtual async System.Threading.Tasks.Task<ResponseApi<ResultDto<T>>> GetResultByPage([FromForm, FromBody] T obj, int page, int size)
        {
            var data = await Repository.FindListByEntityAndPageAsync(obj, page, size);
            var count = await Repository.CountByEntityAsync(obj);
            var res = new ResultDto<T>(data, page, size, count);
            return ResponseApi<ResultDto<T>>.CreateSuccess().SetData(res);
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        [HttpPost("FindResultByPage")]
        public virtual ResponseApi<ResultDto<T>> FindResultByPage([FromForm, FromBody] T obj, int page, int size)
        {
            var data = Repository.FindListByEntityAndPage(obj, page, size);
            var count = Repository.CountByEntity(obj);
            var res = new ResultDto<T>(data, page, size, count);
            return ResponseApi<ResultDto<T>>.CreateSuccess().SetData(res);
        }
    }

}
#endif