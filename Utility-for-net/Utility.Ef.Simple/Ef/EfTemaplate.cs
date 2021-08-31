//#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1

#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System.Data.Entity;
#else
using Microsoft.EntityFrameworkCore;
#endif
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;
using System.Collections.Generic;
using Utility.Application.Services.Dtos;
using Utility.Extensions;
using Utility.Helpers;
using Utility.Threads;
using Utility.Domain.Entities;
using Utility.Domain.Extensions;

namespace Utility.Ef
{

    /// <summary>
    /// 
    /// </summary>
    public class EfTemaplate:IDisposable
    {

        private IDbConnection _connection;//数据库连接对象
        private DbContextProvider write;
        private DbContextProvider read;
        private DbContextProvider dbContext;// 数据库上下文
        public bool UseTransaction { get;  set; } = true;
        /// <summary>
        /// 数据库上下文 写库
        /// </summary>
        public readonly Lazy<DbContextProvider> WriteDbContext;

        /// <summary>
        /// 数据库上下文 读库
        /// </summary>
        public readonly Lazy<DbContextProvider> ReadDbContext;

        /// <summary>
        /// 数据库上下文 写库
        /// </summary>
        public DbContextProvider Write
        {
            get
            {
                write = write ?? WriteDbContext.Value;
                return write;
            }
            protected set => write = value;
        }

        /// <summary>
        /// 数据库上下文 读库
        /// </summary>
        public DbContextProvider Read
        {
            get
            {
                read = read ?? ReadDbContext.Value;
                return read;
            }
            protected set => read = value;
        }

        /// <summary>
        /// 是否启用 
        /// </summary>
        internal bool Enable{ get; set; } = true;

        /// <summary>
        /// 单库
        /// </summary>
        protected bool Single { get; set; } = true;

        /// <summary> 构造函数注入</summary>
        /// <param name="context">数据库上下文</param>
        public EfTemaplate(DbContextProvider context)
        {
            this.DbContext = context;
            this.Write = context;
            this.Read = context;
        }
        /// <summary>
        ///  读写分离
        /// </summary>
        /// <param name="writeDbContext"> 写库</param>
        /// <param name="readDbContext">读库</param>
        public EfTemaplate(Lazy<DbContextProvider> writeDbContext, Lazy<DbContextProvider> readDbContext)
        {
            this.WriteDbContext = writeDbContext;
            this.ReadDbContext = readDbContext;
            //this.DbContext = writeDbContext;
            this.Single = false;
        }


        /// <summary>数据库连接对象 </summary>
        public virtual IDbConnection Connection
        {
            get
            {
                _connection = _connection ?? GetDbConnection(DbContext ?? write ?? read ?? WriteDbContext.Value);
                return _connection;
            }
        }

        /// <summary>
        /// 数据库上下文
        /// </summary>
        public DbContextProvider DbContext {
            get {
                dbContext = dbContext ?? write ?? read ?? WriteDbContext.Value;
                return dbContext;
            } 
            set => dbContext = value; }

