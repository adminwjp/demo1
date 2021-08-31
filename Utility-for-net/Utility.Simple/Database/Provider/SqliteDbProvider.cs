using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Utility.Database.Driver;
using Utility.Database.Entities;
using Utility.Database.Utils;

namespace Utility.Database.Provider
{
    /// <summary>
    ///sqlite 数据库 驱动操作 触发器参考
    /// <![CDATA[
    /// https://www.cnblogs.com/richzhang/archive/2013/04/24/3041280.html
    /// ]]>
    /// 查询表 列信息参考
    /// <![CDATA[
    /// https://blog.csdn.net/why10260922/article/details/80332786
    /// ]]>
    /// "" '' [] `` 默认使用 ""  关键字不用转换 但是 每个数据库 语法不同   .但 '' 列 也会这样 '' 没去掉  
    /// </summary>
    public class SqliteDbProvider : AbstractDbProvider
    {
        public const string VersionSql = "select SQLITE_VERSION();";
        public const string IdentitySql = "SELECT LAST_INSERT_ROWID()";
        //https://blog.csdn.net/qq_18059143/article/details/103323840?utm_medium=distribute.pc_relevant.none-task-blog-BlogCommendFromMachineLearnPai2-2.channel_param&depth_1-utm_source=distribute.pc_relevant.none-task-blog-BlogCommendFromMachineLearnPai2-2.channel_param
        //其实原理就是不让数据库的每次提交写入磁盘，而做一个缓存，每隔一定时间进行提交
        public const string TimerSql = "PRAGMA synchronous=OFF;PRAGMA Journal_Mode=WAL;PRAGMA Cache_Size=5000;";

        /// <summary>
        /// TABLES
        /// </summary>
        public const string Tables = "TABLES";

        /// <summary>
        /// sqlite connection string address
        /// example：Data Source=a.db;Pooling=true;FailIfMissing=false 
        /// </summary>
        public const string ConnectionString = "Data Source={0};Pooling=true;FailIfMissing=false";

        public static readonly SqliteDbProvider Default = new SqliteDbProvider();

        /// <summary>
        /// no param constractor
        ///ininial:
        ///dialect default value sqlite ,
        ///quot "\""
        /// </summary>
        public SqliteDbProvider()
        {
            base.Dialect = DbFlag.Sqlite;
            base.Quot = "\"";
        }




        /// <summary>
        /// 查询数据库所有键 主键,外键 唯一键 信息
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public DataTable FindKey(IDbConnection connection, string databaseName)
        {
            throw new NotSupportedException();
        }


        /**
         CREATE TRIGGER "main"."test2"
BEFORE DELETE
ON "test1"
FOR EACH ROW
begin
        delete from "test1"
        where a = old.a;
            end; 
         */
        public virtual string CreateTriggerSql(string sql)
        {
            return string.Empty;
        }
        public virtual string ReTableSql(string table, string newTable)
        {
            return $"ALTER TABLE {SqlConstant.KeyWordReplace(table, Dialect)} RENAME TO {SqlConstant.KeyWordReplace(newTable, Dialect)};";
        }
        public virtual string AddColumn(string table, string column, string dataType, string @default, string check)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"alter table \"{table}\"  add  column  \"{column}\" {dataType} ");
            if (!string.IsNullOrEmpty(@default))
            {
                builder.Append(" default ").Append(@default);
            }
            if (!string.IsNullOrEmpty(check))
            {
                builder.Append(" check(").Append(check).Append(")");
            }
            builder.Append(";");
            return builder.ToString();
        }
        public virtual string DropColumn(string table, string column, string dataType)
        {
            return $"alter table \"{table}\"  drop  column  \"{column}\" ; ";
        }

        public virtual string RenameColumn(string table, string column, string newColumn)
        {
            return $"alter table \"{table}\"  rename   column  \"{column}\" to \"{newColumn}\"; ";
        }
        public virtual string CopyTable(string table, string copyTable, bool tableStruct = false)
        {
            return $"INSERT INTO {SqlConstant.KeyWordReplace(table, Dialect)}  SELECT * FROM {SqlConstant.KeyWordReplace(copyTable, Dialect)} {(tableStruct ? "WHERE 0" : string.Empty)};";
        }

        /**
         VACUUM 命令通过复制主数据库中的内容到一个临时数据库文件，然后清空主数据库，并从副本中重新载入原始的数据库文件。这消除了空闲页，把表中的数据排列为连续的，另外会清理数据库文件结构。

        如果表中没有明确的整型主键（INTEGER PRIMARY KEY），VACUUM 命令可能会改变表中条目的行 ID（ROWID）。VACUUM 命令只适用于主数据库，附加的数据库文件是不可能使用 VACUUM 命令。

        如果有一个活动的事务，VACUUM 命令就会失败。VACUUM 命令是一个用于内存数据库的任何操作。由于 VACUUM 命令从头开始重新创建数据库文件，所以 VACUUM 也可以用于修改许多数据库特定的配置参数
         */
        public int Vacuum(IDbConnection connection)
        {
            string sql = $"VACUUM {SqlConstant.KeyWordReplace(connection.Database, Dialect)}";
            return DatabaseUtils.ExecuteNonQuery(connection, sql);
        }
        public DataTable FindExplan(IDbConnection connection, string selectSql)
        {
            IDbCommand command = connection.CreateCommand();
            command.CommandText = $"EXPLAIN {selectSql}";
            var dataAdapter = AbstractReflectDbDriver.CreateDatabaseTypeFactory(Dialect).CreateDataAdapter(command);
            return DatabaseUtils.ExecuteDataTable(dataAdapter);
        }
        public DataTable FindExplanQueryPlan(IDbConnection connection, string selectSql)
        {
            IDbCommand command = connection.CreateCommand();
            command.CommandText = $"EXPLAIN QUERY PLAN {selectSql}";
            var dataAdapter = AbstractReflectDbDriver.CreateDatabaseTypeFactory(Dialect).CreateDataAdapter(command);
            return DatabaseUtils.ExecuteDataTable(dataAdapter);
        }
        protected virtual string Count(string name = "*")
        {
            return $"COUNT({name})";
        }
        protected virtual string Min(string name)
        {
            return $"MIN({name})";
        }
        protected virtual string Max(string name)
        {
            return $"MAX({name})";
        }

        protected virtual string Sum(string name)
        {
            return $"SUM({name})";
        }

        protected virtual string Avg(string name)
        {
            return $"AVG({name})";
        }

        protected virtual string Randmon()
        {
            return $"RANDOM ()";
        }

        protected virtual string Upper(string name)
        {
            return $"UPPER({name})";
        }

        protected virtual string Lower(string name)
        {
            return $"LOWER({name})";
        }

        protected virtual string Length(string name)
        {
            return $"LENGTH ({name})";
        }
    }
}
