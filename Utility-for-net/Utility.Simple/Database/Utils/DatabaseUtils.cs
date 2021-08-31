#if !( NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 ||  NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)

using System;
using System.Collections.Generic;
using System.Data;
#if !NET20 && !NET30 && !NET35
using System.Threading.Tasks;
using Utility.Database.Driver;
#endif

namespace Utility.Database.Utils
{
    /// <summary>
    /// database 操作类
    /// </summary>
    public class DatabaseUtils 
    {
        /// <summary> 根据 数据库连接对象 操作 表 </summary>
        /// <param name="connection" cref="IDbConnection">数据库连接对象</param>
        /// <param name="sql">sql</param>
        /// <param name="commandType" cref="CommandType">数据库命令操作</param>
        /// <param name="transaction" cref="IDbTransaction">事务</param>
        /// <param name="has"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(IDbConnection connection, string sql, CommandType commandType = CommandType.Text, IDbTransaction transaction = null,bool has=true)
        {
            if (connection.GetType().Assembly.FullName.StartsWith(MySqlConnectorDbDriver.Empty.AssemblyName))
            {
                Open(connection);
                //The transaction associated with this command is not the connection's active transaction; see https://fl.vu/mysql-trans
                using (transaction = transaction ?? connection.BeginTransaction())
                {
                    using (IDbCommand command = (IDbCommand)Activator.CreateInstance(Type.GetType(AbstractReflectDbDriver.CreateDatabaseTypeFactory(DbFlag.MySql).CommandTypeName), new object[] { connection, transaction }))
                    {
                        command.CommandText = sql;
                        command.CommandType = commandType;
                        return ExecuteNonQuery(command, transaction, has);
                    }
                }

            }
            using (IDbCommand command = CreateCommand(connection, sql, commandType))
            {
                return ExecuteNonQuery(command, transaction, has);
            }
        }

        /// <summary> 
        /// 设置参数
        /// <para>mysql datatable  MySqlConnector 查询不到数据算了 用法不同可能 拼接字符串可以 参数化用法不知道咋用 最好用官方的类库 </para>
        /// <para>Parameter '@databaseName' must be defined. To use this as a variable, set 'Allow User Variables=true' in the connection string.</para>
        /// <para>Database=company;Data Source=127.0.0.1;User Id=root;Password=wjp930514.;Old Guids=True;charset=utf8; Allow User Variables=True;</para>
        /// </summary>
        public static void SetCommandParamter(IDbCommand command, string name,object val)
        {
            if (command.GetType().Assembly.FullName.StartsWith(MySqlConnectorDbDriver.Empty.AssemblyName))
            {
                //参数格式化 要么
                object param1;
                Guid? guid2 = val as Guid?;
                if (guid2 == null)
                {
                   param1= AbstractReflectDbDriver.CreateDatabaseTypeFactory(DbFlag.MySql).CreateDataParamter(name,val is Guid guid ? guid.ToString("N") : val) ;//mysql 语法 报错 列类型 是 varchar(50)
                }
                else
                {
                    param1 = AbstractReflectDbDriver.CreateDatabaseTypeFactory(DbFlag.MySql).CreateDataParamter(name, guid2.HasValue ? guid2.Value.ToString("N") : Guid.NewGuid().ToString("N"));
                }
                command.Parameters.Add(param1);
                return;
            }
            var param = command.CreateParameter();
            command.Parameters.Add(param);
       
          
            Guid? guid1 = val as Guid?;
            if (guid1 == null)
            {
                param.Value = val is Guid guid ? guid.ToString("N") : val;//mysql 语法 报错 列类型 是 varchar(50)
            }
            else
            {
                if (guid1.HasValue)
                {
                    param.Value = guid1.Value.ToString("N");
                }
                else
                {
                    param.Value = Guid.NewGuid().ToString("N");
                }
            }
            //param.Value = val;
            param.ParameterName = name;
        }