        IDbConnection GetDbConnection(DbContextProvider context)
        {
#if NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
            return  context.DbContext.Database.Connection;
#else
            return context.DbContext.Database.GetDbConnection();
#endif
        }
        /// <summary>
        /// 手动赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="flag">1 add 2 update 3 delete</param>
        protected virtual bool UpdateValue<T>(T entity, int flag = 1) where T : class
        {
            return Enable?entity.UpdateValue(flag):false;
        }

        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        public virtual int Delete<T, Key>(Key id) where T : class
        {
            Write.Write = true&&UseTransaction;
            //语法不支持
            //UnitWork.Delete<Entity>(it => it.Id == id);
            Expression<Func<T, bool>> where = LinqExpression.IdEqual<T, Key>(id);
            if (Enable&&typeof(T).IsAssignableFrom(typeof(IHasDeletionTime)))
            {
                where = where.Filter();
                T obj = where==null? Write.DbContext.Set<T>().AsNoTracking().FirstOrDefault() : Write.DbContext.Set<T>().AsNoTracking().FirstOrDefault(where);
                if (obj != null)
                {
                    if (obj is IHasDeletionTime deletionTime)
                    {
                        deletionTime.DeletionTime = CommonHelper.TotalMilliseconds();
                        deletionTime.IsDeleted = true;
                    }
                    Update(obj,2);
                }
                return 1;
            }
            else
            {
               
                this.Delete<T>(where);
                return 1;
            }
           
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        public virtual int DeleteList<T, Key>(Key[] ids) where T : class
        {
            Write.Write = true && UseTransaction;
            //语法不支持
            //Expression<Func<T, bool>> where = null;
            //foreach (var id in ids)
            //{
            //    where = where.Or(it => it.Id == id);
            //}
            //Repository.Delete(where);
            //return 1;
            Expression<Func<T, bool>> where = null;
			foreach (var id in ids)
			{
				where = where.Or(id.IdEqual<T, Key>());
			}
            if (Enable&&typeof(T).IsAssignableFrom(typeof(IHasDeletionTime)))
            {
                where = Enable ? where.Filter() : where;
                var dbSet = Write.DbContext.Set<T>().AsNoTracking().AsQueryable();
                if (where != null)
                    dbSet = dbSet.Where(where);
                List<T> objs = dbSet.ToList();
                foreach (var item in objs)
                {
                    if (item is IHasDeletionTime deletionTime &&!deletionTime.IsDeleted)
                    {
                        deletionTime.DeletionTime = CommonHelper.TotalMilliseconds();
                        deletionTime.IsDeleted = true;
                    }
                    Update(item,2);
                }
                return 1;
            }
            else
            {
                this.Delete(where);
                return 1;
            }
          
        }
        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        public virtual Task<int> DeleteAsync<T, Key>(Key id, CancellationToken cancellationToken = default)
            where T:class
        {
            return Task.FromResult(Delete<T, Key>(id));
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        public virtual Task<int> DeleteListAsync<T, Key>(Key[] ids, CancellationToken cancellationToken = default)
            where T:class
        {
            return Task.FromResult(DeleteList<T, Key>(ids));
        }



        /// <summary>查找单个，且不被上下文所跟踪 </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual T FindSingle<T>(Expression<Func<T, bool>> where=null) where T : class
        {
            Read.Read = true&&UseTransaction;
            where =Enable? where.Filter():where;
            return where == null ? Read.DbContext.Set<T>().AsNoTracking().FirstOrDefault() : Read.DbContext.Set<T>().AsNoTracking().FirstOrDefault(where);
        }

        /// <summary>查找单个，且不被上下文所跟踪 </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual T FindSingleByEntity<T>(T where) where T : class
        {
            throw new NotSupportedException();
        }

        /// <summary>查找单个，且不被上下文所跟踪 </summary>
        /// <param name="id">条件</param>
        /// <returns></returns>
        public virtual T FindSingle<T>(object id) where T : class
        {
            Read.Read = true && UseTransaction;
            return Read.DbContext.Set<T>().Find(new object[] { id });
        }

        /// <summary> 是否存在 </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual bool IsExist<T>(Expression<Func<T, bool>> where = null) where T : class
        {
            Read.Read = true && UseTransaction;
            where =Enable? where.Filter():where;
            return where == null ? Read.DbContext.Set<T>().Any() : Read.DbContext.Set<T>().Any(where);
        }

        /// <summary> 是否存在 </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual bool IsExistByEntity<T>(T where) where T : class
        {
            throw new NotSupportedException();
        }

        /// <summary> 是否存在 </summary>
        /// <param name="id">条件</param>
        /// <returns></returns>
        public virtual bool IsExist<T>(object id) where T : class
        {
            Read.Read = true && UseTransaction;
            return Read.DbContext.Set<T>().Find(new object[] { id })!=null;
        }

        /// <summary>根据过滤条件，获取记录 </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual IQueryable<T> Query<T>(Expression<Func<T, bool>> where = null) where T : class
        {
            Read.Read = true && UseTransaction;
            where =Enable? where.Filter():where;
            return Filter(where);
        }

        /// <summary>根据过滤条件，获取记录 </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual List<T> FindList<T>(Expression<Func<T, bool>> where = null) where T : class
        {
            Read.Read = true && UseTransaction;
            where = where.Filter();
            return Filter(where).ToList();
        }

        /// <summary>根据过滤条件，获取记录 </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual List<T> FindListByEntity<T>(T where ) where T : class
        {
            Read.Read = true && UseTransaction;
            throw new NotSupportedException();
        }

        /// <summary> 分页记录 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual IQueryable<T> QueryByPage<T>(Expression<Func<T, bool>> where = null,int page=1, int size=10) where T : class
        {
            Read.Read = true && UseTransaction;
            PageHelper.Set(ref page, ref size);
            where =Enable? where.Filter():where;
            return Filter(where).Skip(size * (page - 1)).Take(size);
        }

        /// <summary> 分页记录 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual List<T> FindListByPage<T>(Expression<Func<T, bool>> where = null, int page = 1, int size = 10) where T : class
        {
            Read.Read = true && UseTransaction;
            PageHelper.Set(ref page, ref size);
            where =Enable? where.Filter():where;
            return Filter(where).Skip(size * (page - 1)).Take(size).ToList();
        }

        /// <summary> 分页记录 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual List<T> FindListByPageOrEntity<T>(T where , int page = 1, int size = 10) where T : class
        {
            return FindListByPage((Expression<Func<T, bool>>)null,page,size);
        }

        /// <summary> 分页记录 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual Tuple<List<T>, long> FindTupleByPage<T>(Expression<Func<T, bool>> where = null, int page = 1, int size = 10) where T : class
        {
            Read.Read = true && UseTransaction;
            PageHelper.Set(ref page, ref size);
			where =Enable? where.Filter():where;
            var datas = QueryByPage(where,page, size ).ToList();
            var count = Count(where);
            var result = new Tuple<List<T>, long>(datas, count);
            return result;
        }

        /// <summary> 分页记录 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <returns></returns>

        public virtual ResultDto<T> FindResultByPage<T>(Expression<Func<T, bool>> where = null, int page = 1, int size = 10) where T : class
        {
            Read.Read = true && UseTransaction;
            where =Enable? where.Filter():where;
            PageHelper.Set(ref page, ref size);
            var tuple = FindTupleByPage(where, page, size);
            var result = new ResultDto<T>(tuple, page, size);
            return result;
        }

        /// <summary> 分页记录 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResultDto<T>> FindResultByPageAsync<T>(Expression<Func<T, bool>> where = null, int page = 1, int size = 10,
         CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            where = Enable ? where.Filter() : where;
            PageHelper.Set(ref page, ref size);
            var tuple = FindTupleByPageAsync(where, page, size, cancellationToken).Result;
            var result = new ResultDto<T>(tuple, page, size);
            return Task.FromResult(result);
        }

        /// <summary> 分页记录 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<Tuple<List<T>, long>> FindTupleByPageAsync<T>(Expression<Func<T, bool>> where = null, int page = 1, int size = 10,
            CancellationToken cancellationToken = default)where T:class
        {
            Read.Read = true && UseTransaction;
            where = Enable ? where.Filter() : where;
            PageHelper.Set(ref page, ref size);
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
            var datas = QueryByPage(where, page, size).ToListAsync(cancellationToken).Result;
            var count = CountAsync(where, cancellationToken).Result;
#else
            var datas = QueryByPage(where, page,size ).ToList();
            var count = Count(where);
#endif

            var result = new Tuple<List<T>, long>(datas, count);
            return Task.FromResult(result);
        }

