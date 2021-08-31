using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;
using Utility.Database.Entities;
using Utility.Database.Utils;
using Utility.Helpers;

namespace Utility.Database.Provider
{
    /// <summary>
    /// 抽象数据库操作
    /// </summary>
    public  abstract partial class AbstractDbProvider
    {
        internal bool KeywordFilter { get; set; }
        /// <summary>
        /// 数据库驱动
        /// </summary>
        protected DbFlag Dialect { get; set; }
        /// <summary>
        /// 关键字不用转换
        /// mysql 关键字 必须 使用 ``  默认都使用`` 
        ///sqlite "" '' [] `` 默认使用 "" .但 '' 列 也会这样 '' 没去掉  
        ///sqlserver ""  默认使用 "" 
        /// </summary>
        public string Quot { get; protected set; }
        /// <summary>
        /// 版本
        /// </summary>
        public double Version { get; protected set; } = -1;
        /// <summary>
        /// 版本
        /// </summary>
        public string StringVersion { get; set; }
        /// <summary>
        /// 数据库 中 所有 表 缓存
        /// </summary>
        protected List<ClassEntity> ClassEntitiesCache { get; set; }

        internal static IDictionary<Type, DbType> typeMap;
        static AbstractDbProvider()
        {
            typeMap = new Dictionary<Type, DbType>
            {
                [typeof(byte)] = DbType.Byte,
                [typeof(sbyte)] = DbType.SByte,
                [typeof(short)] = DbType.Int16,
                [typeof(ushort)] = DbType.UInt16,
                [typeof(int)] = DbType.Int32,
                [typeof(uint)] = DbType.UInt32,
                [typeof(long)] = DbType.Int64,
                [typeof(ulong)] = DbType.UInt64,
                [typeof(float)] = DbType.Single,
                [typeof(double)] = DbType.Double,
                [typeof(decimal)] = DbType.Decimal,
                [typeof(bool)] = DbType.Boolean,
                [typeof(string)] = DbType.String,
                [typeof(char)] = DbType.StringFixedLength,
                [typeof(Guid)] = DbType.Guid,
                [typeof(DateTime)] = DbType.DateTime,
                [typeof(DateTimeOffset)] = DbType.DateTimeOffset,
                [typeof(TimeSpan)] = DbType.Time,
                [typeof(byte[])] = DbType.Binary,
                [typeof(byte?)] = DbType.Byte,
                [typeof(sbyte?)] = DbType.SByte,
                [typeof(short?)] = DbType.Int16,
                [typeof(ushort?)] = DbType.UInt16,
                [typeof(int?)] = DbType.Int32,
                [typeof(uint?)] = DbType.UInt32,
                [typeof(long?)] = DbType.Int64,
                [typeof(ulong?)] = DbType.UInt64,
                [typeof(float?)] = DbType.Single,
                [typeof(double?)] = DbType.Double,
                [typeof(decimal?)] = DbType.Decimal,
                [typeof(bool?)] = DbType.Boolean,
                [typeof(char?)] = DbType.StringFixedLength,
                [typeof(Guid?)] = DbType.Guid,
                [typeof(DateTime?)] = DbType.DateTime,
                [typeof(DateTimeOffset?)] = DbType.DateTimeOffset,
                [typeof(TimeSpan?)] = DbType.Time,
                [typeof(object)] = DbType.Object
            };
        }
        /// <summary>
        /// 获取 数据库 版本 
        /// 没有 则从 数据库 检索 暂时支持 mysql sqlite sqlserver
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="dialect"></param>
        /// <returns></returns>
        public virtual double FindVersion(DbConnection connection)
        {
            StringVersion= FindVersion(connection,Dialect);
            ParseVersion();
            return Version;
        }
  

        protected virtual void ParseVersion()
        {
            int count = 0;
            //mysql 5.3.1
            string version = Regex.Replace(StringVersion, "\\.", it => {
                if (count > 0)
                {
                    return string.Empty;
                }
                count++;
                return ".";
            });
            double.TryParse(version, out double v);
            Version = v;
        }

        /// <summary>
        /// 获取数据库 中 所有 表 缓存
        /// </summary>
        /// <param name="connection"></param>
        public virtual void FindTableCache(DbConnection connection)
        {
            ClassEntitiesCache = FindTableByDatabase(connection, connection.Database, Dialect);
        }

       
        /// <summary>
        /// 数据库是否存在
        /// <para>mysql:</para>
        /// <para>select count(1) from mysql.db where Db=数据库名称;</para>
        /// </summary>
        /// <param name="databaseName">数据库</param>
        /// <returns></returns>
        public virtual int ExistsDatabase(DbConnection connection, string databaseName)
        {
            return ExistsDatabase(connection, databaseName);
        }
      
    }
}
