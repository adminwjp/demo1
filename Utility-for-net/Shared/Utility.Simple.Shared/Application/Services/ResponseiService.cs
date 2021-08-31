using System.Collections.Generic;
using System.Threading;
#if !(NET20 || NET30 || NET35)
using System.Threading.Tasks;
#endif
using Utility.Cache;
using Utility.Domain.Entities;
using Utility.Application.Services.Dtos;
using System.Linq;
using Utility.Domain.Repositories;
using Utility.Attributes;

namespace Utility.Application.Services
{
    /// <summary>
    ///统一 接口 返回 结果
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    /// <typeparam name="Key"></typeparam>

    public interface IResponseApiService<Create, UpdateInput, Input, Output, Entity, Key> where Entity : class, IEntity<Key>
    {


        /// <summary>添加实体类信息</summary>
        /// <param name="entity">实体类</param>
        ///<param name="language"></param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        ResponseApi Insert(Create entity, Language language = Language.Chinese);


        /// <summary>修改实体类信息</summary>
        /// <param name="entity">实体类</param>
        ///<param name="language"></param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        ResponseApi Update(UpdateInput entity, Language language = Language.Chinese);

        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<param name="language"></param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        ResponseApi Delete(Key id, Language language = Language.Chinese);

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<param name="language"></param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        ResponseApi DeleteList(Key[] ids, Language language = Language.Chinese);

        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="entity">实体类</param>
        ///<param name="language"></param>
        ///<return>返回实体类数据集信息 </return>
        ResponseApi<List<Output>> FindList(Input entity, Language language = Language.Chinese);

        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="entity">实体类</param>
        ///<param name="language"></param>
        ///<return>返回实体类数据集数量信息</return>
        ResponseApi<long> Count(Input entity, Language language = Language.Chinese);

        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="entity">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<param name="language"></param>
        ///<return>返回实体类数据集信息</return>
        ResponseApi<List<Output>> FindListByPage(Input entity, int page, int size, Language language = Language.Chinese);


        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="entity">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<param name="language"></param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        ResponseApi<ResultDto<Output>> FindResultByPage(Input entity, int page, int size, Language language = Language.Chinese);

#if !(NET20 || NET30 || NET35)

        /// <summary>添加实体类信息</summary>
        /// <param name="entity">实体类</param>
        ///<param name="language"></param>
        ///<param name="cancellationToken"></param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        Task<ResponseApi> InsertAsync(Create entity, Language language = Language.Chinese, CancellationToken cancellationToken = default);

        /// <summary>修改实体类信息</summary>
        /// <param name="entity">实体类</param>
        ///<param name="language"></param>
        ///<param name="cancellationToken"></param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        Task<ResponseApi> UpdateAsync(UpdateInput entity, Language language = Language.Chinese, System.Threading.CancellationToken cancellationToken = default);

        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<param name="language"></param>
        ///<param name="cancellationToken"></param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
       Task<ResponseApi> DeleteAsync(Key id, Language language = Language.Chinese, CancellationToken cancellationToken = default);



        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<param name="language"></param>
        ///<param name="cancellationToken"></param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        Task<ResponseApi> DeleteListAsync(Key[] ids, Language language = Language.Chinese, CancellationToken cancellationToken = default);


        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="entity">实体类</param>
        ///<param name="language"></param>
        ///<param name="cancellationToken"></param>
        ///<return>返回实体类数据集信息 </return>
        Task<ResponseApi<List<Output>>> FindListAsync(Input entity, Language language = Language.Chinese, CancellationToken cancellationToken = default);

        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="entity">实体类</param>
        ///<param name="language"></param>
        ///<param name="cancellationToken"></param>
        ///<return>返回实体类数据集数量信息</return>
        Task<ResponseApi<long>> CountAsync(Input entity, Language language = Language.Chinese,CancellationToken cancellationToken = default);


        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="entity">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<param name="language"></param>
        ///<param name="cancellationToken"></param>
        ///<return>返回实体类数据集信息</return>
        Task<ResponseApi<List<Output>>> FindListByPageAsync(Input entity, int page, int size, Language language = Language.Chinese,CancellationToken cancellationToken = default);


        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="entity">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<param name="language"></param>
        ///<param name="cancellationToken"></param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        Task<ResponseApi<ResultDto<Output>>> FindResultByPageAsync(Input entity, int page, int size, Language language = Language.Chinese, CancellationToken cancellationToken = default);
   
#endif
    }