        /// <summary> 根据过滤条件获取记录数 </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual long Count<T>(Expression<Func<T, bool>> where = null) where T : class
        {
            Read.Read = true && UseTransaction;
            where =Enable? where.Filter():where;
            return Filter(where).Count();
        }

        /// <summary> 根据过滤条件获取记录数 </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual long CountByEntity<T>(T where) where T : class
        {
            return Count((Expression<Func<T, bool>>)null);
        }

        /// <summary> 添加 </summary>
        /// <param name="entity">实体</param>
        public virtual int Insert<T>(T entity) where T : class
        {
            Write.Write = true && UseTransaction;
            UpdateValue(entity);
            Write.DbContext.Set<T>().Add(entity);
            //Save();
            //Write.DbContext.Entry(entity).State = EntityState.Detached;
            return 1;
        }

        /// <summary> 批量添加</summary>
        /// <param name="entities">The entities.</param>
        public virtual int BatchInsert<T>(T[] entities) where T : class
        {
            Write.Write = true && UseTransaction;
            foreach (var item in entities)
            {
                UpdateValue(item);
            }
            Write.DbContext.Set<T>().AddRange(entities);
            //foreach (var item in entities)
            //{
            //    Microsoft.EntityFrameworkCore.DbContext context = (Microsoft.EntityFrameworkCore.DbContext)Activator.CreateInstance(DbContext.GetType())
            //    context.Add(item);
            //    context.SaveChanges();
            //    context = null;
            //}
            //Save();
            return 1;
        }

