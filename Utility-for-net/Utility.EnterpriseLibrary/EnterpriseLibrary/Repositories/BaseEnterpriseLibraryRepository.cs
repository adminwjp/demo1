#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using Utility.Domain.Repositories;
using Utility.Application.Services.Dtos;
using Utility.Domain.Entities;
using System.Linq.Expressions;
using Utility.Attributes;
using Utility.Domain.Uow;

namespace Utility.EnterpriseLibrary.Repositories
{
    /// <summary>实体  企业库 数据访问层接口 实现  </summary>
    [Transtation]
    public class BaseEnterpriseLibraryRepository<T, Key> : IRepository<T, Key> where T : class,IEntity<Key>
    {
        /// <summary>
        /// 
        /// </summary>
        protected string ConnectionStringName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected Microsoft.Practices.EnterpriseLibrary.Data.Database Database { get; set; }

        public IDbConnection Connection => throw new NotImplementedException();

        public IUnitWork UnitWork => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        public BaseEnterpriseLibraryRepository()
        {
            Database = DatabaseFactory.CreateDatabase(ConnectionStringName);
        }


        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        public virtual Task<int> InsertAsync(T obj, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Insert(obj));
        }

        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        public virtual int Insert(T obj)
        {
            return AddOrUpdateOperator(obj);
        }

