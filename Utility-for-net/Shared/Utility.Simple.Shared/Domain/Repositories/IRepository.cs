using System;
#if !( NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 ||  NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System.Data;
#endif
#if !(NET20 || NET30 || NET35)
using System.Threading.Tasks;
#endif
#if !(NET20 || NET30)
using System.Linq;
using System.Linq.Expressions;
#endif
using System.Threading;
using Utility.Domain.Entities;
using System.Collections.Generic;
using Utility.Application.Services.Dtos;
using Utility.Domain.Uow;

namespace Utility.Domain.Repositories
{
    /// <summary>
    /// 通用泛型接口
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    /// <typeparam name="Key"></typeparam>
    public interface IRepository<Entity, Key>: IRepository<Entity> where Entity : class, IEntity<Key>
    {
        /// <summary> 删除</summary>
        /// <param name="id">实体 id</param>
        int Delete(Key id);

        /// <summary> 删除</summary>
        /// <param name="ids">实体 id</param>
        int DeleteList(Key[] ids);

#if !(NET20 || NET30 || NET35)
        /// <summary> 查询数据  默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="id">条件</param>
        /// <param name="cancellationToken">dapper ef 无效</param>
        /// <returns></returns>
        Task<int> DeleteAsync(Key id, CancellationToken cancellationToken = default);

        /// <summary> 查询数据  默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="ids">条件</param>
        /// <param name="cancellationToken">dapper ef 无效</param>
        /// <returns></returns>
        Task<int> DeleteListAsync(Key[] ids, CancellationToken cancellationToken = default);
#endif

    }

    /// <summary>
    ///  通用接口
    /// </summary>
    public interface IRepository
    {
        IUnitWork UnitWork { get; }
    }
    /// <summary> 
    /// 通用泛型接口 ef 自动调用save  dapper commit nhiberne commit
    /// <para>dapper CancellationToken 无效</para>
    /// <para>nhibernate CancellationToken 有效</para>
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    public interface IRepository<Entity>:IRepository,IDisposable where Entity : class
    {

       
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
        /// <summary>数据库连接对象 </summary>
        IDbConnection Connection { get; }
#endif

#if !(NET20 || NET30)
        /// <summary>查找单个，且不被上下文所跟踪 ef,nhibernate dapper 支持 linq </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        Entity FindSingle(Expression<Func<Entity, bool>> where = null);

        /// <summary>查找单个</summary>
        /// <param name="entity">条件</param>
        /// <returns></returns>
        Entity FindSingleByEntity(Entity entity);


        /// <summary>查找单个 </summary>
        /// <param name="id">条件</param>
        /// <returns></returns>
        Entity FindSingle(object id);

        /// <summary> 是否存在 默认实现 ef,nhibernate dapper 支持 linq  </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        bool IsExist(Expression<Func<Entity, bool>> where=null);

        /// <summary> 是否存在 默认实现 ef,nhibernate dapper 支持 linq </summary>
        /// <param name="id">条件</param>
        /// <returns></returns>
        bool IsExist(object id);

        /// <summary> 是否存在 默认实现 ef,nhibernate dapper 支持 linq  </summary>
        /// <param name="entity">条件</param>
        /// <returns></returns>
        bool IsExistByEntity(Entity entity);

        /// <summary> 查询数据 默认实现 ef,nhibernate dapper 支持 linq</summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        IQueryable<Entity> Query(Expression<Func<Entity, bool>> where = null);


        /// <summary> 查询数据 默认实现 ef,nhibernate dapper 支持 linq</summary>
        /// <param name="entity">条件</param>
        /// <returns></returns>
        List<Entity> FindListByEntity(Entity entity);

        /// <summary> 查询数据 默认实现 ef,nhibernate dapper 支持 linq</summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        IQueryable<Entity> QueryByPage(Expression<Func<Entity, bool>> where = null,int page = 1, int size = 10);


        /// <summary> 查询数据 默认实现  </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="entity">条件</param>
        /// <returns></returns>
        List<Entity> FindListByEntityAndPage(Entity entity, int page = 1, int size = 10);


        /// <summary>
        /// 根据条件及分页查询实体类数据集信息和实体类数据集数量信息 
        /// (契约接口方法必须要有System.ServiceModel.OperationContract注解 支持web 
        /// 必须要有 System.ServiceModel.Web.WebGet：httpget System.ServiceModel.Web.WebInvoke:httppost)
        /// </summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        ResultDto<Entity> FindResultByEntityAndPage(Entity obj, int page, int size);

        /// <summary>查询数量  默认实现 ef,nhibernate dapper 支持 linq  </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        long Count(Expression<Func<Entity, bool>> where = null);


        /// <summary>查询数量    </summary>
        /// <param name="entity">条件</param>
        /// <returns></returns>
        long CountByEntity(Entity entity);
#endif

        /// <summary> 添加 </summary>
        /// <param name="entity">实体</param>
        int Insert(Entity entity);

        /// <summary>批量 添加 </summary>
        /// <param name="entities">实体</param>
        int BatchInsert(Entity[] entities);

        /// <summary> 更新一个实体的所有属性 </summary>
        /// <param name="entity">实体</param>
        int Update(Entity entity);

        /// <summary> 删除</summary>
        /// <param name="entity">实体</param>
        int Delete(Entity entity);



#if !(NET20 || NET30)
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

        ///// <summary> 操作成功 保存到库里 默认实现 ef 支持  dapper nhibernate 无任何操作 </summary>
        //void Save();

        #region async
#if !(NET20 || NET30 || NET35)
        /// <summary> 查询数据  默认实现 ef,nhibernate dapper 支持 linq  </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken">dapper ef 无效</param>
        /// <returns></returns>
        Task<Entity> FindSingleAsync(Expression<Func<Entity, bool>> where = null,CancellationToken cancellationToken=default);

        /// <summary> 查询数据    </summary>
        /// <param name="entity">条件</param>
        /// <param name="cancellationToken">dapper ef 无效</param>
        /// <returns></returns>
        Task<Entity> FindSingleByEntityAsync(Entity entity, CancellationToken cancellationToken = default);


        /// <summary> 查询数据  默认实现 ef,nhibernate dapper 支持 linq  </summary>
        /// <param name="id">条件</param>
        /// <param name="cancellationToken">dapper ef 无效</param>
        /// <returns></returns>
        Task<Entity> FindSingleAsync(object id, CancellationToken cancellationToken = default);

        /// <summary> 是否存在 默认实现 ef,nhibernate dapper 支持 linq  </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsExistAsync(Expression<Func<Entity, bool>> where=null, CancellationToken cancellationToken = default);

        /// <summary> 是否存在   </summary>
        /// <param name="entity">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsExistByEntityAsync(Entity entity, CancellationToken cancellationToken = default);

        /// <summary> 是否存在  </summary>
        /// <param name="id">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsExistAsync(object id, CancellationToken cancellationToken = default);

        /// <summary> 查询数据 默认实现 ef,nhibernate dapper 支持 linq ef 相当于普通方法 未实现async</summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IQueryable<Entity>> QueryAsync(Expression<Func<Entity, bool>> where = null, CancellationToken cancellationToken = default);

        /// <summary> 查询数据 默认实现</summary>
        /// <param name="entity">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Entity>> FindListByEntityAsync(Entity entity, CancellationToken cancellationToken = default);

        /// <summary> 查询数据 默认实现 ef,nhibernate dapper 支持 linq orderby参数无效 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IQueryable<Entity>> QueryByPageAsync(Expression<Func<Entity, bool>> where = null, int page = 1, int size = 10,
             CancellationToken cancellationToken = default);