        /// <summary> 更新一个实体的所有属性 </summary>
        /// <param name="entity">实体</param>
        public virtual int Update<T>(T entity) where T : class
        {
            if(Update(entity,2))
			{
                return 1;
			}
            return 0;
        }

        /// <summary> 更新一个实体的所有属性 </summary>
        /// <param name="entity">实体</param>
        /// <param name="flag"></param>
        public virtual bool Update<T>(T entity,int flag=2) where T : class
        {
            Write.Write = true && UseTransaction;
            if (Enable)
			{
				UpdateValue(entity, flag);
			}
            var entry = this.Write.DbContext.Entry(entity);
            //entry.CurrentValues.SetValues(entity);
            //更新时可能出现异常 bug 
            entry.State = EntityState.Modified;
            //如果数据没有发生变化
            if (!this.Write.DbContext.ChangeTracker.HasChanges())
            {
                return false;
            }
            //此处则没更新
            // entry.State = EntityState.Modified;
			return true;
        }

        /// <summary> 删除 </summary>
        /// <param name="entity">实体</param>
        public virtual int Delete<T>(T entity) where T : class
        {
            Write.Write = true && UseTransaction;
            if (Enable&&entity is IHasDeletionTime)
            {
                if(Update(entity,3))
				{
                    return 1;
				}
            }
            else
            {
                Write.DbContext.Set<T>().Remove(entity);
            }
            return 1;
        }

        /// <summary> 批量删除 默认实现  nhibernate 支持 EF dapper 未实现</summary>
       public virtual int Delete<T>(object id) where T : class
        {
            Write.Write = true && UseTransaction;
            T obj = Write.DbContext.Set<T>().Find(new object[] { id });
            if(Enable&&obj is  IHasDeletionTime deletionTime)
            {
                if (!deletionTime.IsDeleted)
                {
                    UpdateValue(obj, 3);
                    var entry = this.Write.DbContext.Entry(obj);
                    //entry.CurrentValues.SetValues(entity);
                    //更新时可能出现异常 bug 
                    entry.State = EntityState.Modified;
                    //如果数据没有发生变化
                    if (!this.Write.DbContext.ChangeTracker.HasChanges())
                    {
                        return 0;
                    }
                    //此处则没更新
                    // entry.State = EntityState.Modified;
                }
            }
            else
            {
                Write.DbContext.Set<T>().Remove(obj);
            }
            return 1;

        }
        /// <summary>
        /// 实现按需要只更新部分更新
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="update">The entity.</param>
        public virtual int Update<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> update) where T : class
        {
            Write.Write = true && UseTransaction;
            where =Enable? where.Filter():where;
           return Write.DbContext.Set<T>().Where(where).Update(update);
        }

        /// <summary> 批量删除 </summary>
        /// <param name="where">条件</param>
        public virtual int Delete<T>(Expression<Func<T, bool>> where=null) where T : class
        {
            Write.Write = true && UseTransaction;
            where =Enable? where.Filter():where;
           return Write.DbContext.Set<T>().Where(where).Delete();
        }

        /// <summary> 操作成功 保存到库里 </summary>
        public virtual void Save()
        {
            try
            {
                Write.DbContext.SaveChanges();
            }
            catch //(DbEntityValidationException e)
            {
                throw; //new Exception(e.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage);
            }
        }

        /// <summary> 条件查询 </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        protected virtual IQueryable<T> Filter<T>(Expression<Func<T, bool>> where) where T : class
        {
			where =Enable? where.Filter():where;
            var dbSet = Read.DbContext.Set<T>().AsNoTracking().AsQueryable();
            if (where != null)
                dbSet = dbSet.Where(where);
            return dbSet;
        }

