using System;
using System.Collections.Generic;
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System.Data;
#endif
#if !(NET20 || NET30 )
using System.Linq;
using System.Linq.Expressions;
#endif
using System.Text;
using System.Threading;
#if !(NET20 || NET30 || NET35)
using System.Threading.Tasks;
#endif

namespace Utility.Domain.Uow
{
    /// <summary>
    /// 工作单元接口 ef dapper nhibernate
    /// <para> 适合在一下情况使用:</para>
    /// <para>1 在同一事务中进行多表操作 ef 支持 其他手动控制</para>
    /// <para>2 需要多表联合查询 Dapper不支持</para>
    /// <para>因为架构采用的是EF,Nhibernate Dapper访问数据库，暂时可以不用考虑采用传统Unit Work的注册机制</para>
    /// </summary>
    public interface IUnitWork:IDisposable
    {
        bool UseTransaction { get; set; }
       
        TransactionManager ReadTransaction { get; }
        TransactionManager WriteTransaction { get; }
        void Save();
        /// <summary>
        /// 静态 实现 时 有效 手动 关闭 资源 不然 不好 控制
        /// </summary>
        void Close();
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
        /// <summary>数据库连接对象 </summary>
        IDbConnection Connection { get; }
#endif
#if !(NET20 || NET30)
        /// <summary>查找单个，且不被上下文所跟踪 ef,nhibernate dapper 支持 linq  </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        T FindSingle<T>(Expression<Func<T, bool>> where=null)where T:class;

        /// <summary>查找单个，且不被上下文所跟踪 ef,nhibernate dapper 支持 linq  </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        T FindSingleByEntity<T>(T where) where T : class;

        /// <summary>查找单个，且不被上下文所跟踪 ef,nhibernate dapper 支持 linq  </summary>
        /// <param name="id">条件</param>
        /// <returns></returns>
        T FindSingle<T>(object id) where T : class;

        /// <summary> 是否存在 默认实现 ef,nhibernate dapper 支持 linq dapper  </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        bool IsExist<T>(Expression<Func<T, bool>> where = null) where T : class;

        /// <summary> 是否存在 默认实现 ef,nhibernate dapper 支持 linq dapper  </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        bool IsExistByEntity<T>(T where) where T : class;

        /// <summary> 是否存在 默认实现 ef,nhibernate dapper 支持 linq dapper  </summary>
        /// <param name="id">条件</param>
        /// <returns></returns>
        bool IsExist<T>(object id) where T : class;

        /// <summary> 查询数据 默认实现 ef,nhibernate  dapper 支持 linq </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        IQueryable<T> Query<T>(Expression<Func<T, bool>> where = null) where T : class;

        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集信息</return>
        List<T> FindListByEntity<T>(T obj) where T : class;


        /// <summary> 查询数据 默认实现 ef,nhibernate  dapper 支持 linq </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        IQueryable<T> QueryByPage<T>(Expression<Func<T, bool>> where=null,int page = 1, int size = 10) where T : class;


        /// <summary> 查询数据  </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        List<T> FindListByPageOrEntity<T>(T where, int page = 1, int size = 10) where T : class;

        /// <summary>查询数量  默认实现 ef,nhibernate dapper 支持 linq  </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        long Count<T>(Expression<Func<T, bool>> where = null) where T : class;

        /// <summary>查询数量  默认实现   </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        long CountByEntity<T>(T where) where T : class;
#endif

        /// <summary> 添加 </summary>
        /// <param name="entity">实体</param>
        int Insert<T>(T entity) where T : class;

        /// <summary>批量 添加 </summary>
        /// <param name="entities">实体</param>
        int BatchInsert<T>(T[] entities) where T : class;

        /// <summary> 更新一个实体的所有属性 </summary>
        /// <param name="entity">实体</param>
        int Update<T>(T entity) where T : class;

        /// <summary> 删除</summary>
        /// <param name="entity">实体</param>
        int Delete<T>(T entity) where T : class;

        /// <summary> 批量删除 默认实现</summary>
        int Delete<T>(object id) where T : class;

#if !(NET20 || NET30)
        /// <summary>
        /// 实现按需要只更新部分更新 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper 未实现
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">更新条件</param>
        /// <param name="update">更新后的实体</param>
        int Update<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> update) where T : class;

