#if !(NET10 || NET11 || NET20 || NET30 || NET35  || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
//#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Utility.Dapper
{
    /// <summary> dapper 封装类 </summary>
    public class DapperTemplate : IDisposable
    {
        private  IDbConnection _connection;//数据库连接对象
        private IDbConnection write;
        private IDbConnection read;

        /// <summary> 构造注册数据库连接对象</summary>
        /// <param name="connection">数据库连接对象</param>
        public DapperTemplate(IDbConnection connection)
        {
            this._connection = connection;
            this.write = connection;
            this.read = connection;
        }
        /// <summary>
        ///  读写分离
        /// </summary>
        /// <param name="writeConnection"> 写库</param>
        /// <param name="readConnection">读库</param>
        public DapperTemplate(Lazy<IDbConnection> writeConnection, Lazy<IDbConnection> readConnection)
        {
            this.WriteConnection = writeConnection;
            this.ReadConnection = readConnection;
            this.Single = false;
        }
        /// <summary>
        /// 单库
        /// </summary>
        protected bool Single { get; set; } = true;

        /// <summary>数据库连接对象 </summary>
        public IDbConnection Connection { 
            get {
                _connection = _connection??write??read?? WriteConnection.Value;
                return _connection;
            }
        }

        /// <summary>数据库连接对象 </summary>
        public Lazy<IDbConnection> WriteConnection { get; protected set; }

        /// <summary>数据库连接对象 </summary>
        public Lazy<IDbConnection> ReadConnection { get; protected set; }

        /// <summary>数据库连接对象 </summary>
        public IDbConnection Write
        {
            get
            {
                write = write ?? WriteConnection.Value;
                return write;
            }
            protected set => write = value;
        }

        /// <summary>数据库连接对象 </summary>
        public IDbConnection Read { 
            get {
                read = read ?? ReadConnection.Value;
                return read;
            } 
            protected set => read = value;
        }

        /// <summary>
        /// 
        /// </summary>
        ~DapperTemplate()
        {
            Dispose();
        }


        #region
        /// <summary>添加 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public virtual int? Insert<T>(T obj, IDbTransaction transaction = null) where T : class => Insert(Write, obj, transaction);

        /// <summary>增删改</summary>
        /// <param name="sql">sql</param>
        /// <param name="obj">参数 实体 new{}都支持 </param>
        /// <returns></returns>
        public virtual int Execute(string sql, object obj = null, IDbTransaction transaction = null) => Execute(Write, sql, obj, transaction);

        /// <summary>查询 </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual dynamic FindSingle(string sql, object obj = null, IDbTransaction transaction = null) => FindSingle(Read, sql, obj, transaction);

        /// <summary>查询 </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual IEnumerable<dynamic> FindList(string sql, object obj = null, IDbTransaction transaction = null) => FindList(Read, sql, obj, transaction);

        /// <summary>添加 </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Key"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual Key Insert<T, Key>(T obj, IDbTransaction transaction = null) where T : class => Insert<T, Key>(Write, obj, transaction);

        /// <summary> 修改 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public virtual int Update<T>(T obj, IDbTransaction transaction = null) where T : class => Update(Write, obj, transaction);

        /// <summary> 删除 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public virtual int Delete<T>(T obj, IDbTransaction transaction = null) where T : class => Delete(Write, obj, transaction);


        /// <summary> 删除 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">实体 id</param>
        /// <returns></returns>
        public virtual int Delete<T>(object id, IDbTransaction transaction = null) where T : class => Delete<T>(Write, id, transaction);

        /// <summary> 删除 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual int DeleteList<T>(object where = null, IDbTransaction transaction = null) where T : class => DeleteList<T>(Write, where, transaction);

        /// <summary> 删除 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">条件</param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual int DeleteList<T>(string where = null, object param = null, IDbTransaction transaction = null) where T : class => DeleteList<T>(Write, where, param, transaction);


        /// <summary> 查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual IEnumerable<T> FindList<T>(object where = null, IDbTransaction transaction = null) where T : class => FindList<T>(Read, where, transaction);


        /// <summary> 查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual IEnumerable<T> FindList<T>( IDbTransaction transaction = null) where T : class => FindList<T>(Read,transaction);

        /// <summary> 查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">条件</param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> FindList<T>(string where = null, object param = null, IDbTransaction transaction = null) where T : class => FindList<T>(Read, where, param, transaction);

        /// <summary> 查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">条件</param>
        /// <param name="parameters"></param>
        /// <param name="orderby"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> FindListByPage<T>(string where = null, string orderby = "", object parameters = null, int page = 1, int size = 10, IDbTransaction transaction = null) where T : class => FindListByPage<T>(Read, where, orderby, parameters, page, size, transaction);


        /// <summary> 查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">id</param>
        /// <returns></returns>
        public virtual T FindSingle<T>(object id, IDbTransaction transaction = null) where T : class => FindSingle<T>(Read, id, transaction);


        /// <summary> 查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditions">条件</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public virtual long Count<T>(string conditions, object param, IDbTransaction transaction = null) where T : class => Count<T>(Read, conditions, param, transaction);

        /// <summary> 查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual long Count<T>(object where, IDbTransaction transaction = null) where T : class => Count<T>(Read, where, transaction);
        #endregion

        #region static method
        /// <summary>添加 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public static int? Insert<T>(IDbConnection connection, T obj, IDbTransaction transaction = null) where T : class => connection.Insert(obj, transaction);

        /// <summary>增删改</summary>
        /// <param name="sql">sql</param>
        /// <param name="connection"></param>
        /// <param name="obj">参数 实体 new{}都支持 </param>
        /// <returns></returns>
        public static int Execute(IDbConnection connection, string sql, object obj = null, IDbTransaction transaction = null) => connection.Execute(sql, obj, transaction);

        /// <summary>查询 </summary>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static dynamic FindSingle(IDbConnection connection, string sql, object obj = null, IDbTransaction transaction = null) => connection.QueryFirst(sql, obj, transaction);

        /// <summary>查询 </summary>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> FindList(IDbConnection connection, string sql, object obj = null, IDbTransaction transaction = null) => connection.Query(sql, obj, transaction);


        /// <summary>添加 </summary>
        /// <param name="connection"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Key"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Key Insert<T, Key>(IDbConnection connection, T obj, IDbTransaction transaction = null) where T : class =>
#if !(NET40 || NET45)
            connection.Insert<Key, T>(obj, transaction);
#else
            throw new NotSupportedException();
#endif


        /// <summary> 修改 </summary>
        /// <param name="connection"></param>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public static int Update<T>(IDbConnection connection, T obj, IDbTransaction transaction = null) where T : class => connection.Update(obj, transaction);

        /// <summary> 删除 </summary>
        /// <param name="connection"></param>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public static int Delete<T>(IDbConnection connection, T obj, IDbTransaction transaction = null) where T : class => connection.Delete(obj, transaction);

        /// <summary> 删除 </summary>
        /// <param name="connection"></param>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">实体 id</param>
        /// <returns></returns>
        public static int Delete<T>(IDbConnection connection, object id, IDbTransaction transaction = null) where T : class => connection.Delete<T>(id, transaction);

        /// <summary> 删除 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public static int DeleteList<T>(IDbConnection connection, object where = null, IDbTransaction transaction = null) where T : class => connection.DeleteList<T>(where, transaction);

        /// <summary> 删除 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="where">条件</param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int DeleteList<T>(IDbConnection connection, string where = null, object param = null, IDbTransaction transaction = null) where T : class => connection.DeleteList<T>(where, param, transaction);


        /// <summary>查询 </summary>
        /// <param name="connection"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static IEnumerable<T> FindList<T>(IDbConnection connection, object where = null, IDbTransaction transaction = null) where T : class => connection.GetList<T>(where, transaction);

        /// <summary>查询 </summary>
        /// <param name="connection"></param>
        public static IEnumerable<T> FindList<T>(IDbConnection connection, IDbTransaction transaction = null) where T : class => connection.GetList<T>();

        /// <summary>查询 </summary>
        /// <param name="connection"></param>
        /// <param name="where"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IEnumerable<T> FindList<T>(IDbConnection connection, string where = null, object param = null, IDbTransaction transaction = null) where T : class => connection.GetList<T>(where, param, transaction);

        /// <summary>查询 </summary>
        /// <param name="connection"></param>
        /// <param name="where"></param>
        /// <param name="parameters"></param>
        /// <param name="orderby"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static IEnumerable<T> FindListByPage<T>(IDbConnection connection, string where = null, string orderby = "", object parameters = null, int page = 1, int size = 10, IDbTransaction transaction = null) where T : class => connection.GetListPaged<T>(page, size, where, orderby, parameters, transaction);

        /// <summary>
        /// 根据 id 查询对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T FindSingle<T>(IDbConnection connection, object id, IDbTransaction transaction = null) where T : class
        {
            return connection.Get<T>(id,transaction);
        }

        /// <summary>
        /// 根据条件查询数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="where"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static long Count<T>(IDbConnection connection, string where = null, object param = null, IDbTransaction transaction = null) where T : class => connection.RecordCount<T>(where, param, transaction);

        /// <summary>
        /// 根据条件查询数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static long Count<T>(IDbConnection connection, object where = null, IDbTransaction transaction = null) where T : class => connection.RecordCount<T>(where, transaction);
        #endregion  static method

        #region async

        /// <summary>添加 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public virtual Task<int?> InsertAsync<T>(T obj, IDbTransaction transaction = null) where T : class
        {
            return InsertAsync(Write, obj, transaction);
        }

        /// <summary>
        /// 增 删  改 操作
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual Task<int> ExecuteAsync(string sql, object obj = null, IDbTransaction transaction = null)
        {
            return ExecuteAsync(Write, sql, obj, transaction);
        }

        /// <summary>
        /// 根据条件查询一条数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual Task<dynamic> FindSingleAsync(string sql, object obj = null, IDbTransaction transaction = null)
        {
            return FindSingleAsync(Read, sql, obj, transaction);
        }
        /// <summary>
        /// 根据条件 查询 数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>

        public virtual Task<IEnumerable<dynamic>> FindListAsync(string sql, object obj = null, IDbTransaction transaction = null)
        {
            return FindListAsync(Read, sql, obj, transaction);
        }
        /// <summary>添加 </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Key"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual Task<Key> InsertAsync<T, Key>(T obj, IDbTransaction transaction = null) where T : class
        {
            return InsertAsync<T, Key>(Read, obj, transaction);
        }

        /// <summary> 修改 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public virtual Task<int> UpdateAsync<T>(T obj, IDbTransaction transaction = null) where T : class
        {
            return UpdateAsync<T>(Read, obj, transaction);
        }

        /// <summary> 删除 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public virtual Task<int> DeleteAsync<T>(T obj, IDbTransaction transaction = null) where T : class
        {
            return DeleteAsync<T>(Write, obj, transaction);
        }

        /// <summary>
        /// 根据 id 删除 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<int> DeleteAsync<T>(object id, IDbTransaction transaction = null) where T : class
        {
            return DeleteAsync<T>(Write, id, transaction);
        }

        /// <summary> 删除 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual Task<int> DeleteListAsync<T>(object where = null, IDbTransaction transaction = null) where T : class
        {
            return DeleteListAsync<T>(Write, where, transaction);
        }

        /// <summary> 删除 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">条件</param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual Task<int> DeleteListAsync<T>(string where = null, object param = null, IDbTransaction transaction = null) where T : class
        {
            return DeleteListAsync<T>(Write, where, param, transaction);
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<T>> FindListAsync<T>(object where = null, IDbTransaction transaction = null) where T : class
        {
            return FindListAsync<T>(Read, where, transaction);
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual Task<IEnumerable<T>> FindListAsync<T>(IDbTransaction transaction = null) where T : class
        {
            return FindListAsync<T>(Read, transaction);
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<T>> FindListAsync<T>(string where = null, object param = null, IDbTransaction transaction = null) where T : class
        {
            return FindListAsync<T>(Read, where, param, transaction);
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <param name="parameters"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<T>> FindListByPageAsync<T>(string where = null, string orderby = "", object parameters = null, int page = 1, int size = 10, IDbTransaction transaction = null) where T : class
        {
            return FindListByPageAsync<T>(Read, where, orderby, parameters, page, size, transaction);
        }

        /// <summary>
        /// 根据 id 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<T> FindSingleAsync<T>(object id, IDbTransaction transaction = null) where T : class
        {
            return FindSingleAsync<T>(Read, id, transaction);
        }

        /// <summary>
        /// 根据条件 查询 数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual Task<long> CountAsync<T>(string where = null, object param = null, IDbTransaction transaction = null) where T : class
        {
            return CountAsync<T>(Read, where, param, transaction);
        }

        /// <summary>
        /// 根据条件 查询 数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual Task<long> CountAsync<T>(object where = null, IDbTransaction transaction = null) where T : class
        {
            return CountAsync<T>(Read, where, transaction);
        }

        #endregion async

        #region async static method



        /// <summary>添加 </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Key"></typeparam>
        /// <param name="connection">连接对象</param>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public static Task<Key> InsertAsync<T, Key>(IDbConnection connection, T obj, IDbTransaction transaction = null) where T : class
        {
#if NET40 || NET45
            return Task.FromResult(Insert<T,Key>(connection,obj,transaction));
#else
            return connection.InsertAsync<Key, T>(obj, transaction);
#endif
        }

        /// <summary>添加 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">连接对象</param>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public static Task<int?> InsertAsync<T>(IDbConnection connection, T obj, IDbTransaction transaction = null) where T : class
        {
#if NET40
            return Task.FromResult(Insert(connection,obj,transaction));
#else
            return connection.InsertAsync(obj, transaction);
#endif
        }

        /// <summary>
        /// 增 删 改
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Task<int> ExecuteAsync(IDbConnection connection, string sql, object obj = null, IDbTransaction transaction = null)
        {
#if NET40
            return Task.FromResult(Execute(connection,sql,obj,transaction));
#else
            return connection.ExecuteAsync(sql, obj, transaction);
#endif
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="sql"></param>
        /// <param name="obj">条件</param>
        /// <returns></returns>
        public static Task<dynamic> FindSingleAsync(IDbConnection connection, string sql, object obj = null, IDbTransaction transaction = null)
        {
#if NET40
            return Task.FromResult(FindSingle(connection,sql,obj,transaction));
#else
            return connection.QueryFirstAsync<dynamic>(sql, obj, transaction);
#endif
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="sql"></param>
        /// <param name="obj">条件</param>
        /// <returns></returns>
        public static Task<IEnumerable<dynamic>> FindListAsync(IDbConnection connection, string sql, object obj = null, IDbTransaction transaction = null)
        {
#if NET40
            return Task.FromResult(FindList(connection,sql,obj,transaction));
#else
            return connection.QueryAsync(sql, obj,transaction);
#endif
        }


        /// <summary>添加 </summary>
        /// <typeparam name="Key"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">连接对象</param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Task<Key> InsertAsync<Key, T>(IDbConnection connection, T obj, IDbTransaction transaction = null) where T : class
        {
#if NET40 || NET45
            throw new NotSupportedException();
#else
            return connection.InsertAsync<Key, T>(obj,transaction);
#endif
        }

        /// <summary> 修改 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">连接对象</param>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public static Task<int> UpdateAsync<T>(IDbConnection connection, T obj, IDbTransaction transaction = null) where T : class
        {
#if NET40
            return Task.FromResult(Update(connection,obj,transaction));
#else
            return connection.UpdateAsync(obj,transaction);
#endif
        }

        /// <summary> 删除 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">连接对象</param>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public static Task<int> DeleteAsync<T>(IDbConnection connection, T obj, IDbTransaction transaction = null) where T : class
        {
#if NET40
            return Task.FromResult(Delete(connection,obj,transaction));
#else
            return connection.DeleteAsync(obj,transaction);
#endif
        }

        /// <summary> 删除 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">连接对象</param>
        /// <param name="id">实体 id</param>
        /// <returns></returns>
        public static Task<int> DeleteAsync<T>(IDbConnection connection, object id, IDbTransaction transaction = null) where T : class
        {
#if NET40
            return Task.FromResult(Delete<T>(connection,id,transaction));
#else
            return connection.DeleteAsync<T>(id,transaction);
#endif
        }

        /// <summary> 删除 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">连接对象</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public static Task<int> DeleteListAsync<T>(IDbConnection connection, object where = null, IDbTransaction transaction = null) where T : class
        {
#if NET40
            return Task.FromResult(DeleteList<T>(connection,where,transaction));
#else
            return connection.DeleteListAsync<T>(where,transaction);
#endif
        }

        /// <summary> 删除 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">连接对象</param>
        /// <param name="where">条件</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static Task<int> DeleteListAsync<T>(IDbConnection connection, string where = null, object param = null, IDbTransaction transaction = null) where T : class
        {
#if NET40
            return Task.FromResult(DeleteList<T>(connection,where,param,transaction));
#else
            return connection.DeleteListAsync<T>(where, param,transaction);
#endif
        }


        /// <summary> 查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">连接对象</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public static Task<IEnumerable<T>> FindListAsync<T>(IDbConnection connection, object where = null, IDbTransaction transaction = null) where T : class
        {
#if NET40
            return Task.FromResult(FindList<T>(connection,where,transaction));
#else
            return connection.GetListAsync<T>(where,transaction);
#endif
        }

        /// <summary> 查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">连接对象</param>
        /// <returns></returns>
        public static Task<IEnumerable<T>> FindListAsync<T>(IDbConnection connection, IDbTransaction transaction = null) where T : class
        {
#if NET40
            return Task.FromResult(FindList<T>(connection,transaction));
#else
            return connection.GetListAsync<T>(transaction);
#endif
        }

        /// <summary> 查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">连接对象</param>
        /// <param name="where">条件</param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Task<IEnumerable<T>> FindListAsync<T>(IDbConnection connection, string where = null, object param = null, IDbTransaction transaction = null) where T : class
        {
#if NET40
            return Task.FromResult(FindList<T>(connection,where,param,transaction));
#else
            return connection.GetListAsync<T>(where, param,transaction);
#endif
        }

        /// <summary> 查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">连接对象</param>
        /// <param name="where">条件</param>
        /// <param name="parameters"></param>
        /// <param name="size"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public static Task<IEnumerable<T>> FindListByPageAsync<T>(IDbConnection connection, string where = null, string orderby = "", object parameters = null, int page = 1, int size = 10, IDbTransaction transaction = null) where T : class
        {
#if NET40
            return Task.FromResult(FindListByPage<T>(connection,where, orderby, parameters,page, size,transaction));
#else
            return connection.GetListPagedAsync<T>(page, size, where, orderby, parameters,transaction);
#endif
        }


        /// <summary> 查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">连接对象</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static Task<T> FindSingleAsync<T>(IDbConnection connection, object id, IDbTransaction transaction = null) where T : class
        {
#if NET40
            return Task.FromResult(FindSingle<T>(connection,id,transaction));
#else
            return connection.GetAsync<T>(id,transaction);
#endif
        }

        /// <summary> 查询 数量 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">连接对象</param>
        /// <param name="where">条件</param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Task<long> CountAsync<T>(IDbConnection connection, string where = null, object param = null, IDbTransaction transaction = null) where T : class
        {
#if NET40
            return Task.FromResult((long)Count<T>(connection,where, param,transaction));
#else
            return Task.FromResult((long)connection.RecordCountAsync<T>(where, param, transaction).Result);
#endif
        }

        /// <summary> 查询 数量 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">连接对象</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public static Task<long> CountAsync<T>(IDbConnection connection, object where = null, IDbTransaction transaction = null) where T : class
        {
#if NET40
            return Task.FromResult((long)Count<T>(connection,where,transaction));
#else
            return Task.FromResult((long)connection.RecordCountAsync<T>(where,transaction).Result);
#endif
        }


        /// <summary> 查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">连接对象</param>
        /// <param name="where">条件</param>
        /// <param name="obj"></param>
        /// <param name="size"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static Tuple<List<T>, long> FindTupleByPage<T>(IDbConnection connection, T obj, string where = null, int page = 1, int size = 10, IDbTransaction transaction = null) where T : class
        {
            var res = connection.GetListPaged<T>(page, size, where,/*"  ORDER BY  ID ASC "*/"", obj,transaction);
            var datas = res.ToList();
            var count = Count<T>(connection, where, obj,transaction);
            return new Tuple<List<T>, long>(datas, count);
        }


        /// <summary> 查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">连接对象</param>
        /// <param name="where">条件</param>
        /// <param name="obj"></param>
        /// <param name="size"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static Task<Tuple<List<T>, long>> FindTupleByPageAsync<T>(IDbConnection connection, T obj, string where, int page = 1, int size = 10,IDbTransaction transaction=null) where T : class
        {
#if NET40
            throw new NotSupportedException();
#else
            var res = connection.GetListPagedAsync<T>(page, size, where,/*"  ORDER BY  ID ASC "*/"", obj,transaction).Result;
            var datas = res.ToList();
            var count = CountAsync<T>(connection, where, obj,transaction).Result;
            return Task.FromResult( new Tuple<List<T>, long>(datas, count));
#endif
        }


        /// <summary> 查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">条件</param>
        /// <param name="obj"></param>
        /// <param name="size"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public Tuple<List<T>, long> FindTupleByPage<T>(T obj, string where = null, int page = 1, int size = 10, IDbTransaction transaction = null) where T : class
        {
            return FindTupleByPage(Read, obj, where, page, size,transaction);
        }


        /// <summary> 查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">条件</param>
        /// <param name="obj"></param>
        /// <param name="size"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public Task<Tuple<List<T>, long>> FindTupleByPageAsync<T>(T obj, string where, int page = 1, int size = 10, IDbTransaction transaction = null) where T : class
        {
            return FindTupleByPageAsync(Read, obj, where, page, size,transaction);
        }
        #endregion async static method

        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void Dispose()
        {
            if (this.Single)
            {
                _connection.Close();
                _connection.Dispose();
            }
            else
            {
                if (write != null)
                {
                    write.Close();
                    write.Dispose();
                }
                if (read != null)
                {
                    read.Close();
                    read.Dispose();
                }
            }

            GC.SuppressFinalize(this);
        }
    }
}
#endif