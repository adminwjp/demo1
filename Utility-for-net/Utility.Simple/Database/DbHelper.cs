#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
/**
 Comparer<Key>.Default- Comparer.Default.Compare(这 提示报错) 必须至少有一个对象实现 IComparable。
必须 自定义 IComparer<T> (才可以)或 T 实现 IComparable<T>
 //官方内部代码
          public int Compare(object a, object b)
        {
            if (a == b)
            {
                return 0;
            }

            if (a == null)
            {
                return -1;
            }

            if (b == null)
            {
                return 1;
            }
            //上面怎么执行不了
            if (m_compareInfo != null)
            {
                string text = a as string;
                string text2 = b as string;
                if (text != null && text2 != null)
                {
                    return m_compareInfo.Compare(text, text2);
                }
            }

            IComparable comparable = a as IComparable;
            if (comparable != null)
            {
                return comparable.CompareTo(b);
            }

            IComparable comparable2 = b as IComparable;
            if (comparable2 != null)
            {
                return -comparable2.CompareTo(a);
            }

            throw new ArgumentException(Environment.GetResourceString("Argument_ImplementIComparable"));//老是执行到这一步
        }


----数据查询语言(DQL)：主要是select语句
----数据操纵语言(DML):insert,update,delete
----事务控制语言（TCL）：commit,rollback,savepoint(用于设置保存点)
----数据定义语言(DDL):create(创建),alter(修改),drop删除
----数据控制语言（DCL）：grant(grant命令给用户或角色授予权限),revoke(用于回收用户或角色所具有的权限)
每个数据库 语法 都不同 
mysql:insert into table [(col1,col2...)] values(val1,val2...)[,values(val1,val2...)]
sqlite:insert into table [(col1,col2...)] value(val1,val2...)[,value(val1,val2...)]
sqlserver:
insert into table [(col1,col2...)] value(val1,val2...)[,(val1,val2...)]
insert into ApiScopes([name]) 
SELECT '2';
full join(sqlite mysql 都不支持)
right join(sqlite 不支持)
sqlserver:select * from table where id=['id' | "id" ]
mysql":select * from table where id='id' 

关键字 处理:
注意 ado.net 不是关键字 做处理后 驱动报语法不支持 最好不要用
sqlite: "" '' `` []
sqlserver:"" []
mysql: ``
@NAME 统一 参数格式化 后期 看 驱动 而改

删除表结构 时有问题(先删除外键(需要获取排序), 再次 删除 表 有些未实现) 
ado.net 参数格式化 只能关键字做处理化,不然报语法错误
example:insert into a("a","table") value(@a,@table);//error
insert into a(a,"table") value(@a,@table);//pass


 */
using System;
using System.Collections;
#if !(NET20 || NET30 || NET35)
using System.Collections.Concurrent;
#endif
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
#if !(NET20 || NET30)
using System.Linq;
#endif
using System.Text;
#if !(NET20 || NET30 || NET35)
using System.Threading.Tasks;
#endif
using Utility.Collections;
using Utility.Database.Entities;
using Utility.Database.Provider;
using Utility.Database.Utils;
using Utility.Pool;
using Utility.Helpers;

namespace Utility.Database
{
    /// <summary>
    /// database validate exception help class
    /// </summary>
    public class ExceptionHelper
    {
        /// <summary>
        /// validate  primary  key type
        /// </summary>
        /// <param name="primaryKeyType"> primary  key type</param>
        /// <exception cref="Exception">primary key type can only is Guid type or Guid? type or string type or byte type or byte? type or short type or short? type or int type or int? type   or long type or long? type,other type not support ! </exception>
        public static void ValidatePrimaryKey(Type primaryKeyType)
        {
            if (!(TypeHelper.IsGuid(primaryKeyType) || TypeHelper.IsString(primaryKeyType)
                            || TypeHelper.IsInterger(primaryKeyType)))
            {
                throw new Exception("primary key type can only is Guid type or Guid? type or string or type byte type or byte? type or short type or short? type or int type or int? type   or long type or long? type,other type not support ! ");
            }
        }