        /// <summary> 设置参数 </summary>
        public static void SetCommandParamter(IDbCommand command, string[] names, object[] vals)
        {
            for (int i = 0; i < names.Length; i++)
            {
                SetCommandParamter(command,names[i],vals[i]);
            }
        }
        /// <summary> 查询 DataReader </summary>
        /// <param name="command" cref="IDbCommand">数据库命令对象</param>
        /// <returns cref="IDataReader">返回IDataReader</returns>
        public static IDataReader ExecuteReader(IDbCommand command)
        {
            Open(command);
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// 读取 string 数据
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static string[] ReaderString(IDbCommand command)
        {
            using (IDataReader reader = ExecuteReader(command))
            {
                List<string> values = new List<string>();
                while (reader.Read())
                {
                    if (!(reader.GetValue(0) is DBNull))
                    {
                        values.Add(reader.GetString(0));
                    }
                }
                return values.ToArray();
            }
        }

        /// <summary>
        /// 读取 long 数据
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static long[] ReaderLong(IDbCommand command)
        {
            using (IDataReader reader = ExecuteReader(command))
            {
                List<long> values = new List<long>();
                while (reader.Read())
                {
                    if (!(reader.GetValue(0) is DBNull))
                    {
                        values.Add(reader.GetInt64(0));
                    }
                }
                return values.ToArray();
            }
        }

        /// <summary> 根据 数据库连接对象 操作 表 </summary>
        /// <param name="connection" cref="IDbConnection">数据库连接对象</param>
        /// <param name="sql">sql</param>
        /// <param name="commandType" cref="CommandType">数据库命令操作</param>
        /// <param name="transaction" cref="IDbTransaction">事务</param>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public static object ExecuteScalar(IDbConnection connection, string sql, CommandType commandType = CommandType.Text, IDbTransaction transaction = null)
        {
            using (IDbCommand command = CreateCommand(connection, sql, commandType))
                return ExecuteScalar(command, transaction);
        }

        /// <summary> 根据 数据库连接对象 操作 表 </summary>
        /// <param name="connection" cref="IDbConnection">数据库连接对象</param>
        /// <param name="sql">sql</param>
        /// <param name="commandType" cref="CommandType">数据库命令操作</param>
        /// <returns cref="IDbCommand"></returns>
        public static IDbCommand CreateCommand(IDbConnection connection, string sql, CommandType commandType = CommandType.Text)
        {
            IDbCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = commandType;
            return command;
        }

        /// <summary> 根据 数据库执行命令 操作 </summary>
        /// <param name="command" cref="IDbCommand">数据库执行命令</param>
        /// <param name="transaction" cref="IDbTransaction">事务</param>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public static int ExecuteNonQuery(IDbCommand command, IDbTransaction transaction = null, bool autoCommit = true)
        {
            return Execute(command, it => it.ExecuteNonQuery(), transaction,autoCommit);
        }

        /// <summary>根据 数据库执行命令 操作  </summary>
        /// <param name="command" cref="IDbCommand">数据库执行命令</param>
        /// <param name="transaction" cref="IDbTransaction">事务</param>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public static object ExecuteScalar(IDbCommand command, IDbTransaction transaction = null)
        {
            return Execute(command, it => it.ExecuteScalar(), transaction,false);
        }
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
        /// <summary>
        /// 断开式 执行 适配器
        /// </summary>
        /// <param name="adapter">适配器</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(IDataAdapter adapter)
        {
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            return dataSet.Tables[0];
        }

        /// <summary>
        /// 断开式 执行 适配器
        /// </summary>
        /// <param name="adapter">适配器</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(IDataAdapter adapter)
        {
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            return dataSet;
        }
#if !(NET20 || NET30 ||NET35)
        /// <summary>
        /// 断开式 执行 适配器
        /// </summary>
        /// <param name="adapter">适配器</param>
        /// <returns></returns>
        public static Task<DataSet> ExecuteDataSetAsyn(IDataAdapter adapter)
        {
            TaskCompletionSource<DataSet> taskCompletionSource = new TaskCompletionSource<DataSet>();
            taskCompletionSource.SetResult(ExecuteDataSet(adapter));
            return taskCompletionSource.Task;
        }
#endif
#endif
        /// <summary> 根据 数据库执行命令 操作  </summary>
        /// <param name="command" cref="IDbCommand">数据库执行命令</param>
        /// <param name="func" cref="Func{T, TResult}">数据库操作</param>
        /// <param name="transaction" cref="IDbTransaction">事务</param>
        /// <param name="has">是否是否自动提交 默认是 特殊情况不予许</param>
        /// <exception cref="Exception"></exception>
        /// <returns cref="T"></returns>
        public static T Execute<T>(IDbCommand command, Func<IDbCommand, T> func, IDbTransaction transaction = null,bool has=true)
        {
            bool autoCommit = true;
            try
            {
                Open(command);
                if (transaction != null)
                {
                    autoCommit = false;
                }
                else
                {
                    //查询 用不用 事务(都没关系 用事务还影响性能) 
                    if (!has)
                    {
                        autoCommit = false;
                    }else
                    {
                        transaction = command.Connection.BeginTransaction();
                        command.Transaction = transaction;
                    }
                }
                T result = func(command);
                if(autoCommit)
                {
                    transaction?.Commit();
                }
                return result;
            }
            catch 
            {
                transaction?.Rollback();
                throw;
            }
            finally
            {
                if (autoCommit)
                {
                    transaction?.Dispose();
                    transaction = null;
                    command?.Dispose();
                    command = null;
                }
                //没影响
                //command?.Dispose();
                //command = null; 
            }
        }

#if !(NET20 || NET30 || NET35)
        /// <summary>
        /// 根据 数据库执行命令 操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
      /// <param name="command" cref="IDbCommand">数据库执行命令</param>
        /// <param name="func" cref="Func{T, TResult}">数据库操作</param>
        /// <param name="transaction" cref="IDbTransaction">事务</param>
        /// <returns></returns>
        public static Task<T> ExecuteAsyn<T>(IDbCommand command, Func<IDbCommand, T> func, IDbTransaction transaction = null)
        {
            return Task.Factory.StartNew<T>(() => Execute(command, func, transaction));
        }
#endif
        /// <summary>
        /// 开启数据库 连接
        /// </summary>
        /// <param name="command">数据库命令对象</param>
        public static void Open(IDbCommand command)
        {
            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Close();
                command.Connection.Open();
            }
        }

        /// <summary>
        /// 开启数据库 连接
        /// </summary>
        /// <param name="connection">数据库 连接对象</param>
        public static void Open(IDbConnection connection)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Close();
                connection.Open();
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="reader"></param>
        public static void DisposeDataReader(IDataReader reader)
        {
            reader?.Dispose();
            reader = null;
        }


        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="command"></param>
        public static void DisposeCommand(IDbCommand command)
        {
            command?.Dispose();
            command = null;
        }
    }
}
#endif