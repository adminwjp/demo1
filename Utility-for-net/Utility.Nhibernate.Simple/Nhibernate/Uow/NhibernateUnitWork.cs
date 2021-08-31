#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
//#if NET40 ||NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NET481 || NET482 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETCOREAPP3_2 || NETSTANDARD2_0 || NETSTANDARD2_1
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using Utility.Application.Services.Dtos;
using Utility.Attributes;
using Utility.Domain.Entities;
using Utility.Domain.Extensions;
using Utility.Domain.Uow;
using Utility.Helpers;
using Utility.Nhibernate.Infrastructure;
using Utility.Threads;

namespace Utility.Nhibernate.Uow
{
    /// <summary>
    /// 如果 多线程每次执行 Save怎么确定提交 (怎么追踪每次的任务)
    /// </summary>
    [Transtation]
    public class NhibernateUnitWork : IUnitWork
    {
        SessionProvider write;
        SessionProvider read;
        public bool UseTransaction { get;  set; } = true;
        /// <summary>
        ///默认实现 写 库
        /// </summary>
        public Lazy<SessionProvider> WriteSession;

        /// <summary>
        ///默认实现 
        /// </summary>
        public SessionProvider Session;

        /// <summary>
        /// 读 库
        /// </summary>
        public Lazy<SessionProvider> ReadSession;

        /// <summary>
        ///默认实现 写 库
        /// </summary>
        public SessionProvider Write
        {
            get
            {
                if (write != null)
                {
                    return write;
                }
                return write = Session ?? WriteSession.Value;
            }
            private set
            {
                write = value;
            }
        }

        /// <summary>
        ///默认实现  读 库
        /// </summary>
        public SessionProvider Read
        {
            get
            {
                if (read != null)
                {
                    return read;
                }
                return read = Session ?? ReadSession.Value;
            }
            private set
            {
                read = value;
            }
        }
        /// <summary>数据库连接对象 </summary>
        public IDbConnection Connection
        {
            get
            {
                connection = connection ?? write?.Session.Connection ?? read?.Session.Connection ?? WriteSession.Value.Session.Connection;
                return connection;
            }
            private set => connection = value;
        }

        /// <summary>
        /// NhibernateTemplate
        /// </summary>
        protected NhibernateTemplate Template;
        private IDbConnection connection;
        private TransactionManager transtion;
        private TransactionManager readTranstion;
        private TransactionManager writeTranstion;


        /// <summary>
        /// 默认实现
        /// </summary>
        /// <param name="session"></param>
        public NhibernateUnitWork(SessionProvider session)
        {
            this.Session = session;
            this.write = session;
            this.read = session;
            this.Connection = session?.Session.Connection;
            this.Template = NhibernateTemplate.Empty;
        }
        /// <summary>
        ///  读写分离
        /// </summary>
        /// <param name="writeSession"> 写库</param>
        /// <param name="readSession">读库</param>
        public NhibernateUnitWork(Lazy<SessionProvider> writeSession, Lazy<SessionProvider> readSession)
        {
            this.WriteSession = writeSession;
            this.ReadSession = readSession;
            //this.Connection = writeSession.Value.Connection;
            this.Template = NhibernateTemplate.Empty;
            this.Single = false;
        }
        //public NhibernateUnitWork(AppSessionFactory sessionFactory) : base(sessionFactory)
        //{
        //    this.Connection = sessionFactory.OpenSession().Connection;
        //}



        /// <summary>
        /// 单库
        /// </summary>
        protected bool Single { get; set; } = true;

