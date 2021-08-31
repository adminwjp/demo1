using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Utility.Database.Entities;
using Utility.Database.Provider;
using Utility.Database.Utils;
using Utility.Helpers;

namespace Utility.Database.Provider
{
    public abstract partial   class AbstractDbProvider
    {
        public const string ExistsViewByMySql= "select count(1) from information_schema.VIEWs WHERE TABLE_SCHEMA=@databaseName AND TABLE_NAME=@viewName";
        public static int ExistsView(DbConnection connection,string viewName, string databaseName="",
            DbFlag dialect= DbFlag.MySql)
        {
            ValidateHelper.ValidateArgumentNull("viewName", viewName);
            if(dialect== DbFlag.MySql)
            {
                var command = DatabaseUtils.CreateCommand(connection, ExistsViewByMySql);
                DatabaseUtils.SetCommandParamter(command, "@databaseName", databaseName == string.Empty ? connection.Database : databaseName);
                DatabaseUtils.SetCommandParamter(command, "@viewName", viewName);
                return DatabaseUtils.ExecuteNonQuery(command);
            }
            return -1;
        }
        public  static int ViewOperator(DbConnection connection, string viewName, string suffix, 
            OperatorFlag view,DbFlag dialect= DbFlag.MySql, IDbTransaction transaction = null)
        {
            ValidateHelper.ValidateArgumentNull("viewName", viewName);
            if (dialect == DbFlag.MySql)
            {
                var sql = ToSqlByVIEWOperator(viewName, suffix, view);
                return DatabaseUtils.ExecuteNonQuery(connection, sql,CommandType.Text,transaction);
            }
            return -1;
        }
        public static string ToSqlByVIEWOperator(string name, string suffix, OperatorFlag flag)
        {
            var prefix = DbHelper.PrefixSqlByMySqlOrSqlite( OperatorTypeFlag.View,flag);
            if (flag == OperatorFlag.CreateDropIfExists)
            {
                prefix = string.Format(prefix, name);
            }
            //var sql = $"{prefix} {name }{suffix}";
            var sql = $"{prefix} {suffix}";
            return sql;
        }
    }
}