        /// <summary> 批量删除 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper 未实现</summary>
        /// <param name="exp">条件</param>
        int Delete<T>(Expression<Func<T, bool>> exp = null) where T : class;
       
#endif

        /// <summary> 执行sql </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int ExecuteSql(string sql);

        #region async
#if !(NET20 || NET30 || NET35)
        /// <summary> 查询数据  默认实现 ef,nhibernate dapper 支持 linq  </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken">dapper ef 无效</param>
        /// <returns></returns>
        Task<T> FindSingleAsync<T>(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary> 查询数据    </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken">dapper ef 无效</param>
        /// <returns></returns>
        Task<T> FindSingleByEntityAsync<T>(T where , CancellationToken cancellationToken = default) where T : class;


        /// <summary> 查询数据  默认实现   </summary>
        /// <param name="id">条件</param>
        /// <param name="cancellationToken">dapper ef 无效</param>
        /// <returns></returns>
        Task<T> FindSingleAsync<T>(object id, CancellationToken cancellationToken = default) where T : class;


        /// <summary> 是否存在 默认实现 ef,nhibernate dapper 支持 linq  </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken">ef core 有效 </param>
        /// <returns></returns>
        Task<bool> IsExistAsync<T>(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default) where T : class;


        /// <summary> 是否存在   </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken">ef core 有效 </param>
        /// <returns></returns>
        Task<bool> IsExistByEntityAsync<T>(T where = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary> 是否存在 默认实现   </summary>
        /// <param name="id">条件</param>
        /// <param name="cancellationToken">ef core 有效 </param>
        /// <returns></returns>
        Task<bool> IsExistAsync<T>(object id, CancellationToken cancellationToken = default) where T : class;

        /// <summary> 查询数据 默认实现 ef,nhibernate dapper 支持 linq ef 相当于普通方法 未实现async</summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken">ef core 有效 </param>
        /// <returns></returns>
        Task<IQueryable<T>> QueryAsync<T>(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary> 查询数据 </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken">ef core 有效 </param>
        /// <returns></returns>
        Task<List<T>> FindListByEntityAsync<T>(T where, CancellationToken cancellationToken = default) where T : class;

        /// <summary> 查询数据 默认实现 ef,nhibernate dapper 支持 linq </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IQueryable<T>> QueryByPageAsync<T>(Expression<Func<T, bool>> where = null, int page = 1, int size = 10,
             CancellationToken cancellationToken = default) where T : class;


        /// <summary> 查询数据  </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<T>> FindListByPageOrEntityAsync<T>(T where, int page = 1, int size = 10,
             CancellationToken cancellationToken = default) where T : class;

        /// <summary>查询数量  默认实现 ef,nhibernate dapper 支持 linq</summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> CountAsync<T>(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary> 添加 </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">ef core 有效 </param>
        Task<int> InsertAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class;

        /// <summary>批量 添加 </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="entities">实体</param>
        Task<int> BatchInsertAsync<T>(T[] entities, CancellationToken cancellationToken = default) where T : class;

        /// <summary> 更新一个实体的所有属性 </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">ef 有效</param>
        Task<int> UpdateAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class;

        /// <summary> 删除</summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">ef 有效</param>
        Task<int> DeleteAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class;

        /// <summary> 批量删除 默认实现</summary>
        Task<int> DeleteAsync<T>(object id, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// 实现按需要只更新部分更新 默认实现 ef,nhibernate 支持 linq dapper 不支持linq
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">更新条件</param>
        /// <param name="update">更新后的实体</param>
        /// <param name="cancellationToken">ef 有效</param>
        Task<int> UpdateAsync<T>(Expression<Func<T, bool>> where , Expression<Func<T, T>> update, CancellationToken cancellationToken = default) where T : class;

        /// <summary> 批量删除 默认实现 ef,nhibernate 支持 linq dapper 不支持linq</summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken">ef 有效</param>
        Task<int> DeleteAsync<T>(Expression<Func<T, bool>> where=null, CancellationToken cancellationToken = default) where T : class;


        /// <summary> 执行sql </summary>
        /// <param name="sql"></param>
        /// <param name="cancellationToken">ef 有效</param>
        /// <returns></returns>
        Task<int> ExecuteSqlAsync(string sql, CancellationToken cancellationToken = default) ;
        Task SaveAsync(CancellationToken cancellationToken = default);
#endif
        #endregion async
    }
}