        public TransactionManager Transaction
        { get
            {
                if (transtion == null)
                {
                    transtion = Session.Transaction;
                }
                return transtion;
            }
            internal set => transtion = value; }
        public TransactionManager ReadTransaction
        { get
            {
                if (readTranstion == null)
                {
                    readTranstion = Read.Transaction;
                }
                return readTranstion;
            }
            internal set => readTranstion = value; }
        public TransactionManager WriteTransaction
        { get
            {
                if (writeTranstion == null)
                {
                    writeTranstion = Write.Transaction;
                }
                return writeTranstion;
            }
            internal set => writeTranstion = value; }
        public void UpdaeTransaction()
        {
            if (Single)
            {
                Transaction = Session.Transaction;
            }
            else
            {
                ReadTransaction = Read?.Transaction;
                WriteTransaction = Write?.Transaction;
            }
        }
        public void SetReadTranstion(SessionProvider session)
        {
            ReadTransaction = session.Transaction;
            Transaction = ReadTransaction;
        }
        public void SetWriteTranstion(SessionProvider session)
        {
            WriteTransaction = session.Transaction;
            Transaction = WriteTransaction;
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public virtual ResultDto<T> FindResultByPage<T>(Expression<Func<T, bool>> exp = null, int pageindex = 1, int pagesize = 10) where T : class
        {
            var tuple = FindTupleByPage(exp, pageindex, pagesize);
            return new ResultDto<T>(tuple, pageindex, pagesize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public virtual Tuple<List<T>, long> FindTupleByPage<T>(Expression<Func<T, bool>> exp = null, int pageindex = 1, int pagesize = 10) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetTupleByPage(Read.Session, exp, pageindex, pagesize);
        }
        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="where">条件</param>
        ///<return>返回实体类数据集数量信息</return>

        public virtual long Count<T>(ICriteria where = null) where T : class
        {
            Read.Read = true&&UseTransaction;
            return Template.GetCount<T>(Read.Session, where);
        }

        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="where">条件</param>
        ///<return>返回实体类数据集信息</return>
        public virtual List<T> FindList<T>(ICriteria where = null) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetList<T>(Read.Session, where);
        }

        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="where">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息</return>

        public virtual List<T> FindListByPage<T>(ICriteria where = null, int page = 1, int size = 10) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetListByPage<T>(Read.Session, where, page, size);
        }

        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="where">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息</return>

        public virtual List<T> FindListByPageOrEntity<T>(T where, int page = 1, int size = 10) where T : class
        {
            Read.Read = true && UseTransaction;
            return FindListByPage<T>(null, page, size);
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="where">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        public virtual ResultDto<T> FindResultByPage<T>(ICriteria where = null, int page = 1, int size = 10) where T : class
        {
            Read.Read = true && UseTransaction;
            var res = FindTupleByPage<T>(where, page, size);
            return new ResultDto<T>(res.Item1, page, size, res.Item2);
        }


        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="where">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        public virtual Tuple<List<T>, long> FindTupleByPage<T>(ICriteria where = null, int page = 1, int size = 10) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetTupleByPage<T>(Read.Session, where, page, size);
        }

        #region async



        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回实体类数据集信息</return>

        public virtual Task<List<T>> FindListAsync<T>(ICriteria where = null,
            CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetListAsync<T>(Write.Session, where, cancellationToken);
        }

        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="where">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回实体类数据集信息</return>
        public virtual Task<List<T>> FindListByPageAsync<T>(ICriteria where = null, int page = 1, int size = 10,
             CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetListByPageAsync<T>(Write.Session, where, page, size, cancellationToken);
        }
        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="where">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        public virtual Task<Tuple<List<T>, long>> FindTupleByPageAsync<T>(ICriteria where = null, int page = 1,
            int size = 10, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetTupleByPageAsync<T>(Write.Session, where, page, size, cancellationToken);
        }
        #endregion async


        /// <summary>查找单个，且不被上下文所跟踪 ef,nhibernate 支持 linq dapper 不支持linq </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual T FindSingle<T>(Expression<Func<T, bool>> where = null) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.Get(Read.Session, where);
        }

        /// <summary>查找单个，且不被上下文所跟踪 ef,nhibernate 支持 linq dapper 不支持linq </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual T FindSingleByEntity<T>(T where) where T : class
        {
            Read.Read = true && UseTransaction;
            throw new NotSupportedException();
        }

