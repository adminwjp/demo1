#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
//#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Utility.Database;
using Utility.Domain.Entities;
using Utility.Domain.Extensions;
using Utility.Domain.Uow;
using Utility.Extensions;
using Utility.Helpers;
using DapperExtensions;
using Utility.Attributes;

namespace Utility.Dapper.Uow
{
    /// <summary>dapper linq 不支持 需要自己转换 </summary>
    [Transtation]
    public class DapperUnitWork : IUnitWork
    {

        /// <summary>
        /// 静态 实现 时 有效 手动 关闭 资源 不然 不好 控制
        /// </summary>
        public void Close()
        {

        }
        private DapperConnectionProvider _connection;//数据库连接对象
        private DapperConnectionProvider write;
        private DapperConnectionProvider read;
        private TransactionManager readTranstion;
        private TransactionManager writeTranstion;

        public bool UseTransaction { get;  set; } = true;
        /// <summary>
        /// 数据库连接对象
        /// </summary>

        public DapperConnectionProvider Connection
        {
            get
            {
                _connection = _connection ?? write ?? read ?? WriteConnection.Value;
                return _connection;
            }
            protected set
            {
                _connection = value;
            }
        }

        /// <summary>数据库连接对象 </summary>
        public Lazy<DapperConnectionProvider> WriteConnection { get; protected set; }

        /// <summary>数据库连接对象 </summary>
        public Lazy<DapperConnectionProvider> ReadConnection { get; protected set; }

        /// <summary>数据库连接对象 </summary>
        public DapperConnectionProvider Write
        {
            get
            {
                write = write ?? WriteConnection.Value;
                return write;
            }
            protected set => write = value;
        }

        /// <summary>数据库连接对象 </summary>
        public DapperConnectionProvider Read
        {
            get
            {
                read = read ?? ReadConnection.Value;
                return read;
            }
            protected set => read = value;
        }
        /// <summary>
        /// 
        /// </summary>
        public DapperTemplate DapperTemplate { get; protected set; }

        public TransactionManager ReadTransaction { get
            {
                if (readTranstion == null)
                {
                    readTranstion = Read.Transaction;
                }
                return readTranstion;
            }
            set => readTranstion = value; }

        public TransactionManager WriteTransaction {
            get
            {
                if (writeTranstion == null)
                {
                    writeTranstion = Write.Transaction;
                }
                return writeTranstion;
            }
            set => writeTranstion = value; }

        IDbConnection IUnitWork.Connection
        {
            get
            {
                if (Write == Read)
                {
                    return Write.Connection;
                }
                return Write == null ? Write.Connection : Read?.Connection;
            }
        }

        /// <summary> 构造注册数据库连接对象</summary>
        /// <param name="connection">数据库连接对象</param>
        public DapperUnitWork(DapperConnectionProvider connection)
        {
            this.Connection = connection;
            this.Write = connection;
            this.Read = connection;
            this.DapperTemplate = new DapperTemplate(connection.Connection);
        }
        /// <summary>
        ///  读写分离
        /// </summary>
        /// <param name="writeConnection"> 写库</param>
        /// <param name="readConnection">读库</param>
        public DapperUnitWork(Lazy<DapperConnectionProvider> writeConnection, Lazy<DapperConnectionProvider> readConnection)
        {
            this.ReadConnection = writeConnection;
            this.ReadConnection = readConnection;
            this.DapperTemplate = new DapperTemplate(new Lazy<IDbConnection>(() => writeConnection.Value.Connection), new Lazy<IDbConnection>(() => readConnection.Value.Connection));
        }



        /// <summary>查找单个，且不被上下文所跟踪 ef,nhibernate 支持 linq dapper 不支持linq </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual T FindSingle<T>(Expression<Func<T, bool>> where = null) where T : class
        {
            Read.Read = true && UseTransaction;
            return Read.Connection.FirstOrDefault<T>(where,Read.Transaction.Transaction);
            //throw new NotSupportedException();
            //return Find(where).FirstOrDefault();
        }

        /// <summary>查找单个，且不被上下文所跟踪 ef,nhibernate 支持 linq dapper 不支持linq </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual T FindSingleByEntity<T>(T where) where T : class
        {
            throw new NotSupportedException();
        }

        /// <summary>查找单个，且不被上下文所跟踪 ef,nhibernate 支持 linq dapper 不支持linq </summary>
        /// <param name="id">条件</param>
        /// <returns></returns>
        public virtual T FindSingle<T>(object id) where T : class
        {
            Read.Read = true && UseTransaction;
            return Read.Connection.Get<T>(id, Read.Transaction.Transaction);
            //throw new NotSupportedException();
        }

