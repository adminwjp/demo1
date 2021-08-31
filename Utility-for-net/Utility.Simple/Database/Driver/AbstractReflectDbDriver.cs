#if !( NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 ||  NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System;
using System.Data;
using System.Data.Common;

namespace Utility.Database.Driver
{
    public class DefaultReflectDatabaseDriver : AbstractReflectDbDriver
    {
        public static AbstractReflectDbDriver ReflectDatabaseDriver { get; set; } = new DefaultReflectDatabaseDriver();
        internal DefaultReflectDatabaseDriver():base("","","","","")
        {

        }
    }
    /// <summary>数据库对象反射抽象基类</summary>
    public abstract class AbstractReflectDbDriver
    {
        public static AbstractReflectDbDriver Instnace { get; set; } = DefaultReflectDatabaseDriver.ReflectDatabaseDriver;
        /// <summary> 数据库对象反射 信息</summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="connectionTypeName">IDbConnection 对象名称</param>
        /// <param name="commandTypeName">IDbCommand 对象名称</param>
        /// <param name="dataAdapterTypeName">IDbDataAdapter 对象名称</param>
        /// <param name="dataReaderTypeName">IDataReader 对象名称</param>
        protected AbstractReflectDbDriver(string assemblyName, string connectionTypeName, string commandTypeName, string dataAdapterTypeName, string dataReaderTypeName)
        {
            this.AssemblyName = assemblyName;
            this.ConnectionTypeName = connectionTypeName;
            this.CommandTypeName = commandTypeName;
            this.DataAdapterTypeName = dataAdapterTypeName;
            this.DataReaderTypeName = dataReaderTypeName;
        }

        /// <summary>创建数据库对象反射抽象基</summary>
        /// <param name="dialect"></param>
        /// <returns></returns>
        public static AbstractReflectDbDriver CreateDatabaseTypeFactory(DbFlag dialect)
        {
            if (Instnace == null)
            {
                Instnace = DefaultReflectDatabaseDriver.ReflectDatabaseDriver;
            }
            return Instnace.CreateReflectDatabaseDriver(dialect);
        }
        public virtual AbstractReflectDbDriver CreateReflectDatabaseDriver(DbFlag dialect)
        {
            switch (dialect)
            {
                case DbFlag.Oracle:
                    return OracleDbDriver.Empty;
                case DbFlag.SqlServer:
                    {
                        AbstractReflectDbDriver databaseProvider = SqlClientDbDriver.Empty;
                        if (databaseProvider.Check()) return databaseProvider;
                        databaseProvider = SqlServerDbDriver.Empty;
                        if (databaseProvider.Check()) return databaseProvider;
                        return SqlServerDbDriverV1.Empty;
                    }
                case DbFlag.Postgre:
                    return NpgsqlDbDriver.Empty;
                case DbFlag.Sqlite:
                    return SQLiteDbDriver.Empty;
                case DbFlag.Access:
                    if (AccessDbDriver.Empty.Check())
                    {
                        return AccessDbDriver.Empty;
                    }
                    return AccessV1DatabaseDriver.Empty;
                case DbFlag.MySql:
                default:
                    {
                        AbstractReflectDbDriver databaseProvider = MySqlDbDriver.Empty;
                        if (databaseProvider.Check()) return databaseProvider;
                        if (MySqlConnectorDbDriverV1.Empty.Check())
                        {
                            return MySqlConnectorDbDriverV1.Empty;
                        }
                        return MySqlConnectorDbDriver.Empty;
                    }

            }
        }
        /// <summary> 程序集名称 </summary>
        public string AssemblyName { get; private set; }
        /// <summary> IDbConnection 对象名称 </summary>
        public string ConnectionTypeName { get; private set; }
        /// <summary> IDbCommand 对象名称 </summary>
        public string CommandTypeName { get; private set; }
        /// <summary> IDbDataAdapter 对象名称 </summary>
        public string DataAdapterTypeName { get; private set; }
        /// <summary> IDataReader 对象名称 </summary>
        public string DataReaderTypeName { get; private set; }
        /// <summary>
        /// MySqlConnector 驱动 参数 格式化 只能通过反射实现 不然参数 格式化 无法 实现 要么显示实现
        /// </summary>

        public string DataParamterTypeName { get; set; }