        public static void ValidateColumn(ClassEntity tableEntry, PropertyEntity columnEntry)
        {
            if (string.IsNullOrEmpty(columnEntry.Column))
            {
                throw new Exception($"class type {tableEntry.ClassType} property {columnEntry.Property}  column {columnEntry.Column} is not null  ");
            }
            tableEntry.ValidateColumn(columnEntry.Column);
        }
    }

    internal class StringBuilderPool: ObjectPool<StringBuilder>,IObjectPool<StringBuilder>,IObjectPool
    {
        public StringBuilderPool()
        {
            base.Create = () => new StringBuilder(1000);
        }
    }
    internal class ObjectLock
    {
        /// <summary>
        /// 用于 线程 下 同一个对象的锁,确认 主键 唯一
        /// 以防外键 不正确 .
        ///example:
        ///a:1 b:a b外键 1 线程1
        /// a:1 b:a b外键1 线程2
        /// </summary>
#if !(NET20 || NET30 || NET35 || NETSTANDARD1_0)
        protected static IDictionary<string, object> ObjectLocks = new ConcurrentDictionary<string, object>();
#else
        protected static IDictionary<string, object> ObjectLocks = new CollectionConcurrent<string, object>();
#endif
        public static object Get(string key)
        {
            if (ObjectLocks.ContainsKey(key))
            {
                return ObjectLocks[key];
            }
            else
            {
                object obj = new object();
                ObjectLocks.Add(key, obj);
                return obj;
            }
        }
    }

    internal class CascadeCache
    {
        /// <summary>
        /// 添加 或 修改 时  缓存 防止 级联  添加 或 修改 出错, 不比较 引用 对象, 直接 比较 id, id 存在 则 跳过该对象的  添加 或 修改 
        /// 用完后 删除 (前提 自增 再 分布式 情况下无法 按 单机 情况操作)
        /// </summary>
#if !(NET20 || NET30 || NET35 || NETSTANDARD1_0)
        protected static IDictionary<string, Collections.Array<string>> IdCaches = new ConcurrentDictionary<string, Collections.Array<string>>();
#else
        protected static IDictionary<string, Collections.Array<string>> IdCaches = new CollectionConcurrent<string, Collections.Array<string>>();
#endif

        /// <summary>
        /// 当前 级联 操作 是否 存在 冲突  即级联多次 操作(可能 重复 操作 重复引用 或 id 重复) 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fkId"></param>
        /// <returns>true :级联多次 操作  </returns>
        public static bool Get(string key,string fkId)
        {
            if (IdCaches.ContainsKey(key))
            {
                foreach (var item in IdCaches[key])
                {
                    if (item.Equals(fkId))
                    {
                        return true;
                    }
                }
            }
            else
            {
                IdCaches.Add(key, new Collections.Array<string>(EqualityComparer<string>.Default));
            }
            IdCaches[key].Add(fkId);
            return false;
        }

        /// <summary>
        /// 当前 级联操作 完成则移除 (无论是否异常)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Remove(string key)
        {
            if (IdCaches.ContainsKey(key))
            {
                var res=IdCaches.Remove(key);
                return res;
            }
            return false;
        }
    }
    /// <summary>
    /// 
    /// sqlite:https://www.cnblogs.com/songxingzhu/p/3992884.html 
    ///sqlite "" '' [] `` 默认使用 ""  关键字不用转换 但是 每个数据库 语法不同   .但 '' 列 也会这样 '' 没去掉
    ///mysql 必须 使用 ``
    /// 
    /// </summary>
    public partial class DbHelper 
    {
        /// <summary>
        /// 解析 表 缓存 
        /// </summary>
#if !(NET20 || NET30 || NET35 || NETSTANDARD1_0)
        protected static IDictionary<Type, ClassEntity> ClassEntities = new ConcurrentDictionary<Type, ClassEntity>();
#else
        protected static IDictionary<Type, ClassEntity> ClassEntities = new CollectionConcurrent<Type, ClassEntity>();
#endif
        public static IObjectPool<StringBuilder> StringBuilderPool = new StringBuilderPool();


