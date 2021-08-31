using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Collections;
using Utility.Database.Driver;
using Utility.Database.Entities;
using Utility.Database.Provider;
using Utility.Database.Utils;
using Utility.Helpers;

namespace Utility.Database.Provider
{
    /// <summary>
    /// 表 操作 信息
    /// </summary>
    public abstract partial  class AbstractDbProvider
    {
        /// <summary>
        /// 查询数据库的所有表
        /// 暂时支持 mysql sqlite sqlserver
        /// <para>mysql:SELECT TABLE_NAME from information_schema.`TABLES` WHERE table_schema=@name</para>
        /// <para>sqlite:select name from sqlite_master type='#type';即 select name from sqlite_master type='table';</para>
        /// <para>sqlserver: "SELECT * FROM SysObjects Where XType='u' and category=0  ;"</para>
        /// </summary>
        /// <returns></returns>
        public static string[] FindTable(DbConnection connection, string databaseName, 
            DbFlag dialect = DbFlag.MySql)
        {
            //db table sql
            string tableNameSql = string.Empty;
            switch (dialect)
            {
                case DbFlag.MySql:
                    tableNameSql = "SELECT TABLE_NAME from information_schema.`TABLES` WHERE table_schema=@name";
                    break;
                case DbFlag.SqlServer:
                    tableNameSql = $"use {databaseName};SELECT * FROM SysObjects Where XType='u' and category=0  ;";//use db 
                    break;
                case DbFlag.Sqlite:
                    tableNameSql = "select name from sqlite_master type='table';";//select name from sqlite_master type='table'; //连接 中所有 数据库 所有 表
                    break;

                case DbFlag.Postgre:
                case DbFlag.Oracle:
                case DbFlag.None:
                default:
                    return null;
            }
            using (IDbCommand command = DatabaseUtils.CreateCommand(connection, tableNameSql))
            {
                if (dialect == DbFlag.MySql)
                {
                    DatabaseUtils.SetCommandParamter(command, "@name", $"{SqlConstant.KeyWordReplace(databaseName, dialect)}");
                }
                return DatabaseUtils.ReaderString(command);
            }
        }

        #region 查询表中所有列信息 table pk fk  信息

        /// <summary>
        /// 根据数据库查询所有表 表中所有列信息
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="databaseName"></param>
        /// <param name="dialect"></param>
        /// <returns></returns>
        public static List<ClassEntity> FindTableByDatabase(DbConnection connection, string databaseName, DbFlag dialect = DbFlag.MySql)
        {
            if (dialect == DbFlag.Sqlite)
            {
                ClassEntity[] tables = FindBySqlite(connection);
                if (tables != null && tables.Length > 0)
                {
                    return new List<ClassEntity>(tables);
                }
                return null;
            }
            if (string.IsNullOrEmpty(databaseName))
            {
                databaseName = connection.Database;
            }
            ValidateHelper.ValidateArgumentNull("databaseName", databaseName);

            if (dialect == DbFlag.MySql)
            {
                return FindByMysql(connection, databaseName);
            }
            else if (dialect == DbFlag.SqlServer)
            {
                ClassEntity[] tables = FindBySqlserver(connection, databaseName);
                if (tables != null && tables.Length > 0)
                {
                    return new List<ClassEntity>(tables);
                }
                return null;
            }
            return null;
        }
        //column sql
        const string MySqlValue = @"SELECT  col.TABLE_NAME 表名,col.COLUMN_NAME 列名,
(case  when col.COLUMN_DEFAULT is null then '' else  col.COLUMN_DEFAULT end) 默认值,
(case  WHEN col.IS_NULLABLe='YES' then '1' else '0' end)  是否为null,
col.DATA_TYPE 数据类型,col.CHARACTER_MAXIMUM_LENGTH 数据类型长度,col.NUMERIC_PRECISION 数字数据类型长度_可给可不给,
col.CHARACTER_SET_NAME 编码,COLUMN_TYPE 完整数据类型,
(case when col.COLUMN_KEY='PRI' then 'pk' when col.COLUMN_KEY='MUL' then 'fk' else '' end ) 关联类型PRI_主键MUL_外键,
(case col.EXTRA  when 'auto_increment' then '1' else '' end ) 自增_主键 ,col.COLUMN_COMMENT 注释
,key1.CONSTRAINT_NAME 外键名称 ,key1.REFERENCED_TABLE_NAME 外键表,key1.REFERENCED_COLUMN_NAME 外键列
from  (SELECT * from  information_schema.`COLUMNS` where TABLE_SCHEMA=@databaseName) col left join 
(SELECT * from  information_schema.KEY_COLUMN_USAGE  where TABLE_SCHEMA=@databaseName)  key1 
 on    key1.TABLE_NAME=col.TABLE_NAME and   key1.COLUMN_NAME=col.COLUMN_NAME  ;";//外键 主键 索引 以及普通列

        const string SqliteValue = "pragma table_info ('#table');PRAGMA foreign_key_LIST( '#table'); ";//pragma table_info ('#table'); 查询 列 及主键, PRAGMA foreign_key_LIST( "test2" )外键; 但查询不到外键名称

