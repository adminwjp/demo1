using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Utility.Database.Factory
{
    public abstract class DbFactory
    {
        public readonly static DbFactory Empty = new DefaultFactory();
        public DbFlag Dialect = DbFlag.Access;
        public virtual IDbConnection Connection { get; set; }
        public virtual IDbCommand Command { get; set; }
        public virtual IDbTransaction Transaction { get; set; }
        public virtual IDbConnection CreateConnection()
        {
            return Empty.CreateConnection();
        }

        public virtual IDbConnection CreateConnection(string connectionString)
        {
            return Empty.CreateConnection(connectionString);
        }

        public virtual IDbCommand CreateCommand()
        {
            return Empty.CreateCommand();
        }

        public virtual IDbDataParameter CreateCommand(IDbCommand command)
        {
            return command.CreateParameter();
        }
        public virtual IDbDataParameter CreateParameter(string name, object value)
        {
            var par = Command.CreateParameter();
            par.ParameterName = name;
            par.Value = value;
            return par;
        }
        public virtual IDbDataParameter CreateParameter(IDbCommand command,string name,object value)
        {
            var par= command.CreateParameter();
            par.ParameterName = name;
            par.Value = value;
            return par;
        }
        public virtual IDbCommand CreateCommand(IDbConnection connection)
        {
            return Empty.CreateCommand(connection);
        }

        public virtual IDataAdapter CreateDataAdapter()
        {
            return Empty.CreateDataAdapter();
        }

        public virtual IDataAdapter CreateDataAdapter(IDbCommand command)
        {
            return Empty.CreateDataAdapter(command);
        }


        public virtual IDataReader CreateDataReader(IDbCommand command)
        {
            return command.ExecuteReader();
        }
   

    }
}