        /// <summary>
        /// 数据库 中 表 缓存 用于 比较表结构是否改变
        /// </summary>
#if !(NET20 || NET30 || NET35 || NETSTANDARD1_0)
        protected static IDictionary<string, IList<ClassEntity>> ClassEntitiesCache = new ConcurrentDictionary<string, IList<ClassEntity>>();
#else
        protected static IDictionary<string, IList<ClassEntity>> ClassEntitiesCache = new CollectionConcurrent<string, IList<ClassEntity>>();
#endif
#if !(NET20 || NET30 || NET35 || NETSTANDARD1_0)
         internal static IDictionary<Type, DbConnectionStringEntity> DbConnectionStrings = new Dictionary<Type, DbConnectionStringEntity>();
#else
         internal static IDictionary<Type, DbConnectionStringEntity> DbConnectionStrings = new Collection<Type, DbConnectionStringEntity>();
#endif
        private static readonly object DbLock = new object();
        private static readonly object TableLock = new object();
        internal class DbConnectionStringEntity
        {
            public string ConnectionString { get; set; }
            public DbFlag Dialect { get; set; }
            public IList<string> Tables { get; set; }
        }

        //https://blog.csdn.net/qq_18059143/article/details/103323840?utm_medium=distribute.pc_relevant.none-task-blog-BlogCommendFromMachineLearnPai2-2.channel_param&depth_1-utm_source=distribute.pc_relevant.none-task-blog-BlogCommendFromMachineLearnPai2-2.channel_param
        //其实原理就是不让数据库的每次提交写入磁盘，而做一个缓存，每隔一定时间进行提交
        public const string SqliteTimerSql = "PRAGMA synchronous=OFF;PRAGMA Journal_Mode=WAL;PRAGMA Cache_Size=5000;";

        public static readonly IDictionary<Type, Type[]> IdTypes = new Collections.Collection<Type, Type[]>(new TypeCompare())
        {
           
        };
        public static readonly IDictionary<DbFlag, IList<string>> Keywords = new Collection<DbFlag, IList<string>>()
        {
            [DbFlag.MySql]= new Collections.Array<string>(EqualityComparer<string>.Default),
            [DbFlag.SqlServer] = new Collections.Array<string>(EqualityComparer<string>.Default),
            [DbFlag.Oracle] = new Collections.Array<string>(EqualityComparer<string>.Default),
            [DbFlag.Postgre] = new Collections.Array<string>(EqualityComparer<string>.Default),
            [DbFlag.Sqlite] = new Collections.Array<string>(EqualityComparer<string>.Default),
        };
        //Comparer<Type>.Default 官方 怎么有问题 通过不了 即   Comparer<Key>.Default- Comparer.Default.Compare(这 提示报错) 必须至少有一个对象实现 IComparable。
        //必须 自定义 IComparer<T>(才可以) 或 T 实现 IComparable<T>
        public class TypeCompare : IComparer<Type>
        {
            public int Compare(Type x, Type y)
            {
                if (x == y) return 0;
                if (x == null && y != null) return -1;
                if (x != null && y == null) return 1;
                if (x != null && y != null)
                {
                    return x.GetHashCode() - y.GetHashCode() > 0?1:-1;
                }
                return -1;
            }
        }

