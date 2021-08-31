#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1

#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
#endif
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System.Data.Entity;
#endif
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utility.Domain.Entities;

namespace Utility.Ef
{

    /// <summary>
    /// 数据库 上下文
    /// </summary>
    public class BaseDbContext<Context> :DbContext
        where Context : DbContext
    {
        public EfTransactionAdapter TransactionAdapter { get; protected set; }

#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
        public BaseDbContext(string nameOrConnectionString) : base(nameOrConnectionString) {
            this.TransactionAdapter = new EfTransactionAdapter(this);
        }
#else
        public BaseDbContext(DbContextOptions<Context> options) : base(options) {
            this.TransactionAdapter = new EfTransactionAdapter(this);
        }
#endif
     

        /// <summary>
        /// 处理 事件完成 再数据库持久化
        /// </summary>
        /// <param name="action"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<bool> SaveEntitiesAsync(Action action=null,CancellationToken cancellationToken = default(CancellationToken))
        {
            action?.Invoke();
            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync();

            return true;
        }


        /// <summary>
        /// 处理 事件完成 再数据库持久化
        /// </summary>
        /// <param name="action"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual bool SaveEntities(Action action = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            action?.Invoke();
            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result =  base.SaveChanges();
            return true;
        }
    }

    public class EfTransactionAdapter 
    {
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
        private DbContextTransaction currentTransaction;
#else
        private IDbContextTransaction currentTransaction;
#endif
        public DbContext Context { get; set; }

        public EfTransactionAdapter(DbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// 事务
        /// </summary>
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
        public DbContextTransaction GetCurrentTransaction { get => currentTransaction;internal set => currentTransaction = value; }
#else
        public IDbContextTransaction GetCurrentTransaction { get => currentTransaction;internal set => currentTransaction = value; }
#endif

        /// <summary>
        /// 是否存在事务
        /// </summary>
        public bool HasActiveTransaction => currentTransaction != null;




        /// <summary>
        /// 处理 事件完成 再数据库持久化
        /// </summary>
        /// <param name="action"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<bool> SaveEntitiesAsync(Action action = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            action?.Invoke();
            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await Context.SaveChangesAsync();

            return true;
        }
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        public virtual
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
            DbContextTransaction
#else
           IDbContextTransaction
#endif
            BeginTransaction()
        {
            if (currentTransaction != null) return null;
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
            currentTransaction = Context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            return currentTransaction;
#else
            currentTransaction =  Context.Database.BeginTransaction(IsolationLevel.ReadCommitted);

            return currentTransaction;
#endif
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        public virtual
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
            Task<DbContextTransaction>
#else
            async Task<IDbContextTransaction>
#endif
            BeginTransactionAsync()
        {
            if (currentTransaction != null) return null;
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
            currentTransaction = Context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            return Task.FromResult( currentTransaction);
#else
            currentTransaction = await Context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return currentTransaction;
#endif
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public virtual
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
          async   Task CommitTransactionAsync(DbContextTransaction transaction)
#else
            async Task
            CommitTransactionAsync(IDbContextTransaction transaction)
#endif
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
            if (transaction != currentTransaction) throw new InvalidOperationException($"Transaction {transaction} is not current");
#else
            if (transaction != currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
#endif
            try
            {
                await Context.SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                    currentTransaction = null;
                }
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>

        public virtual void RollbackTransaction()
        {
            try
            {
                currentTransaction?.Rollback();
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                    currentTransaction = null;
                }
            }
        }
    }
}
#endif