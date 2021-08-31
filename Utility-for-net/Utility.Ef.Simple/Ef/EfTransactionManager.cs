//#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1

#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System.Data.Entity;
#else
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
#endif
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utility.Ef
{
    public class EfTransactionManager:TransactionManager
    {
        private bool used;

        /// <summary>
        /// 使用时 标识 true  nhibernate调用 时 给标识
        /// </summary>
        internal virtual new bool Used
        {
            get => used; set
            {
                if (value&&Write && TransactionAdapter.GetCurrentTransaction == null)
                {
                    Id = Guid.NewGuid().ToString();
                    TransactionAdapter.GetCurrentTransaction = TransactionAdapter.BeginTransaction();
                    used = true;
                    Console.WriteLine(Id+ " Transaction Begin");
                }
                used = value; base.Used = value;
            }
        }
        internal virtual bool Read { get; set; }
        internal virtual bool Write { get; set; }
        public EfTransactionAdapter TransactionAdapter { get; protected set; }
        public DbContext Context { get; internal set; }
        public DbTransaction DbTransaction { get; protected set; }
        public string CurrentId { get;protected set; }



        public EfTransactionManager(DbContext context)
        {
            Context = context;
            TransactionAdapter = new EfTransactionAdapter(context);
        }

        public override bool HasTransaction()
        {
            return TransactionAdapter.HasActiveTransaction;
        }
        public virtual void UseTransaction(DbTransaction transaction)
        {
           
            if (!TransactionAdapter.HasActiveTransaction)
            {
                return;
            }
            if (DbTransaction == null)
            {
                return;
            }
            Id = Guid.NewGuid().ToString();
            DbTransaction = transaction;
            Context.Database.UseTransaction(transaction);
            Console.WriteLine(Id + " Transaction Begin");
        }
        public override void Begin()
        {
            if (DbTransaction != null)
            {
                return;
            }
            if (!TransactionAdapter.HasActiveTransaction)
            {
                return;
            }

            //第一次创建 事务 有效 第二次无效 1个 DbContext 只能 存在 1个 Transaction
            //The connection is already in a transaction and cannot participate in another transaction.
            if (!TransactionAdapter.HasActiveTransaction&&Write)
            {
                CurrentId = Guid.NewGuid().ToString("N");
                TransactionAdapter.BeginTransaction();
                Console.WriteLine(Id + " Transaction Begin");
            }
        }
        public override void Commit()
        {
            Commit(TransactionAdapter.GetCurrentTransaction);
        }
        public virtual void Commit(
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
            DbContextTransaction
#else
        IDbContextTransaction
#endif
           transaction)
        {
            if (transaction != TransactionAdapter.GetCurrentTransaction)
            {
                return;
            }
            if (DbTransaction != null && !IsCompleted)
            {
                Context.SaveChanges();
                DbTransaction.Commit();
                DbTransaction.Dispose();
                IsCompleted = true;
                Console.WriteLine(Id + " Transaction Commit");
                return;
            }
            if (TransactionAdapter.GetCurrentTransaction != null && !IsCompleted)
            {
                Context.SaveChanges();
                TransactionAdapter.GetCurrentTransaction.Commit();
                TransactionAdapter.GetCurrentTransaction.Dispose();
                TransactionAdapter.GetCurrentTransaction = null;
                IsCompleted = true;
                Console.WriteLine(Id + " Transaction Commit");
            }
        }
        public override void RollBack()
        {
            RollBack(TransactionAdapter.GetCurrentTransaction);
        }
        public virtual void RollBack(
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
            DbContextTransaction
#else
        IDbContextTransaction
#endif
           transaction)
        {
            if (DbTransaction != null && !IsCompleted)
            {
                DbTransaction.Rollback();
                DbTransaction.Dispose();
                DbTransaction = null;
                IsCompleted = true;
                Console.WriteLine(Id + " Transaction RollBack");
                return;
            }
            if (transaction != TransactionAdapter.GetCurrentTransaction)
            {
                return;
            }
            if (TransactionAdapter.GetCurrentTransaction != null && !IsCompleted)
            {
                TransactionAdapter.GetCurrentTransaction.Rollback();
                TransactionAdapter.GetCurrentTransaction.Dispose();
                TransactionAdapter.GetCurrentTransaction = null;
                Console.WriteLine(Id + " Transaction RollBack");
                IsCompleted = true;
            }
        }
        public override async Task BeginAsync(CancellationToken cancellationToken = default)
        {
            if (DbTransaction != null)
            {
                return;
            }
            if (TransactionAdapter.GetCurrentTransaction != null)
            {
                return;
            }
            if (TransactionAdapter.GetCurrentTransaction == null && Write)
            {
                Id = Guid.NewGuid().ToString("N");
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
                TransactionAdapter.GetCurrentTransaction = Context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
                await Task.FromResult(TransactionAdapter.GetCurrentTransaction);
#else

                TransactionAdapter.GetCurrentTransaction = await Context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted,cancellationToken);

#endif
                Console.WriteLine(Id + " Transaction BeginAsync");
                //Transactions.Add(CurrentId, Transaction);
            }
        }
        public override async Task RollBackAsync(CancellationToken cancellationToken = default)
        {
            await RollBackAsync(TransactionAdapter.GetCurrentTransaction, cancellationToken);
        }
        public virtual async Task RollBackAsync(
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
            DbContextTransaction
#else
        IDbContextTransaction
#endif
           transaction ,CancellationToken cancellationToken = default)
        {
            if (DbTransaction != null && !IsCompleted)
            {
                DbTransaction.Rollback();
                DbTransaction.Dispose();
                IsCompleted = true;
                DbTransaction = null;
                Console.WriteLine(Id + " Transaction RollBackAsync");
                return;
            }
            if (transaction != TransactionAdapter.GetCurrentTransaction)
            {
                return;
            }
            if (TransactionAdapter.GetCurrentTransaction != null && !IsCompleted)
            {
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
               TransactionAdapter.GetCurrentTransaction.Rollback();
               TransactionAdapter.GetCurrentTransaction.Dispose();
#else
                await TransactionAdapter.GetCurrentTransaction.RollbackAsync(cancellationToken);
                await TransactionAdapter.GetCurrentTransaction.DisposeAsync();

#endif
                TransactionAdapter.GetCurrentTransaction = null;
                IsCompleted = true;
                Console.WriteLine(Id + " Transaction RollBackAsync");
            }
        }
        public override async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await CommitAsync(TransactionAdapter.GetCurrentTransaction, cancellationToken);
        }
        public virtual async Task CommitAsync(
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
            DbContextTransaction
#else
        IDbContextTransaction
#endif
           transaction, CancellationToken cancellationToken = default)
        {
            if (DbTransaction != null && !IsCompleted)
            {
                await Context.SaveChangesAsync(cancellationToken);
                DbTransaction.Commit();
                DbTransaction.Dispose();
                DbTransaction = null;
                IsCompleted = true;
                Console.WriteLine(Id + " Transaction CommitAsync");
                return;
            }
            if (transaction != TransactionAdapter.GetCurrentTransaction)
            {
                return;
            }
            if (TransactionAdapter.GetCurrentTransaction != null && !IsCompleted)
            {
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
                Context.SaveChanges();
                TransactionAdapter.GetCurrentTransaction.Commit();
                TransactionAdapter.GetCurrentTransaction.Dispose();
#else
                await Context.SaveChangesAsync(cancellationToken);
                await TransactionAdapter.GetCurrentTransaction.CommitAsync(cancellationToken);
                await TransactionAdapter.GetCurrentTransaction.DisposeAsync();
#endif
                TransactionAdapter.GetCurrentTransaction = null;
                IsCompleted = true;
                Console.WriteLine(Id + " Transaction CommitAsync");
            }
        }
        public override void Dispose()
        {
            DbTransaction?.Dispose();
            TransactionAdapter.GetCurrentTransaction?.Dispose();
            TransactionAdapter.GetCurrentTransaction = null;
            IsCompleted = true;
            DbTransaction = null;
            Console.WriteLine(Id + " Transaction Dispose");
            // Context?.Dispose();
        }
    }
}
#endif