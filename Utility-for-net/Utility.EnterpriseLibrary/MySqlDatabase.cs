//using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
//using Microsoft.Practices.EnterpriseLibrary.Data;
//using MySql.Data;
//using MySql.Data.MySqlClient;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Utility.EnterpriseLibrary
//{
//    //
//    // 摘要:
//    //     Represents a MySQL Database.
//    //
//    // 言论：
//    //     Internally uses MySQL Provider from MySQL (MySQL.Data) to connect to the database.
//    //     Revision 1: Wesley Hobbie Date: 20 January 2006 - Updated for Enterprise Library
//    //     2.0
//    //     Revision 2: Wesley Hobbie Date: 6 February 2007 - Updated to use MySQL Driver
//    //     5.0.3
//    //     Revision 3: Steve Phillips Date: 23 May 2009 - Updated to use EntLib 4.1 core
//    //     and MySQL Driver 6.0.3
//    //     Revision 4: Jeremi Bourgault Date: 19 October 2011 - Updated to use EntLib 5
//    //     core and MySQL Driver 6.4.4
//    [ConfigurationElementType(typeof(MySqlDatabaseData))]
//    public class MySqlDatabase : Database
//    {
//        //
//        // 摘要:
//        //     The parameter token used to delimit parameters for the MySQL database.
//        //
//        // 言论：
//        //     MySQL now recognises '?' as its preferred parameter token, however the .NET data
//        //     provider is still using the '@' sign
//        protected const char ParameterToken = '@';

//        //
//        // 摘要:
//        //     Initializes a new instance of the EntLibContrib.Data.MySql.MySqlDatabase class
//        //     with a connection string.
//        //
//        // 参数:
//        //   connectionString:
//        //     The connection string.
//        public MySqlDatabase(string connectionString)
//            : base(connectionString, MySqlClientFactory.Instance)
//        {
//        }

//        //
//        // 摘要:
//        //     Adds a new In System.Data.Common.DbParameter object to the given command.
//        //
//        // 参数:
//        //   command:
//        //     The command to add the in parameter.
//        //
//        //   name:
//        //     The name of the parameter.
//        //
//        //   dbType:
//        //     One of the MySql.Data.MySqlClient.MySqlDbType values.
//        //
//        // 言论：
//        //     This version of the method is used when you can have the same parameter object
//        //     multiple times with different values.
//        [CLSCompliant(false)]
//        public void AddInParameter(DbCommand command, string name, MySqlDbType dbType)
//        {
//            AddParameter(command, name, dbType, ParameterDirection.Input, string.Empty, DataRowVersion.Default, null);
//        }

//        //
//        // 摘要:
//        //     Adds a new In System.Data.Common.DbParameter object to the given command.
//        //
//        // 参数:
//        //   command:
//        //     The commmand to add the parameter.
//        //
//        //   name:
//        //     The name of the parameter.
//        //
//        //   dbType:
//        //     One of the MySql.Data.MySqlClient.MySqlDbType values.
//        //
//        //   value:
//        //     The value of the parameter.
//        [CLSCompliant(false)]
//        public void AddInParameter(DbCommand command, string name, MySqlDbType dbType, object value)
//        {
//            AddParameter(command, name, dbType, ParameterDirection.Input, string.Empty, DataRowVersion.Default, value);
//        }

//        //
//        // 摘要:
//        //     Adds a new In System.Data.Common.DbParameter object to the given command.
//        //
//        // 参数:
//        //   command:
//        //     The command to add the parameter.
//        //
//        //   name:
//        //     The name of the parameter.
//        //
//        //   dbType:
//        //     One of the MySql.Data.MySqlClient.MySqlDbType values.
//        //
//        //   sourceColumn:
//        //     The name of the source column mapped to the DataSet and used for loading or returning
//        //     the value.
//        //
//        //   sourceVersion:
//        //     One of the System.Data.DataRowVersion values.
//        [CLSCompliant(false)]
//        public void AddInParameter(DbCommand command, string name, MySqlDbType dbType, string sourceColumn, DataRowVersion sourceVersion)
//        {
//            AddParameter(command, name, dbType, 0, ParameterDirection.Input, nullable: true, 0, 0, sourceColumn, sourceVersion, null);
//        }

//        //
//        // 摘要:
//        //     Adds a new Out System.Data.Common.DbParameter object to the given command.
//        //
//        // 参数:
//        //   command:
//        //     The command to add the out parameter.
//        //
//        //   name:
//        //     The name of the parameter.
//        //
//        //   dbType:
//        //     One of the MySql.Data.MySqlClient.MySqlDbType values.
//        //
//        //   size:
//        //     The maximum size of the data within the column.
//        [CLSCompliant(false)]
//        public void AddOutParameter(DbCommand command, string name, MySqlDbType dbType, int size)
//        {
//            AddParameter(command, name, dbType, size, ParameterDirection.Output, nullable: true, 0, 0, string.Empty, DataRowVersion.Default, DBNull.Value);
//        }