        const string SqlServerValue = @"SELECT d.name 表名,
a.colorder 字段序号, a.name 字段名,
 (case when COLUMNPROPERTY(a.id, a.name, 'IsIdentity') = 1 then '1'else '0' end) 标识,
(case when(SELECT count(*) FROM sysobjects
WHERE(name in (SELECT name FROM sysindexes
WHERE(id = a.id) AND(indid in
(SELECT indid FROM sysindexkeys
WHERE(id = a.id) AND(colid in
(SELECT colid FROM syscolumns WHERE(id = a.id) AND(name = a.name)))))))
AND(xtype = 'PK'))> 0 then 'pk' else '' end) 主键,b.name 类型, a.length 占用字节数,
 COLUMNPROPERTY(a.id, a.name, 'PRECISION') as 长度,
isnull(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0) as 小数位数,(case when a.isnullable = 1 then '1'else '0' end) 允许空,
isnull(e.text, '') 默认值,isnull(g.[value], '') AS[说明]
FROM syscolumns a
left join systypes b on a.xtype = b.xusertype
inner join sysobjects d on a.id = d.id and d.xtype = 'U' and d.name <> 'dtproperties'
left join syscomments e on a.cdefault = e.id
left join sys.extended_properties g on a.id = g.major_id AND a.colid = g.minor_id
left join sys.extended_properties f on d.id = f.class and f.minor_id=0
where b.name is not null
--WHERE d.name= '要查询的表'--如果只查询指定表, 加上此条件
order by a.id, a.colorder;";//主键 以及普通列. 外键 必须单独查询

   

        /// <summary>
        /// 注意 这个 查询 暂时 查的 是 当前 数据库的 列 . 要么 要 查询 其他数据库 只能 use database;更改数据库(因为有的数据库用的绝对路劲不好控制)
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        private static ClassEntity[] FindBySqlserver(DbConnection connection, string database)
        {
            DataTable dt = null;//查询所有列
            DataSet ds = null;//查询 外键时用到
            using (DbCommand command = connection.CreateCommand())
            {
                //列
                string sql = $"use [{database}];{SqlServerValue}";//查询当前数据库所有列
                command.CommandText = sql;
                dt = DatabaseUtils.ExecuteDataTable(AbstractReflectDbDriver.CreateDatabaseTypeFactory(DbFlag.SqlServer).CreateDataAdapter(command));
                if (dt == null)
                {
                    return null;
                }
                //表
                sql = $"use[{ database}];SELECT * FROM SysObjects Where XType='u' and category=0  ;";
                command.CommandText = sql;
                var tables = DatabaseUtils.ReaderString(command);
                if (tables == null || tables.Length == 0)
                {
                    return null;
                }
                //外键
                StringBuilder builder = new StringBuilder(100 * tables.Length);
                builder.Append("use [").Append(database).Append("];\r\n");
                for (int i = 0; i < tables.Length; i++)
                {
                    builder.Append("EXEC sp_fkeys '").Append(tables[i]).Append("';");
                }
                sql = builder.ToString();
                command.CommandText = sql;
                ds = DatabaseUtils.ExecuteDataSet(AbstractReflectDbDriver.CreateDatabaseTypeFactory(DbFlag.SqlServer).CreateDataAdapter(command));
                if (ds == null)
                {
                    return null;
                }
            }
            //组装 数据
            if (dt.Rows == null || dt.Rows.Count == 0)
            {
                return null;
            }
            if (ds.Tables != null && ds.Tables.Count == 0)
            {
                return null;
            }
            List<ClassEntity> tableEntries = new List<ClassEntity>(ds.Tables.Count);
            foreach (DataRow row in dt.Rows)
            {
                string table = row[0].ToString();
                //string sequeue = row[1].ToString();//1 2 3 序列号
                string column = row[2].ToString();
                bool identity = row[3].ToString().Equals("1");//1: 自增 ,0 
                string pk = row[4].ToString();//pk: 主键
                string dataType = row[5].ToString();
                // int byteLength = (int)Convert.ChangeType(row[6].ToString(), typeof(int));//数据类型 所占字节数
                long length = (long)Convert.ChangeType(row[7].ToString(), typeof(long));
                //long samllNumber = (int)Convert.ChangeType(row[8].ToString(), typeof(int));//小数位
                bool isNull = row[9].ToString().Equals("1");
                string defaultValure = row[10].ToString();
                //string remark = row[11].ToString();// 说明 ,例如:主键

                ClassEntity tableEntry = FindTableEntity(tableEntries, table);
                PropertyEntity columnEntry = new PropertyEntity() { Flag = ColumnFlag.Column, Column = column, Identity = identity, DataType = dataType, Length = length, IsNull = isNull, Default = defaultValure };
                tableEntry.PropertyEntities.Add(columnEntry);
                if (pk.Equals("pk"))
                {
                    tableEntry.PkQuantity++;
                    tableEntry.IdEntities.Add(columnEntry);
                    columnEntry.Flag = ColumnFlag.PrimaryKey;
                    tableEntry.IsIdentity = columnEntry.Identity;
                }
            }

            foreach (DataTable dataTable in ds.Tables)
            {
                if (dataTable.Rows == null || dataTable.Rows.Count == 0)
                {
                    continue;
                }
                foreach (DataRow row in dataTable.Rows)
                {
                    //string dbName = row[0].ToString();
                    //string prefix = row[1].ToString();//dbo
                    string referenceTable = row[2].ToString();
                    string referenceId = row[3].ToString();
                    //string dbName1 = row[4].ToString();
                    //string prefix1 = row[5].ToString();//dbo
                    string table = row[6].ToString();
                    string column = row[7].ToString();
                    string constraint = row[11].ToString();
                    //其他参数不管了 是 啥 意思
                    ClassEntity tableEntry = FindTableEntity(tableEntries, table);
                    PropertyEntity columnEntry = FindColumnEntry(new List<PropertyEntity>(tableEntry.PropertyEntities), column);
                    tableEntry.FkQuantity++;
                    tableEntry.FkEntities.Add(columnEntry);
                    columnEntry.Flag = ColumnFlag.ForeignKey;
                    FKColumnEntity foreignKeyColumnEntry = new FKColumnEntity() { ReferenceTable = referenceTable, ReferenceId = referenceId, Constraint = constraint };
                    columnEntry.FKColumnEntity = foreignKeyColumnEntry;

                }
            }
            return tableEntries.ToArray();
        }

