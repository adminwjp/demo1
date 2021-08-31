using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Database.Entities;
using Utility.Database.Utils;

namespace Utility.Database.Provider
{
    /// <summary>
    /// 获取 数据库 自增 信息
    /// </summary>
    public abstract partial  class AbstractDbProvider
    {
        #region identity

        /// <summary>
        /// 获取自增id
        /// <para>mysql:参考 https://www.cnblogs.com/w3chen/p/4441512.html</para>
        /// <para>新增表连续的 没错误 才行(实际不可行) :select max(id) from table;</para>
        /// <para>返回最后一个INSERT或 UPDATE 查询中， AUTO_INCREMENT列设置的第一个表的值(实际不可行):select LAST_INSERT_ID();</para>
        /// <para>下一个自增ID的数值(实际可行):show table status where Name='table'</para>
        /// <para>下一个自增ID的数值(实际可行):select table_name, AUTO_INCREMENT from information_schema.tables where table_name="table"</para>
        /// <para>@@IDENTITY表示最近一次向具有identity属性（auto_increment）的表INSERT数据时对应的自增列的值。此处得到的值是0(实际不可行):select @@IDENTITY;</para>
        public static string GetIdentitySql(DbFlag dialect = DbFlag.MySql)
        {
            //db identity sql
            switch (dialect)
            {
                case DbFlag.MySql:
                    return "select  AUTO_INCREMENT from information_schema.tables where ";//select  AUTO_INCREMENT from information_schema.tables where [table_name=@table]
                case DbFlag.SqlServer:
                    return "SELECT IDENT_CURRENT('#table')";//https://www.cnblogs.com/idotnet8/articles/725336.html
                case DbFlag.Sqlite:
                    return "SELECT LAST_INSERT_ROWID()";//SELECT LAST_INSERT_ROWID() [from table]

                case DbFlag.Access:
                    break;
                case DbFlag.Oracle:
                    break;
                case DbFlag.Postgre:
                    break;
                case DbFlag.None:
                default:
                    break;
            }
            return string.Empty;
        }

        public static long FindIdentityId(DbConnection connection, string table, DbFlag dialect = DbFlag.MySql)
        {
            string sql = GetIdentitySql(dialect);
            if (string.IsNullOrEmpty(sql))
            {
                return -1;
            }
            if (dialect == DbFlag.MySql)
            {
                sql += "table_schema=@name ";
                using (IDbCommand command = DatabaseUtils.CreateCommand(connection, sql))
                {
                    DatabaseUtils.SetCommandParamter(command, "@name", $"{SqlConstant.KeyWordReplace(table,dialect)}");
                    var ids = DatabaseUtils.ReaderLong(command);
                    return ids != null && ids.Length > 0 ? ids[0] : -1;
                }
            }
            else if (dialect == DbFlag.Sqlite)
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"{sql} from \"{table}\"";
                    long id = -1;
                    long[] ids = DatabaseUtils.ReaderLong(command);
                    if (ids.Length == 1)
                    {
                        id = ids[0];
                    }
                    return id;
                }
            }
            else if (dialect == DbFlag.SqlServer)
            {
                sql = sql.Replace("#table", table);
                using (IDbCommand command = DatabaseUtils.CreateCommand(connection, sql))
                {
                    var ids = DatabaseUtils.ReaderLong(command);
                    return ids != null && ids.Length > 0 ? ids[0] : -1;
                }
            }
            return -1;
        }


        public static long[] FindIdentityId(IDbConnection connection, string[] tables, DbFlag dialect = DbFlag.MySql)
        {
            string sql = GetIdentitySql(dialect);
            if (string.IsNullOrEmpty(sql))
            {
                return null;
            }
            if (dialect == DbFlag.MySql)
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    const string sufix = "table_schema=@name";
                    int i = 0;
                    string[] names = new string[tables.Length];
                    //sql += string.Join(" OR ", tables.Select(it => {
                    //    string str = $"{sufix}{i}";
                    //    names[i] = str.Split('=')[1].Trim();
                    //    tables[i] = $"`{it}`";
                    //    i++;
                    //    return str;
                    //}));
                    StringBuilder builder = new StringBuilder(10 * tables.Length);
                    for (; i < tables.Length; i++)
                    {

                        string str = $"{sufix}{i}";
                        names[i] = str.Split('=')[1].Trim();
                        tables[i] = $"{SqlConstant.KeyWordReplace(tables[i],dialect)}";
                        builder.Append(str);
                        if (i != tables.Length - 1)
                        {

                            builder.Append(" OR ");
                        }
                    }
                    sql += builder.ToString();
                    DatabaseUtils.SetCommandParamter(command, names, tables);
                    command.CommandText = sql;
                    long[] ids = DatabaseUtils.ReaderLong(command);
                    return ids;
                }
            }
            else if (dialect == DbFlag.Sqlite)
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    string[] names = new string[tables.Length];
                    //var sql1 = string.Join(" union all ", tables.Select(it => {
                    //    return $"{sql} from \"{it}\"";
                    //}));
                    StringBuilder builder = new StringBuilder(10 * tables.Length);
                    for (int i = 0; i < tables.Length; i++)
                    {
                        builder.Append($"{sql} from \"{tables[i]}\"");
                        if (i != tables.Length - 1)
                        {
                            builder.Append(" union all ");
                        }
                    }
                    var sql1 = builder.ToString();
                    command.CommandText = sql1;
                    long[] ids = DatabaseUtils.ReaderLong(command);
                    return ids;
                }
            }
            else if (dialect == DbFlag.SqlServer)
            {
                using (IDbCommand command = DatabaseUtils.CreateCommand(connection, sql))
                {
                    string[] names = new string[tables.Length];
                    //var sql1 = string.Join("", tables.Select(it => {
                    //    return sql.Replace("#table", it);
                    //}));
                    StringBuilder builder = new StringBuilder(sql.Length * tables.Length);
                    foreach (var item in tables)
                    {
                        builder.Append(sql.Replace("#table", item));
                    }
                    var sql1 = builder.ToString();
                    command.CommandText = sql1;
                    long[] ids = DatabaseUtils.ReaderLong(command);
                    return ids;
                }
            }
            return null;
        }


        #endregion identity
    }
}