        static DbHelper()
        {
            string[] names = { "database","table",
                "select","insert","update","delete",
                "from","where","index","foreign","key","primary","unique","view","procedure"
                ,"trigger","create","drop","alter","column","default","check","add","modify"
            };
            ((Collections.Array<string>)Keywords[DbFlag.MySql]).InsertRange(names);
            ((Collections.Array<string>)Keywords[DbFlag.SqlServer]).InsertRange(names);
            ((Collections.Array<string>)Keywords[DbFlag.Oracle]).InsertRange(names);
            ((Collections.Array<string>)Keywords[DbFlag.Postgre]).InsertRange(names);
            ((Collections.Array<string>)Keywords[DbFlag.Sqlite]).InsertRange(names);
#region type
            Type typeString = typeof(string);
            IdTypes[typeString] = new Type[] { typeString };
            //Comparer<Type>.Default 官方 怎么有问题 通过不了 即 调用 Comparer.Default.Compare  原来 Type 没实现 IComparable
            Type typeByte = typeof(byte);
            Type typeByteNull = typeof(byte?);
            Type stypeByte = typeof(sbyte);
            Type stypeByteNull = typeof(sbyte?);
            Type[] typeByteS = new Type[] { typeByte, typeByteNull, stypeByte, stypeByteNull };
            IdTypes[typeByte] = typeByteS;
            IdTypes[typeByteNull] = typeByteS;
            IdTypes[stypeByte] = typeByteS;
            IdTypes[stypeByteNull] = typeByteS;

            Type typeshort = typeof(short);
            Type typeshortNull = typeof(short?);
            Type stypeshort = typeof(ushort);
            Type stypeshortNull = typeof(ushort?);
            Type[] typeshortS = new Type[] { typeshort, typeshortNull, stypeshort, stypeshortNull };
            IdTypes[typeshort] = typeshortS;
            IdTypes[typeshortNull] = typeshortS;
            IdTypes[stypeshort] = typeshortS;
            IdTypes[stypeshortNull] = typeshortS;

            Type typeint = typeof(int);
            Type typeintNull = typeof(int?);
            Type stypeint = typeof(uint);
            Type stypeintNull = typeof(uint?);
            Type[] typeintS = new Type[] { typeint, typeintNull, stypeint, stypeintNull };
            IdTypes[typeint] = typeintS;
            IdTypes[typeintNull] = typeintS;
            IdTypes[stypeint] = typeintS;
            IdTypes[stypeintNull] = typeintS;
            Type typelong = typeof(long);
            Type typelongNull = typeof(long?);
            Type stypelong = typeof(ulong);
            Type stypelongNull = typeof(ulong?);
            Type[] typelongtS = new Type[] { typelong, typelongNull, stypelong, stypelongNull };
            IdTypes[typelong] = typelongtS;
            IdTypes[typelongNull] = typelongtS;
            IdTypes[stypelong] = typelongtS;
            IdTypes[stypelongNull] = typelongtS;

            Type typeGuid = typeof(Guid);
            Type typeGuidNull = typeof(Guid?);
            Type[] typeGuidS = new Type[] { typeGuid, typeGuidNull };
            IdTypes[typeGuid] = typeGuidS;
            IdTypes[typeGuidNull] = typeGuidS;

#endregion type

            //connection string 
          //  ConnectionAddressTemplate.SqliteValue = "Data Source={0};Pooling=true;FailIfMissing=false";



            //分离数据库
           // DetachSql.SqliteValue = "DETACH DATABASE ";//DETACH DATABASE "database";

            //附加数据库
           // AttachSql.SqliteValue = "ATTACH DATABASE ";//ATTACH DATABASE "database" as asliasName;
        }
        /// <summary>
        /// "" []
        /// </summary>
        public const string SqlserverQuot = "\"";

        /// <summary>
        /// ``
        /// </summary>
        public const string MysqlQuot = "`";

        /// <summary>
        /// //"" '' `` [] 都支持
        /// </summary>
        public const string SqliteQuot = SqlserverQuot;

        /// <summary>
        /// ""
        /// </summary>
        public const string OracleQuot = SqlserverQuot;

        /// <summary>
        /// ""
        /// </summary>
        public const string AccesssQuot = SqlserverQuot;