        private static ClassEntity[] FindBySqlite(DbConnection connection)
        {
            using (DbCommand command = connection.CreateCommand())
            {
                string sql = $"select name from sqlite_master where type='table';";//查询所有表
                command.CommandText = sql;
                var tables = DatabaseUtils.ReaderString(command);
                if (tables != null && tables.Length > 0)
                {
                    List<ClassEntity> tableEntries = new List<ClassEntity>(tables.Length);
                    foreach (var item in tables)
                    {
                        sql = SqliteValue;
                        sql = sql.Replace("#table", item);
                        command.CommandText = sql;
                        IDbDataAdapter dataAdapter = AbstractReflectDbDriver.CreateDatabaseTypeFactory(DbFlag.Sqlite).CreateDataAdapter(command);
                        var ds = DatabaseUtils.ExecuteDataSet(dataAdapter);
                        var dt = ds.Tables[0];//列 及主键 列
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            ClassEntity tableEntry = new ClassEntity() { Table = item, PropertyEntities = new Utility.Collections.Array<PropertyEntity>() };
                            foreach (DataRow row in dt.Rows)
                            {
                                //var newRow = dataTable.NewRow();
                                //newRow["cid"] = row["cid"];//序列号
                                //newRow["name"] = row["name"];//列名
                                //newRow["type"] = row["type"];//数据类型
                                //newRow["notnull"] = row["notnull"];//是否为null 1 不能为null 0:null
                                //newRow["dflt_value"] = row["dflt_value"];//默认值 default 1
                                //newRow["pk"] = row["pk"];//是否是主键 >0 是主键(1,2,3...(多个主键时)) 0:非主键
                                //dataTable.Rows.Add(newRow);
                                int pk = (int)Convert.ChangeType(row["pk"], typeof(int));
                                PropertyEntity columnEntry = new PropertyEntity() { Column = row["name"]?.ToString() };
                                tableEntry.PropertyEntities.Add(columnEntry);
                                columnEntry.DataType = row["type"]?.ToString();
                                int notnull = (int)Convert.ChangeType(row["notnull"], typeof(int));
                                columnEntry.IsNull = notnull == 0;
                                columnEntry.Default = row["dflt_value"] is DBNull ? string.Empty : row["dflt_value"]?.ToString(); //这个 值 需要 统一 处理 转成 其他数据库 了
                                if (pk > 0)
                                {

                                    tableEntry.PkQuantity++;
                                    tableEntry.IdEntities.Add(columnEntry);
                                    columnEntry.Flag = ColumnFlag.PrimaryKey;
                                }
                                else
                                {
                                    //普通 列 或 外键 列 (无法识别 只能根据创建 表 sql区分)
                                    columnEntry.Flag = ColumnFlag.Column;
                                }
                            }
                            //dataTable.Merge(dt);
                            if (tableEntry != null)
                            {
                                tableEntries.Add(tableEntry);
                            }
                        }
                        var dt1 = ds.Tables[1];//外键列 
                        if (dt1 != null && dt1.Rows.Count > 0)
                        {
                            ClassEntity tableEntry = FindTableEntity(tableEntries, item);
                            foreach (DataRow row in dt1.Rows)
                            {
                                string referenceColumn = row["to"].ToString();//外键列
                                string referenceTable = row["table"].ToString();//外键表
                                string column = row["from"].ToString();//列
                                string update = row["on_update"].ToString();//外键 更新级联
                                string delete = row["on_delete"].ToString();//外键 删除 级联

                                PropertyEntity columnEntry = FindColumnEntry(new List<PropertyEntity>(tableEntry.PropertyEntities), column);

                                tableEntry.FkQuantity++;
                                tableEntry.FkEntities.Add(columnEntry);
                                columnEntry.Flag = ColumnFlag.ForeignKey;
                                FKColumnEntity foreignKeyColumnEntry = new FKColumnEntity();
                                columnEntry.FKColumnEntity = foreignKeyColumnEntry;
                                foreignKeyColumnEntry.ReferenceId = referenceColumn;
                                foreignKeyColumnEntry.ReferenceTable = referenceTable;
                                foreignKeyColumnEntry.ForeignKeyCascade(update, true, DbFlag.Sqlite);
                                foreignKeyColumnEntry.ForeignKeyCascade(delete, false, DbFlag.Sqlite);
                            }
                        }
                    }
                    return tableEntries.ToArray();
                }
                else
                {
                    return null;
                }
            }
        }

