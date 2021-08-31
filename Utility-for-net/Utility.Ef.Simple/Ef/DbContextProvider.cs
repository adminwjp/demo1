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
using System.Text;

namespace Utility.Ef
{
    public class DbContextProvider<Context> : DbContextProvider
        where Context : DbContext
    {
        private Context dbContext;
        public DbContextProvider()
        {

        }
        public DbContextProvider(Context dbContext):base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public new Context DbContext { get => dbContext; set {
                dbContext = value;
                base.DbContext = value;
            } }
    }
    public class DbContextProvider:IDisposable
    {
        private bool read;
        private bool write;
        public bool UseTransaction { get; set; } = true;
        internal virtual bool Read { get => read; set { read = value; Transaction.Read = value; Transaction.Used = true;  } }
        internal virtual bool Write { get => write; set { write = value; Transaction.Write = value; Transaction.Used = true; } }

        private EfTransactionManager transaction;

        public virtual DbContext DbContext { get; set; }
        public DbContextProvider()
        {

        }
        public DbContextProvider(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual EfTransactionManager Transaction { get
            {
                if (transaction == null)
                {
                    transaction = new EfTransactionManager(DbContext);
                }
                return transaction;
            }
            set => transaction = value; }

        public void Dispose()
        {
            DbContext?.Dispose();
        }
    }
}
#endif 