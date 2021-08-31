using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utility.Database.Driver;
using Utility.Database.Entities;
using Utility.Database.Mapping.Resolver;
using Utility.Database.Utils;
using Utility.Helpers;

namespace Utility.Database.Provider
{
    /// <summary>
    /// 添加 操作
    /// </summary>
    public abstract partial  class AbstractDbProvider
    {
        #region insert dml


        /// <summary>
        /// 支持对象 new A() 支持简单键值 集合new Dictionary<string,object>(){} 支持匿名类 new {}(或3者组合) (大量任务一起提交不知道主键自增会不会出现错误)
        /// <para>
        /// INSERT INTO table_name ( field1, field2,...fieldN )
        ///                               VALUES
        ///                              (value1, value2,...valueN );
        ///</para>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>

        public static long Insert(IDbConnection connection, ClassEntity tableEntry, object obj,
            bool skipNull = false, DbFlag dialect = DbFlag.MySql, IDbTransaction transaction = null)
        {

            if (connection.GetType().Assembly.FullName.StartsWith(MySqlConnectorDbDriver.Empty.AssemblyName))
            {
                DatabaseUtils.Open(connection);
                //The transaction associated with this command is not the connection's active transaction; see https://fl.vu/mysql-trans
                transaction = transaction ?? connection.BeginTransaction();
                //using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    using (IDbCommand command = (IDbCommand)Activator.CreateInstance(Type.GetType(AbstractReflectDbDriver.CreateDatabaseTypeFactory(DbFlag.MySql).CommandTypeName), new object[] { connection, transaction }))
                    {
                        bool autoCommit = false;
                        long res = Insert(tableEntry, obj, skipNull, ref autoCommit, transaction, command, dialect);
                        if (!autoCommit)
                        {
                            transaction.Commit();
                        }
                        return res;
                    }
                }
            }
            transaction = transaction ?? connection.BeginTransaction();
           // using (IDbTransaction transaction = connection.BeginTransaction())
            {

                using (IDbCommand command = connection.CreateCommand())
                {
                    bool autoCommit = false;
                    long res = Insert(tableEntry, obj, skipNull, ref autoCommit, transaction, command, dialect);
                    if (!autoCommit)
                    {
                        transaction.Commit();
                    }
                    return res;
                }
            }
        }