        private static List<ClassEntity> FindByMysql(DbConnection connection, string databaseName)
        {
            string sql = MySqlValue;
            DataTable dt = null;
            using (IDbCommand command = DatabaseUtils.CreateCommand(connection, sql))
            {
                var a = connection.GetType().Assembly;
                command.Parameters.Clear();
                DatabaseUtils.SetCommandParamter(command, "@databaseName", databaseName);
                IDbDataAdapter dataAdapter = AbstractReflectDbDriver.CreateDatabaseTypeFactory(DbFlag.MySql).CreateDataAdapter(command);
                dt = DatabaseUtils.ExecuteDataTable(dataAdapter);
                if (dt == null)
                {
                    return null;
                }
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                List<ClassEntity> tableEntries = new List<ClassEntity>();
                foreach (DataRow item in dt.Rows)
                {
                    string tableName = item[0].ToString();
                    string columnName = item[1].ToString();
                    string defaultValue = item[2].ToString();//这个 值 需要 处理 下 'a'  1 后面 统一处理
                    bool isNull = item[3].ToString() == "0";
                    string dataType = item[4].ToString();//varchar 好统一管理
                    long length = item[5] is DBNull ?
                        (item[6] is DBNull ? 0 : (long)Convert.ChangeType(item[6], typeof(long)))
                        : (long)Convert.ChangeType(item[5], typeof(long)); //varchar 50,int null
                    // string encoding = item[7].ToString();
                    //string dType = item[8].ToString();//varchar(50) int(10) 这个不要 每个数据库 不一致 后期 不好 统一 转 数据库
                    string type = item[9].ToString();//pk:主键, fk:外键,  ""
                    string identity = item[10].ToString();//1:自增, ""
                    string comment = item[11] is DBNull ? string.Empty : item[11].ToString();
                    string constraint = item[12] is DBNull ? string.Empty : item[12].ToString();//PRIMARY:主键, 外键 索引 不好区分 .根据 type 区分
                    string referenceTable = item[13] is DBNull ? string.Empty : item[13].ToString();
                    string referenceId = item[14] is DBNull ? string.Empty : item[14].ToString();



                    ClassEntity tableEntry = FindTableEntity(tableEntries, tableName);
                    PropertyEntity columnEntry = new PropertyEntity() { Column = columnName, Default = defaultValue, IsNull = isNull, DataType = dataType, Length = length, Comment = comment };
                    tableEntry.PropertyEntities.Add(columnEntry);
                    //主键
                    if (constraint.Equals("PRIMARY") || type.Equals("pk"))
                    {
                        tableEntry.PkQuantity++;
                        tableEntry.IdEntities.Add(columnEntry);
                        columnEntry.Flag = ColumnFlag.PrimaryKey;
                        columnEntry.Identity = identity.Equals("1");
                        continue;
                    }
                    //外键 
                    else if (!string.IsNullOrEmpty(referenceTable) && type.Equals("fk"))
                    {
                        tableEntry.FkQuantity++;
                        tableEntry.FkEntities.Add(columnEntry);
                        columnEntry.Flag = ColumnFlag.ForeignKey;
                        columnEntry.FKColumnEntity = new FKColumnEntity();
                        columnEntry.FKColumnEntity.ReferenceTable = referenceTable;
                        columnEntry.FKColumnEntity.ReferenceId = referenceId;
                        columnEntry.FKColumnEntity.Constraint = constraint;
                    }
                    else
                    {
                        //唯一键 索引 普通列 等等之类
                        columnEntry.Flag = ColumnFlag.Column;

                    }

                }
                return tableEntries;
            }
            return null;
        }


        private static PropertyEntity FindColumnEntry(List<PropertyEntity> data, string column)
        {
            PropertyEntity columnEntry = data.Find(it => it.Column.ToLower() == column.ToLower());
            if (columnEntry == null)
            {
                throw new ArgumentNullException($"{column} 找不到!");
            }
            return columnEntry;
        }

        private static ClassEntity FindTableEntity(List<ClassEntity> tableEntries, string table)
        {
            ClassEntity tableEntry = null;
            if (tableEntries != null && tableEntries.Count > 0)
            {
                tableEntry = tableEntries.Find(it => it.Table.ToLower() == table.ToLower());
                if (tableEntry != null)
                {
                    return tableEntry;
                }
            }
            tableEntry = new ClassEntity() { 
                PropertyEntities = new Collections.Array<PropertyEntity>(),
                IdEntities=new List<PropertyEntity>(),FkEntities=new List<PropertyEntity>() };
            tableEntries.Add(tableEntry);
            tableEntry.Table = table;
            return tableEntry;
        }





        #endregion 查询表中所有列信息 table pk fk  信息

        #region table sql

        public static int TableOperator(DbConnection connection, ClassEntity classEntity, 
            OperatorFlag flag = OperatorFlag.CreateIfNotExists, DbFlag dialect = DbFlag.MySql, 
            IDbTransaction transaction = null)
        {
            string sql = CreateTableSql(classEntity,flag,dialect);
            int result = DatabaseUtils.ExecuteNonQuery(connection, sql,CommandType.Text,transaction);
            return result;
        }