        /// <summary> 查询数据   </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="entity">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Entity>> FindListByEntityAndPageAsync(Entity entity, int page = 1, int size = 10,
             CancellationToken cancellationToken = default);

        /// <summary>查询数量  默认实现 ef,nhibernate dapper 支持 linq </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> CountAsync(Expression<Func<Entity, bool>> where = null, CancellationToken cancellationToken = default);

        /// <summary>查询数量   </summary>
        /// <param name="entity">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> CountByEntityAsync(Entity entity, CancellationToken cancellationToken = default);

        /// <summary> 添加 </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">ef core 有效 </param>
        Task<int> InsertAsync(Entity entity, CancellationToken cancellationToken = default);

        /// <summary>批量 添加 </summary>
        /// <param name="entities">实体</param>
        /// <param name="cancellationToken"></param>
        Task<int> BatchInsertAsync(Entity[] entities, CancellationToken cancellationToken = default);

        /// <summary> 更新一个实体的所有属性 </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">ef 有效</param>
        Task<int> UpdateAsync(Entity entity, CancellationToken cancellationToken = default);

        /// <summary> 删除</summary>
        /// <param name="entity">实体</param>
       /// <param name="cancellationToken">ef 有效</param>
        Task<int> DeleteAsync(Entity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 实现按需要只更新部分更新 默认实现 ef,nhibernate 支持 linq dapper 不支持linq
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">更新条件</param>
        /// <param name="update">更新后的实体</param>
        /// <param name="cancellationToken">ef 有效</param>
        Task<int> UpdateAsync(Expression<Func<Entity, bool>> where, Expression<Func<Entity, Entity>> update, CancellationToken cancellationToken = default);

        /// <summary> 批量删除 默认实现 ef,nhibernate 支持 linq dapper 不支持linq</summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken">ef 有效</param>
        Task<int> DeleteAsync(Expression<Func<Entity, bool>> where=null, CancellationToken cancellationToken = default);
     

        /// <summary> 执行sql </summary>
        /// <param name="sql"></param>
        /// <param name="cancellationToken">ef 有效</param>
        /// <returns></returns>
        Task<int> ExecuteSqlAsync(string sql, CancellationToken cancellationToken = default);

        ///// <summary> 操作成功 保存到库里 默认实现 ef save 支持  dapper nhibernate commit </summary>
        //Task SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
#endif
#endregion async
    }
}
