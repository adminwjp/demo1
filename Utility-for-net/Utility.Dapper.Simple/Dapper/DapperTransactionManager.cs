#if !(NET10 || NET11 || NET20 || NET30 || NET35  || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utility.Dapper
{
    public class DapperTransactionManager : TransactionManager
    {
        private bool used;
        private DbTransaction transaction;

        /// <summary>
        /// 使用时 标识 true  dapper 调用 时 给标识
        /// </summary>
        internal virtual new bool Used
        {
            get => used; set
            {
                if (value&&Write && transaction == null)
                {
                    Id = Guid.NewGuid().ToString();
                    transaction = Connection.BeginTransaction();
                    used = true;
                    Console.WriteLine(Id + " Transaction Begin");
                }
                used = value; base.Used = value;
            }
        }
        public DbConnection Connection { get; internal set; }
        public DbTransaction Transaction { get => transaction; internal set => transaction = value; }
        public DapperTransactionManager(DbConnection connection)
        {
            Connection = connection;
        }
        internal virtual bool Read { get; set; }
        internal virtual bool Write { get; set; }
        public override void Begin()
        {
            if (Used &&Write&& transaction == null)
            {
                Id = Guid.NewGuid().ToString();
                Transaction = Connection.BeginTransaction();
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
                    Console.WriteLine(Id + " Transaction Commit");
                }
                IsCompleted = true;
                this.Dispose();
            }
            IsCompleted = true;
        }
        public override void RollBack()
        {
            if (Used&&Write)
            {
                transaction.Rollback();
                Console.WriteLine(Id + " Transaction RollBack");
                this.Dispose();
            }
        }
        public override Task BeginAsync(CancellationToken cancellationToken = default)
        {
            base.Begin();
            return Task.CompletedTask;
        }

        public override Task RollBackAsync(CancellationToken cancellationToken = default)
        {
            base.RollBack();
            return Task.CompletedTask;
        }
        public override Task CommitAsync(CancellationToken cancellationToken = default)
        {
            base.Commit();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Used = false;//使用完成
            Transaction?.Dispose();
            Transaction = null;
            Console.WriteLine(Id + " Transaction Dispose");
            //Connection?.Dispose();

        }
    }
}
#endif