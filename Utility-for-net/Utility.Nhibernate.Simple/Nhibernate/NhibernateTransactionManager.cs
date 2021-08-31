#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
//#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utility.Nhibernate
{
    public class NhibernateTransactionManager : TransactionManager, IDisposable
    {
        private ITransaction transaction;
        private bool used;

        /// <summary>
        /// 使用时 标识 true  nhibernate调用 时 给标识
        /// </summary>
        internal virtual new bool Used { get => used; set {
                if (value&&transaction == null)
                {
                    Id = Guid.NewGuid().ToString();
                    transaction = Session.BeginTransaction();
                    used = true;
                    Console.WriteLine(Id + " Transaction Begin");
                }
                used = value; base.Used = value;
            } 
        }
        internal virtual bool Read { get; set; }
        internal virtual bool Write { get; set; }
        public virtual ISession Session { get; internal set; }
        public virtual ITransaction Transaction
        {
            get
            {
                //说明已 调用 则 begin 。 dapper 调用 nh 则不 调用 怎么知道 用了 没有 
                //if (transaction != null)
                //{
                //    Id = Guid.NewGuid().ToString();
                //    transaction = Session.BeginTransaction();
                //    Used = true;
                //}
                return transaction;
            }
            protected set => transaction = value;
        }

        public override bool HasTransaction()
        {
            return transaction != null;
        }
        public NhibernateTransactionManager(ISession session)
        {
            Session = session;
            //Begin();//使用时则启动事务
        }
        public override void Begin()
        {
            if (Used&&transaction==null)
            {
                Id = Guid.NewGuid().ToString();
                Transaction = Session.BeginTransaction();
                Console.WriteLine(Id + " Transaction Begin");
            }
          
        }
        public override void Commit()
        {
            if (Used)
            {
                //读话 没必须 commit 默认 commit 无法区分
                if (Write)
                {
                    Transaction.Commit();
                    Console.WriteLine(Transaction.WasCommitted);
                    Console.WriteLine(Id + " Transaction Commit");
                    IsCompleted = Transaction.WasCommitted;
                }
                else
                {
                    IsCompleted = true;
                }
                this.Dispose();
            }
        }
        public override void RollBack()
        {
            if (Used)
            {
                transaction.Rollback();
                Console.WriteLine(Id + " Transaction Rollback");
                this.Dispose();
            }
        }
        public override async Task BeginAsync(CancellationToken cancellationToken = default)
        {
            base.Begin();
        }

        public override async Task RollBackAsync(CancellationToken cancellationToken = default)
        {
            if (Used)
            {
                await Transaction.RollbackAsync(cancellationToken);
                Console.WriteLine(Id + " Transaction RollBackAsync");
                this.Dispose();
            }

        }
        public override async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (Used)
            {
                //读话 没必须 commit 默认 commit 无法区分
                if (Write)
                {
                    await Transaction.CommitAsync(cancellationToken);
                    Console.WriteLine(Transaction.WasCommitted);
                    IsCompleted = Transaction.WasCommitted;
                    Console.WriteLine(Id + " Transaction CommitAsync");
                }
                else
                {
                    IsCompleted = true;
                }
                this.Dispose();
            }
        }

        public override void Dispose()
        {
            if(Used)
            {
                Used = false;//使用完成
                Transaction?.Dispose();
                transaction = null;
                Console.WriteLine(Id + " Transaction Dispose");
                Read = false;
                Write = false;
                //Session?.Dispose();
            }

        }
    }
}
#endif