        /// <summary>
        /// , Version=1.0.0.0, Culture=neutral, PublicKeyToken=d33d3e53aa5f8c92
        /// </summary>
        public virtual string VersionName { get; set; } = "";
        protected virtual bool IsSupportVersion { get; set; } = true;
        protected virtual Type ConnectionType { get; set; }
        public static DbFlag GetDialect(Type type)
        {
            var name = type.Name;
            if (name.Contains("OleDb"))
            {
                return DbFlag.Access;
             }
            else if (name.Contains("MySql"))
            {
                return DbFlag.MySql;
            }
            else if (name.Contains("Oracle"))
            {
                return DbFlag.Oracle;
            }
            else if (name.Contains("Npgsql"))
            {
                return DbFlag.Postgre;
            }
            else if (name.Contains("SQLite"))
            {
                return DbFlag.Sqlite;
            }
            else if (name.StartsWith("Sql"))
            {
                return DbFlag.SqlServer;
            }
            return DbFlag.None;
        }
        #region 反射创建对象
        /// <summary> 创建 IDbCommand 对象 </summary>
        /// <param name="typeName">IDbCommand 对象全名名称</param>
        /// <exception cref="DllNotFoundException"></exception>
        /// <returns></returns>
        public virtual DbCommand CreateCommand()
        {
            Type type = Type.GetType(this.CommandTypeName);
            if (type == null)
                throw new DllNotFoundException(this.CommandTypeName);
            return (DbCommand)Activator.CreateInstance(type);
        }

        /// <summary> 创建 IDbConnection 对象 </summary>
        /// <param name="connectionString">数据库连接地址</param>
        /// <exception cref="DllNotFoundException"></exception>
        /// <returns></returns>
        public virtual DbConnection CreateConnection(string connectionString)
        {
            string typeName = this.ConnectionTypeName;
            if (IsSupportVersion)
            {
                typeName = this.ConnectionTypeName + VersionName;
            }
            Type type = Type.GetType(typeName);
            if (type == null)
                throw new DllNotFoundException(typeName);
            return (DbConnection)Activator.CreateInstance(type, new object[] { connectionString });
        }

        /// <summary> 创建 IDbConnection 对象 </summary>
        /// <exception cref="DllNotFoundException"></exception>
        /// <returns></returns>
        public virtual DbConnection CreateConnection()
        {
            string typeName = this.ConnectionTypeName;
            if (IsSupportVersion)
            {
                typeName = this.ConnectionTypeName + VersionName;
            }
            Type type = Type.GetType(typeName);
            if (type == null)
                throw new DllNotFoundException(typeName);
            return (DbConnection)Activator.CreateInstance(type);
        }

        /// <summary>
        /// 检测不通过话  用 其他 包
        /// </summary>
        /// <returns></returns>
        protected virtual bool Check()
        {
            if (ConnectionType != null)
            {
                return true;
            }
            string typeName = this.ConnectionTypeName;
            if (IsSupportVersion)
            {
                typeName = this.ConnectionTypeName + VersionName;
            }
            Type type = Type.GetType(typeName);
            ConnectionType = type;
            return type != null;
        }
        /// <summary> 创建 IDbDataAdapter 对象 </summary>
        /// <param name="command" cref="IDbCommand"></param>
        /// <exception cref="DllNotFoundException"></exception>
        /// <returns></returns>
        public virtual DbDataAdapter CreateDataAdapter(IDbCommand command)
        {
            string typeName = this.DataAdapterTypeName;
            if (IsSupportVersion)
            {
                typeName = this.DataAdapterTypeName + VersionName;
            }
            Type type = Type.GetType(typeName);
            if (type == null)
                throw new DllNotFoundException(typeName);
            return (DbDataAdapter)Activator.CreateInstance(type, new object[] { command });
        }

        /// <summary> 创建 IDbDataAdapter 对象 </summary>
        /// <param name="command" cref="IDbCommand"></param>
        /// <exception cref="DllNotFoundException"></exception>
        /// <returns></returns>
        public virtual DbParameter CreateDataParamter(string name,object val)
        {
            string typeName = this.DataParamterTypeName;
            if (IsSupportVersion)
            {
                typeName = this.DataParamterTypeName + VersionName;
            }
            Type type = Type.GetType(typeName);
            if (type == null)
                throw new DllNotFoundException(typeName);
            return (DbParameter)Activator.CreateInstance(type, new object[] { name,val });
        }
        #endregion 反射创建对象
    }


   

}
#endif