        /// <summary> 是否存在 默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual bool IsExist<T>(Expression<Func<T, bool>> where = null) where T : class
        {
            Read.Read = true && UseTransaction;
            return Count(where) > 0;
        }

        /// <summary> 是否存在 默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual bool IsExistByEntity<T>(T where) where T : class
        {
            throw new NotSupportedException();
        }

        /// <summary> 是否存在 默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="id">条件</param>
        /// <returns></returns>
        public virtual bool IsExist<T>(object id) where T : class
        {
            Read.Read = true&&UseTransaction;
            return Read.Connection.Get<T>(id, Read.Transaction.Transaction)!=null;
            //throw new NotSupportedException();
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper默认查询所有结果集基于内存 条件查询</summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual IQueryable<T> Query<T>(Expression<Func<T, bool>> where = null) where T : class
        {
            Read.Read = true && UseTransaction;
            where = where.Filter();
            return Read.Connection.GetList<T>(where,null, Read.Transaction.Transaction).AsQueryable();
            //return DapperTemplate.FindList<T>(null, Read.Transtion.Transaction).AsQueryable().Where(where);
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper默认查询所有结果集基于内存 条件查询</summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual List<T> FindListByEntity<T>(T where) where T : class
        {
            throw new NotSupportedException();
        }



        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq orderby参数无效 dapper默认查询所有结果集基于内存 条件查询 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual IQueryable<T> QueryByPage<T>(Expression<Func<T, bool>> where = null, int page = 1, int size = 10) where T : class
        {
            Read.Read = true && UseTransaction;
            where = where.Filter();
            return Read.Connection.GetList<T>(where, null, Read.Transaction.Transaction).Skip((page-1)*size).Take(size).AsQueryable();
            //return DapperTemplate.FindListByPage<T>("", "", null, page, size, Read.Transtion.Transaction).AsQueryable().Where(where);
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq orderby参数无效 dapper默认查询所有结果集基于内存 条件查询 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual List<T> FindListByPageOrEntity<T>(T where, int page = 1, int size = 10) where T : class
        {
            throw new NotSupportedException();
        }

        /// <summary>查询数量  默认实现 ef,nhibernate 支持 linq dapper 不支持linq  dapper默认查询所有结果数量  </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual long Count<T>(Expression<Func<T, bool>> where = null) where T : class
        {
            Read.Read = true && UseTransaction;
            where = where.Filter();
            return Read.Connection.Count<T>(where, Read.Transaction.Transaction);
            //return DapperTemplate.Count<T>("", null, Read.Transtion.Transaction);
        }

        /// <summary>查询数量  默认实现 ef,nhibernate 支持 linq dapper 不支持linq  dapper默认查询所有结果数量  </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual long CountByEntity<T>(T where) where T : class
        {
            throw new NotSupportedException();
        }

        /// <summary> 添加 </summary>
        /// <param name="entity">实体</param>
        public virtual int Insert<T>(T entity) where T : class
        {
            Write.Write = true&&UseTransaction;
            UpdateValue(entity);
            int? res = DapperTemplate.Insert(Write.Connection, entity, Write.Transaction.Transaction);
            return res == null ? 0 : res.Value;
        }

        /// <summary>
        /// 手动赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="flag">1 add 2 update 3 delete</param>
        protected virtual bool UpdateValue<T>(T entity, int flag = 1) where T : class
        {
            return entity.UpdateValue(flag);
        }

        /// <summary>批量 添加 </summary>
        /// <param name="entities">实体</param>
        public virtual int BatchInsert<T>(T[] entities) where T : class
        {
            Write.Write = true && UseTransaction;
            int res = 0;
            foreach (T entity in entities)
            {
                UpdateValue(entity);
                int? count = Write.Connection.Insert<T>(entity, Write.Transaction.Transaction);
                res += count == null ? 0 : count.Value;
            }
            return res;
        }

        /// <summary> 更新一个实体的所有属性 </summary>
        /// <param name="entity">实体</param>
        public virtual int Update<T>(T entity) where T : class
        {
            Write.Write = true && UseTransaction;
            UpdateValue(entity, 2);
            return DapperTemplate.Update(Write.Connection, entity, Write.Transaction.Transaction);
        }

        /// <summary> 删除</summary>
        /// <param name="entity">实体</param>
        public virtual int Delete<T>(T entity) where T : class
        {
            Write.Write = true && UseTransaction;
            if (UpdateValue(entity, 3))
            {
                return DapperTemplate.Update(Write.Connection, entity, Write.Transaction.Transaction);
            }
            else
            {
                return DapperTemplate.Delete(Write.Connection, entity, Write.Transaction.Transaction);
            }
        }
        /// <summary> 批量删除 默认实现  nhibernate 支持 EF dapper 未实现</summary>
        public virtual int Delete<T>(object id) where T : class
        {
            Write.Write = true && UseTransaction;
            if (typeof(T).IsAssignableFrom(typeof(IHasDeletionTime)))
            {
                T obj = Write.Connection.Get<T>(id, Write.Transaction.Transaction);
                if (obj is IHasDeletionTime deletionTime)
                {
                    deletionTime.DeletionTime = CommonHelper.TotalMilliseconds();
                    deletionTime.IsDeleted = true;
                }
                return DapperTemplate.Update(Write.Connection, obj, Write.Transaction.Transaction);
            }
            else
            {
                return DapperTemplate.Delete<T>(Write.Connection, id, Write.Transaction.Transaction);
            }

        }
        /// <summary>
        /// 实现按需要只更新部分更新 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper 未实现
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">更新条件</param>
        /// <param name="update">更新后的实体</param>
        public virtual int Update<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> update) where T : class
        {
            throw new NotImplementedException();
        }

        /// <summary> 批量删除 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper 未实现</summary>
        /// <param name="where">条件</param>
        public virtual int Delete<T>(Expression<Func<T, bool>> where) where T : class
        {
            throw new NotImplementedException();
        }

        /// <summary> 执行sql </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual int ExecuteSql(string sql)
        {
            Write.Write = true && UseTransaction;
            return DapperTemplate.Execute(Write.Connection, sql, Write.Transaction.Transaction);
        }


        /// <summary> 操作成功 保存到库里 默认实现 ef 支持  dapper nhibernate 不支持 </summary>
        public virtual void Save()
        {
            Write?.Transaction?.Commit();
        }



        #region async

        /// <summary>查找单个，且不被上下文所跟踪 ef,nhibernate 支持 linq dapper 不支持linq </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<T> FindSingleAsync<T>(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default) where T : class
        {
            return Task.FromResult(FindSingle(where));
        }

        /// <summary>查找单个，且不被上下文所跟踪 ef,nhibernate 支持 linq dapper 不支持linq </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<T> FindSingleByEntityAsync<T>(T where, CancellationToken cancellationToken = default) where T : class
        {
            return Task.FromResult(FindSingleByEntity(where));
        }

        /// <summary>查找单个，且不被上下文所跟踪 ef,nhibernate 支持 linq dapper 不支持linq </summary>
        /// <param name="id">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<T> FindSingleAsync<T>(object id, CancellationToken cancellationToken = default) where T : class
        {
            return Task.FromResult(FindSingle<T>(id));
        }

        /// <summary> 是否存在 默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual
#if !NET40
        async
#endif
        Task<bool> IsExistAsync<T>(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default) where T : class
        {
#if !NET40
            var res = await CountAsync(where, cancellationToken);
            return res > 0;
#else
            return Task.FromResult(Count(where) > 0);
#endif

        }


        /// <summary> 是否存在 默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> IsExistByEntityAsync<T>(T where, CancellationToken cancellationToken = default) where T : class
        {
            return Task.FromResult(IsExistByEntity(where));

        }

        /// <summary> 是否存在 默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="id">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> IsExistAsync<T>(object id, CancellationToken cancellationToken = default) where T : class
        {
            return Task.FromResult(IsExist<T>(id));

        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper默认查询所有结果集基于内存 条件查询</summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual
#if !NET40
        async
#endif
        Task<IQueryable<T>> QueryAsync<T>(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            where = where.Filter();
#if !NET40
            var res = await DapperTemplate.FindListAsync<T>(Read.Connection,null, Read.Transaction.Transaction);
            return res.AsQueryable().Where(where);
#else
            return Task.FromResult(DapperTemplate.FindList<T>(Read.Connection,Read.Transtion.Transaction).AsQueryable().Where(where));
#endif
        }



        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper默认查询所有结果集基于内存 条件查询</summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<List<T>> FindListByEntityAsync<T>(T where, CancellationToken cancellationToken = default) where T : class
        {
            return Task.FromResult(FindListByEntity<T>(where));
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq orderby参数无效 dapper默认查询所有结果集基于内存 条件查询 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual
#if !NET40
        async
#endif
        Task<IQueryable<T>> QueryByPageAsync<T>(Expression<Func<T, bool>> where = null, int page = 1, int size = 10, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            where = where.Filter();
#if !NET40
            var res = await DapperTemplate.FindListByPageAsync<T>(Read.Connection, "", "", null, page, size, Read.Transaction.Transaction);
            return res.AsQueryable().Where(where);
#else
            return Task.FromResult(DapperTemplate.FindListByPage<T>(Read.Connection,"","",null,page, size,Read.Transtion.Transaction).AsQueryable().Where(where));
#endif
        }


        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq orderby参数无效 dapper默认查询所有结果集基于内存 条件查询 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<List<T>> FindListByPageOrEntityAsync<T>(T where, int page = 1, int size = 10, CancellationToken cancellationToken = default) where T : class
        {
            return Task.FromResult(FindListByPageOrEntity<T>(where, page, size));
        }

        /// <summary>查询数量  默认实现 ef,nhibernate 支持 linq dapper 不支持linq  dapper默认查询所有结果数量  </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<long> CountAsync<T>(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            where = where.Filter();
            return DapperTemplate.CountAsync<T>(Read.Connection, "", null, Read.Transaction.Transaction);
        }

        /// <summary>查询数量  默认实现 ef,nhibernate 支持 linq dapper 不支持linq  dapper默认查询所有结果数量  </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<long> CountAsync<T>(T where, CancellationToken cancellationToken = default) where T : class
        {
            return Task.FromResult(CountByEntity<T>(where));
        }

        /// <summary> 添加 </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> InsertAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
            UpdateValue(entity);
            int? res = DapperTemplate.InsertAsync(Write.Connection,entity, Write.Transaction.Transaction).GetAwaiter().GetResult();
            return Task.FromResult(res == null ? 0 : res.Value);
        }

        /// <summary>批量 添加 </summary>
        /// <param name="entities">实体</param>
        ///         /// <param name="cancellationToken"></param>
        public virtual
#if !NET40
        async
#endif
Task<int> BatchInsertAsync<T>(T[] entities, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
#if !NET40
            foreach (T entity in entities)
            {
                UpdateValue(entity);
                await Write.Connection.InsertAsync<T>(entity, Write.Transaction.Transaction);
            }
            return 1;
#else
            return Task.FromResult(BatchInsert(entities));
#endif
        }

        /// <summary> 更新一个实体的所有属性 </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> UpdateAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
            UpdateValue(entity, 2);
            return DapperTemplate.UpdateAsync(Write.Connection,entity, Write.Transaction.Transaction);
        }

        /// <summary> 删除</summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> DeleteAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
            UpdateValue(entity, 3);
            return DapperTemplate.UpdateAsync(Write.Connection,entity,Write.Transaction.Transaction);
            //return DapperTemplate.DeleteAsync(entity);
        }

