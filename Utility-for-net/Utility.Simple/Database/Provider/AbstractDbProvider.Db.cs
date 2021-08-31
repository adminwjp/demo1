using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Database.Entities;
using Utility.Database.Provider;
using Utility.Database.Utils;
using Utility.Helpers;

namespace Utility.Database.Provider
{
    public abstract partial class AbstractDbProvider
    {  
        //db exists sql
        //  const string ExistsDatabaseSqlByMySqlValue = "select count(1) from mysql.db where Db=@name";//没有数据
        const string ExistsDatabaseSqlByMySqlValue = "SELECT  1 from information_schema.`TABLES` where TABLE_SCHEMA=@name HAVING count(1)> 1";//可能返回 1 可能 返回 Null
        const string ExistsDatabaseSqlBySqlServerValue = "select count(1) from sys.databases where name=@name;";

        //select all database sql
        const string SelectDatabaseSqlMySqlValue = "SHOW DATABASES";
        const string SelectDatabaseSqlSqlServerValue = "select name from sys.databases;";
        const string SelectDatabaseSqlSqliteValue = "PRAGMA database_list;";//获取所有数据库连接

        public static void InitialConnectionString(DbConnection connection)
        {
            InitialConnectionStringByMySqlOrSqlServer(connection);
        }

        /// <summary>
        /// 前提格式 ：server=.;database=a;
        /// 暂时支持 mysql sqlserver sqlite无效
        /// 根据 连接地址 检测 数据库 是否存在 不存在 则创建(地址 内部 更新 不然 ado.net 报错)
        /// </summary>
        /// <param name="connection"></param>
        internal static void InitialConnectionStringByMySqlOrSqlServer(DbConnection connection)
        {
            var databaseName = string.Empty;
            string[] strs = connection.ConnectionString.Split(new char[] { ';' });
            StringBuilder builder = new StringBuilder(connection.ConnectionString.Length);
            for (int i = 0; i < strs.Length; i++)
            {
                var item = strs[i].ToLower();
                if (item.Contains("database"))
                {
                    databaseName = strs[i].Split('=')[1];
                    continue;
                }
                else
                {
                    builder.Append(strs[i]).Append(";");
                }
            }
            if (!string.IsNullOrEmpty(databaseName))
            {
                var connectionString = builder.ToString();
                connection.ConnectionString = connectionString;
                DatabaseOperator(connection, new DatabaseEntity() { Database = databaseName }, OperatorFlag.CreateIfNotExists);
                connection.ChangeDatabase(databaseName);
            }
        }

        #region db