        private static void GetForeiginKey(IDbConnection connection, IList<FkModel> fkEntries, 
            ClassEntity[] classEntities, DbFlag dialect)
        {

            using (IDbCommand command = connection.CreateCommand())
            {
                string sql = string.Empty;
                if (dialect == DbFlag.MySql)
                {
                    //查询的是 主键 外键 索引 等信息 (打乱的信息 删除也有优先级)
                    //information_schema.KEY_COLUMN_USAGE 排序 好 的信息
                    sql = @" SELECT  key1.CONSTRAINT_NAME 外键名称 ,key1.table_name 
from (SELECT COLUMN_NAME,CONSTRAINT_NAME,table_name  from  information_schema.KEY_COLUMN_USAGE  where table_schema='#Database')  key1 ,
(SELECT (case when COLUMN_KEY='PRI' then 'pk' when COLUMN_KEY='MUL' then 'fk' else '' end ) name,COLUMN_NAME from  information_schema.`COLUMNS`  where table_schema='#Database') col 
 where    key1.CONSTRAINT_NAME is not null and  key1.COLUMN_NAME=col.COLUMN_NAME and col.name='fk'   ;";
                    sql = sql.Replace("#Database", connection.Database);
                    command.CommandText = sql;
                    using (IDataReader reader = DatabaseUtils.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            FkModel fkEntry = new FkModel() { Constraint = reader.GetString(0), Table = reader.GetString(1) };
                            fkEntries.Add(fkEntry);
                        }

                    }
                    return;
                }
                //constraint 怎么查询到了 sqlite 无法查询到?
                for (int i = 0; i < classEntities.Length; i++)
                {
                    ClassEntity tableEntry = classEntities[i];
                    if (dialect == DbFlag.Sqlite)
                    {
                        //sql = $"pragma table_info ('{tableEntry.Table}');";
                        //command.CommandText = sql;
                        //string[] constrains = DatabaseUtils.ReaderString(command);
                        return;
                    }
                    else if (dialect == DbFlag.SqlServer)
                    {
                        //(打乱的信息 删除也有优先级)
                        sql = $"EXEC sp_fkeys '{tableEntry.Table}';";
                        DataTable dataTable = DatabaseUtils.ExecuteDataTable(AbstractReflectDbDriver.CreateDatabaseTypeFactory(dialect).CreateDataAdapter(command));
                        if (dataTable.Rows == null || dataTable.Rows.Count == 0)
                        {
                            continue;
                        }
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string constraint = row[11].ToString();
                            string referenceTable = row[2].ToString();
                            string referenceId = row[3].ToString();
                            //string dbName1 = row[4].ToString();
                            //string prefix1 = row[5].ToString();//dbo
                            //string table = row[6].ToString();
                            string column = row[7].ToString();
                            FkModel fkEntry = new FkModel() { Constraint = constraint, Table = tableEntry.Table, Column = column, ReferenceId = referenceId, ReferenceTable = referenceTable };
                            fkEntries.Add(fkEntry);
                        }
                        //排序 按 外键 优先级倒过来
                        for (int j = 0; j < fkEntries.Count; j++)
                        {

                        }
                        continue;
                    }
                    else
                    {
                        return;
                    }
                }

            }
            //alter table authority_operator_info  drop foreign key FK_3C88F528
        }


        public static int TableOperator(IDbConnection connection, ClassEntity[] tableEntries, 
            OperatorFlag flag = OperatorFlag.CreateIfNotExists, DbFlag dialect = DbFlag.MySql
            , IDbTransaction transaction = null)
        {
            //方案1: 删除 :  先排序(外键级联) 再删除所有表 ,创建: 先排序 再创建所有表(前提 表结构差不多相同)
            //方案2: 删除: 先删除所有外键,再所有表 创建:先创建所有表 再创建所有外键
            if (flag == OperatorFlag.None)
            {
                flag = OperatorFlag.CreateIfNotExists;
            }
            StringBuilder builder = new StringBuilder();
            if (flag == OperatorFlag.Create || flag == OperatorFlag.CreateDropIfExists || flag == OperatorFlag.CreateIfNotExists)
            {
                //删除前提 下:
                //1 外键 要 先删除有关联的表(但创建时有需要先创建不关联的表) 否则 报级联外键 错误. 不可行
                //2 要么 先 删除 外键(但不知道 constraint 只能先查询库里,后再删除) 再 删除 表
                //IList<FkEntity> fkEntries = new Collections.Array<FkEntity>(5 * tableEntries.Length);
                //GetForeiginKey(connection, fkEntries, tableEntries, dialect);
                //if (fkEntries.Count > 0)
                //{
                //    GetDropFk(builder, fkEntries, dialect);
                //}
                //for (int i = 0; i < tableEntries.Length; i++)
                //{
                //    ClassEntity tableEntry = tableEntries[i];
                //    builder.Append(tableEntry.DropTableIfExists?.ToSql(dialect)).Append("\r\n");
                //}
            }

            if (flag == OperatorFlag.Drop || flag == OperatorFlag.DropIfExists)
            {
                string sql1 = builder.ToString();
                if (!string.IsNullOrEmpty(sql1))
                {
                    int result = DatabaseUtils.ExecuteNonQuery(connection, sql1,CommandType.Text,transaction);
                    return result;
                }
                else
                {
                    return -1;
                }
            }
            for (int i = 0; i < tableEntries.Length; i++)
            {
                ClassEntity tableEntry = tableEntries[i];
                builder.Append(CreateTableSql(tableEntry,flag,dialect)).Append("\r\n");
            }
            if (dialect != DbFlag.Sqlite)
            {
                //sqlite 暂时无法外键 名称(比较麻烦 要么从表sql中提取) 怎么删除 
                for (int i = 0; i < tableEntries.Length; i++)
                {
                    ClassEntity tableEntry = tableEntries[i];
                    GetCreateFk(builder, tableEntry);
                }
            }

            string sql = builder.ToString();
            if (!string.IsNullOrEmpty(sql))
            {
                int result = DatabaseUtils.ExecuteNonQuery(connection, sql,CommandType.Text,transaction);
                return result;
            }
            else
            {
                return -1;
            }
        }

        internal static string CreateTableSql(ClassEntity tableEntry, OperatorFlag flag, DbFlag dialect = DbFlag.MySql)
        {
            if (dialect == DbFlag.None || dialect == DbFlag.Access)
            {
                return string.Empty;
            }
            string prefix = string.Empty;
            string table = tableEntry.Table;
            //语法不同 
            if (dialect == DbFlag.MySql || dialect == DbFlag.Sqlite)
            {
                prefix = DbHelper.PrefixSqlByMySqlOrSqlite(OperatorTypeFlag.Table, flag);
                if (flag == OperatorFlag.CreateDropIfExists)
                {
                    prefix = string.Format(prefix, table);
                }
            }
            else if (dialect == DbFlag.SqlServer)
            {
                prefix = PrefixSqlBySqlserver(flag);
                prefix = string.Format(prefix, tableEntry.Table);
            }
            else if (dialect == DbFlag.Oracle)
            {
                prefix = PrefixSqlByOracle(OperatorTypeFlag.Table, flag);
            }
            if (string.IsNullOrEmpty(prefix))
            {
                return string.Empty;
            }
            switch (flag)
            {
                case OperatorFlag.Drop:
                case OperatorFlag.DropIfExists:
                    return $"{prefix} {table};";
            }

            string sql = CreateTableSqlByColumn(tableEntry, dialect);
            sql = $"{prefix}{sql}";
            return sql;

        }
        private static string PrefixSqlByOracle(OperatorTypeFlag operatorTypeFlag, OperatorFlag flag)
        {
            if (operatorTypeFlag == OperatorTypeFlag.None)
            {
                throw new System.NotSupportedException();
            }
            string name = operatorTypeFlag.ToString().ToUpper();
            switch (flag)
            {
                case OperatorFlag.Drop:
                case OperatorFlag.DropIfExists:
                    return $"DROP {name}  ";
                case OperatorFlag.CreateDropIfExists:
                case OperatorFlag.CreateIfNotExists:
                    return $"CREATE OR REPLACE {name} ";
                case OperatorFlag.Create:
                default: return $"CREATE {name}  ";
            }
        }

        private static string PrefixSqlBySqlserver(OperatorFlag flag)
        {
            switch (flag)
            {
                case OperatorFlag.Drop:
                case OperatorFlag.DropIfExists:
                case OperatorFlag.CreateDropIfExists:
                    return "if OBJECT_ID('{0}') is not  null drop table ";
                case OperatorFlag.CreateIfNotExists:
                case OperatorFlag.Create:
                default:
                    return "if OBJECT_ID('{0}') is  null create table ";
            }
        }


        /**
         *
        sqlite:insert into `table1`(id,bb,name) values(1,1,1); insert into `table1`(id,bb,name) values(1,1,1),(2,2,2)  ;  insert into `table1`(`id`,bb,`name`) values(1,1,1),(2,2,2)  ; 
         sqlite:

        drop TABLE if  EXISTS "main"."table";

        CREATE TABLE if not EXISTS "main"."table" (
          "a" integer NOT NULL ,
          "b" integer NOT NULL ,
          "c" integer NOT NULL,
	        "d" integer default 1 CHECK('d'>0) ,
        PRIMARY KEY ("a", "b", "c")
        )
        WITHOUT ROWID;

        CREATE TRIGGER if not EXISTS "main"."test2"
        BEFORE DELETE
        ON "test1"
        FOR EACH ROW
        begin
                delete  from "test1"
                where a=old.a ;
            end;
		

        CREATE TABLE if not EXISTS "main"."test2" (
          "aa" integer NOT NULL DEFAULT 1 CHECK("aa">0) CONSTRAINT `aa` PRIMARY KEY AUTOINCREMENT ,
          "bb" integer   DEFAULT 1 CHECK("bb">0) ,
	        CONSTRAINT "bb"  FOREIGN KEY ("bb") REFERENCES "test1" ("b") ON DELETE Set Default ON UPDATE NO ACTION DEFERRABLE INITIALLY DEFERRED
        );
        SELECT "a",'b',`c`,d from [table];

       CREATE UNIQUE INDEX "IX_Clients_ClientId" ON "Clients" ("ClientId");

       CREATE INDEX "IX_ClientScopes_ClientId" ON "ClientScopes" ("ClientId");

        mysql:insert into `table1`(id,bb,name) value(1,1,1); insert into `table1`(id,bb,name) value(1,1,1),(2,2,2)  ;  insert into `table1`(`id`,bb,`name`) value(1,1,1),(2,2,2)  ; 

        mysql:
         SELECT VERSION();
         insert into `table1`(id,bb,name) value(1,1,1),(2,2,2)  ; 
        drop TABLE if  EXISTS `table`;
        drop TABLE if  EXISTS `table1`;
        create TABLE if not EXISTS `table1`(
        `id`  int not null ,
        `bb`  int not null ,
        `name` int  null default 1 CHECK('name'>0),
        xx varchar(20) default '1' ,
        PRIMARY key(`id`,`bb`)
        );

        create TABLE if not EXISTS `table`(
        `id`  int not null  CHECK(`id`>0)    PRIMARY key AUTO_INCREMENT ,
        `bb`  int not null  ,
        `name` int   not null  default 1 CHECK('name'>0),
         CONSTRAINT `FK_`  FOREIGN KEY (`bb`) REFERENCES `table1` (`id`) ON DELETE CASCADE
        )

        oracle:
         CREATE OR REPLACE TABLE  tableName 
        (
        `a` NUMBER [ PRIMARY KEY] NOT NULL,
        `b` varchar(255) NOT NULL ,
        [s_cid INT REFERENCES class(cid),]
        [CONSTRAINT fk_student_class FOREIGN KEY(s_cid) REFERENCES class(cid),]
       [PRIMARY KEY(`a`)]
       [FOREIGN KEY (s_cid) REFERENCES class(cid)]
        );

        sqlserver: IF OBJECT_ID('tableName') is  null CREATE TABLE  
        (
        `a` INT [Identity(1,1) PRIMARY KEY | PRIMARY KEY] NOT NULL,
         `b` varchar(255) NOT NULL ,
        [s_cid INT REFERENCES class(cid),]
       [CONSTRAINT fk_student_class FOREIGN KEY(s_cid) REFERENCES class(cid),]
        [PRIMARY KEY(`a`)]</para>
        [FOREIGN KEY (s_cid) REFERENCES class(cid)]
        );
        

          //sqlite "" '' `` [] 都支持
        //mysql `` 支持 这 
        //sqlserver "" [] 

        exaample:table(id integer);
        */
        protected static string CreateTableSqlByColumn(ClassEntity tableEntry, DbFlag dialect)
        {
            // 注意 primary key  foreign key 放的位置 也 影响 语法
            StringBuilder builder = new StringBuilder();
            builder.Append(tableEntry.Table).Append(" (\r\n");
            var has = false;
            //普通列 
            for (int i = 0; i < tableEntry.PropertyEntities.Count; i++)
            {
                var columnEntry = tableEntry.PropertyEntities[i];
                if (!columnEntry.Valid())
                    continue;
                //这里需要处理下 如果 只有 集合 了 没必要 了(自关联可以(ParentId) )
                if (columnEntry.Flag == ColumnFlag.ForeignKey && !columnEntry.UseForeignKey())
                {
                    continue;
                }
                if (has)
                {
                    builder.Append(",\r\n");
                }
                if (columnEntry.Flag == ColumnFlag.PrimaryKey)
                {
                    columnEntry.IsNull = false;
                }
                builder.Append("  ").Append(columnEntry.Column).Append(" ").Append(columnEntry.DataType).Append(" ")
                                 .Append(columnEntry.IsNull ? " NULL " : " NOT NULL ");
                if (dialect == DbFlag.MySql && columnEntry.Flag == ColumnFlag.PrimaryKey)
                {
                    //default check 不支持
                }
                else
                {
                    if (!string.IsNullOrEmpty(columnEntry.Default))
                    {
                        builder.Append(" DEFAULT ").Append(columnEntry.Default).Append(" ");
                    }
                    columnEntry.ToCheck(builder);
                }

                if (!string.IsNullOrEmpty(columnEntry.Comment))
                {
                    if (dialect == DbFlag.MySql)
                    {
                        builder.Append($" COMMENT  ").Append("'").Append(columnEntry.Comment).Append("'");
                    }
                    if (dialect == DbFlag.SqlServer)
                    {
                        tableEntry.SqlServerCommentBuilder = tableEntry.SqlServerCommentBuilder ?? new StringBuilder(tableEntry.PropertyEntities.Count * 100);
                        tableEntry.SqlServerCommentBuilder.Append("EXEC sys.sp_addextendedproperty @name = N'MS_Description',@value = N'").Append(columnEntry.Comment)
                            .Append("', @level0type = N'SCHEMA',@level0name = N'dbo', @level1type = N'TABLE',@level1name = N'").Append(tableEntry.Table)
                            .Append("', @level2type = N'COLUMN',@level2name = N'").Append(columnEntry.Column).Append("';\r\n");
                    }
                }
                has = true;
            }

            //要么跟列一起创建, 要么 外键 统一放在 末尾 不然 报语法错误 (sqlite)
            bool hasFk = false;//tableEntry.ForeiginKeyQuantity > 0;//建完表后再统一建外键
            if (hasFk)
            {
                for (int i = 0; i < tableEntry.FkEntities.Count; i++)
                {
                    var columnEntry = tableEntry.FkEntities[i];
                    //这里需要处理下 如果 只有 集合 了 没必要 了(自关联可以(ParentId) )
                    if (!columnEntry.UseForeignKey())
                    {
                        continue;
                    }
                    if (!columnEntry.FKColumnEntity.Has)
                    {
                        continue;
                    }
                    //CONSTRAINT fk_student_class FOREIGN KEY(s_cid) REFERENCES class(cid)
                    has = columnEntry.ToForeignKey(builder);
                    if (!(dialect == DbFlag.Sqlite || dialect == DbFlag.MySql))
                    {
                        continue;//除了 mysql sqlite 之外 暂时不知道支持 哪些级联
                    }
                    if (has)
                    {
                        //sqlite 不给 时 "" 即 ON DELETE NO ACTION ON UPDATE NO ACTION
                        //mysql 不给 时 "" 即 ON DELETE restrict ON UPDATE restrict
                        // 删除 更新 延迟
                        //builder.Append(" ON DELETE NO ACTION ON UPDATE NO ACTION DEFERRABLE INITIALLY DEFERRED ");
                        if ((dialect == DbFlag.Sqlite && columnEntry.FKColumnEntity.Update != DbFKFlag.None)
                            || dialect == DbFlag.MySql && columnEntry.FKColumnEntity.Update != DbFKFlag.None &&
                            columnEntry.FKColumnEntity.Update != DbFKFlag.SetDefault)
                        {
                            builder.Append(" ON UPDATE ").Append(GetFKFlagKeyWord(columnEntry.FKColumnEntity.Update, dialect));//更新
                        }
                        if ((dialect == DbFlag.Sqlite && columnEntry.FKColumnEntity.Delete != DbFKFlag.None)
                           || dialect == DbFlag.MySql && columnEntry.FKColumnEntity.Delete != DbFKFlag.None &&
                           columnEntry.FKColumnEntity.Delete != DbFKFlag.SetDefault)
                        {
                            builder.Append(" ON DELETE ").Append(GetFKFlagKeyWord(columnEntry.FKColumnEntity.Delete, dialect));//删除
                        }
                        //sqlite 才支持
                        if (dialect == DbFlag.Sqlite && columnEntry.FKColumnEntity.Lazy)
                        {
                            builder.Append(" DEFERRABLE INITIALLY DEFERRED ");//延迟
                        }
                    }
                }
            }
            //sqlserver 主键 自动创建聚集索引 

            //主键 统一放在 末尾 不然 报语法错误
            //主键有多个 只能是 联合主键
            if (tableEntry.PkQuantity > 0 && !tableEntry.IgnorePk)
            {
                builder.Append(",\r\n").Append("  PRIMARY KEY(");
                has = false;
                for (int i = 0; i < tableEntry.PropertyEntities.Count; i++)
                {
                    var columnEntry = tableEntry.PropertyEntities[i];
                    if (columnEntry.Flag == ColumnFlag.PrimaryKey)
                    {
                        if (has)
                        {
                            builder.Append(",");
                        }
                        builder.Append(columnEntry.Column);
                        has = true;
                    }
                }
                builder.Append(")\r\n");
            }
            builder.Append(");");
            string sql = builder.ToString();
            return sql;
        }

        private static string GetFKFlagKeyWord(DbFKFlag fKFlag, DbFlag dialect = DbFlag.MySql)
        {
            //mysql sqlite 都支持
            switch (fKFlag)
            {
                case DbFKFlag.Cascade:
                    return "CASCADE";
                case DbFKFlag.SetNull:
                    return "SET NULL";
                case DbFKFlag.NoAction:
                    return "NO ACTION"; ;
                case DbFKFlag.Restrict:
                    return "RESTRICT"; ;
                case DbFKFlag.SetDefault:
                    if (dialect == DbFlag.MySql)
                    {
                        return string.Empty;
                    }
                    return "SET DEFAULT"; //mysql不支持
                case DbFKFlag.None:
                default:
                    return string.Empty; ;
            }
        }

        private static void GetCreateFk(StringBuilder builder, ClassEntity tableEntry)
        {
            //  alter table work_info  add index(user_id),  add constraint fk_user_id foreign key(user_id) references user_info(id)
            if (!tableEntry.IgnoreFk && tableEntry.FkQuantity>0)
            {
                for (int i = 0; i < tableEntry.FkEntities.Count; i++)
                {
                    if (tableEntry.FkEntities[i].FKColumnEntity.Has)
                    {
                        builder.Append(" alter table ").Append(tableEntry.Table).Append("  add constraint ").Append(tableEntry.FkEntities[i].FKColumnEntity.Constraint)
                  .Append(" foreign key(").Append(tableEntry.FkEntities[i].Column)
                  .Append(") references ").Append(tableEntry.FkEntities[i].FKColumnEntity.ReferenceTable)
                  .Append("(").Append(tableEntry.FkEntities[i].FKColumnEntity.ReferenceId).Append(") ;\r\n");
                    }
                }

            }
        }
        private static void GetDropFk(StringBuilder builder, IList<FkModel> fkEntries)
        {
            //alter table admin_info  drop foreign key fk_file_id
            for (int i = 0; i < fkEntries.Count; i++)
            {
                builder.Append(" alter table ").Append(fkEntries[i].Table).Append("  drop foreign key ").Append(fkEntries[i].Constraint).Append(" ;\r\n");
            }
        }
        #endregion   table sql

    }
}