        /// <summary>
        /// ""
        /// </summary>
        public const string PostgreQuot = SqlserverQuot;

      
        /// <summary>
        /// according schema  selecct database information   
        /// </summary> 
        /// <param name="connection"></param>
        /// <param name="schema">according schema(TABLES)  selecct database data type information</param>
        /// <returns></returns> 
        public static DataTable FindSchema(DbConnection connection, string schema= "TABLES")
        {
            try
            {
                DatabaseUtils.Open(connection);
                //connection.Open();
                DataTable data =!string.IsNullOrEmpty(schema)? connection.GetSchema(schema) : connection.GetSchema();
                return data;
            }
            finally
            {
                //connection.Close();
            }
           
        }











        public static string[] FindIndexNameBySqlite(IDbConnection connection)
        {
            return FindNameByTypeAndSqlite(connection, "index");
        }
        public static string[] FindViewNameBySqlite(IDbConnection connection)
        {
            return FindNameByTypeAndSqlite(connection, "view");
        }
        public static string[] FindTriggerNameBySqlite(IDbConnection connection)
        {
            return FindNameByTypeAndSqlite(connection, "trigger");
        }
        public static string[] FindNameByTypeAndSqlite(IDbConnection connection, string type = "table")
        {
            using (IDbCommand command = connection.CreateCommand())
            {
                string sql = $"select name from sqlite_master where type='{type}';";
                command.CommandText = sql;
                return DatabaseUtils.ReaderString(command);
            }
        }




        public static string PrefixSqlByMySqlOrSqlite(OperatorTypeFlag operatorTypeFlag, OperatorFlag flag)
        {
            if (operatorTypeFlag == OperatorTypeFlag.None)
            {
                throw new System.NotSupportedException();
            }
            string name = operatorTypeFlag.ToString().ToUpper();
            switch (flag)
            {
                case OperatorFlag.Drop:
                    return $"DROP {name} ";
                case OperatorFlag.DropIfExists:
                    return $"DROP {name}  IF EXISTS";
                case OperatorFlag.CreateDropIfExists:
                    return "DROP " + name + "  IF EXISTS {0} ; CREATE " + name + "  ";
                case OperatorFlag.CreateIfNotExists:
                    return $"CREATE {name}  IF NOT EXISTS   ";
                case OperatorFlag.Create:
                case OperatorFlag.None:
                default: return $"CREATE {name}  ";
            }
        }

        /// <summary>
        /// 更新 信息 外键 信息(引用 类型 外键 和 普通 属性 外键)
        /// </summary>
        /// <param name="tableEntries"></param>
        public static void UpdateForeignKey(params ClassEntity[] tableEntries)
        {
            //不要 索引 获取 老是索引 写错了 
            //匹配 引用 类型 外键 (相互 关联 已存在的 实体 类型)
            /**
             * example:
             * class a{int id; b b1;}//不关联
             * class b{int id;a a1,int a1id;icolllection<a> children; }//之关联 a1
             */
            for (int i = 0; i < tableEntries.Length; i++)
            {
                ClassEntity tableEntry = tableEntries[i];
                for (int j = 0; j < tableEntry.PropertyEntities.Count; j++)
                {
                    PropertyEntity columnEntry = tableEntry.PropertyEntities[j];
                    //处理 该信息 
                    if (columnEntry.Flag == ColumnFlag.Ignore)
                    {
                        UpdateForeignKey(columnEntry, tableEntries, tableEntry);
                    }
                   
                }
            }

            //整理 普通 属性 外键 信息
            for (int i = 0; i < tableEntries.Length; i++)
            {
                ClassEntity tableEntry = tableEntries[i];
                if(tableEntry.FkQuantity>0)
                {
                    for (int k = 0; k < tableEntry.FkEntities.Count; k++)
                    {
                        //不可能为null
                        FKColumnEntity foreignKeyColumnEntry = tableEntry.FkEntities[k].FKColumnEntity;
                        //if(foreignKeyColumnEntry==null)
                        //{
                        //    continue;
                        //}
                        string fkId;
                        if (foreignKeyColumnEntry.Single != null)
                        {
                            fkId = tableEntry.FkEntities[k].Column;
                        }
                        else
                        {
                            if (!foreignKeyColumnEntry.IsParent)
                            {
                                foreignKeyColumnEntry.Has = false;//只不过没有外键列 说明需要关联 添加 更新 或 删除 或 查询时用到
                                continue;
                            }
                            fkId = "ParentId";
                        }
                        UpdateForeignKey(tableEntry, tableEntry.FkEntities[k],fkId);//更新符合规则的普通外键列
                    }
                }
            }
        }

