//#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
#if  NET40 ||NET45 || NET451 || NET452 || NET46 ||NET461 || NET462|| NET47 || NET471 || NET472|| NET48 ||  NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1


#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System.Data.Entity;
#else
using Microsoft.EntityFrameworkCore;
#endif
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Utility.Attributes;
using Utility.Domain.Entities;
using Utility.Domain.Repositories;
using Utility.Ef.Uow;

namespace Utility.Ef.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Context"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Key"></typeparam>
    [Transtation]
    public class BaseEfRepository<Context,T, Key> : BaseEfRepository<T,Key> , IRepository<T, Key>, IRepository<T>
        where Context:DbContext
        where T : class, IEntity<Key>
    {
        /// <summary> 构造函数注入</summary>
        /// <param name="context">数据库上下文</param>
        public BaseEfRepository(DbContextProvider<Context> context) : base(context)
        {
        }

        /// <summary>
        ///  读写分离
        /// </summary>
        /// <param name="writeDbContext"> 写库</param>
        /// <param name="readDbContext">读库</param>
        public BaseEfRepository(Lazy<DbContextProvider<Context>> writeDbContext, Lazy<DbContextProvider<Context>> readDbContext) : base(new Lazy<DbContextProvider>(()=>writeDbContext.Value), new Lazy<DbContextProvider>(() => readDbContext.Value))
        {
          
        }

        public  virtual Context Read { get { return (Context)base.UnitWork.Read.DbContext; } }

        public virtual Context Write { get { return (Context)base.UnitWork.Write.DbContext; } }


    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Key"></typeparam>
    [Transtation]
    public class BaseEfRepository<T,Key>: BaseEfRepository<T>, IRepository<T, Key>, IRepository<T> where T : class, IEntity<Key>
    {
        /// <summary> 构造函数注入</summary>
        /// <param name="context">数据库上下文</param>
        public BaseEfRepository(DbContextProvider context):base(context)
        {
        }
        /// <summary>
        ///  读写分离
        /// </summary>
        /// <param name="writeDbContext"> 写库</param>
        /// <param name="readDbContext">读库</param>
        public BaseEfRepository(Lazy<DbContextProvider> writeDbContext, Lazy<DbContextProvider> readDbContext):base(writeDbContext,readDbContext)
        {
          
        }

        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        public virtual int Delete(Key id)
        {
            return base.UnitWork.Delete<T, Key>(id);
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        public virtual int DeleteList(Key[] ids)
        {
            return base.UnitWork.DeleteList<T, Key>(ids);
        }

        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        public virtual Task<int> DeleteAsync(Key id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Delete(id));
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        public virtual Task<int> DeleteListAsync(Key[] ids, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(DeleteList(ids));
        }
    }

    /// <summary> ef </summary>
    /// <typeparam name="T"></typeparam>
    [Transtation]
    public  class BaseEfRepository<T> : BaseRepository<T>,IDisposable, IRepository<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        public new EfUnitWork UnitWork { get;protected set; }

        /// <summary> 构造函数注入</summary>
        /// <param name="context">数据库上下文</param>
        public BaseEfRepository(DbContextProvider context)
        {
            UnitWork = new EfUnitWork(context);
            UnitWork.UseTransaction = false;
            base.UnitWork = this.UnitWork;
        }

        /// <summary>
        ///  读写分离
        /// </summary>
        /// <param name="writeDbContext"> 写库</param>
        /// <param name="readDbContext">读库</param>
        public BaseEfRepository(Lazy<DbContextProvider> writeDbContext, Lazy<DbContextProvider> readDbContext)
        {
            UnitWork = new EfUnitWork(writeDbContext, readDbContext);
            base.UnitWork = this.UnitWork;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual Expression<Func<T,bool>> GetWhere(T entity)
        {
            return null;
        }

    }
}
#endif