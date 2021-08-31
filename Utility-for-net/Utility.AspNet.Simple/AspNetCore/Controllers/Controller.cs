#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0
using System;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Utility.Application.Services;
using Utility.Application.Services.Dtos;
using Utility.Domain.Entities;
using Utility.Domain.Repositories;
using Utility.Domain.Services;
using Utility.Json;

namespace Utility.AspNetCore.Controllers
{
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
    [ProducesResponseType(typeof(IResponseApi), 200)]

    public class BaseController<ResponseService, Service, RepositoryImpl, Create, UpdateInput, Input, Output, T, Key> : BaseController
        where ResponseService : ResponseApiService<Service, RepositoryImpl, Create, UpdateInput, Input, Output, T, Key>
        where Service : CrudAppService<RepositoryImpl, Create, UpdateInput, Input, Output, T, Key>//, new()
        where RepositoryImpl : IRepository<T, Key>
        where Create : class
        where UpdateInput : class
        where Input : class
        where Output : class
        where T : class, IEntity<Key>
    {
        private ResponseService apiService;

        protected ResponseService ApiService { get => apiService; set { apiService = value; base.Service = value?.Service; } }
        protected virtual Language GetLanguage()
        {
            return Language.Chinese;
        }
        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        [HttpPost("Add")]
        public virtual async Task<ResponseApi> Add([FromForm, FromBody] Create obj)
        {
            var res = await ApiService.InsertAsync(obj, GetLanguage());
            return res;
        }

        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        [HttpPost("Insert")]
        public virtual ResponseApi Insert([FromForm, FromBody] Create obj)
        {
            var res = ApiService.Insert(obj, GetLanguage());
            return res;
        }


        /// <summary>修改实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        [HttpPost("Modify")]
        public virtual async System.Threading.Tasks.Task<ResponseApi> Modify([FromForm, FromBody] UpdateInput obj)
        {
            var res = await ApiService.UpdateAsync(obj, GetLanguage());
            return res;
        }

        /// <summary>修改实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        [HttpPost("Update")]
        public virtual ResponseApi Update([FromForm, FromBody] UpdateInput obj)
        {
            var res = ApiService.Update(obj, GetLanguage());
            return res;
        }

        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        [HttpGet("Remove")]
        public virtual async System.Threading.Tasks.Task<ResponseApi> Remove(Key id)
        {
            var res = await ApiService.DeleteAsync(id, GetLanguage());
            return res;
        }

        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        [HttpGet("Delete")]
        public virtual ResponseApi Delete(Key id)
        {
            var res = ApiService.Delete(id, GetLanguage());
            return res;
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        [HttpPost("RemoveList")]
        public virtual async System.Threading.Tasks.Task<ResponseApi> RemoveList([FromForm, FromBody] DeleteEntity<Key> ids)
        {
            var res = await ApiService.DeleteListAsync(ids.Ids, GetLanguage());
            return res;
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        [HttpPost("DeleteList")]
        public virtual ResponseApi DeleteList([FromForm, FromBody] DeleteEntity<Key> ids)
        {
            var res = ApiService.DeleteList(ids.Ids, GetLanguage());
            return res;
        }

        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集信息 </return>
        [HttpPost("GetList")]
        public virtual async System.Threading.Tasks.Task<ResponseApi<System.Collections.Generic.List<Output>>> GetList([FromForm, FromBody] Input obj)
        {
            var res = await ApiService.FindListAsync(obj, GetLanguage());
            return res;
        }

        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集信息 </return>
        [HttpPost("FindList")]
        public virtual ResponseApi<System.Collections.Generic.List<Output>> FindList([FromForm, FromBody] Input obj)
        {
            var res = ApiService.FindList(obj, GetLanguage());
            return res;
        }


        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集数量信息</return>
        [HttpPost("GetCount")]
        public virtual async System.Threading.Tasks.Task<ResponseApi<long>> GetCount([FromForm, FromBody] Input obj)
        {
            var res = await ApiService.CountAsync(obj, GetLanguage());
            return res;
        }

        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集数量信息</return>
        [HttpPost("Count")]
        public virtual ResponseApi<long> Count([FromForm, FromBody] Input obj)
        {
            var res = ApiService.Count(obj, GetLanguage());
            return res;
        }

        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息</return>
        [HttpPost("GetListByPage")]
        public virtual async System.Threading.Tasks.Task<ResponseApi<System.Collections.Generic.List<Output>>> GetListByPage([FromForm, FromBody] Input obj, int page, int size)
        {
            var res = await ApiService.FindListByPageAsync(obj, page, size, GetLanguage());
            return res;
        }

        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息</return>
        [HttpPost("FindListByPage")]
        public virtual ResponseApi<System.Collections.Generic.List<Output>> FindListByPage([FromForm, FromBody] Input obj, int page, int size)
        {
            var res = ApiService.FindListByPage(obj, page, size, GetLanguage());
            return res;
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        [HttpPost("GetResultByPage")]
        public virtual async System.Threading.Tasks.Task<ResponseApi<ResultDto<Output>>> GetResultByPage([FromForm, FromBody] Input obj, int page, int size)
        {
            var res = await ApiService.FindResultByPageAsync(obj, page, size, GetLanguage());
            return res;
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        [HttpPost("FindResultByPage")]
        public virtual ResponseApi<ResultDto<Output>> FindResultByPage([FromForm, FromBody] Input obj, int page, int size)
        {
            var res = ApiService.FindResultByPage(obj, page, size, GetLanguage());
            return res;
        }

    }
    [Route("api/[controller]")]
#if !NETCOREAPP2_0
    [ApiController]
#endif
    [ProducesResponseType(typeof(IResponseApi), 200)]

    public class BaseController<Service, RepositoryImpl, Create, UpdateInput, Input, Output, T, Key> : BaseController
    where Service : ICrudAppService<RepositoryImpl, Create, UpdateInput, Input, Output, T, Key>//, new()
    where RepositoryImpl : IRepository<T, Key>
    where Create : class
    where UpdateInput : class
    where Input : class
    where Output : class
    where T : class, IEntity<Key>
    {
        private Service apiService;

        protected Service ApiService { get => apiService; set { apiService = value; base.Service = value as DomainService; } }
        public BaseController()
        {

        }
        public BaseController(Service apiService)
        {
            this.ApiService = apiService;
        }
        protected virtual Language GetLanguage()
        {
            return Language.Chinese;
        }
        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        [HttpPost("Add")]
        public virtual async Task<ResponseApi> Add([FromForm, FromBody] Create obj)
        {
            var res = await ApiService.InsertAsync(obj);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }

        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        [HttpPost("Insert")]
        public virtual ResponseApi Insert([FromForm, FromBody] Create obj)
        {
            var res = ApiService.Insert(obj);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }


        /// <summary>修改实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        [HttpPost("Modify")]
        public virtual async System.Threading.Tasks.Task<ResponseApi> Modify([FromForm, FromBody] UpdateInput obj)
        {
            var res = await ApiService.UpdateAsync(obj);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }

        /// <summary>修改实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        [HttpPost("Update")]
        public virtual ResponseApi Update([FromForm, FromBody] UpdateInput obj)
        {
            var res = ApiService.Update(obj);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }

        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        [HttpGet("Remove")]
        public virtual async System.Threading.Tasks.Task<ResponseApi> Remove(Key id)
        {
            var res = await ApiService.DeleteAsync(id);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }

        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        [HttpGet("Delete")]
        public virtual ResponseApi Delete(Key id)
        {
            var res = ApiService.Delete(id);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        [HttpPost("RemoveList")]
        public virtual async System.Threading.Tasks.Task<ResponseApi> RemoveList([FromForm, FromBody] DeleteEntity<Key> ids)
        {
            var res = await ApiService.DeleteListAsync(ids.Ids);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        [HttpPost("DeleteList")]
        public virtual ResponseApi DeleteList([FromForm, FromBody] DeleteEntity<Key> ids)
        {
            var res = ApiService.DeleteList(ids.Ids);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }

        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集信息 </return>
        [HttpPost("GetList")]
        public virtual async System.Threading.Tasks.Task<ResponseApi<System.Collections.Generic.List<Output>>> GetList([FromForm, FromBody] Input obj)
        {
            var res = await ApiService.FindListAsync(obj);
            return ResponseApi<System.Collections.Generic.List<Output>>.CreateSuccess().SetData(res);
        }

        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集信息 </return>
        [HttpPost("FindList")]
        public virtual ResponseApi<System.Collections.Generic.List<Output>> FindList([FromForm, FromBody] Input obj)
        {
            var res = ApiService.FindList(obj);
            return ResponseApi<System.Collections.Generic.List<Output>>.CreateSuccess().SetData(res);
        }


        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集数量信息</return>
        [HttpPost("GetCount")]
        public virtual async System.Threading.Tasks.Task<ResponseApi<long>> GetCount([FromForm, FromBody] Input obj)
        {
            var res = await ApiService.CountAsync(obj);
            return ResponseApi<long>.CreateSuccess().SetData(res);
        }

        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集数量信息</return>
        [HttpPost("Count")]
        public virtual ResponseApi<long> Count([FromForm, FromBody] Input obj)
        {
            var res = ApiService.Count(obj);
            return ResponseApi<long>.CreateSuccess().SetData(res);
        }

        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息</return>
        [HttpPost("GetListByPage")]
        public virtual async System.Threading.Tasks.Task<ResponseApi<System.Collections.Generic.List<Output>>> GetListByPage([FromForm, FromBody] Input obj, int page, int size)
        {
            var res = await ApiService.FindListByPageAsync(obj, page, size);
            return ResponseApi<System.Collections.Generic.List<Output>>.CreateSuccess().SetData(res);
        }

        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息</return>
        [HttpPost("FindListByPage")]
        public virtual ResponseApi<System.Collections.Generic.List<Output>> FindListByPage([FromForm, FromBody] Input obj, int page, int size)
        {
            var res = ApiService.FindListByPage(obj, page, size);
            return ResponseApi<System.Collections.Generic.List<Output>>.CreateSuccess().SetData(res);
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        [HttpPost("GetResultByPage")]
        public virtual async System.Threading.Tasks.Task<ResponseApi<ResultDto<Output>>> GetResultByPage([FromForm, FromBody] Input obj, int page, int size)
        {
            var data = ApiService.FindListByPage(obj, page, size);
            var count = ApiService.Count(obj);
            var res = new ResultDto<Output>(data, page, size, count);
            return ResponseApi<ResultDto<Output>>.CreateSuccess().SetData(res);
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        [HttpPost("FindResultByPage")]
        public virtual ResponseApi<ResultDto<Output>> FindResultByPage([FromForm, FromBody] Input obj, int page, int size)
        {
            var data = ApiService.FindListByPage(obj, page, size);
            var count = ApiService.Count(obj);
            var res = new ResultDto<Output>(data, page, size, count);
            return ResponseApi<ResultDto<Output>>.CreateSuccess().SetData(res);
        }

    }


}
#endif