//        //
//        // 摘要:
//        //     Adds a new instance of a System.Data.Common.DbParameter object to the command.
//        //
//        // 参数:
//        //   command:
//        //     The command to add the parameter.
//        //
//        //   name:
//        //     The name of the parameter.
//        //
//        //   dbType:
//        //     One of the System.Data.DbType values.
//        //
//        //   size:
//        //     The maximum size of the data within the column.
//        //
//        //   direction:
//        //     One of the System.Data.ParameterDirection values.
//        //
//        //   nullable:
//        //     A value indicating whether the parameter accepts null (Nothing in Visual Basic)
//        //     values.
//        //
//        //   precision:
//        //     The maximum number of digits used to represent the value.
//        //
//        //   scale:
//        //     The number of decimal places to which value is resolved.
//        //
//        //   sourceColumn:
//        //     The name of the source column mapped to the DataSet and used for loading or returning
//        //     the value.
//        //
//        //   sourceVersion:
//        //     One of the System.Data.DataRowVersion values.
//        //
//        //   value:
//        //     The value of the parameter.
//        [CLSCompliant(false)]
//        public virtual void AddParameter(DbCommand command, string name, MySqlDbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
//        {
//            DbParameter value2 = CreateParameter(name, dbType, size, direction, nullable, precision, scale, sourceColumn, sourceVersion, value);
//            command.Parameters.Add(value2);
//        }

//        //
//        // 摘要:
//        //     Adds a new instance of a System.Data.Common.DbParameter object to the command.
//        //
//        // 参数:
//        //   command:
//        //     The command to add the parameter.
//        //
//        //   name:
//        //     The name of the parameter.
//        //
//        //   dbType:
//        //     One of the MySql.Data.MySqlClient.MySqlDbType values.
//        //
//        //   direction:
//        //     One of the System.Data.ParameterDirection values.
//        //
//        //   sourceColumn:
//        //     The name of the source column mapped to the DataSet and used for loading or returning
//        //     the value.
//        //
//        //   sourceVersion:
//        //     One of the System.Data.DataRowVersion values.
//        //
//        //   value:
//        //     The value of the parameter.
//        [CLSCompliant(false)]
//        public void AddParameter(DbCommand command, string name, MySqlDbType dbType, ParameterDirection direction, string sourceColumn, DataRowVersion sourceVersion, object value)
//        {
//            AddParameter(command, name, dbType, 0, direction, nullable: false, 0, 0, sourceColumn, sourceVersion, value);
//        }

//        //
//        // 摘要:
//        //     Builds a value parameter name for the current database.
//        //
//        // 参数:
//        //   name:
//        //     The name of the parameter.
//        //
//        // 返回结果:
//        //     A correctly formatted parameter name.
//        public override string BuildParameterName(string name)
//        {
//            if (name[0] != '@')
//            {
//                return name.Insert(0, new string('@', 1));
//            }

//            return name;
//        }

//        //
//        // 摘要:
//        //     Configures a given System.Data.Common.DbParameter.
//        //
//        // 参数:
//        //   parameter:
//        //     The parameter.
//        //
//        //   name:
//        //     The name of the parameter.
//        //
//        //   dbType:
//        //     One of the MySql.Data.MySqlClient.MySqlDbType values.
//        //
//        //   size:
//        //     The maximum size of the data within the column.
//        //
//        //   direction:
//        //     One of the System.Data.ParameterDirection values.
//        //
//        //   nullable:
//        //     A value indicating whether the parameter accepts null (Nothing in Visual Basic)
//        //     values.
//        //
//        //   precision:
//        //     The maximum number of digits used to represent the value.
//        //
//        //   scale:
//        //     The number of decimal places to which value is resolved.
//        //
//        //   sourceColumn:
//        //     The name of the source column mapped to the DataSet and used for loading or returning
//        //     the value.
//        //
//        //   sourceVersion:
//        //     One of the System.Data.DataRowVersion values.
//        //
//        //   value:
//        //     The value of the parameter.
//        [CLSCompliant(false)]
//        protected virtual void ConfigureParameter(MySqlParameter parameter, string name, MySqlDbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
//        {
//            parameter.MySqlDbType = dbType;
//            parameter.Size = size;
//            parameter.Value = ((value == null) ? DBNull.Value : value);
//            parameter.Direction = direction;
//            parameter.IsNullable = nullable;
//            parameter.Precision = precision;
//            parameter.Scale = scale;
//            parameter.SourceColumn = sourceColumn;
//            parameter.SourceVersion = sourceVersion;
//        }