        private static long Insert(ClassEntity tableEntry, object obj, bool skipNull, ref bool autoCommit, IDbTransaction transaction, IDbCommand command, DbFlag dialect = DbFlag.MySql,
             Func<string, DbFlag, object> getDefaultValue = null, bool cascade = true, string cascadeId = "")
        {
            if (obj == null)
            {
                return -1;
            }
            string sql;
            if (skipNull)
            {
                sql = GetInsertSqlBySkipNull(tableEntry, obj, command, string.Empty);
            }
            else
            {
                sql = tableEntry.SqlEntry.GetCache(SqlEntity.Insert);
                sql = ToIdentityInsertSql(dialect, sql, tableEntry.IsIdentity);
                BindInsert(tableEntry, command, obj, getDefaultValue, dialect);
            }
            command.CommandText = sql;


            long result = 0;

            //数据绑定完 这里 执行 外键 子集 
            if (tableEntry.FkQuantity>0)
            {
                bool first = false; // 如果有多个了 这只能执行一次 
                bool c = autoCommit;
                bool GetId()
                {
                    if (tableEntry.IsIdentity)
                    {
                        //应该最后一次提交
                        //暂时 先 这样
                        c = false;
                         ExecuteInsert(tableEntry, transaction, command, obj, tableEntry.IsIdentity, c);//有自增绑定自增id 
                        result++;
                        //transaction.Commit();
                        //transaction = command.Connection.BeginTransaction();
                        // autoCommit = true;//语法不支持
                    }
                    else
                    {
                        c = false;
                        ExecuteInsert(tableEntry, transaction, command, obj, c);//有自增绑定自增id 
                        result++;
                    }
                    //缓存 外键 id
                    cascadeId = string.Empty.Equals(cascadeId) ? Guid.NewGuid().ToString() : cascadeId;
                    string fkid = tableEntry.IdEntities[0].GetValue(obj).ToString();
                    command.Parameters.Clear();//每次执行完清空参数
                    return CascadeCache.Get(cascadeId, fkid);
                }
                for (int i = 0; i < tableEntry.FkEntities.Count; i++)
                {
                    PropertyEntity columnEntry = tableEntry.FkEntities[i];
                    //是否 允许 级联 操作 默认 允许
                    if (cascade && columnEntry.FKColumnEntity.Many != null)
                    {
                        //外键子集 添加操作
                        var objs = columnEntry.FKColumnEntity.Many.GetValue(obj);
                        if (objs != null && objs is IEnumerable list)
                        {
                            //没执行 外键 id 怎么 绑定 上去 ,要么 先 执行 后 绑定,再次修改 id 相当于 是 多次 操作影响性能
                            //(默认 : 自增 id 拿取到 ,高并发 分布式情况 下 如何 做到 id 同步, 不适用 自增 id 要么一个个)
                            //这里要 开始 执行  获取 主键 如何不是 数据库 生成 的id 则直接绑定外键 
                            if (!first)
                            {
                                if (GetId())
                                {
                                    continue;//怎样 关联 
                                    //throw new NotSupportedException("多次出现该外键!");
                                }
                                autoCommit = c;
                                first = true;
                            }
                            command.Parameters.Clear();//每次执行完清空参数
                            IEnumerator enumerator = list.GetEnumerator();
                            if (enumerator.MoveNext())
                            {
                                //有数据
                                ClassEntity entry = columnEntry.FKColumnEntity.ForeignKeyClassEntry;
                                foreach (object it in list)
                                {

                                    if (entry.FkQuantity > 0)
                                    {
                                        foreach (var item in entry.FkEntities)
                                        {
                                            if (item.FKColumnEntity.ForeignKeyClassEntry.ClassType == tableEntry.ClassType)
                                            {
                                                item.FKColumnEntity.SetValue1(it, obj);//绑定外键
                                            }
                                        }
                                    }
                                    //递归子集 重复终止
                                    result += Insert(entry, it, skipNull, ref autoCommit, transaction, command, dialect, getDefaultValue, cascade, cascadeId);
                                }
                            }
                        }
                    }
                    else if (cascade && columnEntry.FKColumnEntity.Single != null)
                    {
                        if (!first)
                        {
                            GetId();
                            autoCommit = c;
                            first = true;
                        }
                        //有数据
                        ClassEntity entry = columnEntry.FKColumnEntity.ForeignKeyClassEntry;
                        var sing = columnEntry.FKColumnEntity.Single.GetValue(obj);
                        if (sing != null)
                        {
                            command.Parameters.Clear();//每次执行完清空参数
                            BindInsert(entry, command, sing, getDefaultValue, dialect);
                        }
                    }
                }
            }
            else
            {
                ExecuteInsert(tableEntry, transaction, command, obj, tableEntry.IsIdentity, autoCommit);//有自增绑定自增id 
            }
            return result;

        }

        internal static string ToIdentityInsertSql(DbFlag dialect, string sql, bool identity)
        {
            if (identity)
            {
                switch (dialect)
                {
                    case DbFlag.Oracle:
                        break;
                    case DbFlag.MySql:
                        sql += "select LAST_INSERT_ID();";
                        break;
                    case DbFlag.SqlServer:
                        sql += "SELECT IDENT_CURRENT";
                        break;
                    case DbFlag.Postgre:
                        break;
                    case DbFlag.Sqlite:
                        sql += "select LAST_INSERT_ID();";
                        break;
                    case DbFlag.Access:
                        break;
                    default:
                        break;
                }
            }
            return sql;
        }

