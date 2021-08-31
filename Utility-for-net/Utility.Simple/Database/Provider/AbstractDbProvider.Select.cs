using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
    public abstract partial class AbstractDbProvider
    {

        #region select linq
#if !(NET20 || NET30)
        /// <summary>关联集合外键 (外关联 则 要么 一步步查询(再绑定) 要么左连接查询 未实现)</summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> GetList<T>(IDbConnection connection, Func<Type, ClassEntity> func, 
            Expression<Func<T, T, bool>> expression, DbFlag dialect = DbFlag.MySql)
        {
            string where = string.Empty;
            WhereCollectionEntity wheres = new WhereCollectionEntity();
            RecursionGeneratorOrOrAnd(wheres, expression.Body);
            if (wheres.Ors.Count > 0 || wheres.Ands.Count > 0)
            {

            }
            return GetList<T>(connection, func, where, null, dialect);
        }

        /// <summary>
        /// 复杂 linq 提取 sql 条件 用递归  提取linq 好累 啊 复杂情况 了 自己去调试修改
        /// </summary>
        /// <param name="expression"></param>
        internal static void RecursionGeneratorOrOrAnd(WhereCollectionEntity whereCollectionEntry, Expression expression)
        {
            BinaryExpression binaryExpression = expression as BinaryExpression;
            if (binaryExpression != null)
            {
                //left right 深度未知 需要递归遍历
                //需要遍历where 条件生成sql 目前暂时逻辑写的有点死 后期自己去调试改
                WhereEntity whereEntry = null;
                bool or = binaryExpression.NodeType == ExpressionType.OrElse;
                if (binaryExpression.Method == null)
                {
                    if (binaryExpression.NodeType == ExpressionType.OrElse || binaryExpression.NodeType == ExpressionType.AndAlso)
                    {
                        whereEntry = GeneratorWhere(whereCollectionEntry, binaryExpression.Left as BinaryExpression);
                        if (whereEntry != null)
                        {
                            if (or)
                            {
                                whereCollectionEntry.Ors.Add(whereEntry);
                            }
                            else
                            {
                                whereCollectionEntry.Ands.Add(whereEntry);
                            }
                        }
                        whereEntry = GeneratorWhere(whereCollectionEntry, binaryExpression.Right as BinaryExpression);
                        if (whereEntry != null)
                        {
                            if (or)
                            {
                                whereCollectionEntry.Ors.Add(whereEntry);
                            }
                            else
                            {
                                whereCollectionEntry.Ands.Add(whereEntry);
                            }
                        }
                    }

                }
                else
                {
                    whereEntry = GeneratorWhere(whereCollectionEntry, binaryExpression);
                    if (whereEntry != null)
                    {
                        if (or)
                        {
                            whereCollectionEntry.Ors.Add(whereEntry);
                        }
                        else
                        {
                            whereCollectionEntry.Ands.Add(whereEntry);
                        }
                    }
                }
            }
        }
        internal static WhereEntity GeneratorWhere(WhereCollectionEntity whereCollectionEntry, BinaryExpression expression)
        {
            if (expression == null)
            {
                return null;
            }
            WhereFlag whereFlag = WhereFlag.None;
            if (expression.NodeType == ExpressionType.Equal)
            {
                whereFlag = WhereFlag.Equal;
            }
            else if (expression.NodeType == ExpressionType.GreaterThan)
            {
                whereFlag = WhereFlag.Gt;
            }
            else
            {
                RecursionGeneratorOrOrAnd(whereCollectionEntry, expression);
            }
            MemberExpression member = null;
            if (expression.Left != null)
            {
                if (expression.Left.NodeType == ExpressionType.MemberAccess)
                {
                    member = (MemberExpression)expression.Left;
                }
            }
            else if (expression.Right != null)
            {
                if (expression.Right.NodeType == ExpressionType.MemberAccess)
                {
                    member = (MemberExpression)expression.Right;
                }
                else if (expression.Right.NodeType == ExpressionType.OrElse || expression.Right.NodeType == ExpressionType.AndAlso)
                {

                }
            }
            if (member != null)
            {
                Type type = null;
                if (member.Expression != null && member.Expression.NodeType == ExpressionType.Parameter)
                {
                    type = member.Expression.Type;
                }

                WhereEntity whereEntry = new WhereEntity() { Name = member.Member.Name, Flag = whereFlag, TargetType = type };
                if (expression.Right.NodeType == ExpressionType.Constant)
                {
                    //it.name=="1"
                    ConstantExpression constantExpression = (ConstantExpression)expression.Right;
                    whereEntry.Value = constantExpression.Value;
                    whereEntry.ValueType = constantExpression.Type;
                }
                else if (expression.Right.NodeType == ExpressionType.MemberAccess)
                {
                    //it.name==it1.name
                    member = (MemberExpression)expression.Right;
                    if (member.Expression != null && member.Expression.NodeType == ExpressionType.Parameter)
                    {
                        //it1.name
                        type = member.Expression.Type;
                        whereEntry.ValueName = member.Member.Name;
                        whereEntry.ValueType = type;
                    }
                }
                return whereEntry;
            }
            else
            {
                return null;
            }
        }