        /// <summary> 批量删除 默认实现  nhibernate 支持 EF dapper 未实现</summary>
        ///<param name="id"></param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> DeleteAsync<T>(object id, CancellationToken cancellationToken = default) where T : class
        {
            return Task.FromResult(Delete<T>(id));
        }
        /// <summary>
        /// 实现按需要只更新部分更新 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper 未实现
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">更新条件</param>
        /// <param name="update">更新后的实体</param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> UpdateAsync<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> update, CancellationToken cancellationToken = default) where T : class
        {
            return Task.FromResult(Update(where, update));
        }

        /// <summary> 批量删除 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper 未实现</summary>
        /// <param name="cancellationToken">dapper 无效</param>
        /// <param name="where">条件</param>
        public virtual Task<int> DeleteAsync<T>(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default) where T : class
        {
            return Task.FromResult(Delete(where));
        }

        /// <summary> 执行sql </summary>
        /// <param name="sql"></param>
        /// <param name="cancellationToken">dapper 无效</param>
        /// <returns></returns>
        public virtual Task<int> ExecuteSqlAsync(string sql, CancellationToken cancellationToken = default)
        {
            Write.Write = true && UseTransaction;
            return DapperTemplate.ExecuteAsync(Write.Connection,sql, Write.Transaction.Transaction);
        }


        /// <summary> 操作成功 保存到库里 默认实现 ef 支持  dapper nhibernate 不支持 </summary>
        /// <param name="cancellationToken"></param>
        public virtual Task SaveAsync(CancellationToken cancellationToken = default)
        {
           return Write?.Transaction?.CommitAsync();
          
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void Dispose()
        {
            Connection.Dispose();
        }
        #endregion async
    }
}
#endif