using Utility.Domain.Services;
using Utility.Domain.Repositories;
using System.Linq.Expressions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Utility.Domain.Entities;
using System.Collections.Generic;
using Utility.Attributes;
using Utility.Application.Services.Dtos;

namespace Utility.Application.Services
{
    public interface ICrudAppService<RepositoryImpl, Create, UpdateInput, Input, Output, Entity, Key> :  IAppService
        ,IDisposable
            where RepositoryImpl : IRepository<Entity, Key>
            where Create : class
            where UpdateInput : class
            where Input : class
            where Output : class
        where Entity : class, IEntity<Key>
    {
        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="input">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
         ResultDto<Output> FindResultByPage(Input input, int page, int size);
#if !(NET20 || NET30)
        /// <summary>查找单个，且不被上下文所跟踪  </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
         Output FindSingle(Expression<Func<Entity, bool>> where = null);

        /// <summary>查找单个，且不被上下文所跟踪  </summary>
        /// <param name="input">条件</param>
        /// <returns></returns>
         Output FindSingle(Input input);

        /// <summary>查找单个，且不被上下文所跟踪  </summary>
        /// <param name="id">条件</param>
        /// <returns></returns>
        Output FindSingle(object id);

        /// <summary> 是否存在 默认实现   </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
         bool IsExist(Expression<Func<Entity, bool>> where = null);

        /// <summary> 是否存在 默认实现   </summary>
        /// <param name="input">条件</param>
        /// <returns></returns>
        bool IsExist(Input input);

        /// <summary> 是否存在 默认实现   </summary>
        /// <param name="id">条件</param>
        /// <returns></returns>
        bool IsExist(object id);

        /// <summary> 查询数据 默认实现</summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        IQueryable<Entity> Query(Expression<Func<Entity, bool>> where = null);

        /// <summary> 查询数据 默认实现 </summary>
        /// <param name="input">条件</param>
        /// <returns></returns>
         List<Output> FindList(Input input);
        /// <summary> 查询数据 默认实现 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        IQueryable<Entity> QueryByPage(Expression<Func<Entity, bool>> where = null, int page = 1, int size = 10);
        /// <summary> 查询数据 默认实现 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="input">条件</param>
        /// <returns></returns>
        List<Output> FindListByPage(Input input, int page = 1, int size = 10);

        /// <summary>查询数量  默认实现   </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
         long Count(Expression<Func<Entity, bool>> where = null);

        /// <summary>查询数量  默认实现   </summary>
        /// <param name="input">条件</param>
        /// <returns></returns>
         long Count(Input input);
#endif

        /// <summary> 添加 </summary>
        /// <param name="create">实体</param>
         int Insert(Create create);

        /// <summary>批量 添加 </summary>
        /// <param name="creates">实体</param>
         int BatchInsert(Create[] creates);

        /// <summary> 更新一个实体的所有属性 </summary>
        /// <param name="update">实体</param>
         int Update(UpdateInput update);

        /// <summary> 删除</summary>
        /// <param name="entity">实体</param>
         int Delete(Entity entity);

        /// <summary> 删除</summary>
        /// <param name="ids">实体</param>
        int DeleteList(Key[] ids);

#if !(NET20 || NET30)
#if !(NET10 || NET20 || NET30 || NET35)
        /// <summary> 删除</summary>
        /// <param name="id">实体 id</param>
        int Delete(Key id);
#endif
        /// <summary>
        /// 实现按需要只更新部分更新 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper 未实现
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">更新条件</param>
        /// <param name="update">更新后的实体</param>
       int Update(Expression<Func<Entity, bool>> where, Expression<Func<Entity, Entity>> update);

        /// <summary> 批量删除 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper 未实现</summary>
        /// <param name="where">条件</param>
         int Delete(Expression<Func<Entity, bool>> where);
#endif
        /// <summary> 执行sql </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int ExecuteSql(string sql);

        ///// <summary> 操作成功  </summary>
        //void Save();

        #region async
#if !(NET20 || NET30 || NET35)
        /// <summary> 查询数据  默认实现   </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken">dapper ef 无效</param>
        /// <returns></returns>
          Task<Output> FindSingleAsync(Expression<Func<Entity, bool>> where = null, CancellationToken cancellationToken = default);

        /// <summary> 查询数据  默认实现   </summary>
        /// <param name="input">条件</param>
        /// <param name="cancellationToken">dapper ef 无效</param>
        /// <returns></returns>
          Task<Output> FindSingleAsync(Input input, CancellationToken cancellationToken = default);
        /// <summary> 查询数据  默认实现   </summary>
        /// <param name="id">条件</param>
        /// <param name="cancellationToken">dapper ef 无效</param>
        /// <returns></returns>
          Task<Output> FindSingleAsync(object id, CancellationToken cancellationToken = default);

        /// <summary> 是否存在 默认实现   </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
         Task<bool> IsExistAsync(Expression<Func<Entity, bool>> where = null, CancellationToken cancellationToken = default);

        /// <summary> 是否存在 默认实现   </summary>
        /// <param name="input">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
         Task<bool> IsExistAsync(Input input, CancellationToken cancellationToken = default);
        /// <summary> 是否存在 默认实现   </summary>
        /// <param name="id">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
         Task<bool> IsExistAsync(object id, CancellationToken cancellationToken = default);

        /// <summary> 查询数据 默认实现 ef 相当于普通方法 未实现async</summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
         Task<IQueryable<Entity>> QueryAsync(Expression<Func<Entity, bool>> where = null, CancellationToken cancellationToken = default);

        /// <summary> 查询数据 默认实现  ef 相当于普通方法 未实现async</summary>
        /// <param name="input">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
          Task<List<Output>> FindListAsync(Input input, CancellationToken cancellationToken = default);

        /// <summary> 查询数据 默认实现 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
         Task<IQueryable<Entity>> QueryByPageAsync(Expression<Func<Entity, bool>> where = null, int page = 1, int size = 10,
             CancellationToken cancellationToken = default);

        /// <summary> 查询数据 默认实现  </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="input">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
         Task<List<Output>> FindListByPageAsync(Input input, int page = 1, int size = 10,
             CancellationToken cancellationToken = default);

        /// <summary>查询数量  默认实现  </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
         Task<long> CountAsync(Expression<Func<Entity, bool>> where = null,
            CancellationToken cancellationToken = default);

        /// <summary>查询数量  默认实现   </summary>
        /// <param name="input">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
         Task<long> CountAsync(Input input, CancellationToken cancellationToken = default);

        /// <summary> 添加 </summary>
        /// <param name="create">实体</param>
        /// <param name="cancellationToken">ef core 有效 </param>
         Task<int> InsertAsync(Create create, CancellationToken cancellationToken = default);

        /// <summary>批量 添加 </summary>
        /// <param name="creates">实体</param>
        /// <param name="cancellationToken"></param>
        Task<int> BatchInsertAsync(Create[] creates, CancellationToken cancellationToken = default);

        /// <summary> 更新一个实体的所有属性 </summary>
        /// <param name="update">实体</param>
        /// <param name="cancellationToken">ef 有效</param>
         Task<int> UpdateAsync(UpdateInput update, CancellationToken cancellationToken = default);

        /// <summary> 删除</summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">ef 有效</param>
        Task<int> DeleteAsync(Entity entity, CancellationToken cancellationToken = default);

        /// <summary> 删除</summary>
        /// <param name="id">实体</param>
        /// <param name="cancellationToken">ef 有效</param>
         Task<int> DeleteAsync(Key id, CancellationToken cancellationToken = default);



        /// <summary> 删除</summary>
        /// <param name="ids">实体</param>
        /// <param name="cancellationToken">ef 有效</param>
         Task<int> DeleteListAsync(Key[] ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// 实现按需要只更新部分更新 默认实现 ef,nhibernate 支持 linq dapper 不支持linq
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">更新条件</param>
        /// <param name="update">更新后的实体</param>
        /// <param name="cancellationToken">ef 有效</param>
         Task<int> UpdateAsync(Expression<Func<Entity, bool>> where, Expression<Func<Entity, Entity>> update,
            CancellationToken cancellationToken = default);

        /// <summary> 批量删除 默认实现 ef,nhibernate 支持 linq dapper 不支持linq</summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken">ef 有效</param>
         Task<int> DeleteAsync(Expression<Func<Entity, bool>> where = null, 
            CancellationToken cancellationToken = default);


        /// <summary> 执行sql </summary>
        /// <param name="sql"></param>
        /// <param name="cancellationToken">ef 有效</param>
        /// <returns></returns>
         Task<int> ExecuteSqlAsync(string sql, CancellationToken cancellationToken = default);

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="input">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
          Task<ResultDto<Output>> FindResultByPageAsync(Input input, int page, int size);
        ///// <summary> 操作成功 保存到库里 默认实现 ef 支持  dapper nhibernate 不支持 </summary>
        //Task SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
#endif
        #endregion async

        /// <summary>
        /// 释放资源
        /// </summary>
         void Dispose();
   
   

    }
    /// <summary>
    /// crud  application service interface implement
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    /// <typeparam name="Key"></typeparam>
    [Transtation]
    public class CrudAppService<RepositoryImpl,Create, UpdateInput, Input,Output, Entity,Key> : DomainService,
        IAppService, ICrudAppService<RepositoryImpl, Create, UpdateInput, Input, Output, Entity, Key>
          where RepositoryImpl : IRepository<Entity, Key>
          where Create : class
          where UpdateInput : class
          where Input : class
          where Output : class
            where Entity :class,IEntity<Key> 
    {

        protected Type EntityType { get; set; }
        protected Type CreateType { get; set; }
        protected Type UpdateType { get; set; }
        protected Type QueryType { get; set; }
        protected Type OutputType { get; set; }
        /// <summary>
        /// 仓库 接口
        /// </summary>
        protected RepositoryImpl Repository;
        /// <summary>
        /// no param application service  constractor 
        /// </summary>
        public CrudAppService(RepositoryImpl repository) : base() {
            Repository = repository;
            UnitWork = repository?.UnitWork;
            CreateType = typeof(Create);
            UpdateType = typeof(UpdateInput);
            QueryType = typeof(Input);
            OutputType = typeof(Output);
            EntityType = typeof(Entity);
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="input">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        public virtual ResultDto<Output> FindResultByPage(Input input, int page, int size)
        {
            Entity entity = QueryBindBefore(input);
            var res = Repository.FindListByEntityAndPage(entity, page, size);
            var data= QueryOutputBindBefore(res);
            var count = Repository.CountByEntity(entity);
            var result = new ResultDto<Output>(data, page, size, count);
            return result;
        }
#if !(NET20 || NET30)
        /// <summary>查找单个，且不被上下文所跟踪 ef,nhibernate 支持 linq dapper 不支持linq </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual Output FindSingle(Expression<Func<Entity, bool>> where = null)
        {
            var output = QueryOutputBindBefore(Repository.FindSingle(where));
            return output;
        }

        /// <summary>查找单个，且不被上下文所跟踪 ef,nhibernate 支持 linq dapper 不支持linq </summary>
        /// <param name="input">条件</param>
        /// <returns></returns>
        public virtual Output FindSingle(Input input)
        {
            Entity entity = QueryBindBefore(input);
            var res=  Repository.FindSingleByEntity(entity);
            var output = QueryOutputBindBefore(res);
            return output;
        }

        /// <summary>查找单个，且不被上下文所跟踪 ef,nhibernate 支持 linq dapper 不支持linq </summary>
        /// <param name="id">条件</param>
        /// <returns></returns>
        public virtual Output FindSingle(object id)
        {
            var output = QueryOutputBindBefore(Repository.FindSingle(id));
            return output;
        }

        /// <summary> 是否存在 默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual bool IsExist(Expression<Func<Entity, bool>> where = null)
        {
            return Repository.IsExist(where);
        }

        /// <summary> 是否存在 默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="input">条件</param>
        /// <returns></returns>
        public virtual bool IsExist(Input input)
        {
            Entity entity = QueryBindBefore(input);
            return Repository.IsExistByEntity(entity);
        }

        /// <summary> 是否存在 默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="id">条件</param>
        /// <returns></returns>
        public virtual bool IsExist(object id)
        {
            return Repository.IsExist(id);
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper默认查询所有结果集基于内存 条件查询</summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual IQueryable<Entity> Query(Expression<Func<Entity, bool>> where = null)
        {
            return Repository.Query(where);
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper默认查询所有结果集基于内存 条件查询</summary>
        /// <param name="input">条件</param>
        /// <returns></returns>
        public virtual List<Output> FindList(Input input)
        {
            Entity entity = QueryBindBefore(input);
            var res= Repository.FindListByEntity(entity);
            return QueryOutputBindBefore(res);
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq orderby参数无效 dapper默认查询所有结果集基于内存 条件查询 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual IQueryable<Entity> QueryByPage(Expression<Func<Entity, bool>> where = null, int page = 1, int size = 10)
        {
            return Repository.QueryByPage(where, page, size);
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq orderby参数无效 dapper默认查询所有结果集基于内存 条件查询 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="input">条件</param>
        /// <returns></returns>
        public virtual List<Output> FindListByPage(Input input, int page = 1, int size = 10)
        {
            Entity entity = QueryBindBefore(input);
            var res= Repository.FindListByEntityAndPage(entity,page,size);
            return QueryOutputBindBefore(res);
        }

        /// <summary>查询数量  默认实现 ef,nhibernate 支持 linq dapper 不支持linq  dapper默认查询所有结果数量  </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual long Count(Expression<Func<Entity, bool>> where = null)
        {
            return Repository.Count(where);
        }

        /// <summary>查询数量  默认实现 ef,nhibernate 支持 linq dapper 不支持linq  dapper默认查询所有结果数量  </summary>
        /// <param name="input">条件</param>
        /// <returns></returns>
        public virtual long Count(Input input)
        {
            Entity entity = QueryBindBefore(input);
            return Repository.CountByEntity(entity);
        }
#endif

        /// <summary> 添加 </summary>
        /// <param name="create">实体</param>
        public virtual int Insert(Create create)
        {
            Entity entity = AddBindBefore(create);
            return Repository.Insert(entity);
        }

        /// <summary>批量 添加 </summary>
        /// <param name="creates">实体</param>
        public virtual int BatchInsert(Create[] creates)
        {
            Entity[] entities = AddBindBefore(new List<Create>(creates)).ToArray();
           return Repository.BatchInsert(entities);
        }

        /// <summary> 更新一个实体的所有属性 </summary>
        /// <param name="update">实体</param>
        public virtual int Update(UpdateInput update)
        {
            Entity entity = UpdateBindBefore(update);
           return  Repository.Update(entity);
        }

        /// <summary> 删除</summary>
        /// <param name="entity">实体</param>
        public virtual int Delete(Entity entity)
        {
           return Repository.Delete(entity);
        }

        /// <summary> 删除</summary>
        /// <param name="ids">实体</param>
        public virtual int DeleteList(Key[] ids)
        {
           return Repository.DeleteList(ids);
        }

#if !(NET20 || NET30)
#if !(NET10 || NET20 || NET30 || NET35)
        /// <summary> 删除</summary>
        /// <param name="id">实体 id</param>
        public virtual int Delete(Key id)
        {
           return Repository.Delete(id);
        }
#endif
        /// <summary>
        /// 实现按需要只更新部分更新 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper 未实现
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">更新条件</param>
        /// <param name="update">更新后的实体</param>
        public virtual int Update(Expression<Func<Entity, bool>> where, Expression<Func<Entity, Entity>> update)
        {
           return Repository.Update(where, update);
        }

        /// <summary> 批量删除 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper 未实现</summary>
        /// <param name="where">条件</param>
        public virtual int Delete(Expression<Func<Entity, bool>> where)
        {
           return Repository.Delete(where);
        }
#endif
        /// <summary> 执行sql </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual int ExecuteSql(string sql)
        {
            return Repository.ExecuteSql(sql);
        }

        ///// <summary> 操作成功 保存到库里 默认实现 ef 支持  dapper nhibernate 无任何操作 </summary>
        //void Save();

        #region async
#if !(NET20 || NET30 || NET35)
        /// <summary> 查询数据  默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken">dapper ef 无效</param>
        /// <returns></returns>
        public virtual async Task<Output> FindSingleAsync(Expression<Func<Entity, bool>> where = null, CancellationToken cancellationToken = default)
        {
            var res=await Repository.FindSingleAsync(where, cancellationToken);
            return QueryOutputBindBefore(res);
        }

        /// <summary> 查询数据  默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="input">条件</param>
        /// <param name="cancellationToken">dapper ef 无效</param>
        /// <returns></returns>
        public virtual async Task<Output> FindSingleAsync(Input input, CancellationToken cancellationToken = default)
        {
            Entity entity =QueryBindBefore(input);
            var res=await Repository.FindSingleByEntityAsync(entity, cancellationToken);
            return QueryOutputBindBefore(res);
        }

        /// <summary> 查询数据  默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="id">条件</param>
        /// <param name="cancellationToken">dapper ef 无效</param>
        /// <returns></returns>
        public virtual async Task<Output> FindSingleAsync(object id, CancellationToken cancellationToken = default)
        {
            var res = await Repository.FindSingleAsync(id, cancellationToken);
            return QueryOutputBindBefore(res);
        }

        /// <summary> 是否存在 默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> IsExistAsync(Expression<Func<Entity, bool>> where = null, CancellationToken cancellationToken = default)
        {
            return Repository.IsExistAsync(where, cancellationToken);
        }

        /// <summary> 是否存在 默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="input">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> IsExistAsync(Input input, CancellationToken cancellationToken = default)
        {
            Entity entity = QueryBindBefore(input);
            return Repository.IsExistByEntityAsync(entity, cancellationToken);
        }


        /// <summary> 是否存在 默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="id">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> IsExistAsync(object id, CancellationToken cancellationToken = default)
        {
            return Repository.IsExistAsync(id, cancellationToken);
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq ef 相当于普通方法 未实现async</summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<IQueryable<Entity>> QueryAsync(Expression<Func<Entity, bool>> where = null, CancellationToken cancellationToken = default)
        {
            return Repository.QueryAsync(where, cancellationToken);
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq ef 相当于普通方法 未实现async</summary>
        /// <param name="input">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<List<Output>> FindListAsync(Input input, CancellationToken cancellationToken = default)
        {
            Entity entity = QueryBindBefore(input);
            var res=await Repository.FindListByEntityAsync(entity, cancellationToken);
            return QueryOutputBindBefore(res);
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq orderby参数无效 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<IQueryable<Entity>> QueryByPageAsync(Expression<Func<Entity, bool>> where = null, int page = 1, int size = 10,
             CancellationToken cancellationToken = default)
        {
            return Repository.QueryByPageAsync(where, page, size, cancellationToken);
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq orderby参数无效 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="input">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual  async Task<List<Output>> FindListByPageAsync(Input input, int page = 1, int size = 10,
             CancellationToken cancellationToken = default)
        {
            Entity entity = QueryBindBefore(input);
            var res=await Repository.FindListByEntityAndPageAsync(entity,page,size, cancellationToken);
            return QueryOutputBindBefore(res);
        }

        /// <summary>查询数量  默认实现 ef,nhibernate 支持 linq dapper 不支持linq  dapper默认查询所有结果数量  </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<long> CountAsync(Expression<Func<Entity, bool>> where = null, CancellationToken cancellationToken = default)
        {
            return Repository.CountAsync(where, cancellationToken);
        }


        /// <summary>查询数量  默认实现 ef,nhibernate 支持 linq dapper 不支持linq  dapper默认查询所有结果数量  </summary>
        /// <param name="input">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<long> CountAsync(Input input, CancellationToken cancellationToken = default)
        {
            Entity entity = QueryBindBefore(input);
            return Repository.CountByEntityAsync(entity,  cancellationToken);
        }

        /// <summary> 添加 </summary>
        /// <param name="create">实体</param>
        /// <param name="cancellationToken">ef core 有效 </param>
        public virtual Task<int> InsertAsync(Create create, CancellationToken cancellationToken = default)
        {
            Entity entity = AddBindBefore(create);
            return Repository.InsertAsync(entity, cancellationToken);
        }

        /// <summary>批量 添加 </summary>
        /// <param name="creates">实体</param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> BatchInsertAsync(Create[] creates, CancellationToken cancellationToken = default)
        {
            Entity[] entities = AddBindBefore(new List<Create>(creates)).ToArray();
            return Repository.BatchInsertAsync(entities, cancellationToken);
        }

        /// <summary> 更新一个实体的所有属性 </summary>
        /// <param name="update">实体</param>
        /// <param name="cancellationToken">ef 有效</param>
        public virtual Task<int> UpdateAsync(UpdateInput update, CancellationToken cancellationToken = default)
        {
            Entity entity = UpdateBindBefore(update);
            return Repository.UpdateAsync(entity, cancellationToken);
        }

        /// <summary> 删除</summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">ef 有效</param>
        public virtual Task<int> DeleteAsync(Entity entity, CancellationToken cancellationToken = default)
        {
            return Repository.DeleteAsync(entity, cancellationToken);
        }


        /// <summary> 删除</summary>
        /// <param name="id">实体</param>
        /// <param name="cancellationToken">ef 有效</param>
        public virtual Task<int> DeleteAsync(Key id, CancellationToken cancellationToken = default)
        {
            return Repository.DeleteAsync(id, cancellationToken);
        }



        /// <summary> 删除</summary>
        /// <param name="ids">实体</param>
        /// <param name="cancellationToken">ef 有效</param>
        public virtual Task<int> DeleteListAsync(Key[] ids, CancellationToken cancellationToken = default)
        {
            return Repository.DeleteListAsync(ids, cancellationToken);
        }

        /// <summary>
        /// 实现按需要只更新部分更新 默认实现 ef,nhibernate 支持 linq dapper 不支持linq
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">更新条件</param>
        /// <param name="update">更新后的实体</param>
        /// <param name="cancellationToken">ef 有效</param>
        public virtual Task<int> UpdateAsync(Expression<Func<Entity, bool>> where, Expression<Func<Entity, Entity>> update, CancellationToken cancellationToken = default)
        {
            return Repository.UpdateAsync(where, update, cancellationToken);
        }

        /// <summary> 批量删除 默认实现 ef,nhibernate 支持 linq dapper 不支持linq</summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken">ef 有效</param>
        public virtual Task<int> DeleteAsync(Expression<Func<Entity, bool>> where = null, CancellationToken cancellationToken = default)
        {
            return Repository.DeleteAsync(where, cancellationToken);
        }


        /// <summary> 执行sql </summary>
        /// <param name="sql"></param>
        /// <param name="cancellationToken">ef 有效</param>
        /// <returns></returns>
        public virtual Task<int> ExecuteSqlAsync(string sql, CancellationToken cancellationToken = default)
        {
            return Repository.ExecuteSqlAsync(sql, cancellationToken);
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="input">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        public virtual async Task<ResultDto<Output>> FindResultByPageAsync(Input input, int page, int size)
        {
            Entity entity = QueryBindBefore(input);
            //var t = Repository.GetType().GetMethods();
            //Object reference not set to an instance of an object.
            //阻塞了 任务怎么执行不下去啊
            var res =await Repository.FindListByEntityAndPageAsync(entity, page, size);
            var data = QueryOutputBindBefore(res);
            var count =await Repository.CountByEntityAsync(entity);
            var result = new ResultDto<Output>(data, page, size, count);
            return result;
        }
        ///// <summary> 操作成功 保存到库里 默认实现 ef 支持  dapper nhibernate 不支持 </summary>
        //Task SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
#endif
        #endregion async

        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void Dispose()
        {
            Repository.Dispose();
        }
        protected internal virtual Entity AddBindBefore(Create create)
        {
            Entity entity = default(Entity);
            if (create== default(Create))
            {
                return entity;
            }
            if (EntityType != CreateType)
            {
                entity = Mapper.Map<Entity>(create);
                AddBindAfter(create, entity);
            }
            else
            {
                entity = (Entity)Convert.ChangeType(create, CreateType);
            }
            return entity;
        }
        protected internal virtual Entity[] AddBindBefore(Create[] creates)
        {
            if (creates == null || creates.Length == 0)
            {
                return null;
            }
            Entity[] entities = new Entity[creates.Length];
            if (EntityType != CreateType)
            {
                var entity = Mapper.Map<Entity[]>(creates);
                AddBindAfter(creates, entity);
            }
            else
            {
                entities = (Entity[])Convert.ChangeType(creates, typeof(Entity[]));
            }
            return entities;
        }
        protected internal virtual List<Entity> AddBindBefore(List<Create> creates)
        {
            if (creates == null || creates.Count == 0)
            {
                return null;
            }
            List<Entity> entities = new List<Entity>(creates.Count);
            if (EntityType != CreateType)
            {
                var entity = Mapper.Map<List<Entity>>(creates);
                AddBindAfter(creates, entity);
            }
            else
            {
                entities = (List<Entity>)Convert.ChangeType(creates, typeof(List<Entity>));
            }
            return entities;
        }
        protected virtual void AddBindAfter(Create create, Entity entity)
        {

        }
        protected virtual void AddBindAfter(Create[] creates, Entity[] entities)
        {

        }
        protected virtual void AddBindAfter(List<Create> creates, List<Entity> entities)
        {

        }
        protected internal virtual Entity UpdateBindBefore(UpdateInput update)
        {
            Entity entity = default(Entity); 
            if (update== default(UpdateInput))
            {
                return entity;
            }
            if (EntityType != UpdateType)
            {
                entity = Mapper.Map<Entity>(update);
                UpdateBindAfter(update, entity);
            }
            else
            {
                entity = (Entity)Convert.ChangeType(update, CreateType);
            }
            return entity;
        }
        protected internal virtual Entity[] UpdateBindBefore(UpdateInput[] updates)
        {
            if (updates == null || updates.Length == 0)
            {
                return null;
            }
            Entity[] entities = new Entity[updates.Length];
            if (EntityType != UpdateType)
            {
                var entity = Mapper.Map<Entity[]>(updates);
                UpdateBindAfter(updates, entity);
            }
            else
            {
                entities = (Entity[])Convert.ChangeType(updates, typeof(Entity[]));
            }
            return entities;
        }
        protected internal virtual List<Entity> UpdateBindBefore(List<UpdateInput> updates)
        {
            if (updates == null || updates.Count == 0)
            {
                return null;
            }
            List<Entity> entities = new List<Entity>(updates.Count);
            if (EntityType != UpdateType)
            {
                var entity = Mapper.Map<List<Entity>>(updates);
                UpdateBindAfter(updates, entity);
            }
            else
            {
                entities = (List<Entity>)Convert.ChangeType(updates, typeof(List<Entity>));
            }
            return entities;
        }
        protected virtual void UpdateBindAfter(UpdateInput update, Entity entity)
        {

        }

        protected virtual void UpdateBindAfter(UpdateInput[] updates, Entity[] entities)
        {

        }
        protected virtual void UpdateBindAfter(List<UpdateInput> updates, List<Entity> entities)
        {

        }
        protected internal virtual Entity QueryBindBefore(Input input)
        {
            Entity entity = default(Entity);
            if (input== default(Input))
            {
                return entity;
            }
            if (EntityType != QueryType)
            {
                entity = Mapper.Map<Entity>(input);
                QueryBindAfter(input, entity);
            }
            else
            {
                entity = (Entity)Convert.ChangeType(input, EntityType);
            }
            return entity;
        }
        protected virtual void QueryBindAfter(Input input, Entity entity)
        {

        }
        protected internal virtual Output QueryOutputBindBefore(Entity entity)
        {
            Output output = default(Output);
            if (entity== default(Entity))
            {
                return output;
            }
            if (EntityType != OutputType)
            {
                output = Mapper.Map<Output>(entity);
                QueryOutputBindAfter(entity, output);
            }
            else
            {
                output = (Output)Convert.ChangeType(entity, OutputType);
            }
            return output;
        }
        protected internal virtual List<Output> QueryOutputBindBefore(List<Entity> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                return null;
            }
            List<Output> output = new List<Output>(entities.Count);
            if (EntityType != OutputType)
            {
                output = Mapper.Map<List<Output>>(entities);
                for (int i = 0; i < entities.Count; i++)
                {
                    QueryOutputBindAfter(entities[i], output[i]);
                }
            }
            else
            {
                output = (List<Output>)Convert.ChangeType(entities, typeof(List<Output>));
            }
            return output;
        }
        protected virtual void QueryOutputBindAfter(Entity entity, Output output)
        {

        }
        protected virtual void QueryOutputBindAfter(Entity[] entities, Output[] outputs)
        {

        }
      
    }
    public interface ICrudAppService<RepositoryImpl, Entity, Key>: ICrudAppService<RepositoryImpl, Entity, Entity, Entity, Entity
        , Entity, Key>, IAppService
          where RepositoryImpl : IRepository<Entity, Key>
            where Entity : class, IEntity<Key>
    {

    }
    /// <summary>
    /// crud  application service interface implement
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    /// <typeparam name="Key"></typeparam>
    [Transtation]
    public class CrudAppService<RepositoryImpl,  Entity, Key> : CrudAppService<RepositoryImpl, Entity, Entity, Entity, Entity
        , Entity, Key>, IAppService, ICrudAppService<RepositoryImpl, Entity, Key>
          where RepositoryImpl : IRepository<Entity, Key>
            where Entity : class, IEntity<Key>
    {
       
        /// <summary>
        /// no param application service  constractor 
        /// </summary>
        public CrudAppService(RepositoryImpl repository) : base(repository)
        {
        }
    }
    public interface ICrudAppService<Entity, Key>: ICrudAppService<IRepository<Entity, Key>, Entity, Key>
        , IAppService
        where Entity : class, IEntity<Key>
    {

    }
    /// <summary>
    /// crud  application service interface implement
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    /// <typeparam name="Key"></typeparam>
    [Transtation]
    public class CrudAppService<Entity, Key> : CrudAppService<IRepository<Entity, Key>, Entity, Key>,
        IAppService, ICrudAppService<Entity, Key> where Entity : class, IEntity<Key>
    {
        /// <summary>
        /// no param application service  constractor 
        /// </summary>
        public CrudAppService(IRepository<Entity, Key> repository) : base(repository)
        {

        }

    }

}