//        //
//        // 摘要:
//        //     Adds a new instance of a System.Data.Common.DbParameter object.
//        //
//        // 参数:
//        //   name:
//        //     The name of the parameter.
//        //
//        //   dbType:
//        //     One of the System.Data.DbType values.
//        //
//        //   size:
//        //     The maximum size of the data within the column.
//        //
//        //   direction:
//        //     One of the System.Data.ParameterDirection values.
//        //
//        //   nullable:
//        //     A value indicating whether the parameter accepts null (Nothing in Visual Basic)
//        //     values.
//        //
//        //   precision:
//        //     The maximum number of digits used to represent the value.
//        //
//        //   scale:
//        //     The number of decimal places to which value is resolved.
//        //
//        //   sourceColumn:
//        //     The name of the source column mapped to the DataSet and used for loading or returning
//        //     the value.
//        //
//        //   sourceVersion:
//        //     One of the System.Data.DataRowVersion values.
//        //
//        //   value:
//        //     The value of the parameter.
//        [CLSCompliant(false)]
//        protected DbParameter CreateParameter(string name, MySqlDbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
//        {
//            MySqlParameter mySqlParameter = CreateParameter(name) as MySqlParameter;
//            ConfigureParameter(mySqlParameter, name, dbType, size, direction, nullable, precision, scale, sourceColumn, sourceVersion, value);
//            return mySqlParameter;
//        }

//        //
//        // 摘要:
//        //     Retrieves parameter information from the stored procedure specified in the System.Data.Common.DbCommand
//        //     and populates the Parameters collection of the specified System.Data.Common.DbCommand
//        //     object.
//        //
//        // 参数:
//        //   discoveryCommand:
//        //     The System.Data.Common.DbCommand to do the discovery.
//        //
//        // 言论：
//        //     The System.Data.Common.DbCommand must be a MySql.Data.MySqlClient.MySqlCommand
//        //     instance.
//        protected override void DeriveParameters(DbCommand discoveryCommand)
//        {
//            MySqlCommandBuilder.DeriveParameters((MySqlCommand)discoveryCommand);
//        }

//        //
//        // 摘要:
//        //     Determines if the number of parameters in the command matches the array of parameter
//        //     values.
//        //
//        // 参数:
//        //   command:
//        //     The System.Data.Common.DbCommand containing the parameters.
//        //
//        //   values:
//        //     The array of parameter values.
//        //
//        // 返回结果:
//        //     true if the number of parameters and values match; otherwise, false.
//        protected override bool SameNumberOfParametersAndValues(DbCommand command, object[] values)
//        {
//            int num = 0;
//            int num2 = command.Parameters.Count - num;
//            int num3 = values.Length;
//            return num2 == num3;
//        }

//        //
//        // 摘要:
//        //     Sets the RowUpdated event for the data adapter.
//        //
//        // 参数:
//        //   adapter:
//        //     The System.Data.Common.DbDataAdapter to set the event.
//        protected override void SetUpRowUpdatedEvent(DbDataAdapter adapter)
//        {
//            ((MySqlDataAdapter)adapter).RowUpdated += OnMySqlRowUpdated;
//        }

//        //
//        // 摘要:
//        //     Checks if a database command is a MySql command and converts.
//        //
//        // 参数:
//        //   command:
//        //     The command.
//        //
//        // 返回结果:
//        //     converted MySqlCommand
//        public static MySqlCommand CheckIfMySqlCommand(DbCommand command)
//        {
//            MySqlCommand mySqlCommand = command as MySqlCommand;
//            if (mySqlCommand == null)
//            {
//                throw new ArgumentException(Resources.ExceptionCommandNotSqlCommand, "command");
//            }

//            return mySqlCommand;
//        }

//        //
//        // 摘要:
//        //     Called when [my SQL row updated].
//        //
//        // 参数:
//        //   sender:
//        //     The sender.
//        //
//        //   rowThatCouldNotBeWritten:
//        //     The MySql.Data.MySqlClient.MySqlRowUpdatedEventArgs instance containing the event
//        //     data.
//        private void OnMySqlRowUpdated(object sender, MySqlRowUpdatedEventArgs rowThatCouldNotBeWritten)
//        {
//            if (rowThatCouldNotBeWritten.RecordsAffected == 0 && rowThatCouldNotBeWritten.Errors != null)
//            {
//                rowThatCouldNotBeWritten.Row.RowError = Resources.ExceptionMessageUpdateDataSetRowFailure;
//                rowThatCouldNotBeWritten.Status = UpdateStatus.SkipCurrentRow;
//            }
//        }
//    }
//}