    /// <summary>
    ///统一 接口 返回 结果
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    /// <typeparam name="Key"></typeparam>
    public interface IResponseApiService<Entity, Key> : IResponseApiService<Entity, Entity, Entity, Entity, Entity, Key> where Entity : class, IEntity<Key>
    {


    }


    /// <summary>
    /// 统一 接口 返回 结果
    /// </summary>
    /// <typeparam name="ServiceImpl"></typeparam>
    /// <typeparam name="Entity"></typeparam>
    /// <typeparam name="Key"></typeparam>
    [Transtation]
    public class ResponseApiService<ServiceImpl, RepositoryImpl, Create, UpdateInput, Input, Output, Entity, Key> : IResponseApiService<Create, UpdateInput, Input, Output, Entity, Key>
        where ServiceImpl : CrudAppService<RepositoryImpl, Create, UpdateInput, Input, Output, Entity, Key>//, new()
        where RepositoryImpl : IRepository<Entity, Key>
        where Create : class
        where UpdateInput : class
        where Input : class
        where Output : class
        where Entity : class, IEntity<Key>
    {
     
        /// <summary>
        /// 
        /// </summary>
        public ServiceImpl Service { get;protected set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public ResponseApiService():this(new ServiceImpl())
        //{
            
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public ResponseApiService(ServiceImpl service)
        {
            Service = service;
        }


        /// <summary>添加实体类信息</summary>
        /// <param name="entity">实体类</param>
        ///<param name="language"></param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        public virtual ResponseApi Insert(Create entity, Language language = Language.Chinese)
        {
             Service.Insert(entity);
            return ResponseApi.Create(language, Code.AddSuccess);
        }




        /// <summary>修改实体类信息</summary>
        /// <param name="entity">实体类</param>
        ///<param name="language"></param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        public virtual ResponseApi Update(UpdateInput entity, Language language = Language.Chinese)
        {
            Service.Update(entity);
            return ResponseApi.Create(language,  Code.ModifySuccess);
        }


        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<param name="language"></param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        public virtual ResponseApi Delete(Key id, Language language = Language.Chinese)
       {
             Service.Delete(id);
            return ResponseApi.Create(language, Code.DeleteSuccess );
       }


        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<param name="language"></param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
       public virtual ResponseApi DeleteList(Key[] ids, Language language = Language.Chinese)
        {
            Service.DeleteList(ids);
            return ResponseApi.Create(language,Code.DeleteSuccess);
        }


        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="entity">实体类</param>
        ///<param name="language"></param>
        ///<return>返回实体类数据集信息 </return>
        public virtual ResponseApi<List<Output>> FindList(Input entity, Language language = Language.Chinese)
        {
            var res =  Service.FindList(entity);
            return ResponseApi<List<Output>>.Create(language, Code.QuerySuccess).SetData(res);
        }


        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="entity">实体类</param>
        ///<param name="language"></param>
        ///<return>返回实体类数据集数量信息</return>
        public virtual ResponseApi<long> Count(Input entity, Language language = Language.Chinese)
        {
            var res =  Service.Count(entity);
            return ResponseApi<long>.Create(language, Code.QuerySuccess).SetData(res);
        }


        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="entity">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<param name="language"></param>
        ///<return>返回实体类数据集信息</return>
        public virtual ResponseApi<List<Output>> FindListByPage(Input entity, int page, int size, Language language = Language.Chinese)
        {
            var res =  Service.FindListByPage(entity, page, size);
            return ResponseApi<List<Output>>.Create(language, Code.QuerySuccess).SetData(res);
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="entity">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<param name="language"></param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        public virtual ResponseApi<ResultDto<Output>> FindResultByPage(Input entity, int page, int size, Language language = Language.Chinese)
        {
            var data = Service.FindListByPage(entity, page, size);
            var count = Service.Count(entity);
            var res = new ResultDto<Output>(data,page,size, count);
            return ResponseApi<ResultDto<Output>>.Create(language, Code.QuerySuccess).SetData(res);
        }

#if !(NET20 || NET30 || NET35)

        /// <summary>添加实体类信息</summary>
        /// <param name="entity">实体类</param>
        ///<param name="language"></param>
        ///<param name="cancellationToken"></param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>

        public virtual Task<ResponseApi> InsertAsync(Create entity, Language language = Language.Chinese, CancellationToken cancellationToken = default)
        {
            Service.InsertAsync(entity, cancellationToken).GetAwaiter().GetResult();
            return Task.FromResult(ResponseApi.Create(language,  Code.AddSuccess ));
        }

        /// <summary>修改实体类信息</summary>
        /// <param name="entity">实体类</param>
        ///<param name="language"></param>
        ///<param name="cancellationToken"></param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        public virtual Task<ResponseApi> UpdateAsync(UpdateInput entity, Language language = Language.Chinese,
            CancellationToken cancellationToken = default)
        {
             Service.UpdateAsync(entity, cancellationToken).GetAwaiter().GetResult();
            return Task.FromResult(ResponseApi.Create(language, Code.ModifySuccess ));

        }

        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<param name="language"></param>
        ///<param name="cancellationToken"></param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        public virtual Task<ResponseApi> DeleteAsync(Key id, Language language = Language.Chinese,
            CancellationToken cancellationToken = default)
        {
             Service.DeleteAsync(id, cancellationToken).GetAwaiter().GetResult();
            return Task.FromResult(ResponseApi.Create(language,Code.DeleteSuccess ));
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<param name="language"></param>
        ///<param name="cancellationToken"></param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        public virtual Task<ResponseApi> DeleteListAsync(Key[] ids, Language language = Language.Chinese,
            CancellationToken cancellationToken = default)
        {
            Service.DeleteListAsync(ids, cancellationToken).GetAwaiter().GetResult();
            return Task.FromResult( ResponseApi.Create(language,  Code.DeleteSuccess));
        }

        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="entity">实体类</param>
        ///<param name="language"></param>
        ///<param name="cancellationToken"></param>
        ///<return>返回实体类数据集信息 </return>
        public virtual Task<ResponseApi<List<Output>>> FindListAsync(Input entity, Language language = Language.Chinese,
            CancellationToken cancellationToken = default)
        {
            var res =  Service.FindListAsync(entity, cancellationToken).Result;
            return Task.FromResult( ResponseApi<List<Output>>.Create(language, Code.QuerySuccess).SetData(res));
        }

        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="entity">实体类</param>
        ///<param name="language"></param>
        ///<param name="cancellationToken"></param>
        ///<return>返回实体类数据集数量信息</return>
        public virtual Task<ResponseApi<long>> CountAsync(Input entity, Language language = Language.Chinese,CancellationToken cancellationToken = default)
        {
            var res =  Service.CountAsync(entity, cancellationToken).Result;
            return Task.FromResult( ResponseApi<long>.Create(language, Code.QuerySuccess).SetData(res));
        }

        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="entity">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<param name="language"></param>
        ///<param name="cancellationToken"></param>
        ///<return>返回实体类数据集信息</return>
        public virtual Task<ResponseApi<List<Output>>> FindListByPageAsync(Input entity, int page, int size,
            Language language = Language.Chinese, CancellationToken cancellationToken = default)
        {
            var res =  Service.FindListByPageAsync(entity, page, size, cancellationToken).Result;
            return Task.FromResult(ResponseApi<List<Output>>.Create(language, Code.QuerySuccess).SetData(res));
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="entity">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<param name="language"></param>
        ///<param name="cancellationToken"></param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        public virtual Task<ResponseApi<ResultDto<Output>>> FindResultByPageAsync(Input entity, int page, int size,
           Language language = Language.Chinese, CancellationToken cancellationToken = default)
        {
           
            return Task.FromResult(FindResultByPage(entity,page,size));
        }
#endif

        }


    /// <summary>
    /// 统一 接口 返回 结果
    /// </summary>
    /// <typeparam name="ServiceImpl"></typeparam>
    /// <typeparam name="Entity"></typeparam>
    /// <typeparam name="Key"></typeparam>
    [Transtation]
    public class ResponseApiService<ServiceImpl, Entity, Key> : ResponseApiService<ServiceImpl, IRepository<Entity, Key>, Entity, Entity, Entity, Entity, Entity, Key>, IResponseApiService<Entity, Key>
                where ServiceImpl : CrudAppService<Entity, Key>, new()
        where Entity : class, IEntity<Key>
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public ResponseApiService(ServiceImpl service) : base(service)
        {
            Service = service;
        }


    }

}