        #region 数据库 创建  删除 ddl
        /// <summary>
        /// 数据库 操作 创建 删除 ddl 暂时 实现 mysql sqlserver
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="databaseEntry"></param>
        /// <param name="flag"></param>
        /// <param name="dialect"></param>
        /// <returns></returns>
        public static int DatabaseOperator(DbConnection connection, DatabaseEntity databaseEntry, OperatorFlag flag,
            DbFlag dialect = DbFlag.MySql, IDbTransaction transaction = null)
        {
            ValidateHelper.ValidateArgumentNull("database", databaseEntry.Database);
            if (dialect == DbFlag.MySql)
            {
                var sql = DatabaseOperatorSqlByMysql(databaseEntry, flag);
                return DatabaseUtils.ExecuteNonQuery(connection, sql);
            }
            else if (dialect == DbFlag.SqlServer)
            {
                //不然提示有多个 事务 sql 直接在 sqlserver执行没问题  begin end(无效)
                //DROP DATABASE statement cannot be used inside a user transaction.
                //CREATE DATABASE statement not allowed within multi - statement transaction.
                var sql = DatabaseOperatorSqlBySqlserver(databaseEntry, flag);
                return DatabaseUtils.ExecuteNonQuery(connection, sql, CommandType.Text, transaction, false);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// sqlserver 数据库 创建  删除 ddl
        /// </summary>
        /// <param name="databaseEntry"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        internal static string DatabaseOperatorSqlBySqlserver(DatabaseEntity databaseEntry, OperatorFlag flag)
        {
            //不然提示有多个 事务 sql 直接在 sqlserver执行没问题 最好 用 begin end
            //DROP DATABASE statement cannot be used inside a user transaction.
            //CREATE DATABASE statement not allowed within multi - statement transaction.
            var builder = new StringBuilder();
            string db = databaseEntry.Database;
            builder.Append("begin\r\n");
            switch (flag)
            {
                case OperatorFlag.Drop:
                    builder.Append("DROP DATABASE ").Append(db).Append(";");
                    break;
                case OperatorFlag.DropIfExists:
                    builder.Append(" IF DB_ID('").Append(databaseEntry.Database)
                        .Append("') IS NOT NULL  DROP DATABASE ").Append(db).Append(";");
                    break;
                case OperatorFlag.CreateDropIfExists:
                    builder.Append(" IF DB_ID('").Append(databaseEntry.Database)
                      .Append("') IS NOT NULL  DROP DATABASE ").Append(db)
                      .Append(" ;\r\n ");
                    CreateByPathAndSize(builder, databaseEntry);
                    break;
                case OperatorFlag.CreateIfNotExists:
                    builder.Append(" IF DB_ID('").Append(databaseEntry.Database)
                       .Append("') IS  NULL  ");
                    CreateByPathAndSize(builder, databaseEntry);
                    break;
                case OperatorFlag.Create:
                default:
                    CreateByPathAndSize(builder, databaseEntry);
                    break;
            }
            builder.Append("\r\nend\r\n");
            builder.Append($" go \r\n ALTER DATABASE [{databaseEntry.Database}] COLLATE Chinese_PRC_CS_AI_WS\r\n");//解决中文乱码 
            return builder.ToString();
        }

        private static void CreateByPathAndSize(StringBuilder builder, DatabaseEntity databaseEntry)
        {
            string db = databaseEntry.Database;
            builder.Append("CREATE DATABASE ").Append(db).Append(" \r\n");
            if (!string.IsNullOrEmpty(databaseEntry.Path))
            {
                builder.Append(" ON PRIMARY (NAME = ").Append(db).Append(",");
                builder.Append("FILENAME=\"").Append(databaseEntry.Path).Append(databaseEntry.Database).Append(".mdf\",SIZE=")
                    .Append(databaseEntry.MdfSize).Append(",MAXSIZE= ").Append(databaseEntry.MdfMaxSize)
                    .Append(",FILEGROWTH=").Append(databaseEntry.MdfFileGrowth).Append(")\r\n LOG ON(NAME=\"")
                    .Append(databaseEntry.Database).Append("_log\",FILENAME= \"").Append(databaseEntry.Path)

                    .Append(databaseEntry.Database)
                    .Append(".ldf\",SIZE=")
                    .Append(databaseEntry.LdfSize).Append(",MAXSIZE= ").Append(databaseEntry.LdfMaxSize)
                    .Append(",FILEGROWTH=").Append(databaseEntry.LdfFileGrowth)
                    .Append("); ");
            }
            else
            {
                builder.Append(";");
            }
        }


        /// <summary>
        /// mysql 数据库 操作 创建 删除 ddl
        /// </summary>
        /// <param name="databaseEntry"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        internal static string DatabaseOperatorSqlByMysql(DatabaseEntity databaseEntry, OperatorFlag flag)
        {
            var prefix = DbHelper.PrefixSqlByMySqlOrSqlite(OperatorTypeFlag.Database, flag);
            if (flag == OperatorFlag.CreateDropIfExists)
            {
                prefix = string.Format(prefix, databaseEntry.Database);
            }
            if (flag == OperatorFlag.Drop || flag == OperatorFlag.DropIfExists)
            {
                return $@"{prefix}  { databaseEntry.Database};";
            }
            if (!string.IsNullOrEmpty(databaseEntry.Charset) && !string.IsNullOrEmpty(databaseEntry.Collate))
            {
                string sql = $@"{prefix}  { databaseEntry.Database} DEFAULT CHARSET  { databaseEntry.Charset} COLLATE {databaseEntry.Collate};";
                return sql;
            }
            else
            {
                string sql = $@"{prefix}  { databaseEntry.Database} ;";
                return sql;
            }
        }

        #endregion 数据库 创建  删除 ddl



        #region  数据库是否存在 dql(select)
        /// <summary>
        /// 数据库是否存在
        /// 暂时支持 sqlserver sqlite mysql
        /// </summary>
        /// <param name="databaseName">数据库</param>
        /// <returns></returns>
        public static int ExistsDatabase(DbConnection connection, string databaseName = "",
            DbFlag dialect = DbFlag.MySql, IDbTransaction transaction = null)
        {
            //connection.Database sqlserver 后面怎么不知道 清空了
            string db = databaseName;
            if (string.IsNullOrEmpty(db))
            {
                db = connection.Database;
            }
            ValidateHelper.ValidateArgumentNull("databaseName", db);
            string sql =dialect== DbFlag.MySql? ExistsDatabaseSqlByMySqlValue:
                dialect== DbFlag.SqlServer?ExistsDatabaseSqlBySqlServerValue:string.Empty;
            if (string.IsNullOrEmpty(sql))
            {
                if (dialect == DbFlag.Sqlite)
                {
                    string[] dbs = FindDatabase(connection, DbFlag.Sqlite);
                    if (dbs != null && dbs.Length > 0)
                    {
                        for (int i = 0; i < dbs.Length; i++)
                        {
                            if (dbs[i].ToLower().Equals(databaseName.ToLower()))
                                return 1;
                        }
                    }
                }
                return -1;
            }
            IDbCommand command = DatabaseUtils.CreateCommand(connection, sql);
            DatabaseUtils.SetCommandParamter(command, "@name", db);
            object result = DatabaseUtils.ExecuteScalar(command,transaction);//
            //int count = (int)result;//指定的转换无效。//0 
            if (result == null)
            {
                return -1;
            }
            int count = (int)Convert.ChangeType(result, typeof(int));
            return count;
        }

        #endregion  数据库是否存在 dql




        #region  查询所有数据库 sqlserver: select sqlite or mysql :函数 
        /// <summary> 
        /// 查询所有数据库 
        /// 暂时支持 mysql sqlite(连接 上 数据库 )  sqlserver
        /// </summary>
        /// <returns></returns>
        public static string[] FindDatabase(DbConnection connection, DbFlag dialect = DbFlag.MySql)
        {
            string sql = dialect == DbFlag.MySql ? SelectDatabaseSqlMySqlValue :
            dialect == DbFlag.SqlServer ? SelectDatabaseSqlSqlServerValue :
             dialect == DbFlag.Sqlite ? SelectDatabaseSqlSqliteValue:string.Empty;
            if (!string.IsNullOrEmpty(sql))
            {
                if (dialect == DbFlag.Sqlite)
                {
                    using (IDbCommand command = DatabaseUtils.CreateCommand(connection, sql))
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            Collections.Array<string> dbs = new Collections.Array<string>();
                            while (reader.Read())
                            {
                                if (!(reader.GetValue(1) is DBNull))
                                {
                                    dbs.Add(reader.GetString(1));
                                }
                            }
                            return dbs.ToArray();
                        }
                    }
                }
                else
                {
                    //mysql sqlserver
                    using (IDbCommand command = DatabaseUtils.CreateCommand(connection, sql))
                    {
                        string[] values = DatabaseUtils.ReaderString(command);
                        return values;
                    }
                }
            }
            return null;
        }

        #endregion  查询所有数据库 dql

        #endregion db
    }
}