#endif

        #endregion select linq

        #region select datatable dataset Dictionary 
        public const string SelectAllTable = "Select * from #table";
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
        /// <summary>
        /// 查询 表 数据
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="table">表名</param>
        /// <param name="dialect">方言</param>
        /// <returns></returns>
        public static DataTable GetTable(IDbConnection connection, string table, DbFlag dialect = DbFlag.MySql)
        {
            string sql = SelectAllTable.Replace("#table", table);
            IDbCommand command = connection.CreateCommand();
            command.CommandText = sql;
            IDbDataAdapter dataAdapter = AbstractReflectDbDriver.CreateDatabaseTypeFactory(dialect).CreateDataAdapter(command);
            DataTable dataTable = DatabaseUtils.ExecuteDataTable(dataAdapter);
            DatabaseUtils.DisposeCommand(command);
            return dataTable;
        }

        /// <summary>
        /// 根据 id 查询表 数据
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="classEntity">表映射信息</param>
        /// <param name="id">实体 id</param>
        /// <param name="dialect">方言</param>
        /// <returns></returns>
        public static DataTable GetDataTable(IDbConnection connection, ClassEntity classEntity, object id, DbFlag dialect)
        {
            IDbCommand command = GetQueryCommand(connection, classEntity);
            IDbDataAdapter dataAdapter = AbstractReflectDbDriver.CreateDatabaseTypeFactory(dialect).CreateDataAdapter(command);
            DataTable dataTable = DatabaseUtils.ExecuteDataTable(dataAdapter);
            DatabaseUtils.DisposeCommand(command);
            return dataTable;
        }


        /// <summary>
        ///  查询表 数据
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="classEntity">表映射信息</param>
        /// <param name="id">实体 id</param>
        /// <param name="dialect">方言</param>
        /// <returns></returns>
        public static DataTable GetDataTable(IDbConnection connection, ClassEntity classEntity, DbFlag dialect)
        {
            IDbCommand command = GetQueryCommand(connection, classEntity);
            IDbDataAdapter dataAdapter = AbstractReflectDbDriver.CreateDatabaseTypeFactory(dialect).CreateDataAdapter(command);
            DataTable dataTable = DatabaseUtils.ExecuteDataTable(dataAdapter);
            DatabaseUtils.DisposeCommand(command);
            return dataTable;
        }

        /// <summary>
        ///  查询表 数据
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="classEntity">表映射信息</param>
        /// <param name="where">条件 </param>
        /// <param name="dialect">方言</param>
        /// <returns></returns>

        public static DataTable GetDataTable(IDbConnection connection, ClassEntity tableEntry, string where, DbFlag dialect)
        {
            IDbCommand command = GetQueryCommand(connection, tableEntry);
            command.CommandText += where;
            IDbDataAdapter dataAdapter = AbstractReflectDbDriver.CreateDatabaseTypeFactory(dialect).CreateDataAdapter(command);
            DataTable dataTable = DatabaseUtils.ExecuteDataTable(dataAdapter);
            DatabaseUtils.DisposeCommand(command);
            return dataTable;
        }