        /// <summary>执行sql </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual int ExecuteSql(string sql)
        {
            Write.Write = true && UseTransaction;
            //return  context.Database.ExecuteSqlRaw(sql);
#pragma warning disable CS0618 // 类型或成员已过时
#if NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_1
            return Write.DbContext.Database.ExecuteSqlRaw(sql);
#else
            return Write.DbContext.Database.ExecuteSqlCommand(sql);
#endif
#pragma warning restore CS0618 // 类型或成员已过时
        }

        #region async

        /// <summary>查找单个，且不被上下文所跟踪 </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<T> FindSingleAsync<T>(Expression<Func<T, bool>> where=null, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
#if !NET40
            where =Enable? where.Filter():where;
            return where == null ? Read.DbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync() : Read.DbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(where);
#else
            return  Task.FromResult(FindSingle(where));
#endif
        }

        /// <summary>查找单个，且不被上下文所跟踪 </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<T> FindSingleByEntityAsync<T>(T where, CancellationToken cancellationToken = default) where T : class
        {
            return  Task.FromResult( FindSingleByEntity(where));
        }

        /// <summary>查找单个，且不被上下文所跟踪 </summary>
        /// <param name="id">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<T> FindSingleAsync<T>(object id, CancellationToken cancellationToken = default) where T : class
        {
            return  Task.FromResult(FindSingle<T>(id));
        }

        /// <summary> 是否存在 </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> IsExistAsync<T>(Expression<Func<T, bool>> where=null, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
#if !NET40
            where =Enable? where.Filter():where;
            return where == null ? Read.DbContext.Set<T>().AnyAsync() : Read.DbContext.Set<T>().AnyAsync(where);
#else
            return Task.FromResult(IsExist(where));
#endif
        }

        /// <summary> 是否存在 </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> IsExistByEntityAsync<T>(T where , CancellationToken cancellationToken = default) where T : class
        {
            return Task.FromResult( IsExistByEntity<T>(where));
        }

        /// <summary> 是否存在 </summary>
        /// <param name="id">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> IsExistAsync<T>(object id, CancellationToken cancellationToken = default) where T : class
        {
            return Task.FromResult(IsExist<T>(id));
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq</summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<IQueryable<T>> QueryAsync<T>(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default) where T : class
        {
            return Task.FromResult(Query(where));
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq</summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        public virtual Task<List<T>> FindListAsync<T>(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
#if !(NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48)
            return Query(where).ToListAsync(cancellationToken);
#else
            return Task.FromResult( FindList(where));
#endif
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq</summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        public virtual Task<List<T>> FindListByEntityAsync<T>(T where , CancellationToken cancellationToken = default) where T : class
        {
            return  Task.FromResult(FindListByEntity(where));
        }



        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq orderby参数无效 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<IQueryable<T>> QueryByPageAsync<T>(Expression<Func<T, bool>> where = null, int page = 1, int size = 10, 
             CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            PageHelper.Set(ref page, ref size);
            return Task.FromResult(QueryByPage(where,page, size));
        }


        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq orderby参数无效 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<List<T>> FindListByPageAsync<T>(Expression<Func<T, bool>> where = null, int page = 1, int size = 10, 
           CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            PageHelper.Set(ref page, ref size);
#if !(NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48)
            return QueryByPage(where,page, size).ToListAsync(cancellationToken);
#else
              return   Task.FromResult(FindListByPage(where,page, size));
#endif
        }

        /// <summary> 查询数据 默认实现 ef,nhibernate 支持 linq dapper 不支持linq orderby参数无效 </summary>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<List<T>> FindListByPageOrEntityAsync<T>(T where , int page = 1, int size = 10,
           CancellationToken cancellationToken = default) where T : class
        {
            return Task.FromResult(FindListByPageOrEntity(where,page,size));
        }


        /// <summary> 根据过滤条件获取记录数 </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<long> CountAsync<T>(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
#if !NET40
            where =Enable? where.Filter():where;
            return Task.FromResult((long)Filter(where).CountAsync().Result);
#else
            return Task.FromResult((long)Filter(where).Count());
#endif
        }

        /// <summary> 根据过滤条件获取记录数 </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<long> CountAsync<T>(T where, CancellationToken cancellationToken = default) where T : class
        {
            return Task.FromResult(CountByEntity(where));
        }

        /// <summary> 添加 </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="entity">实体</param>
        public virtual 
#if !NET40
            async
#endif
            Task<int> InsertAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
#if !(NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48)
            UpdateValue(entity);
            await Write.DbContext.Set<T>().AddAsync(entity, cancellationToken);
            Write.DbContext.Entry(entity).State = EntityState.Detached;
            return 1;
#else
#if !(NET40)
            return await  Task.FromResult(Insert(entity));
#else
            return  Task.FromResult(Insert(entity));
#endif
#endif
        }


        /// <summary> 批量添加</summary>
        /// <param name="cancellationToken"></param>
        /// <param name="entities">The entities.</param>
        public virtual 