        /// <summary>
        /// 匹配 引用 类型 外键 (相互 关联 已存在的 实体 类型) 
        /// </summary>
        /// <param name="columnEntry">列</param>
        /// <param name="tableEntries">所有实体映射(用于匹配外键关系)</param>
        /// <param name="tableEntry">列 所属 实体映射</param>
        private static void UpdateForeignKey(PropertyEntity columnEntry,ClassEntity[] tableEntries,ClassEntity tableEntry)
        {
#if !(NET20 || NET30 || NET35 || NET40)
            //匹配 外键 实体 类型 信息
            bool children =TypeHelper.IsICollection(columnEntry.PropertyType, typeof(IEnumerable<>));
            Type type = children ? columnEntry.PropertyType.GenericTypeArguments[0] : columnEntry.PropertyType;
            for (int i = 0; i < tableEntries.Length; i++)
            {
                // 同一个不用判断 
                //if (tableEntries[i] == tableEntry) continue;
                //找到关系 
                if (tableEntries[i].ClassType.Equals(type))
                { 
                    //如果 是联合 主键 怎么 判断 关联 的是  哪个 主键 了 默认 优先级 第一个
                    if (tableEntries[i].PkQuantity <= 0)
                    {
                        throw new Exception($"{tableEntries[i].ClassType} not has primary key ");
                    }
                    if (columnEntry.FKColumnEntity == null)
                    {
                        columnEntry.FKColumnEntity = new FKColumnEntity() { ReferenceType = type };
                        tableEntry.FkQuantity++;//更新外键 数量 逻辑判断时用到
                    }
                    BaseEntity baseEntry = new BaseEntity(columnEntry.Property);//更新 后期删除 匹配后的信息 (现在删除 不然造成索引 找不到 以及 数组 大小更新 了)
                    columnEntry.Flag = ColumnFlag.ForeignKey;
                    if (children)
                    {
                        columnEntry.FKColumnEntity.Many = baseEntry;
                    }
                    else
                    {
                        columnEntry.Column = $"{baseEntry.PropertyName}Id";//外键列 
                        columnEntry.FKColumnEntity.Single = baseEntry;
                    }
                    columnEntry.FKColumnEntity.IsParent = type.Equals(tableEntry.ClassType);//自关联 
                   // if(columnEntry.ForeignKeyColumnEntry.IsParent|| !children)
                    {
                        //实际 有外键列 情况下:
                        columnEntry.FKColumnEntity.ReferenceId = tableEntries[i].IdEntities[0].Column;
                        columnEntry.FKColumnEntity.ReferenceTable = tableEntries[i].Table;
                        columnEntry.FKColumnEntity.PrimaryKeyPropertyName = tableEntries[i].IdEntities[0].PropertyName;
                        columnEntry.FKColumnEntity.PrimaryKeyPropertyType = tableEntries[i].IdEntities[0].Property.PropertyType;
                        columnEntry.FKColumnEntity.PrimaryKeyProperty = tableEntries[i].IdEntities[0].Property;
                        columnEntry.FKColumnEntity.ForeignKeyClassEntry = tableEntries[i];
                        //删除时(从库里查询获取最新的) 外键 时要用到. 创建时 给不给 无所谓
                        columnEntry.FKColumnEntity.Constraint = $"FK_{tableEntry.Table}_{tableEntries[i].Table}_{columnEntry.FKColumnEntity.ReferenceId}";
                    }
                    break;
                }
            }
#endif
        }
        /// <summary>
        /// 更新符合规则的普通外键列
        /// </summary>
        /// <param name="tableEntry">外键列 所在 实体类型</param>
        /// <param name="columnEntryFk">外键列 (分组)</param>
        /// <param name="fkId">外键名称(规则)</param>