        /// <summary>查找单个，且不被上下文所跟踪 ef,nhibernate 支持 linq dapper 不支持linq </summary>
        /// <param name="id">条件</param>
        /// <returns></returns>
        public virtual T FindSingle<T>(object id) where T : class
        {
            Read.Read = true && UseTransaction;
            return Read.Session.Get<T>(id);
        }
        /// <summary> 是否存在 默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual bool IsExist<T>(Expression<Func<T, bool>> where) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetCount<T>(Read.Session, where) >= 1;
        }

        /// <summary> 是否存在 默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual bool IsExistByEntity<T>(T where) where T : class
        {
            Read.Read = true && UseTransaction;
            throw new NotSupportedException();
        }

        /// <summary> 是否存在 默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="id">条件</param>
        /// <returns></returns>
        public virtual bool IsExist<T>(object id) where T : class
        {
            Read.Read = true && UseTransaction;
            return Read.Session.Get<T>(id) != null;
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper默认查询所有结果集基于内存 条件查询</summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual IQueryable<T> Query<T>(Expression<Func<T, bool>> where = null) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetQuery(Read.Session, where);
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper默认查询所有结果集基于内存 条件查询</summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual List<T> FindListByEntity<T>(T where) where T : class
        {
            Read.Read = true && UseTransaction;
            throw new NotSupportedException();
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq orderby参数无效 dapper默认查询所有结果集基于内存 条件查询 </summary>
        /// <param name="where">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <returns></returns>
        public virtual IQueryable<T> QueryByPage<T>(Expression<Func<T, bool>> where = null, int page = 1, int size = 10) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetByPage(Read.Session, where, page, size);
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq orderby参数无效 dapper默认查询所有结果集基于内存 条件查询 </summary>
        /// <param name="where">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <returns></returns>
        public virtual List<T> FindByListPage<T>(T where, int page = 1, int size = 10) where T : class
        {
            Read.Read = true && UseTransaction;
            return FindListByPage<T>(null, page, size);
        }

        /// <summary>查询数量  默认实现 ef,nhibernate 支持 linq dapper 不支持linq  dapper默认查询所有结果数量  </summary>
        /// <param name="wehre">条件</param>
        /// <returns></returns>
        public virtual long Count<T>(Expression<Func<T, bool>> wehre = null) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetCount<T>(Read.Session, wehre);
        }

        /// <summary>查询数量  默认实现 ef,nhibernate 支持 linq dapper 不支持linq  dapper默认查询所有结果数量  </summary>
        /// <param name="wehre">条件</param>
        /// <returns></returns>
        public virtual long CountByEntity<T>(T wehre) where T : class
        {
            Read.Read = true && UseTransaction;
            return Count<T>((ICriteria)null);
        }

        /// <summary> 添加 </summary>
        /// <param name="entity">实体</param>
        public virtual int Insert<T>(T entity) where T : class
        {
            Write.Write = true && UseTransaction;
            UpdateValue(entity);
            Template.Add<T>(Write.Session, entity);
            return 1;
        }

        /// <summary>批量 添加 </summary>
        /// <param name="entities">实体</param>
        public virtual int BatchInsert<T>(T[] entities) where T : class
        {
            Write.Write = true && UseTransaction;
            foreach (var entity in entities)
            {
                UpdateValue(entity);
            }
            return Template.BatchAdd<T>(Write.Session, entities);
        }

        /// <summary> 更新一个实体的所有属性 </summary>
        /// <param name="entity">实体</param>
        public virtual int Update<T>(T entity) where T : class
        {
            Write.Write = true && UseTransaction;
            return Update(entity, 2);
        }

        /// <summary> 更新一个实体的所有属性 </summary>
        /// <param name="entity">实体</param>
        public virtual int Update<T>(T entity, int flag) where T : class
        {
            Write.Write = true && UseTransaction;
            UpdateValue(entity, flag);
            Template.Modify<T>(Write.Session, entity);
            return 1;
        }

        /// <summary> 删除</summary>
        /// <param name="entity">实体</param>
        public virtual int Delete<T>(T entity) where T : class
        {
            Write.Write = true && UseTransaction;
            if (entity is IHasDeletionTime)
            {
                Update(entity, 3);
            }
            else
            {
                Template.Remove<T>(Write.Session, entity);
            }
            return 1;
        }

        /// <summary> 批量删除 默认实现  nhibernate 支持 EF dapper 未实现</summary>
        public virtual int Delete<T>(object id) where T : class
        {
            Write.Write = true && UseTransaction;
            if (typeof(T).IsAssignableFrom(typeof(IHasDeletionTime)))
            {
                if (string.IsNullOrEmpty(id?.ToString())) throw new ArgumentNullException("id");
                var obj = Write.Session.Get<T>(id);
                if (obj is IHasDeletionTime deletionTime)
                {
                    if (!deletionTime.IsDeleted)
                    {
                        UpdateValue(obj, 3);
                        Write.Session.Update(obj);
                    }
                }
            }
            else
            {
                Template.Remove<T>(Write.Session, id);
            }
            return 1;
        }

        /// <summary> 删除</summary>
        /// <param name="ids">实体 id</param>
        public virtual int DeleteList<Entity, Key>(Key[] ids) where Entity : class
        {
            Write.Write = true && UseTransaction;
            int update = 0;
            if (typeof(Entity).IsAssignableFrom(typeof(IHasDeletionTime)))
            {
                if (ids == null && ids.Length == 0) throw new ArgumentNullException("ids");
                foreach (var id in ids)
                {
                    var obj = Write.Session.Get<Entity>(id);
                    if (obj is IHasDeletionTime deletionTime)
                    {
                        if (!deletionTime.IsDeleted)
                        {
                            UpdateValue(obj, 3);
                            Write.Session.Update(obj);
                            update++;
                        }
                    }
                }
            }
            else
            {
                foreach (var id in ids)
                {
                    var obj = Write.Session.Get<Entity>(id);
                    Template.Remove<Entity>(Write.Session, id);
                    update++;
                }
            }
            return update;
        }
        /// <summary>
        /// 实现按需要只更新部分更新 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper 未实现
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">更新条件</param>
        /// <param name="update">更新后的实体</param>
        public virtual int Update<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> update) where T : class
        {
            Write.Write = true && UseTransaction;
            return Template.Modify<T>(Write.Session, where, update);
        }

        /// <summary> 批量删除 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper 未实现</summary>
        /// <param name="where">条件</param>

        public virtual int Delete<T>(Expression<Func<T, bool>> where = null) where T : class
        {
            Write.Write = true && UseTransaction;
            return Template.Remove<T>(Write.Session, where);
        }


        /// <summary> 执行sql </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual int ExecuteSql(string sql)
        {
            Write.Write = true && UseTransaction;
            return Template.GetExecuteQuery(Write.Session, sql);
        }
        /// <summary> 执行sql </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual Task<int> ExecuteSqlAsync(string sql)
        {
            Write.Write = true && UseTransaction;
            return Template.GetExecuteQueryAsync(Write.Session, sql);
        }
        /// <summary> 操作成功 保存到库里 默认实现 ef 支持  dapper nhibernate 无任何操作 </summary>
        public virtual void Save()
        {
            Write?.Transaction?.Commit();
        }

        /// <summary>
        /// 静态 时候 有效
        /// </summary>
        public void Close()
        {
            if (read == write)
            {
                write?.Dispose();
            }
            else
            {
                write?.Dispose();
                read?.Dispose();
            }
            write = null;
            read = null;
        }

        #region async
        /// <summary>查找单个，且不被上下文所跟踪 ef,nhibernate 支持 linq dapper 不支持linq </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<T> FindSingleAsync<T>(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetAsync(Read.Session, where, cancellationToken);
        }

        /// <summary>查找单个，且不被上下文所跟踪 ef,nhibernate 支持 linq dapper 不支持linq </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<T> FindSingleByEntityAsync<T>(T where, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Task.FromResult(FindSingleByEntity(where));
        }

        /// <summary>查找单个，且不被上下文所跟踪 ef,nhibernate 支持 linq dapper 不支持linq </summary>
        /// <param name="id">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<T> FindSingleAsync<T>(object id, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Task.FromResult(FindSingle<T>(id));
        }

        /// <summary> 是否存在 默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> IsExistAsync<T>(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Task.FromResult(Template.GetCountAsync<T>(Read.Session, where).Result >= 1);
        }

        /// <summary> 是否存在 默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> IsExistByEntityAsync<T>(T where, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Task.FromResult(IsExistByEntity(where));
        }

        /// <summary> 是否存在 默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="id">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> IsExistAsync<T>(object id, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Task.FromResult(IsExist<T>(id));
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper默认查询所有结果集基于内存 条件查询</summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<IQueryable<T>> QueryAsync<T>(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default
            ) where T : class
        {
            Read.Read = true && UseTransaction;
            return Task.FromResult(Query(where));
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper默认查询所有结果集基于内存 条件查询</summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<List<T>> FindListByEntityAsync<T>(T where, CancellationToken cancellationToken = default
            ) where T : class
        {
            Read.Read = true && UseTransaction;
            return Task.FromResult(FindListByEntity(where));
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq orderby参数无效 dapper默认查询所有结果集基于内存 条件查询 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<IQueryable<T>> QueryByPageAsync<T>(Expression<Func<T, bool>> where = null, int page = 1, int size = 10, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Task.FromResult(Template.GetByPage(Read.Session, where, page, size));
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq orderby参数无效 dapper默认查询所有结果集基于内存 条件查询 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<List<T>> FindListByPageOrEntityAsync<T>(T where, int page = 1, int size = 10, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Task.FromResult(FindListByPageOrEntity(where, page, size));
        }

        /// <summary>查询数量  默认实现 ef,nhibernate 支持 linq dapper 不支持linq  dapper默认查询所有结果数量  </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        /// <param name="cancellationToken"></param>
        public virtual Task<long> CountAsync<T>(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetCountAsync<T>(Read.Session, where, cancellationToken);
        }


        /// <summary>查询数量  默认实现 ef,nhibernate 支持 linq dapper 不支持linq  dapper默认查询所有结果数量  </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        /// <param name="cancellationToken"></param>
        public virtual Task<long> CountAsync<T>(T where, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Task.FromResult(CountByEntity(where));
        }

        /// <summary>查询数量  默认实现 ef,nhibernate 支持 linq dapper 不支持linq  dapper默认查询所有结果数量  </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        /// <param name="cancellationToken"></param>
        public virtual Task<long> CountAsync<T>(ICriteria where = null, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetCountAsync<T>(Read.Session, where, cancellationToken);
        }
        /// <summary> 添加 </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> InsertAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
            Template.AddAsync<T>(Write.Session, entity, cancellationToken);
            return Task.FromResult(1);
        }

        /// <summary>批量 添加 </summary>
        /// <param name="entities">实体</param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> BatchInsertAsync<T>(T[] entities, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
            return Template.BatchAddAsync<T>(Write.Session, entities, cancellationToken);
        }

        /// <summary> 更新一个实体的所有属性 </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> UpdateAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
            return UpdateAsync(entity, 2, cancellationToken);
        }
        /// <summary> 更新一个实体的所有属性 </summary>
        /// <param name="entity">实体</param>
        /// <param name="flag"></param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> UpdateAsync<T>(T entity, int flag = 2, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
            UpdateValue(entity, flag);
            return Template.ModifyAsync<T>(Write.Session, entity, cancellationToken);
        }
        /// <summary> 删除</summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> DeleteAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
            if (entity is IHasDeletionTime)
            {
                return UpdateAsync(entity, 3, cancellationToken);
            }
            return Template.RemoveAsync<T>(Write.Session, entity, cancellationToken);
        }

        /// <summary> 批量删除 默认实现  nhibernate 支持 EF dapper 未实现</summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> DeleteAsync<T>(object id, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
            if (typeof(T).IsAssignableFrom(typeof(IHasDeletionTime)))
            {
                if (string.IsNullOrEmpty(id?.ToString())) throw new ArgumentNullException("id");
#if NET40 || NET45 || NET451 || NET452 || NET46
                            var obj = Session.Get<T>(id);
#else
                var obj = Write.Session.GetAsync<T>(id, cancellationToken).Result;
#endif
                if (obj is IHasDeletionTime deletionTime)
                {
                    if (!deletionTime.IsDeleted)
                    {
                        UpdateValue(obj, 3);
#if NET40 || NET45 || NET451 || NET452 || NET46
                                    Session.Update(obj);
#else
                        Write.Session.UpdateAsync(obj).GetAwaiter().GetResult();
                    }
#endif
                }

            }
            else
            {
                Template.RemoveAsync<T>(Write.Session, id, cancellationToken);
            }
            return Task.FromResult(1);
        }
        /// <summary> 查询数据  默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="ids">条件</param>
        /// <param name="cancellationToken">dapper ef 无效</param>
        /// <returns></returns>
        public virtual async Task<int> DeleteListAsync<Entity, Key>(Key[] ids, CancellationToken cancellationToken = default) where Entity : class
        {
            Write.Write = true && UseTransaction;
            int update = 0;
            if (typeof(Entity).IsAssignableFrom(typeof(IHasDeletionTime)))
            {
                if (ids == null && ids.Length == 0) throw new ArgumentNullException("ids");

                foreach (var id in ids)
                {
                    var obj = await Write.Session.GetAsync<Entity>(id, cancellationToken);
                    if (obj is IHasDeletionTime deletionTime)
                    {
                        if (!deletionTime.IsDeleted)
                        {
                            obj.UpdateValue(3);
                            UpdateValue(obj, 3);
                            await NhibernateTemplate.Empty.ModifyAsync(Write.Session, obj, cancellationToken);
                            update++;
                        }
                    }
                }
            }
            else
            {
                foreach (var id in ids)
                {
                    var obj = await Write.Session.GetAsync<Entity>(id, cancellationToken);
                    await Template.RemoveAsync<Entity>(Write.Session, id, cancellationToken);
                    update++;
                }
            }
            return update;
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
            Write.Write = true && UseTransaction;
            return Template.ModifyAsync<T>(Write.Session, where, update, cancellationToken);
        }

        /// <summary> 批量删除 默认实现 ef,nhibernate 支持 linq dapper 不支持linq dapper 未实现</summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> DeleteAsync<T>(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
            return Template.RemoveAsync<T>(Write.Session, where, cancellationToken);
        }


        /// <summary> 执行sql </summary>
        /// <param name="sql"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> ExecuteSqlAsync(string sql, CancellationToken cancellationToken = default)
        {
            Write.Write = true && UseTransaction;
            return Template.GetExecuteQueryAsync(Write.Session, sql, cancellationToken);
        }


        /// <summary> 操作成功 保存到库里 默认实现 ef 支持  dapper nhibernate 无任何操作 </summary>
        /// <param name="cancellationToken"></param>
        public virtual Task SaveAsync(CancellationToken cancellationToken = default)
        {
            return Write?.Transaction?.CommitAsync();
        }
        #endregion async
        /// <summary>
        /// 
        /// </summary>
        void IDisposable.Dispose()
        {
            //SessionFactory.Dispose();
            if (this.Single)
            {
                Session?.Dispose();
                Session.Transaction?.Dispose();
            }
            else
            {
                write?.Dispose();
                write?.Transaction?.Dispose();
                read?.Dispose();
                read?.Transaction?.Dispose();
            }
        }


    }
}
#endif