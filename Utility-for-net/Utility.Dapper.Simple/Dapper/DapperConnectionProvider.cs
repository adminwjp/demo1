using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Utility.Database;

namespace Utility.Dapper
{
    public class DapperConnectionProvider : ConnectionProvider
    {
        public DapperConnectionProvider()
        {

        }
        public DapperConnectionProvider(DbConnection connection)
        {
            this.Connection = connection;
        }
        private DapperTransactionManager transaction;
        private bool read;
        private bool write;

        internal virtual bool Read { get => read; set { read = value; Transaction.Read = value; Transaction.Used = true;  } }
        internal virtual bool Write { get => write; set { write = value; Transaction.Write = value; Transaction.Used = true;  } }

        public new DapperTransactionManager Transaction
        {
            get
            {
                if (transaction == null)
                {
                    transaction = new DapperTransactionManager(Connection);
                }
                return transaction;
            }
           protected  set
            {
                transaction = value;
                base.Transaction = value;
            }
        }
    }
}