        protected virtual int AddOrUpdateOperator(T obj)
        {
            Action<DbCommand> action = (command) => { AddOrUpdate(command, obj); };
            return EnterpriseLibraryDbHelper.Operator(action);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="obj"></param>
        protected virtual int AddOrUpdate(DbCommand command, T obj)
        {
            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual string GetTable()
        {
            return string.Empty;
        }
        /// <summary>修改实体类信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        public virtual System.Threading.Tasks.Task<int> UpdateAsync(T obj, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Update(obj));
        }

        /// <summary>修改实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        public virtual int Update(T obj)
        {
            return AddOrUpdateOperator(obj);
        }

        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        public virtual Task<int> DeleteAsync(Key id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Delete(id));
        }

        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        public virtual int Delete(Key id)
        {
            return EnterpriseLibraryDbHelper.Delete(GetTable(), id);
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        public virtual Task<int> DeleteListAsync(Key[] ids, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(DeleteList(ids));
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        public virtual int DeleteList(Key[] ids)
        {
          return   EnterpriseLibraryDbHelper.DeleteList(GetTable(), ids);
        }

        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回实体类数据集信息 </return>
        public virtual Task<List<T>> FindListAsync(T obj, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(FindList(obj));
        }

        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集信息 </return>
        public virtual List<T> FindList(T obj)
        {
            DbConnection connection = null;
            DbCommand command = null;
            try
            {
                connection = Database.CreateConnection();
                command = connection.CreateCommand();
                command.CommandText = $"SELECT {QuerySelectColumn()} FROM {GetTable()}";
                GetWhere(obj, command);
                var reader = Database.ExecuteReader(command);
                try
                {
                    return Reader(reader);
                }
                finally
                {
                    reader.Dispose();
                }
            }
            finally
            {
                command?.Dispose();
                connection?.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected virtual List<T> Reader(IDataReader reader)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="command"></param>
        protected virtual void GetWhere(T obj, DbCommand command)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual string QuerySelectColumn()
        {
            return string.Empty;
        }
        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回实体类数据集数量信息</return>
        public virtual Task<long> CountAsync(T obj,CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Count(obj));
        }
        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集数量信息</return>
        public virtual long Count(T obj)
        {
            DbConnection connection = null;
            DbCommand command = null;
            try
            {
                connection = Database.CreateConnection();
                command = connection.CreateCommand();
                GetWhere(obj, command);
                string where = command.CommandText;
                long num = Count(where, command);
                return num;
            }
            finally
            {
                command.Dispose();
                connection.Dispose();
            }
        }

        private long Count(string where, DbCommand command)
        {
            return EnterpriseLibraryDbHelper.Count(GetTable(), where, command);
        }

        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回实体类数据集信息</return>
        public Task<List<T>> FindListByPageAsync(T obj, int page, int size,CancellationToken cancellationToken = default)
        {
            return Task.FromResult(FindListByPage(obj, page, size));
        }
        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息</return>
        public List<T> FindListByPage(T obj, int page, int size)
        {
            DbConnection connection = null;
            DbCommand command = null;
            try
            {
                connection = Database.CreateConnection();
                command = connection.CreateCommand();
                //mysql
                GetWhere(obj, command);
                string where = command.CommandText;
                return FindListByPage(page, size, where, command);
            }
            finally
            {
                command?.Dispose();
                connection?.Dispose();
            }
        }

        private List<T> FindListByPage(int page, int size, string where, DbCommand command)
        {
            //mysql
            command.CommandText = $"SELECT {QuerySelectColumn()} FROM {GetTable()} {where} LIMIT {page - 1},{size};";
            var reader = Database.ExecuteReader(command);
            try
            {
                return Reader(reader);
            }
            finally
            {
                reader.Dispose();
            }
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        public Task<ResultDto<T>> FindResultModelByPageAsync(T obj, int page, int size,CancellationToken cancellationToken = default)
        {
            return Task.FromResult(FindResultModelByPage(obj, page, size));
        }
        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        public virtual ResultDto<T> FindResultModelByPage(T obj, int page, int size)
        {
            DbConnection connection = null;
            DbCommand command = null;
            try
            {
                connection = Database.CreateConnection();
                command = connection.CreateCommand();
                GetWhere(obj, command);
                string where = command.CommandText;
                //mysql
                List<T> datas = FindListByPage(page, size, where, command);
                long count = Count(where, command);
                ResultDto<T> result = new ResultDto<T>();
                result.Data = datas.Any() ? datas : null;
                result.Result = new PageResultDto() { Records = count, Total =(int) (count == 0 ? 0 : count % size == 0 ? count / size : (count / size + 1)), Size = size, Page = page };
                return result;
            }
            finally
            {
                command?.Dispose();
                connection?.Dispose();
            }
        }

    

        public T FindSingle(Expression<Func<T, bool>> where = null)
        {
            throw new NotImplementedException();
        }

        public T FindSingleByEntity(T entity)
        {
            throw new NotImplementedException();
        }

        public T FindSingle(object id)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(Expression<Func<T, bool>> where = null)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(object id)
        {
            throw new NotImplementedException();
        }

        public bool IsExistByEntity(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> where = null)
        {
            throw new NotImplementedException();
        }

        public List<T> FindListByEntity(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> QueryByPage(Expression<Func<T, bool>> where = null, int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }

        public List<T> FindListByEntityAndPage(T entity, int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }

        public ResultDto<T> FindResultByEntityAndPage(T obj, int page, int size)
        {
            throw new NotImplementedException();
        }

        public long Count(Expression<Func<T, bool>> where = null)
        {
            throw new NotImplementedException();
        }

        public long CountByEntity(T entity)
        {
            throw new NotImplementedException();
        }

        public int BatchInsert(T[] entities)
        {
            throw new NotImplementedException();
        }

        public int Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public int Update(Expression<Func<T, bool>> where, Expression<Func<T, T>> update)
        {
            throw new NotImplementedException();
        }

        public int Delete(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public int ExecuteSql(string sql)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindSingleAsync(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindSingleByEntityAsync(T entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindSingleAsync(object id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistAsync(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistByEntityAsync(T entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistAsync(object id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<T>> QueryAsync(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> FindListByEntityAsync(T entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<T>> QueryByPageAsync(Expression<Func<T, bool>> where = null, int page = 1, int size = 10, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> FindListByEntityAndPageAsync(T entity, int page = 1, int size = 10, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountByEntityAsync(T entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> BatchInsertAsync(T[] entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Expression<Func<T, bool>> where, Expression<Func<T, T>> update, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExecuteSqlAsync(string sql, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
#endif