#if !NET40
            async
#endif
            Task<int> BatchInsertAsync<T>(T[] entities, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
#if !(NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48)
            if (Enable)
			{
			    foreach (var entity in entities)
				{
					UpdateValue(entity);
				}
			}
            await Write.DbContext.Set<T>().AddRangeAsync(entities);
            return 1;
#else
             Write.DbContext.Set<T>().AddRange(entities);
             return 1;
#endif
        }

        /// <summary> 更新一个实体的所有属性 </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="entity">实体</param>
        public virtual Task<int> UpdateAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            return Task.FromResult(Update(entity));
        }

        /// <summary> 删除 </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="entity">实体</param>
        public virtual Task<int> DeleteAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            return Task.FromResult(Delete(entity));
        }

       
        /// <summary> 批量删除 默认实现  nhibernate 支持 EF dapper 未实现</summary>
        public virtual Task<int> DeleteAsync<T>(object id, CancellationToken cancellationToken = default) where T : class
        {
            return Task.FromResult(Delete<T>(id));
        }



        /// <summary>
        /// 实现按需要只更新部分更新
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="update">The entity.</param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> UpdateAsync<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> update, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
#if !(NET40)
            where =Enable? where.Filter():where;
            return Write.DbContext.Set<T>().Where(where).UpdateAsync(update);
#else
            return Task.FromResult(Update(where,update));
#endif

        }

        /// <summary> 批量删除 </summary>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> DeleteAsync<T>(Expression<Func<T, bool>> where=null, CancellationToken cancellationToken = default) where T:class
        {
            Write.Write = true && UseTransaction;
#if !(NET40)
            where =Enable? where.Filter():where;
            return Write.DbContext.Set<T>().Where(where).DeleteAsync(cancellationToken);
#else
            return Task.FromResult( Delete(where));
#endif
        }


        /// <summary>执行sql </summary>
        /// <param name="sql"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> ExecuteSqlAsync(string sql, CancellationToken cancellationToken = default)
        {
            Write.Write = true && UseTransaction;
#if !(NET40)
            //return  _context.Database.ExecuteSqlRaw(sql);
#pragma warning disable CS0618 // 类型或成员已过时
#if NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_1
            return Write.DbContext.Database.ExecuteSqlRawAsync(sql, cancellationToken);
#else
            return Write.DbContext.Database.ExecuteSqlCommandAsync(sql, cancellationToken);
#endif
#pragma warning restore CS0618 // 类型或成员已过时
#else
            return Task.FromResult(ExecuteSql(sql));
#endif
        }

        /// <summary> 操作成功 保存到库里 </summary>
        /// <param name="cancellationToken"></param>
        public virtual Task SaveAsync(CancellationToken cancellationToken = default)
        {
            //return TaskHelper.CompletedTask;
#if !(NET40)
            try
            {
                return Write.DbContext.SaveChangesAsync();
            }
            catch //(DbEntityValidationException e)
            {
                throw;// new Exception(e.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage);
            }
#else
            Save();
            return TaskHelper.CompletedTask;
#endif
        }

        #endregion async

        void IDisposable.Dispose()
        {
            if (Single)
            {
                DbContext.Dispose();
            }
            else
            {
                write?.Dispose();
                read?.Dispose();
            }
        }
    }
}
#endif