        internal static long ExecuteInsert(ClassEntity tableEntry, IDbTransaction transaction, IDbCommand command, object obj, bool identity = true, bool autoCommit = true)
        {
            long result = -1;
            if (tableEntry.PkQuantity>0 && identity)
            {
                using (IDataReader reader = DatabaseUtils.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        result = reader.GetInt64(0);
                        if (identity)
                        {
                            ReflectHelper.SetValue(obj, tableEntry.IdEntities[0].PropertyName, Convert.ChangeType(result, tableEntry.IdEntities[0].PropertyType));
                        }
                    }
                }
            }
            else
            {
                //Nested transactions are not supported. 
                result = DatabaseUtils.ExecuteNonQuery(command, transaction, autoCommit);
            }
            return result;
        }


        /// <summary>
        /// 添加时 相互 引用(关联)如何处理死循环或重复数据入库(暂时不管了:前提 不能相互引用(手动去引用,像nhibernate 那样, 自己去管理) ,否则 栈溢出 异常)
        /// </summary>
        /// <param name="classEntity"></param>
        /// <param name="command"></param>
        /// <param name="obj"></param>
        /// <param name="getDefaultValue"></param>
        /// <param name="dialect"></param>
        private static void BindInsert(ClassEntity classEntity, IDbCommand command, object obj, Func<string, DbFlag, object> getDefaultValue = null, DbFlag dialect = DbFlag.MySql)
        {
            foreach (var columnEntry in classEntity.PropertyEntities)
            {
                if (!columnEntry.Valid())
                {
                    continue;
                }
                if (columnEntry.Flag == ColumnFlag.PrimaryKey && columnEntry.Identity)
                {
                    continue;
                }
                object val;
                if (columnEntry.Flag == ColumnFlag.ForeignKey && columnEntry.FKColumnEntity.Has)
                {
                    val = columnEntry.FKColumnEntity.GetValue1(obj);//关联 单 关系不处理 
                }
                else
                {
                    val = columnEntry.GetValue(obj);
                    if (val == null)
                    {
                        val = getDefaultValue?.Invoke(columnEntry.Default, dialect);
                    }
                }
                DatabaseUtils.SetCommandParamter(command, columnEntry.ColumnForamat, val);
            }
        }

        /// <summary>
        /// 获取 insert sql 参数化
        /// </summary>
        /// <param name="tableEntry"></param>
        /// <param name="obj"></param>
        /// <param name="command"></param>
        /// <param name="suffix"></param>
        /// <param name="dialect"></param>
        /// <returns></returns>
        private static string GetInsertSqlBySkipNull(ClassEntity tableEntry, object obj, IDbCommand command, string suffix = "", DbFlag dialect = DbFlag.MySql)
        {
            StringBuilder builderSuffix = new StringBuilder(10 * tableEntry.PropertyEntities.Count); //StringBuilderPool.Get();
            StringBuilder builderPrefix = new StringBuilder(10 * tableEntry.PropertyEntities.Count); //StringBuilderPool.Get();
            bool has = false;
            for (int i = 0; i < tableEntry.PropertyEntities.Count; i++)
            {
                PropertyEntity columnEntry = tableEntry.PropertyEntities[i];
                if (!columnEntry.Valid())
                {
                    continue;
                }
                // 自增 必须主键
                if (columnEntry.Identity)
                {
                    continue;
                }
                object val;
                if (columnEntry.Flag == ColumnFlag.ForeignKey)
                {
                    val = columnEntry.FKColumnEntity.GetValue1(obj);
                }
                else
                {
                    val = ReflectHelper.GetValue(obj, columnEntry.PropertyName);// item.GetValue(obj);//值不一样 引用不一样导致的
                }
                if (val == null)
                {
                    continue;
                }
                if (TypeHelper.IsString(columnEntry.PropertyType))
                {
                    if (string.IsNullOrEmpty(val.ToString()))
                    {
                        continue;
                    }
                }
                if (has)
                {
                    builderPrefix.Append(",");
                    builderSuffix.Append(",");
                }
                builderPrefix.Append(columnEntry.Column);
                builderSuffix.Append(columnEntry.ColumnForamat);
                DatabaseUtils.SetCommandParamter(command, columnEntry.ColumnForamat, val);
                has = true;
            }
            // var sql = $" INSERT INTO {tableEntry.TableSql.ToSql(dialect)} ({tableEntry.SqlEntry.GetCache(SqlEntity.InsertPrefix)}) VALUE{(dialect== Dialect.MySql?"S":string.Empty)}({builderSuffix.ToString()});";
            var sql = $" INSERT INTO {tableEntry.Table} ({builderPrefix.ToString()}) VALUES({builderSuffix.ToString()});";
            //builderPrefix.Clear();
            //StringBuilderPool.Release(builderPrefix);
            //builderSuffix.Clear();
            //StringBuilderPool.Release(builderSuffix);
            return sql;
        }


        //public long InsertDynamic(IDbConnection connection,ClassEntity classEntity, object obj, string tab = null)
        //{
        //    if (obj == null)
        //    {
        //        throw new ArgumentNullException();
        //    }
        //    if (!(obj.GetType().Name.Contains("AnonymousType") || obj.GetType().FullName.Equals("System.Dynamic.ExpandoObject")))
        //    {
        //        Insert(connection, classEntity, obj, true);
        //    }
        //    IDbCommand command = connection.CreateCommand();
        //    IDbTransaction transaction = connection.BeginTransaction();
        //    long result = 0;
        //    InsertDynamicSql(ref result, classEntity, obj, command, ref transaction, tab);
        //    transaction.Commit();
        //    transaction.Dispose();
        //    command.Dispose();
        //    return result;
        //}
        //private void InsertDynamicSql(ref long result,ClassEntity classEntity, object obj, IDbCommand command, ref IDbTransaction transaction, string tab = null, object id = null)//多层递归了 外键 如何更新 多线程 多进程 分布式 情况下不考虑
        //{
        //    var type = obj.GetType();
        //    ClassEntity tableEntry = null;
        //    string sql = string.Empty;
        //    IEnumerable<dynamic> lists = obj as IEnumerable<dynamic>;
        //    //没法 区分 匿名类型
        //    if (type.Name.Contains("AnonymousType")
        //        || type.FullName.Equals("System.Dynamic.ExpandoObject")
        //        || lists != null)
        //    {
        //        //其他 子集 怎么 确定
        //        if (lists != null)
        //        {
        //            if (tableEntry == null)
        //            {
        //                throw new Exception($"TableEntry 找不到,tab :{tab ?? "null"}!");
        //            }
        //            foreach (var item in lists)
        //            {
        //                InsertDynamicSql(ref result,classEntity, item, command, ref transaction, classEntity.Table, id);
        //            }
        //            return;
        //        }
        //        if (obj is IDictionary<string, object> objs)//System.Dynamic.ExpandoObject
        //        {
        //            var keys = objs.Keys.ToList();
        //            tableEntry = GetTableModelByExpandoObject(objs, keys, tab);
        //            var data = objs.Keys.ToList().Find(it => it.ToLower() == "data");
        //            if (data != null)
        //            {
        //                InsertDynamicSql(ref result, classEntity, objs[data], command, ref transaction, tableEntry.Table, id);
        //                return;
        //            }
        //            InsertDynamicSql(ref result, objs, null, null, command, ref transaction, tableEntry, id);
        //            var children = GetKey(keys, "children");
        //            if (children != null)
        //            {
        //                if (id == null)
        //                {
        //                    //获取 外键 
        //                    //id = this.Identity.GetId(tableEntry.Table);
                     
        //                }
        //                if (!tableEntry.HasPrimaryKey)
        //                {
        //                    throw new Exception("not exists pk");
        //                }
        //                if (objs[children] == null)
        //                {
        //                    return;
        //                }
        //                InsertDynamicSqlItemOrList(ref result, classEntity, objs[children], command, ref transaction, tableEntry.Table, id);
        //            }
        //        }
        //        else
        //        {
        //            var pros = type.GetProperties().ToList();
        //            tableEntry = GetTableEntryByDynamic(classEntity,obj, pros, tab);
        //            var pr = pros.Find(it => it.Name.ToLower() == "data"); //new { table = "a",data=new {col1=1,col2=2} };
        //            if (pr != null && pr.ReflectedType.Name.Contains("AnonymousType"))
        //            {
        //                var temp = pr.GetValue(obj);
        //                InsertDynamicSql(ref result, temp, command, ref transaction, tableEntry.Table, id);
        //                return;
        //            }
        //            InsertDynamicSql(ref result, null, pros, obj, command, ref transaction, tableEntry, id);
        //            pr = pros.Find(it => it.Name.ToLower() == "children");
        //            if (pr != null && pr.ReflectedType.Name.Contains("AnonymousType"))
        //            {
        //                if (id == null)
        //                {
        //                    id = this.Identity.GetId(tableEntry.Table);
        //                }
        //                if (!tableEntry.HasPrimaryKey)
        //                {
        //                    throw new Exception("not exists pk");
        //                }
        //                var temp = pr.GetValue(obj);
        //                if (temp == null)
        //                {
        //                    return;
        //                }
        //                InsertDynamicSqlItemOrList(ref result, tableEntry, temp, command, ref transaction, tableEntry.Table, id);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (obj is IEnumerable enumerable)
        //        {
        //            tableEntry = Mapp(type.GenericTypeArguments[0]);
        //            IEnumerator enumerator = enumerable.GetEnumerator();
        //            while (enumerator.MoveNext())
        //            {
        //                Insert(enumerator.Current, true, id, transaction, command);
        //            }
        //        }
        //        else
        //        {
        //            tableEntry = Mapp(type);
        //            Insert(obj, true, id, transaction, command);
        //        }
        //    }
        //}
        //private void InsertDynamicSqlItemOrList(ref long result, ClassEntity tableEntry, object obj, IDbCommand command, ref IDbTransaction transaction, string tab = null, object id = null)
        //{
        //    if (obj is IEnumerable<dynamic>)
        //    {
        //        foreach (var item in ((IEnumerable<dynamic>)obj))//new { table = "a",data=new {col1=1,col2=2},children=new List<dynamic>(){new {col1=1,col2}} };
        //        {
        //            InsertDynamicSql(ref result, item, command, ref transaction, tableEntry.Table, id);
        //        }
        //    }
        //    else if (obj is IEnumerable)
        //    {
        //        foreach (var item in ((IEnumerable)obj))//泛型类型未知;
        //        {
        //            InsertDynamicSql(ref result, item, command, ref transaction, tableEntry.Table, id);
        //        }
        //    }
        //    else
        //    {
        //        InsertDynamicSql(ref result, obj, command, ref transaction, tableEntry.Table, id);
        //    }
        //}
        //private string GetKey(List<string> keys, string key)
        //{
        //    return keys.Find(it => it.ToLower() == key);
        //}
        //private ClassEntity GetTableModelByExpandoObject(IDictionary<string, object> objs, List<string> keys, string tab = null)
        //{
        //    TableEntry tableEntry=null;
        //    var table = GetKey(keys, "table");
        //    if (table != null)
        //    {
        //        //tableEntry = TableCache.GetXmlMapp(null, objs[table].ToString());//new { table = "a",data=new {col1=1,col2=2} };
        //    }
        //    else
        //    {
        //        //tableEntry = TableCache.GetXmlMapp(null, tab);
        //    }
        //    return tableEntry;
        //}
        //private ClassEntity GetTableEntryByDynamic(ClassEntity classEntity,object obj, List<PropertyInfo> pros, string tab = null)
        //{
        //    ClassEntity fkClassEntity=null;//外键 表
        //    var table = pros.Find(it => it.Name.ToLower() == "table");//new { table = "a",col1=1,col2=2 };
        //    if (!classEntity.HasForeignKey)
        //    {
        //        throw new Exception("not has relation foreign key");
        //    }
        //    if (table != null)
        //    {
        //        string tableName = table.Name;
        //        //存在 可能 有 1-n 个 外键
        //        foreach (var item in classEntity.FkEntities)
        //        {
        //            if (item.ForeignKeyColumnEntity.ReferenceTable.Equals(tableName))
        //            {
        //                fkClassEntity = item.ForeignKeyColumnEntity.ForeignKeyTableEntry;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        fkClassEntity = classEntity.FkEntities[0].ForeignKeyColumnEntity.ForeignKeyTableEntry;
        //    }
        //    return fkClassEntity;
        //}
        //private void InsertDynamicSql(ref long result, IDictionary<string, object> objs, List<PropertyInfo> pros, object obj, IDbCommand command, ref IDbTransaction transaction, TableEntry tableEntry, object id = null)//多层递归了 外键 如何更新 多线程 多进程 分布式 情况下不考虑
        //{
        //    var keys = (List<string>)null;
        //    bool isDynamic = true;
        //    if (objs != null)
        //    {
        //        keys = objs.Keys.ToList();
        //        isDynamic = false;
        //    }
        //    string sql = tableEntry.SqlEntry.GetSql(SqlEntry.Insert);
        //    foreach (var item in tableEntry.Entries)
        //    {
        //        if (item is IdEntry idEntry && idEntry.Identity)
        //        {
        //            continue;
        //        }
        //        string str = string.Empty;
        //        string column = string.Empty;
        //        object val = (object)null;
        //        if (item is BaseColumnEntry baseColumnEntry)
        //        {
        //            column = baseColumnEntry.Column;
        //            str = $"@{baseColumnEntry.Column.Replace("`", string.Empty)}";
        //        }
        //        else if (item is FKEntry fKEntry)
        //        {
        //            FKGroupEntry fKGroupEntry = tableEntry.GetFKGroupEntry(fKEntry);
        //            column = fKGroupEntry.ReferenceColumn;
        //            str = $"@{fKGroupEntry.ReferenceColumn.Replace("`", string.Empty)}";
        //            val = id;//如果有多个外键了 先 不管
        //        }
        //        if (isDynamic)
        //        {
        //            var pro = pros.Find(it => it.Name.ToLower() == column.Replace("`", "").ToLower());
        //            if (pro != null)
        //            {
        //                val = pro.GetValue(obj);
        //            }
        //        }
        //        else
        //        {
        //            var key = GetKey(keys, column.Replace("`", "").ToLower());
        //            if (key != null)
        //            {
        //                val = objs[key];
        //            }
        //        }
        //        if (val == null && item is ColumnEntry columnEntry)
        //        {
        //            val = GetDefaultValue(columnEntry.Default);
        //        }
        //        DatabaseUtils.SetCommandParamter(command, str, val);

        //    }
        //    sql = OrmUtils.ToIdentityInsertSql(Dialect, sql, tableEntry.HasIdentity);
        //    command.CommandText = sql;
        //    long res = OrmUtils.ExecuteInsert(tableEntry, command, obj, false);
        //    id = res;
        //    Identity.SetId(tableEntry.Table, res);
        //    result += res;
        //    command.Parameters.Clear();//清空 不然 name key 冲突
        //}
        ///// <summary>
        ///// 
        ///// <para>错误语句</para>
        ///// <para>
        ///// INSERT INTO table_name ( field1, field2,...fieldN )
        /////                               VALUE
        /////                              (value1, value2,...valueN )
        /////                              SELECT (value1, value2,...valueN ) UNION
        /////                              SELECT (value1, value2,...valueN );
        /////</para>
        /////<para>正确</para>
        /////<para>INSERT INTO table_name (列1, 列2,...) select 值1, 值2,.... from table_name2 </para>
        /////<para>INSERT INTO table_name (列1, 列2,...) </para>
        /////<para>select 值1, 值2,....</para>
        ///// </summary>
        ///// <param name="objs"></param>
        ///// <param name="isNull"></param>
        //public int InsertBatch(IDbConnection connection, object[] objs, bool isNull = true, string tab = null)
        //{
        //    if (objs == null || objs.Length == 1)
        //    {
        //        return -1;
        //    }
        //    else if (objs.Length == 1)
        //    {
        //        if (tab == null)
        //        {
        //            return (int)Insert(connection, objs[0], isNull);
        //        }
        //        else
        //        {
        //            return (int)InsertDynamic(objs[0], tab);
        //        }
        //    }

        //    ClassEntity tableEntry = null;
        //    if (tableEntry.HasPrimaryKey && tableEntry.IsIdentity)
        //    {
        //        //存在子集比较麻烦了 主要是自增需要原子性(外键用到)
        //        foreach (var item in objs)
        //        {
        //            if (tab == null)
        //            {
        //                Insert(connection, item, isNull);
        //            }
        //            else
        //            {
        //                InsertDynamic(connection, item, tab);
        //            }
        //        }
        //        return 1;
        //    }
        //    StringBuilder builder = new StringBuilder(100 * objs.Length);
        //    string sql = string.Empty;
        //    IDbCommand command = connection.CreateCommand();
        //    if (isNull)
        //    {
        //        builder.Append(" INSERT INTO ").Append(tableEntry.Table);
        //        builder.Append("(").Append(tableEntry.SqlEntry.GetCache(SqlEntity.InsertPrefix)).Append(") ");
        //        for (int i = 0; i < objs.Length; i++)
        //        {
        //            builder.Append(" SELECT ");
        //            //自身
        //            for (int j = 0; j < tableEntry.ColumnEntries.Count; j++)
        //            {
        //                var item = tableEntry.ColumnEntries[j];
        //                if (item != null)
        //                {
        //                    var columnName = $"@{item.Column.Replace("`", string.Empty)}{i}";
        //                    builder.Append(columnName);
        //                    object val = item.GetValue(objs[i]);
        //                    if (item != null)
        //                    {
        //                        val = val ?? GetDefaultValue(item.Default);
        //                    }
        //                    DatabaseUtils.SetCommandParamter(command, columnName, val);
        //                }
        //                else if (fKGroupEntry != null)
        //                {
        //                    var columnName = $"@{fKGroupEntry.ReferenceColumn.Replace("`", string.Empty)}{i}";
        //                    builder.Append(columnName);
        //                    object val = item.GetValue(objs[i]);
        //                    DatabaseUtils.SetCommandParamter(command, fKGroupEntry.ReferenceColumn, val);
        //                }
        //                if (j != tableEntry.Entries.Count - 1)
        //                {
        //                    builder.Append(",");
        //                }
        //            }
        //            //关联外键
        //            if (i != objs.Length - 1)
        //                builder.Append(" UNION ");
        //        }
        //        sql = builder.ToString();
        //    }
        //    else
        //    {
        //        for (int i = 0; i < objs.Length; i++)
        //        {
        //            builder.Append(DbHelper.Insert(connection, tableEntry, objs[i], command, i.ToString())).Append(";");
        //            sql = builder.ToString();
        //        }
        //    }
        //    command.CommandText = sql;
        //    int result = DatabaseUtils.ExecuteNonQuery(command);
        //    return result;
        //}
        #endregion insert dml
    }
}
