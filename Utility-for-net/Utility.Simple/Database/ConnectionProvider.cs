using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Utility.Database
{
    public class ConnectionProvider:IDisposable
    {
        public virtual TransactionManager Transaction { get; set; }
        public virtual DbConnection Connection { get; set; }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