#endif

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="func"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IDictionary<string, object> GetMap<T>(IDbConnection connection, Func<Type, ClassEntity> func, object id)
        {
            using (IDbCommand command = GetQueryCommand(connection, func?.Invoke(typeof(T)), id))
            {
                using (IDataReader reader = DatabaseUtils.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        IDictionary<string, object> datas = GetMap(reader);
                        return datas;
                    }
                }
            }
            return (IDictionary<string, object>)null;
        }

        /// <summary>
        /// 根据读取器读取数据
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static IDictionary<string, object> GetMap(IDataReader reader)
        {
            IDictionary<string, object> datas = new Dictionary<string, object>(reader.FieldCount);
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string name = reader.GetName(i);
                object val = reader.GetValue(i);
                datas.Add(name, val);
            }
            return datas;
        }

        /// <summary>
        /// 根据读取器读取数据
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IList<IDictionary<string, object>> GetListMap<T>(IDbConnection connection, Func<Type, ClassEntity> func)
        {
            IList<IDictionary<string, object>> results = new List<IDictionary<string, object>>();
            using (IDbCommand command = GetQueryCommand(connection, func?.Invoke(typeof(T))))
            {
                using (IDataReader reader = DatabaseUtils.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        IDictionary<string, object> datas = GetMap(reader);
                        results.Add(datas);
                    }
                }
            }
            return results;
        }
        #endregion select datatable database Dictionary

        public static T Get<T>(IDbConnection connection, Func<Type, ClassEntity> func, object id, FetchFlag fetch = FetchFlag.None)
        {
            Type type = typeof(T);
            Tuple<IDataReader, IDbCommand> tuple = GetDataReaderAndCommand(connection, func(type), id);
            IDataReader reader = tuple.Item1;
            IDbCommand command = tuple.Item2;
            try
            {
                if (reader.Read())
                {
                    Tuple<string[], object[]> queryResult = GetColumnAndValue(reader);
                    DatabaseUtils.DisposeDataReader(reader);
                    string[] columns = queryResult.Item1;
                    object[] objs = queryResult.Item2;
                    ClassEntity tableEntry = func(type);
                    T obj = Activator.CreateInstance<T>();
                    SetProperty(func, tableEntry, columns, objs, obj, true);
                    if (tableEntry.FkQuantity>0)
                    {
                        switch (fetch)
                        {
                            case FetchFlag.Select:
                                SetSelect(type, func, obj, command);
                                break;
                            case FetchFlag.Join:
                                break;
                            case FetchFlag.None:
                            default:
                                break;
                        }
                    }
                    return obj;
                }
            }
            finally
            {
                DatabaseUtils.DisposeDataReader(reader);
                DatabaseUtils.DisposeCommand(command);
            }
            return default(T);
        }
        //sqlite 不支持 join 
        private static void SetJoin(ClassEntity tableEntry, object obj, IDbCommand command)
        {

        }

        private static void SetSelect(Type type, Func<Type, ClassEntity> func, object obj, IDbCommand command)
        {
            ClassEntity tableEntry = func.Invoke(type);
            foreach (var item in tableEntry.FkEntities)
            {
                object fkVal = null;
                //单
                if (item.FKColumnEntity.Single != null)
                {
                    object single = item.FKColumnEntity.Single.GetValue(obj);
                    if (single != null)
                    {
                        command.Parameters.Clear();
                        ClassEntity referenceTableEntry = func(item.FKColumnEntity.ReferenceType);
                        fkVal = ReflectHelper.GetValue(single, referenceTableEntry.IdEntities[0].PropertyName);
                        SetSelect(type, func, obj, command, fkVal, item.FKColumnEntity, referenceTableEntry, false);//该逻辑 递归 (取消逻辑 递归)
                    }
                }
                //多
                if (item.FKColumnEntity.Many != null)
                {
                    command.Parameters.Clear();
                    object val = ReflectHelper.GetValue(obj, tableEntry.IdEntities[0].PropertyName);
                    ClassEntity referenceTableEntry = func.Invoke(item.FKColumnEntity.ReferenceType);
                    string name = $"@{item.FKColumnEntity.ReferenceId}";
                    command.CommandText = $"{referenceTableEntry.SqlEntry.GetCache(SqlEntity.SelectAll)} WHERE {item.FKColumnEntity.ReferenceId}={name}";
                    DatabaseUtils.SetCommandParamter(command, name, val);
                    IDataReader reader = DatabaseUtils.ExecuteReader(command);
                    IList datas = GetList(reader, item.FKColumnEntity.ReferenceType, func);
                    DatabaseUtils.DisposeCommand(command);
                    if (datas.Count > 0)
                    {
                        foreach (object chil in datas)
                        {
                            //自关联 外关联会异常 稍后解决
                            ReflectHelper.SetValue(chil, item.FKColumnEntity.Single.PropertyName, obj);
                        }
                    }
                    ReflectHelper.SetValue(obj, item.FKColumnEntity.Many.PropertyName, datas);
                }
            }
        }

        private static void SetSelect(Type type, Func<Type, ClassEntity> func, object obj, IDbCommand command, object fkVal, FKColumnEntity fKGroupEntry, ClassEntity referenceTableEntry, bool cursion)
        {
#if !(NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
            ClassEntity tableEntry = func?.Invoke(type);
            string name = $"@{referenceTableEntry.IdEntities[0].Column}";
            command.CommandText = $"{referenceTableEntry.SqlEntry.GetCache(SqlEntity.SelectAll)} WHERE {referenceTableEntry.IdEntities[0].Column}={name}";
            DatabaseUtils.SetCommandParamter(command, name, fkVal);
            IDataReader dataReader = DatabaseUtils.ExecuteReader(command);
            Tuple<string[], object[]> queryResult = null;
            if (dataReader.Read())
            {
                queryResult = GetColumnAndValue(dataReader);
                DatabaseUtils.DisposeDataReader(dataReader);
            }
            else
            {
                return;//没有数据算了
            }
            string[] columns = queryResult.Item1;
            object[] objs = queryResult.Item2;
            object referenceObj = Activator.CreateInstance(referenceTableEntry.ClassType);
            SetProperty(func, referenceTableEntry, columns, objs, referenceObj, true);
            ReflectHelper.SetValue(obj, fKGroupEntry.Single.PropertyName, referenceObj);
            if (!cursion)
            {
                return;// 取消逻辑 递归
            }
            //如果一直嵌套 了则 递归
            if (referenceTableEntry.FkQuantity>0)
            {
                if (tableEntry.ClassType == referenceTableEntry.ClassType)
                {
                    object single = ReflectHelper.GetValue(referenceObj, fKGroupEntry.Single.PropertyName);
                    if (single == null)
                    {
                        return;
                    }
                    fkVal = tableEntry.IdEntities[0].GetValue(single);
                    //判断 不然 进入死循环
                    if (fkVal != null)
                    {
                        SetSelect(type, func, referenceObj, command);
                    }
                }

            }
#endif
        }

        public static Tuple<string[], object[]> GetColumnAndValue(IDataReader reader)
        {
            string[] columns = new string[reader.FieldCount];
            object[] objs = new object[reader.FieldCount];
            for (int i = 0; i < reader.FieldCount; i++)
            {
                columns[i] = reader.GetName(i);
                objs[i] = reader.GetValue(i);
            }
            return new Tuple<string[], object[]>(columns, objs);
        }

        /// <summary>关联集合外键 (外关联 则 要么 一步步查询(再绑定) 要么左连接查询 未实现)</summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> GetList<T>(IDbConnection connection, Func<Type, ClassEntity> func, string where = StringHelper.Empty, object param = null, DbFlag dialect = DbFlag.MySql)
        {
            Type type = typeof(T);
            IDataReader reader = GetDataReader(connection, func(type), where, param, dialect);
            IList<T> datas = (IList<T>)GetList(reader, type, func);
            return datas;
        }

        private static IList GetList(IDataReader reader, Type type, Func<Type, ClassEntity> func)
        {

            IList datas = (IList)TypeHelper.CreateList(type);
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
            ClassEntity tableEntry = func(type);
            string[] columns = new string[reader.FieldCount];
            for (int i = 0; i < reader.FieldCount; i++)
            {
                columns[i] = reader.GetName(i);
            }
            List<FKColumnDto> fKColumnEntries = new List<FKColumnDto>();
            while (reader.Read())
            {
                object[] objs = new object[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    objs[i] = reader.GetValue(i);
                }
                object obj = Activator.CreateInstance(type);
                FKColumnDto fKColumnEntry = SetProperty(func, tableEntry, columns, objs, obj, false);
                if (fKColumnEntry != null)
                {
                    fKColumnEntries.Add(fKColumnEntry);
                }
                datas.Add(obj);
            }
            reader.Dispose();
            reader = null;
            if (datas.Count == 0)
            {
                return datas;
            }
            //关联集合外键
            foreach (PropertyEntity item in tableEntry.FkEntities)
            {
                if (item.FKColumnEntity.Many == null)
                {
                    continue;
                }
#if !(NET20 || NET30 || NET35 || NET40)
                //自关联就直接 关联 (外关联 则 要么 一步步查询(再绑定) 要么左连接查询 未实现)
                if (item.FKColumnEntity.Many.PropertyType.GenericTypeArguments[0] == tableEntry.ClassType)
                {
                    //自关联就直接 关联
                    Dictionary<string, IList> children = new Dictionary<string, IList>();
                    foreach (FKColumnDto fKColumnEntry in fKColumnEntries)
                    {
                        if (fKColumnEntry.Column.Equals(item.FKColumnEntity.ReferenceId))
                        {
                            SetRemove(func, tableEntry, item.FKColumnEntity, fKColumnEntry, datas, children);
                        }
                    }
                    //单独查询 子集使用 Fetch.Select
                    if (datas.Count == 0)
                    {
                        if (children.Values.Count > 0)
                        {
                            IList[] values = new IList[children.Values.Count];
                            CollectionHelper.CopyTo(children.Values, values, 0, children.Values.Count);
                            return values[0];
                        }
                        return null;
                    }
                    foreach (KeyValuePair<string, IList> keyValuePair in children)
                    {
                        foreach (object data in datas)
                        {
                            object id = tableEntry.IdEntities[0].GetValue(data);
                            //无法比较 全部转换为string  比较
                            string idVal = id.ToString();
                            if (idVal.Equals(keyValuePair.Key))
                            {
                                //子集更新 可能 类型 不同 设置时异常 先 不判断 类型 了
                                item.FKColumnEntity.Many.Property.SetValue(data, keyValuePair.Value, BindingFlags.Instance
                                    | BindingFlags.Public | BindingFlags.SetProperty
                                    | BindingFlags.SetField, null, null, CultureInfo.CurrentCulture);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    //外关联 则 要么 一步步查询(再绑定) 要么左连接查询 未实现

                }
#endif
            }
#endif
            return datas;
        }
        private static void SetRemove(Func<Type, ClassEntity> func, ClassEntity classEntity, FKColumnEntity foreignKeyColumnEntity, FKColumnDto fKColumnEntity, IList datas, Dictionary<string, IList> children)
        {
            object temp = null;
            object id = null;
            string idVal = string.Empty;
            ClassEntity referenceTableEntry = null;
            foreach (object data in datas)
            {
                if (foreignKeyColumnEntity.Single != null)
                {
                    object sing = ReflectHelper.GetValue(data, foreignKeyColumnEntity.Single.PropertyName);
                    if (sing != null)
                    {
                        referenceTableEntry = func(foreignKeyColumnEntity.ReferenceType);
                        id = ReflectHelper.GetValue(sing, referenceTableEntry.IdEntities[0].PropertyName);
                    }
                }
                if (id == null && foreignKeyColumnEntity.Basic != null)
                {
                    id = ReflectHelper.GetValue(data, foreignKeyColumnEntity.Basic.PropertyName);
                }
                if (id == null)
                {
                    continue;
                }
                //无法比较 全部转换为string  比较
                idVal = id.ToString();
                if (idVal.Equals(fKColumnEntity.StrValue))
                {

                }
                if (!children.ContainsKey(idVal))
                {

                    children.Add(idVal, TypeHelper.CreateList(foreignKeyColumnEntity.ReferenceType));
                }
                datas.Remove(data);//遍历中不能移除 否则异常
                children[idVal].Add(data);
                temp = data;
                break;
            }
            if (foreignKeyColumnEntity.Single != null && temp != null)
            {
                foreach (object data in datas)
                {
                    object pk = ReflectHelper.GetValue(data, classEntity.IdEntities[0].PropertyName);
                    if (idVal.Equals(pk.ToString()))
                    {
                        ReflectHelper.SetValue(temp, foreignKeyColumnEntity.Single.PropertyName, data);
                        break;
                    }
                }
            }
        }


        private static FKColumnDto SetProperty(Func<Type, ClassEntity> func, ClassEntity classEntity, string[] columns, object[] vals, object obj, bool single)
        {
            FKColumnDto fKColumnEntry = null;
            List<string> temps = new List<string>(columns);
            foreach (var item in classEntity.PropertyEntities)
            {
                if (item is BaseEntity baseColumnEntry)
                {
                    int index = temps.IndexOf(baseColumnEntry.PropertyName);
                    if (index > -1)
                    {
                        item.SetValue(obj, vals[index]);
                    }
                }
            }
            if (classEntity.FkQuantity>0)
            {
                foreach (var item in classEntity.FkEntities)
                {
                    int index = temps.IndexOf(item.FKColumnEntity.ReferenceId);
                    if (index > -1)
                    {
                        if (vals[index] == null || vals[index] is DBNull)
                        {
                            continue;
                        }
                        if (!single)
                        {
                            fKColumnEntry = new FKColumnDto();
                            fKColumnEntry.Column = item.Column;
                            fKColumnEntry.Value = vals[index];
                            fKColumnEntry.StrValue = fKColumnEntry.Value.ToString();
                        }
                        if (item.FKColumnEntity.Single != null)
                        {
                            object singleObj = Activator.CreateInstance(item.FKColumnEntity.Single.PropertyType);
                            item.FKColumnEntity.Single.SetValue(obj, singleObj);
                            Type fk = item.FKColumnEntity.Single.PropertyType;
                            ClassEntity fkEntry = func(fk);
                            ReflectHelper.SetValue(singleObj, fkEntry.IdEntities[0].PropertyName, vals[index]);
                        }
                        if (item.FKColumnEntity.Basic != null)
                        {
                            item.FKColumnEntity.Basic.SetValue(obj, vals[index]);
                        }
                    }
                }
            }
            return fKColumnEntry;
        }

        /// <summary>
        /// 根据条件查询所有 返回读取命令
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="tableEntry">映射实体信息</param>
        /// <param name="where">条件(需要自己写 每个数据库语法不一致 )
        /// where Id=@Id or Id1=@Id1 or Id2=@Id2 
        /// where Id=? or Id1=? or Id2=? (内部转换 where Id=@Id or Id1=@Id1 or Id2=@Id2)
        /// </param>
        /// <param name="param">参数(未实现 暂时不支持条件参数化查询) new object[]{1,2,3} new {Id=1,Id1=2,Id3=3} new Entity(){} </param>
        /// <param name="dialect">数据库驱动</param>
        /// <returns></returns>
        public static IDataReader GetDataReader(IDbConnection connection, ClassEntity tableEntry, string where = StringHelper.Empty, object param = null, DbFlag dialect = DbFlag.MySql)
        {
            IDbCommand command = GetQueryCommand(connection, tableEntry);
            if (!string.IsNullOrEmpty(where))
            {
                //mysqlconnector @name 参数化会失败 不支持
                string name = dialect == DbFlag.MySql ? connection.GetType().Assembly.GetName().Name : string.Empty;//?name
                List<string> names = new List<string>();
                if (where.IndexOf("?") > 0)
                {
                    where = System.Text.RegularExpressions.Regex.Replace(where, "\\s*(.*?)\\s*=\\s*\\?", 
                        it => { 
                            var str = $"{(string.IsNullOrEmpty(name) ? "@" : "?")}{it.Groups[1].Value}"; 
                            names.Add(str); 
                            return str;
                        }
                        );
                }
                command.CommandText += where;
                if (param != null)
                {
                    if (param is object[] objs)
                    {
                        if (names.Count != objs.Length)
                        {
                            throw new Exception("param count match fail");
                        }
                        DatabaseUtils.SetCommandParamter(command, names.ToArray(), objs);
                    }
                    else
                    {
                        //
                    }
                    //do nothing

                }
            }
            IDataReader reader = DatabaseUtils.ExecuteReader(command);
            DatabaseUtils.DisposeCommand(command);
            return reader;
        }

        /// <summary>
        /// 根据主键 返回读取命令和查询命令
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="classEntry">映射实体信息</param>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public static Tuple<IDataReader, IDbCommand> GetDataReaderAndCommand(IDbConnection connection, ClassEntity classEntry, object id)
        {
            IDbCommand command = GetQueryCommand(connection, classEntry, id);
            IDataReader reader = DatabaseUtils.ExecuteReader(command);
            return new Tuple<IDataReader, IDbCommand>(reader, command);
        }

        /// <summary>
        /// 根据主键 返回读取命令
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="classEntry">映射实体信息</param>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public static IDataReader GetDataReader(IDbConnection connection, ClassEntity classEntry, object id)
        {
            Tuple<IDataReader, IDbCommand> tuple = GetDataReaderAndCommand(connection, classEntry, id);
            DatabaseUtils.DisposeCommand(tuple.Item2);
            return tuple.Item1;
        }

        #region select command

        /// <summary>
        /// 查询所有 返回查询命令
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="classEntry">映射实体信息</param>
        /// <param name="dialect">数据库驱动</param>
        /// <returns></returns>
        public static IDbCommand GetQueryCommand(IDbConnection connection, ClassEntity classEntry)
        {
            IDbCommand command = connection.CreateCommand();
            command.CommandText = classEntry.SqlEntry.GetCache(SqlEntity.SelectAll);
            return command;
        }

        /// <summary>
        /// 根据主键 返回查询命令
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="classEntry">映射实体信息</param>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public static IDbCommand GetQueryCommand(IDbConnection connection, ClassEntity classEntry, object id)
        {
            if (id == null) return null;
            if (classEntry.PkQuantity==0) throw new Exception($"{classEntry.ClassType} 主键属性不存在!");
            IDbCommand command = connection.CreateCommand();
            command.CommandText = classEntry.SqlEntry.GetCache(SqlEntity.SelectByWhereId);
            //mysqlconnector @name 参数化会失败 不支持
            DatabaseUtils.SetCommandParamter(command, classEntry.IdEntities[0].ColumnForamat, id);
            return command;
        }

        #endregion select command
    }
}