        private static void UpdateForeignKey(ClassEntity tableEntry,PropertyEntity columnEntryFk,string fkId)
        {
            for (int i = 0; i < tableEntry.PropertyEntities.Count; i++)
            {
                PropertyEntity columnEntry = tableEntry.PropertyEntities[i];
                if (columnEntry.Flag == ColumnFlag.Column)
                {
                    //找到 符合规则 的外键 普通列
                    if (fkId == columnEntry.PropertyName)
                    {
                        //然后判断类型 是否符合 
                        if (IdTypes.ContainsKey(columnEntry.PropertyType))
                        {
                            Collections.Array<Type> types = new Collections.Array<Type>(EqualityComparer<Type>.Default);//hashset
                            var vals = IdTypes[columnEntry.PropertyType];
                            types.InsertRange(vals);
                            int length = types.Count;
                            types.InsertRange(IdTypes[columnEntryFk.FKColumnEntity.PrimaryKeyProperty.PropertyType]);
                            //相等 说明 类型 相同.example: int? - int
                            if (length == types.Count)
                            {
                                columnEntry.Flag = ColumnFlag.None;//抛弃的普通列
                                columnEntryFk.FKColumnEntity.Basic = new BaseEntity(columnEntry.Property);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 更新信息 数据类型 以及外键列名 (生成创建表 sql) 缓存 通用 sql
        /// </summary>
        /// <param name="tableEntries"></param>
        public static void UpdateInformation(params ClassEntity[] tableEntries)
        {
            UpdataDbType(tableEntries);//更新 数据库 列 数据 类型

            //解决创建 表时 外键冲突(相互外键,都创建情况下)
            /**
             example:
            create tabe a{id int primary key,id1 int,forign key id1 reference b(id)};
            create tabe b{id int primary key,id1 int,forign key id1 reference a(id)};
             */
            for (int i = 0; i < tableEntries.Length; i++)
            {
                ClassEntity tableEntry = tableEntries[i];
                //说明有外键
                if (tableEntry.FkQuantity > 0)
                {
                    for (int k = 0; k < tableEntry.FkEntities.Count; k++)
                    {
                        if (tableEntry.FkEntities[k].FKColumnEntity.ForeignKeyClassEntry == tableEntry)
                        {
                            continue;
                        }
                        if (!tableEntry.FkEntities[k].UseForeignKey())
                        {
                            continue;
                        }
                        FKColumnEntity foreignKeyColumnEntry = tableEntry.FkEntities[k].FKColumnEntity;//不可能为null
                        //if (foreignKeyColumnEntry == null) continue;
                        if (foreignKeyColumnEntry.OnConflict)
                        {
                            continue;//已经匹配过
                        }
                       //不是自关联
                        if (foreignKeyColumnEntry.ReferenceType != tableEntry.ClassType)
                        {
                            //查询 外键列 对应的  实体 是否 也关联 该 外键列 的实体
                             FkConfilct(tableEntry.FkEntities[k], tableEntry.ClassType);
                        }
                    }
                }
            }
          
            //生成通用 增删 改 查sql

            for (int i = 0; i < tableEntries.Length; i++)
            {
                ClassEntity tableEntry = tableEntries[i];
                tableEntry.SqlEntry.GeneratorCache(tableEntry);
            }
        }

        /// <summary>
        /// 查询 外键列 对应的  实体 是否 也关联 该 外键列 的实体
        /// </summary>
        /// <param name="columnEntry">外键列</param>
        /// <param name="classType">外键列 所属 实体 类型</param>
        /// <returns></returns>
        private static bool FkConfilct(PropertyEntity columnEntry,Type classType)
        {
            FKColumnEntity foreignKeyColumnEntry = columnEntry.FKColumnEntity;
            if (foreignKeyColumnEntry.ForeignKeyClassEntry.FkQuantity > 0)
            {
                ClassEntity tableEntry = foreignKeyColumnEntry.ForeignKeyClassEntry;
                for (int i = 0; i < tableEntry.FkEntities.Count; i++)
                {
                    FKColumnEntity foreignKeyColumnEntry1 = tableEntry.FkEntities[i].FKColumnEntity;
                    if (foreignKeyColumnEntry.OnConflict) continue;
                    if(foreignKeyColumnEntry1.ReferenceType== classType)
                    {
                        foreignKeyColumnEntry1.OnConflict = true;
                        return true;
                    }
                }
            }
            return false;
        }

        private static void UpdataDbType(ClassEntity[] tableEntries)
        {
            //更新 数据库 列 数据 类型
            for (int i = 0; i < tableEntries.Length; i++)
            {
                ClassEntity tableEntry = tableEntries[i];
                for (int j = 0; j < tableEntry.PropertyEntities.Count; j++)
                {
                    PropertyEntity columnEntry = tableEntry.PropertyEntities[j];
                    if (!columnEntry.Valid())
                    {
                        continue;
                    }
                    Type type = columnEntry.Flag == ColumnFlag.ForeignKey ? columnEntry.FKColumnEntity.PrimaryKeyPropertyType : columnEntry.PropertyType;
                    UpdataDbType(columnEntry, type);
                }
            }
        }

        private static void UpdataDbType(PropertyEntity columnEntry,Type type)
        {
            //columnEntry.Length = 500;
           // columnEntry.DataType = AbstractDbTypeParseProvider.Empty.Parse(type, (int)columnEntry.Length);
        }


        /// <summary>
        /// 检测 表 结构是否改变 多了 则 添加 少 了 不做任何 操作
        /// 暂时 未实现
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="classEntities">解析后数据库里的信息</param>
        /// <param name="classEntity">当前信息</param>
        internal static void CheckColumnUpdate(DbConnection connection,ClassEntity[] classEntities, ClassEntity classEntity,DbFlag dialect= DbFlag.MySql)
        {
         
            ClassEntity temp=null;
            for (int i = 0; i < classEntities.Length; i++)
            {
                if (classEntities[i].Table.ToLower().Equals(classEntity.Table.ToLower()))
                {
                    temp = classEntities[i];
                    break;
                }
            }
            if (temp != null)
            {
                //只区别 列 是否 存在 或 类型 是否 改变 其他 暂时 不管
                StringBuilder builder = new StringBuilder(1000);
                //主键 是否改变
                if (temp.PkQuantity > 0)
                {
                    foreach (var item in temp.IdEntities)
                    {
                        var has = false;
                        if (classEntity.PkQuantity > 0)
                        {
                            foreach (var id in classEntity.IdEntities)
                            {
                                if (item.Column.ToLower().Equals(id.Column.ToLower()) && item.DbDataType == id.DbDataType)
                                {
                                    has = true;
                                    break;
                                }
                            }
                        }
                        //未找到 则 更新
                        if (!has)
                        {

                        }
                    }
                }

                //外键 是否 改变 
                if (temp.FkQuantity>0)
                {
                    foreach (var item in temp.FkEntities)
                    {
                        var has = false; 
                        if (classEntity.FkQuantity > 0)
                        {
                            foreach (var id in classEntity.FkEntities)
                            {
                                if (item.Column.ToLower().Equals(id.Column.ToLower()) && item.DbDataType == id.DbDataType)
                                {
                                    has = true;
                                    break;
                                }
                            }
                        }
                        //未找到 则 更新
                        if (!has)
                        {

                        }
                    }
                }

                //普通 列 是否 改变
            }
        }
    }


}
#endif