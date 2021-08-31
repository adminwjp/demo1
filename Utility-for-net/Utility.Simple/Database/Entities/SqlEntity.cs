using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Database.Entities
{
    /// <summary>普通sql语句缓存 关键字处理 每个数据库关键字处理不同 `table` mysql "table"  sqlserver  "table" 'table' sqlite </summary>
    public class SqlEntity
    {
        /** sql常量key 用于缓存 sql*/

        /// <summary>
        /// 普通的操作 存储 也 支持
        /// </summary>
        public const string Procedure = "Procedure";

        /// <summary>
        /// 普通的操作 视图只支持查询
        /// </summary>
        public const string View = "View";

        /// <summary>
        /// 用于特殊情况下
        /// </summary>
        public const string Trigger = "Trigger";

        /// <summary>
        /// 添加 insert into table(name,name1) values(@name,@name1);
        /// </summary>
        public const string Insert = "Insert";

        /// <summary>
        /// 添加前缀 name,name1
        /// </summary>
        public const string InsertPrefix = "InsertPrefix";

        /// <summary>
        /// 添加后缀 通用 @name,@name1
        /// </summary>
        public const string InsertSufix = "InsertSufix";

        /// <summary>
        /// 更新 update  table name=@name,name1=@name1 [where condnation];
        /// </summary>
        public const string Update = "Update";

        /// <summary>
        /// 更新 update  table name=@name,name1=@name1 where id=@id;
        /// </summary>
        public const string UpdateByWhereId = "UpdateByWhereId";

        /// <summary>
        /// 更新 前缀  a=@a,b=@b
        /// </summary>
        public const string UpdatePrifix = "UpdatePrifix";

        /// <summary>
        /// 删除 delete from  table [where condnation];
        /// </summary>
        public const string Delete = "Delete";


        /// <summary>
        /// 删除 delete from  table  where id=@id;
        /// </summary>
        public const string DeleteByWhereId = "DeleteByWhereId";

        /// <summary>
        /// 查询 select name,name1 from  table [where condnation]; 无效
        /// </summary>
        public const string Select = "Select";

        /// <summary>
        /// 查询 select name,name1 from  table id=@id;
        /// </summary>
        public const string SelectByWhereId = "SelectByWhereId ";

        /// <summary>
        /// 查询列  name,name1 
        /// </summary>
        public const string SelectColumn = "SelectColumn";

        /// <summary>
        /// 查询 select name,name1 from  table ;
        /// </summary>
        public const string SelectAll = "SelectAll";

        /// <summary>
        /// 查询 条件 id=@id
        /// </summary>
        public const string WhereId = "WhereId";

        /// <summary>普通sql语句缓存 </summary>
        protected readonly Dictionary<string, string> Sqls = new Dictionary<string, string>();
        public void Clean()
        {
            Sqls.Clear();
        }
        public string GetCache(string key)
        {
            return Sqls.ContainsValue(key) ? Sqls[key] : null;
        }

#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
        /// <summary>sql语句缓存(增 删 改 查 (增 删 改 查 存储)) (默认生成所有数据库语法(sql关键字(只能单独处理,其他的不能处理,否则ado.net 报语法错误)已处理))</summary>
        public void GeneratorCache(ClassEntity tableEntry)
        {
            int length = 10 * tableEntry.PropertyEntities.Count;
            StringBuilder insertBuilder = new StringBuilder(length);
            StringBuilder insertBuilderFormat = new StringBuilder(length);

            StringBuilder updateBuilder = new StringBuilder(length);

            StringBuilder selectBuilder = new StringBuilder(length);

            bool hasInsert = false;
            bool hasUpdate = false;
            bool hasSelect = false;
            string id = string.Empty;
            int indexId = 0;
            for (int i = 0; i < tableEntry.PropertyEntities.Count; i++)
            {
                PropertyEntity column = tableEntry.PropertyEntities[i];
                //列是否有效
                if (!column.Valid())
                {
                    continue;
                }
                if (column.Flag == ColumnFlag.ForeignKey && !column.FKColumnEntity.Has)
                {
                    continue;
                }
                if (hasSelect)
                {
                    selectBuilder.Append(",");
                }
                selectBuilder.Append(column.Column);
                hasSelect = true;

                //主键自增 insert 不需要
                if (column.Flag == ColumnFlag.PrimaryKey && column.Identity)
                {
                    continue;
                }
                InsertSql(insertBuilder, insertBuilderFormat, column, ref hasInsert);//添加

                UpdateSql(updateBuilder, column, ref hasUpdate);//修改

                if (column.Flag == ColumnFlag.PrimaryKey)
                {
                    if (id.Length > 0)
                    {
                        //联合主键 
                        id += " OR ";
                    }
                    id += $"{column.Column}={column.ColumnForamat}{(indexId == 0 ? "" : indexId.ToString())} ";
                    indexId++;
                }

            }

            //添加 sql
            string insertPrefix = insertBuilder.ToString();
            string insertSuffix = insertBuilderFormat.ToString();
            if (insertPrefix.Length > 0)
            {
                InsertSqlCache(insertPrefix, insertSuffix, tableEntry);//说明有数据
            }

            //修改 sql
            string updatePrefix = updateBuilder.ToString();
            if (updateBuilder.Length > 0)
            {
                UpdateSqlCache(updatePrefix, tableEntry, id);
            }

            DeleteSqlCache(tableEntry, id);//删除 sql

            SelectSqlCache(selectBuilder, tableEntry, id); //查询 sql

            //id sql
            if (id.Length > 0)
            {
                Sqls[WhereId] = id;
            }
        }

        private void SelectSqlCache(StringBuilder selectBuilder, ClassEntity classEntry, string id)
        {
            string column = selectBuilder.ToString();
            if (column.Length > 0)
            {
                //查询列
                Sqls[SelectColumn] = column;

                //查询所有
                string select = $"SELECT {column} FROM {classEntry.Table}";
                Sqls[SelectAll] = select;

                //根据id查询
                if (id.Length > 0)
                {
                    string selectById = $"{select} WHERE {id}";
                    Sqls[SelectByWhereId] = selectById;
                }
            }
        }

        private void DeleteSqlCache(ClassEntity classEntry, string id)
        {
            string delete = $"Delete From {classEntry.Table} ";
            Sqls[Delete] = delete;
            if (id.Length > 0)
            {
                string deleteById = $"{delete} WHERE {id}";
                Sqls[DeleteByWhereId] = deleteById;
            }
        }

        private void UpdateSqlCache(string updatePrefix, ClassEntity classEntry, string id)
        {
            //修改 sql 前缀 name=@name,name1=@name1
            Sqls[UpdatePrifix] = updatePrefix;

            //修改全表 update table set name=@name,name1=@name1
            string update = $"UPDATE {classEntry.Table} SET {updatePrefix}";
            Sqls[Update] = update;

            //根据主键修改
            if (id.Length > 0)
            {
                string updateById = $"{update} WHERE {id}";
                Sqls[UpdateByWhereId] = updateById;
            }
        }


        private void InsertSqlCache(string insertPrefix, string insertSuffix, ClassEntity classEntry)
        {
            //添加 sql
            string insert = $"INSERT INTO {classEntry.Table}({insertPrefix}) VALUES({insertSuffix});";

            Sqls[Insert] = insert;

            //sql 前缀 name,name1
            Sqls[InsertPrefix] = insertPrefix;

            //sql 后缀 @name,@name1
            Sqls[InsertSufix] = insertSuffix;
        }
        /// <summary>
        /// 插入 sql
        /// </summary>
        /// <param name="insertBuilder"></param>
        /// <param name="insertBuilderFormat"></param>
        /// <param name="mysqlInsertBuilder"></param>
        /// <param name="column"></param>
        /// <param name="hasInsert"></param>
        private void InsertSql(StringBuilder insertBuilder, StringBuilder insertBuilderFormat, PropertyEntity column, ref bool hasInsert)
        {
            //添加
            if (hasInsert)
            {
                insertBuilder.Append(",");
            }
            insertBuilder.Append(column.Column);
            insertBuilderFormat.Append(column.ColumnForamat);
            hasInsert = true;
        }

        private void UpdateSql(StringBuilder updateBuilder, PropertyEntity column, ref bool hasUpdate)
        {
            //修改
            if (column.Flag != ColumnFlag.PrimaryKey)
            {
                if (hasUpdate)
                {
                    updateBuilder.Append(",");
                    updateBuilder.Append(",");
                }
                updateBuilder.Append(column.Column).Append("=").Append(column.ColumnForamat);//ado.net 驱动 执行 异常, 不能 转换. 直接 执行sql 支持
                hasUpdate = true;
            }
        }
#endif

    }
}
