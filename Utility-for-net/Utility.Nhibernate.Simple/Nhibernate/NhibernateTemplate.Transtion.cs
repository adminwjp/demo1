#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
//#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;
using NHibernate.Linq;
using Utility.Nhibernate.Infrastructure;
using System.Threading;
using NHibernate.Criterion;
using Utility.Helpers;
using Utility.Threads;
//async await >=45 

namespace Utility.Nhibernate
{
    /// <summary> Nihernate 模板 </summary>
    public partial class NhibernateTemplate
    { 

#region ISession
        /// <summary> Nihernate 添加操作 </summary>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public virtual object Insert<T>(ISession session, T obj) where T : class
        {
            if (obj == null) throw new ArgumentNullException("obj");
			ITransaction transaction = null;
			try
			{
				using (transaction = session.BeginTransaction())
				{
					object result =  Add(session,obj);
					transaction.Commit();
					return result;
				}
			}
			catch (Exception e)
			{
				if (transaction != null)
				{
					transaction.Rollback();
				}
                return -1;
			}
			
        }

     
        /// <summary> Nihernate 添加操作 </summary>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public virtual int BatchInsert<T>(ISession session, T[] obj) where T : class
        {
            if (obj == null||obj.Length==0) throw new ArgumentNullException("obj");
			using (ITransaction transaction = session.BeginTransaction())
			{
				int res = BatchAdd(session,obj);
				transaction.Commit();
				return res;
			}
        }


        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="obj"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual
#if !(NET40 || NET45 || NET451 || NET452 || NET46)
            async
#endif
            Task<int> BatchInsertAsync<T>(ISession session, T[] obj, CancellationToken cancellationToken = default) where T : class
        {
#if (NET40 || NET45 || NET451 || NET452 || NET46)
           return Task.FromResult(BatchInsert(session,obj));
#else
            if (obj == null || obj.Length == 0) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    int res =await BatchAddAsync(session,obj,cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    return res;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                   await transaction.RollbackAsync(cancellationToken);
                }
                throw e;
            }
#endif
		}

     
    /// <summary> Nihernate 添加异步操作 </summary>
    /// <param name="session"><see cref="ISession"/></param>
    /// <param name="obj"></param>
    ///<param name="cancellationToken"></param>
    /// <returns>获取主键编号</returns>
    public virtual 
#if !(NET40 || NET45 || NET451 || NET452 || NET46)
    async
#endif
    Task<object> InsertAsync<T>(ISession session, T obj, CancellationToken cancellationToken = default) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return Task.FromResult(Insert(session,obj));
#else
            if (obj == null) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction =  session.BeginTransaction())
                {
                    var  result =await AddAsync(session,obj, cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    return result;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    await transaction.RollbackAsync(cancellationToken);
                }
                return Task.FromResult((object)null);
            }
#endif
        }


        /// <summary> Nihernate 修改操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual void Update<T>(ISession session, T obj) where T : class
        {
            if (obj == null) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    Modify(session,obj);
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
            }
        }
		

        /// <summary> Nihernate 修改操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual void BatchUpdate<T>(ISession session, T[] obj) where T : class
        {
            if (obj == null || obj.Length == 0) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    BatchModify(session,obj);
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
            }
        }
	

        /// <summary> Nihernate 修改异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="obj"></param>
        ///<param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task UpdateAsync<T>(ISession session, T obj, CancellationToken cancellationToken = default) where T : class
        {

#if NET40 || NET45 || NET451 || NET452 || NET46
            Update(session, obj);
           return Task.CompletedTask;
#else
            if (obj == null ) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    Task task = ModifyAsync(session,obj,cancellationToken);
                    transaction.CommitAsync();
                    return task;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.RollbackAsync();
                }
                throw e;
            }
#endif
        }
		
     

        /// <summary> Nihernate 删除操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="obj"></param>
        public virtual void Delete<T>(ISession session, T obj) where T : class
        {
            if (obj == null ) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    Remove(session,obj);
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
            }
        }

		
        /// <summary> Nihernate 删除操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="id"></param>
        public virtual void Delete<T>(ISession session, object id) where T : class
        {
            if (string.IsNullOrEmpty(id?.ToString())) throw new ArgumentNullException("id");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    Remove<T>(session,id);
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
            }
        }
		

        /// <summary> Nihernate 删除异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="obj"></param>
        ///<param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task DeleteAsync<T>(ISession session, T obj, CancellationToken cancellationToken = default) where T : class
        {


#if NET40 || NET45 || NET451 || NET452 || NET46
            Delete(session,obj);
            return Task.CompletedTask;
#else
            if (obj == null ) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    Task task = RemoveAsync(session,obj,cancellationToken);
                    transaction.CommitAsync();
                    return task;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                   return transaction.RollbackAsync();
                }
                return TaskHelper.CompletedTask;
            }
#endif
        }

       

        /// <summary> Nihernate 删除异步操作 </summary>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="id"></param>
        ///<param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task DeleteAsync<T>(ISession session, object id, CancellationToken cancellationToken = default) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            Delete<T>(session,id);
            return Task.CompletedTask;
#else
            if (string.IsNullOrEmpty(id?.ToString())) throw new ArgumentNullException("id");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    Task task = RemoveAsync<T>(session,id,cancellationToken);
                    transaction.CommitAsync(cancellationToken);
                    return task;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                 return   transaction.RollbackAsync(cancellationToken);
                }
                return TaskHelper.CompletedTask;
            }
#endif
        }

        /// <summary> Nihernate 查询操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T FindSingle<T>(ISession session, object id) where T : class
        {
            if (string.IsNullOrEmpty(id?.ToString())) throw new ArgumentNullException("id");
            using (ITransaction transaction = session.BeginTransaction())
            {
                return Get<T>(session,id);
            }
        }
		
    
		
        /// <summary> Nihernate 查询异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<T> FindSingleAsync<T>(ISession session, object id) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return Task.FromResult(FindSingle<T>(session,id));
#else
            if (string.IsNullOrEmpty(id?.ToString())) throw new ArgumentNullException("id");
            using (ITransaction transaction = session.BeginTransaction())
                return GetAsync<T>(session,id);
#endif
        }

        /// <summary>
        /// 根据实体名称查询，没有则根据泛型查询  异常
        /// identifier of an instance of  was altered from 10 to 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public virtual IQueryable<T> Query<T>(ISession session, string entityName = null) where T : class
        {
            using (ITransaction transaction = session.BeginTransaction())
			{
				return GetQuery<T>(session,entityName);
			}
        }

    
        /// <summary>
        /// 根据实体名称查询，没有则根据泛型查询  异常
        /// identifier of an instance of  was altered from 10 to 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public virtual IQueryable<T> Query<T>(IStatelessSession session, string entityName = null) where T : class
        {
            using (ITransaction transaction = session.BeginTransaction())
			{
				return GetQuery<T>(session,entityName);
			}
        }

       
		
        /// <summary>
        /// 根据条件查询，没有则根据泛型查询  异常
        /// identifier of an instance of  was altered from 10 to 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IQueryable<T> Query<T>(ISession session, Expression<Func<T, bool>> where = null) where T : class
        {
            using (ITransaction transaction = session.BeginTransaction())
			{
				return GetQuery<T>(session,where);
			}
        }


        /// <summary>
        /// 根据条件查询，没有则根据泛型查询  异常
        /// identifier of an instance of  was altered from 10 to 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IQueryable<T> Query<T>(IStatelessSession session, Expression<Func<T, bool>> where = null) where T : class
        {
            using (ITransaction transaction = session.BeginTransaction())
			{
				return GetQuery<T>(session,where);
			}
        }


        /// <summary>根据实体类型查询，没有则根据泛型查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public virtual IList<T> FindList<T>(ISession session, Expression<Func<T>> alias = null) where T : class
        {
			using (ITransaction transaction = session.BeginTransaction())
			{
				return GetList<T>(session,alias);
			}
        }



        /// <summary> Nihernate 添加操作 </summary>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public virtual object InsertBySession(object obj)
        {
            using (ISession session =GetSession())
            {
                return Insert(session, obj);
            }
        }

        /// <summary> Nihernate 添加操作 </summary>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public virtual int BatchInsertBySession<T>(T[] obj) where T : class
        {
            using (ISession session =GetSession())
            {
                return BatchInsert(session, obj);
            }
        }

        /// <summary> Nihernate 添加异步操作 </summary>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public virtual Task<object> InsertAsyncBySession(object obj)
        {
            using (ISession session = GetSession())
            {
                return InsertAsync(session, obj);
            }
        }

        /// <summary> Nihernate 修改操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual void UpdateBySession<T>(T obj) where T : class
        {
            using (ISession session = GetSession())
            {
                Update(session, obj);
            }
        }

        /// <summary> Nihernate 修改操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual void BatchUpdateBySession<T>(T[] obj) where T : class
        {
            using (ISession session =GetSession())
            {
                BatchUpdate(session, obj);
            }
        }

        /// <summary> Nihernate 修改异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual Task UpdateAsyncBySession<T>(T obj) where T : class
        {
            using (ISession session = GetSession())
            {
                return UpdateAsync(session, obj);
            }
        }

        /// <summary> Nihernate 删除操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public virtual void DeleteBySession<T>(T obj) where T : class
        {
            using (ISession session =GetSession())
            {
                Delete(session, obj);
            }
        }

        /// <summary> Nihernate 删除操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        public virtual void DelBySession<T>(object id) where T : class
        {
            using (ISession session = GetSession())
            {
                Delete<T>(session, id);
            }
        }

        /// <summary> Nihernate 删除异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual Task DeleteAsyncBySession<T>(T obj) where T : class
        {
            using (ISession session =GetSession())
            {
                return DeleteAsync(session, obj);
            }
        }

        /// <summary> Nihernate 删除异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task DeleteAsyncBySession<T>(object id) where T : class
        {
            using (ISession session = GetSession())
            {
                return DeleteAsync<T>(session, id);
            }
        }

        /// <summary> Nihernate 查询操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T FindSingleBySession<T>(object id) where T : class
        {
            using (ISession session = GetSession())
            {
                return FindSingle<T>(session, id);
            }
        }

       

        /// <summary> Nihernate 查询异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<T> FindSingleAsyncBySession<T>(object id) where T : class
        {
            using (ISession session = GetSession())
            {
                return FindSingleAsync<T>(session, id);
            }
        }

        /// <summary>根据实体名称查询，没有则根据泛型查询  异常</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public virtual  IQueryable<T> FindBySession<T>(string entityName = null) where T : class
        {
            using (ISession session = GetSession())
            {
                return Query<T>(session, entityName);
            }
        }

        /// <summary>根据实体类型查询，没有则根据泛型查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="alias"></param>
        /// <returns></returns>
        public virtual IList<T> FindListBySession<T>(Expression<Func<T>> alias = null) where T : class
        {
            using (ISession session = GetSession())
            {
                return FindList<T>(session, alias);
            }
        }

        /// <summary>
        ///update or delete or add 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual int ExecuteBySession(string sql)
        {
            if (string.IsNullOrEmpty(sql)) throw new ArgumentNullException("sql");
            ITransaction transaction = null;
            try
            {
                using (ISession session =GetSession())
                {
                    using (transaction = session.BeginTransaction())
                    {
                        int res = session.CreateQuery(sql).ExecuteUpdate();
                        transaction.Commit();
                        return res;
                    }
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                return -1;
            }
        }
		        
		
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="size">每页记录</param>
        /// <returns></returns>
        public virtual IList<T> FindListBySession<T>(string sql, int size) where T : class
        {
            if (string.IsNullOrEmpty(sql)) throw new ArgumentNullException("sql");
            size = size > 1 ? 10 : size > 100 ? 100 : size;
            using (ISession session =GetSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                    return session.CreateQuery(sql).SetMaxResults(size).List<T>();
            }
        }

        /// <summary>
        /// 根据 条件查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual T FindSingleBySession<T>(string sql, string[] param) where T : class
        {
            if (string.IsNullOrEmpty(sql)) throw new ArgumentNullException("sql");
            using (ISession session =GetSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                   return Get<T>(session,sql,param);
                }
            }
        }


        #region linq https://nhibernate.info/previous-doc/v5.1/ref/querylinq.html

        /// <summary>
        /// 根据条件 查询 数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<long> CountAsync<T>(ISession session, Expression<Func<T, bool>> where, CancellationToken cancellationToken = default) where T : class
        {
			using (ITransaction tx = session.BeginTransaction())
			{
				return GetCountAsync(session,where,cancellationToken);
			}
        }


        /// <summary>
        /// 根据条件 查询 数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> CountAsync<T>(IStatelessSession session, Expression<Func<T, bool>> where, CancellationToken cancellationToken = default) where T : class
        {
			using (ITransaction tx = session.BeginTransaction())
			{
				return GetCountAsync(session,where,cancellationToken);
			}
        }


        /// <summary>
        /// 根据条件 查询 数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual long Count<T>(ISession session, Expression<Func<T, bool>> where=null) where T : class
        {
			using (ITransaction tx= session.BeginTransaction())
			{
				return GetCount(session,where);
			}
        }
		

        /// <summary>
        /// 获取 最大 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual T Max<T>(ISession session, Expression<Func<T, bool>> where=null) where T : class
        {
			using (ITransaction tx = session.BeginTransaction())
			{
				return GetMax(session, where);
			}
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public virtual IList<T> FindList<T>(ISession session, Expression<Func<T, bool>> where=null, string entityName = "") where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
                return GetList(session,where,entityName);
        }




        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual int Delete<T>(ISession session, Expression<Func<T, bool>> where=null) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return -1;
#else
            using (ITransaction tx= session.BeginTransaction())
              return Remove(session, where);
#endif
        }


        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public virtual Task<int> DeleteAsync<T>(ISession session, Expression<Func<T, bool>> where=null, CancellationToken cancellation = default) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return Task.FromResult(Delete(session,where));
#else
            using (ITransaction tx= session.BeginTransaction())
				return RemoveAsync(session, where,cancellation);
#endif
        }


        /// <summary>
        /// 根据条件修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <param name="update"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public virtual   Task<int> UpdateAsync<T>(ISession session, Expression<Func<T, bool>> where, Expression<Func<T, T>> update, CancellationToken cancellation = default) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return Task.FromResult(Update(session,where,update));
#else
            using (ITransaction tx= session.BeginTransaction())
				return  ModifyAsync(session, where,update, cancellation);
#endif
        }


        /// <summary>
        /// 根据条件修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual int Update<T>(ISession session, Expression<Func<T, bool>> where, Expression<Func<T, T>> update) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return -1;
#else
			using (ITransaction tx= session.BeginTransaction())
				return Modify(session, where,update);
#endif
        }


        /// <summary>
        /// 根据条件修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual int Update<T, TProp>(ISession session, Expression<Func<T, bool>> where, Expression<Func<T, TProp>>[] update) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return -1;
#else
            using (ITransaction tx= session.BeginTransaction())
				return Modify(session, where,update);
#endif
        }


        /// <summary>
        /// 根据条件修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual int Update<T>(ISession session, Expression<Func<T, bool>> where, Expression<Func<T, dynamic>> update) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return -1;
#else
           using (ITransaction tx= session.BeginTransaction())
				return Modify(session, where,update);
#endif
        }

       
		
#endregion linq https://nhibernate.info/previous-doc/v5.1/ref/querylinq.html

#endregion ISession

#region IStatelessSession

        /// <summary> Nihernate 添加操作 </summary>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public virtual object Insert<T>(IStatelessSession session,T obj) where T : class
        {
            if (obj == null) throw new ArgumentNullException("obj");
            ITransaction transaction=null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    object result = Add(session,obj);
                    transaction.Commit();
                    return result;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw e;
            }
        }
	
		
        /// <summary> Nihernate 添加操作 </summary>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public virtual int BatchInsert<T>(IStatelessSession session, T[] obj) where T : class
        {
            if (obj == null|| obj.Length==0) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    int res = BatchAdd(session,obj);
                    transaction.Commit();
                    return res;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                return -1;
            }
        }
	
        /// <summary> Nihernate 添加异步操作 </summary>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public virtual Task<object> InsertAsync<T>(IStatelessSession session, T obj, CancellationToken cancellationToken = default) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return Task.FromResult(Insert(session,obj));
#else
            if (obj == null) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    Task<object> result = AddAsync(session,obj,cancellationToken);
                    transaction.CommitAsync(cancellationToken);
                    return result;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.RollbackAsync(cancellationToken);
                }
                return Task.FromResult((object)null);
            }
#endif
        }


       
      
        /// <summary> Nihernate 修改操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual void Update<T>(IStatelessSession session, T obj) where T : class
        {
            if (obj == null) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    Modify(session,obj);
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
            }
        }


        /// <summary> Nihernate 修改操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual void BatchUpdate<T>(IStatelessSession session, T[] obj) where T : class
        {
            if (obj == null || obj.Length == 0) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    BatchModify(session,obj);
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
            }
        }

        

        /// <summary> Nihernate 修改异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual Task UpdateAsync<T>(IStatelessSession session, T obj, CancellationToken cancellationToken = default) where T : class
        {
#if (NET40 || NET45 || NET451 || NET452 || NET46)
            Update(session,obj);
            return Task.CompletedTask;
#else
            if (obj == null) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    Task task = ModifyAsync(session,obj,cancellationToken);
                    transaction.CommitAsync(cancellationToken);
                    return task;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                   return transaction.RollbackAsync(cancellationToken);
                }
                return TaskHelper.CompletedTask;
            }
#endif
        }

      
        /// <summary> Nihernate 删除操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="obj"></param>
        public virtual void Delete<T>(IStatelessSession session, T obj) where T : class
        {
            if (obj == null) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    Remove(session,obj);
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
            }
        }


        /// <summary> Nihernate 删除操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="id"></param>
        public virtual void Delete<T>(IStatelessSession session, object id) where T:class
        {
            if (string.IsNullOrEmpty(id?.ToString())) throw new ArgumentNullException("id");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                   Remove<T>(session,id);
                   transaction.Commit();
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
            }
        }

        /// <summary> Nihernate 删除异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual Task DeleteAsync<T>(IStatelessSession session, T obj, CancellationToken cancellationToken = default) where T : class
        {
#if (NET40 || NET45 || NET451 || NET452 || NET46)
            Delete(session,obj);
            return Task.CompletedTask;
#else
            if (obj == null) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    Task task = RemoveAsync(session,obj,cancellationToken);
                    transaction.CommitAsync(cancellationToken);
                    return task;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                   return transaction.RollbackAsync(cancellationToken);
                }
                return TaskHelper.CompletedTask;
            }
#endif
        }


        /// <summary> Nihernate 删除异步操作 </summary>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task DeleteAsync<T>(IStatelessSession session, object id, CancellationToken cancellationToken = default) where T : class
        {
#if (NET40 || NET45 || NET451 || NET452 || NET46)
            Delete(session,id);
            return Task.CompletedTask;
#else
            if (string.IsNullOrEmpty(id?.ToString())) throw new ArgumentNullException("id");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    Task task = RemoveAsync<T>(session,id,cancellationToken);
                    transaction.CommitAsync(cancellationToken);
                    return task;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                   return transaction.RollbackAsync(cancellationToken);
                }
                return TaskHelper.CompletedTask;
            }
#endif
        }


        /// <summary> Nihernate 根据 id 查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T FindSingle<T>(IStatelessSession session, object id) where T : class
        {
            if (string.IsNullOrEmpty(id?.ToString())) throw new ArgumentNullException("id");
            using (ITransaction transaction = session.BeginTransaction())
            {
                return Get<T>(session,id);
            }
        }


        /// <summary> 根据 id 查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<T> FindSingleAsync<T>(IStatelessSession session, object id, CancellationToken cancellationToken = default) where T : class
        {
#if (NET40 || NET45 || NET451 || NET452 || NET46)
           return  Task.FromResult(FindSingle<T>(session,id));
#else
            if (string.IsNullOrEmpty(id?.ToString())) throw new ArgumentNullException("id");
            using (ITransaction transaction = session.BeginTransaction())
                return GetAsync<T>(session,id,cancellationToken);
#endif
        }

      
        /// <summary>根据实体类型查询，没有则根据泛型查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public virtual IList<T> FindList<T>(IStatelessSession session, Expression<Func<T>> alias=null) where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
                return GetList<T>(session,alias);
        }


        /// <summary> Nihernate 添加操作 </summary>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public virtual object Insert(object obj)
        {
            using (IStatelessSession session = SessionFactory.OpenStatelessSession())
            {
                return Insert(session,obj);
            }
        }
        /// <summary> Nihernate 添加操作 </summary>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public virtual int BatchInsert<T>(T[] obj) where T : class
        {
            using (IStatelessSession session = SessionFactory.OpenStatelessSession())
            {
                return BatchInsert(session, obj);
            }
        }

        /// <summary> Nihernate 添加异步操作 </summary>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public virtual Task<object> InsertAsync( object obj, CancellationToken cancellationToken = default)
        {
            using (IStatelessSession session = SessionFactory.OpenStatelessSession())
            {
#if (NET40 || NET45 || NET451 || NET452 || NET46)
                return  Task.FromResult(Insert(session,obj));
#else
                return InsertAsync(session, obj, cancellationToken);
#endif
            }
        }

        /// <summary> Nihernate 修改操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual void Update<T>( T obj) where T : class
        {
            using (IStatelessSession session = SessionFactory.OpenStatelessSession())
            {
                Update(session, obj);
            }
        }

        /// <summary> Nihernate 修改操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual void BatchUpdate<T>( T[] obj) where T : class
        {
            using (IStatelessSession session = SessionFactory.OpenStatelessSession())
            {
                BatchUpdate(session, obj);
            }
        }

        /// <summary> Nihernate 修改异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual Task UpdateAsync<T>( T obj, CancellationToken cancellationToken = default) where T : class
        {
            using (IStatelessSession session = SessionFactory.OpenStatelessSession())
            {
#if (NET40 || NET45 || NET451 || NET452 || NET46)
            Update(session,obj);
            return Task.CompletedTask;
#else
                return UpdateAsync(session, obj, cancellationToken);
#endif
            }
        }

        /// <summary> Nihernate 删除操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public virtual void Delete<T>(T obj) where T : class
        {
            using (IStatelessSession session = SessionFactory.OpenStatelessSession())
            {
                 Delete(session, obj);
            }
        }

        /// <summary> Nihernate 删除操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        public virtual void Delete<T>(object id) where T : class
        {
            using (IStatelessSession session = SessionFactory.OpenStatelessSession())
            {
                Delete<T>(session,id);
            }
        }

        /// <summary> Nihernate 删除异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual Task DeleteAsync<T>( T obj, CancellationToken cancellationToken = default) where T : class
        {
            using (IStatelessSession session = SessionFactory.OpenStatelessSession())
            {
#if (NET40 || NET45 || NET451 || NET452 || NET46)
               Delete(session,obj);
               return Task.CompletedTask;
#else
                return DeleteAsync(session, obj, cancellationToken);
#endif
            }
        }

        /// <summary> Nihernate 删除异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task DeleteAsync<T>( object id, CancellationToken cancellationToken = default) where T : class
        {
            using (IStatelessSession session = SessionFactory.OpenStatelessSession())
            {
#if (NET40 || NET45 || NET451 || NET452 || NET46)
               Delete(session,id);
               return Task.CompletedTask;
#else
                return DeleteAsync<T>(session, id, cancellationToken);
#endif
            }
        }

        /// <summary> Nihernate 查询操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T FindSingle<T>(object id) where T : class
        {
            using (IStatelessSession session = SessionFactory.OpenStatelessSession())
            {
                return FindSingle<T>(session, id);
            }
        }


        /// <summary> Nihernate 查询异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<T> FindSingleAsync<T>( object id, CancellationToken cancellationToken = default) where T : class
        {
            using (IStatelessSession session = SessionFactory.OpenStatelessSession())
            {
#if (NET40 || NET45 || NET451 || NET452 || NET46)
			   return  Task.FromResult(FindSingle<T>(session,id));
#else
                return FindSingleAsync<T>(session, id, cancellationToken);
#endif
            }
        }
        
        /// <summary>根据实体名称查询，没有则根据泛型查询  异常</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public virtual IQueryable<T> Query<T>( string entityName = null) where T : class
        {
            using (IStatelessSession session = SessionFactory.OpenStatelessSession())
            {
                return Query<T>(session, entityName);
            }
        }

        /// <summary>根据实体类型查询，没有则根据泛型查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="alias"></param>
        /// <returns></returns>
        public virtual IList<T> FindList<T>( Expression<Func<T>> alias = null) where T : class
        {
            using (IStatelessSession session = SessionFactory.OpenStatelessSession())
            {
                return FindList<T>(session, alias);
            }
        }
        /// <summary>
        ///增 删 该
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual int  ExecuteQuery(string sql)
        {
            using (IStatelessSession session = SessionFactory.OpenStatelessSession())
            {
                return ExecuteQuery(session, sql);
            }
        }

        /// <summary>
        /// 增 删 该
        /// </summary>
        /// <param name="session"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual int ExecuteQuery(ISession session, string sql)
        {
           ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                   
                    int res= GetExecuteQuery(session,sql);
                    transaction.Commit();
                    return res;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                return -1;
            }
        }

        /// <summary>
        /// 增 删 改 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="sql"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual
#if !(NET40 || NET45 || NET451 || NET452 || NET46)
            async
#endif
            Task<int> ExecuteQueryAsync(ISession session, string sql, CancellationToken cancellationToken = default)
        {
#if (NET40 || NET45 || NET451 || NET452 || NET46)
           return  Task.FromResult(ExecuteQuery(session,sql));
#else
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {

                    //select table 
                    //int res = session.CreateQuery(sql).ExecuteUpdate();  //from tble
                    int res =await session.CreateSQLQuery(sql).ExecuteUpdateAsync(cancellationToken);  ////select table 
                    await transaction.CommitAsync(cancellationToken);
                    return res;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                   await  transaction.RollbackAsync(cancellationToken);
                }
                return -1;
            }
#endif
        }

        /// <summary>
        /// 增 删 改 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual int ExecuteQuery(IStatelessSession session,string sql)
        {
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    int res = session.CreateSQLQuery(sql).ExecuteUpdate();
                    transaction.Commit();
                    return res;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                return -1;
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="size">每页记录</param>
        /// <returns></returns>
        public virtual IEnumerable<T> FindList<T>(string sql, int size) where T:class
        {  
            using (IStatelessSession session = SessionFactory.OpenStatelessSession())
            {
                using (ITransaction tx = session.BeginTransaction())
                    return session.CreateQuery(sql).SetMaxResults(size).List<T>();
            }
        }
		
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual T FindSingle<T>(string sql,string[] param) where T:class
        {
            using (IStatelessSession session = SessionFactory.OpenStatelessSession())
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    IQuery query = session.CreateQuery(sql);
                    for (int i = 0; i < param.Length; i++)
                    {
                        query = query.SetString(i, param[i]);
                    }
                    return query.List<T>()[0];
                }
            }
        }
#region linq https://nhibernate.info/previous-doc/v5.1/ref/querylinq.html
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual int Count<T>(IStatelessSession session, Expression<Func<T, bool>> where) where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
            {
                return GetCount(session,where);
            }
        }
		
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public virtual IList<T> FindList<T>(IStatelessSession session, Expression<Func<T, bool>> where,string entityName="") where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
                return GetList<T>(session, where,entityName);
        }
	
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public virtual IQueryable<T> Query<T>(IStatelessSession session, Expression<Func<T, bool>> where, string entityName = "") where T : class
        {
			using (ITransaction tx1 = session.BeginTransaction())
				return GetQuery<T>(session, where,entityName);
        }
	
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual int Delete<T>(IStatelessSession session, Expression<Func<T, bool>> where) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return -1;
#else
            using (ITransaction tx = session.BeginTransaction())
                return Remove(session, where);
#endif
        }
		

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public virtual Task<int> DeleteAsync<T>(IStatelessSession session, Expression<Func<T, bool>> where, System.Threading.CancellationToken cancellation= default) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return Task.FromResult(-1) ;
#else
            using (ITransaction tx = session.BeginTransaction())
                return  RemoveAsync(session, where,cancellation);
#endif
        }
		
		
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <param name="update"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public virtual   Task<int> UpdateAsync<T>(IStatelessSession session, Expression<Func<T, bool>> where, Expression<Func<T, T>> update, CancellationToken cancellation = default) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return Task.FromResult(-1) ;
#else
            using (ITransaction tx = session.BeginTransaction())
                return  ModifyAsync(session,where,update,cancellation);
#endif
        }
		

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual int Update<T>(IStatelessSession session, Expression<Func<T, bool>> where,Expression<Func<T,T>> update) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return -1 ;
#else
            using (ITransaction tx = session.BeginTransaction())
               return  Modify(session,where,update);
#endif
        }
		
#endregion linq https://nhibernate.info/previous-doc/v5.1/ref/querylinq.html

#endregion IStatelessSession

       

        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="where">条件</param>
        /// <param name="session"></param>
        /// <param name="cancellationToken"></param>
        ///<return>返回实体类数据集数量信息</return>
        public virtual  Task<long> CountAsync<T>(ISession session,ICriteria where = null, CancellationToken cancellationToken = default) where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
			{
				return GetCountAsync<T>(session,where,cancellationToken);
			}
        }
		
	   

        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="where">条件</param>
        /// <param name="session"></param>
        /// <param name="cancellationToken"></param>
        ///<return>返回实体类数据集数量信息</return>
        public virtual  Task<int> CountAsync<T>(IStatelessSession session,ICriteria where = null, CancellationToken cancellationToken = default) where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
			{
				return GetCountAsync<T>(session,where,cancellationToken);
			}
        }
		

        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="where">条件</param>
        /// <param name="session"></param>
        ///<return>返回实体类数据集数量信息</return>

        public virtual long Count<T>(ISession session,ICriteria where = null) where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
			{
				return GetCount<T>(session,where);
			}
        }
		
		
        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="where">条件</param>
        /// <param name="session"></param>
        ///<return>返回实体类数据集数量信息</return>

        public virtual int Count<T>(IStatelessSession session, ICriteria where = null) where T : class
        {
             using (ITransaction tx = session.BeginTransaction())
			{
				return GetCount<T>(session,where);
			}
        }
		
		

        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="where">条件</param>
        /// <param name="session"></param>
        ///<return>返回实体类数据集信息</return>
        public virtual List<T> FindList<T>(ISession session,ICriteria where = null) where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
			{
				var data = GetList<T>(session,where);
				return data;
			}
        }
	
        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="where">条件</param>
        /// <param name="session"></param>
        ///<return>返回实体类数据集信息</return>
        public virtual List<T> FindList<T>(IStatelessSession session, ICriteria where = null) where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
			{
				var data = GetList<T>(session,where);
				return data;
			}
        }
		
        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="where">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        /// <param name="session"></param>
        ///<return>返回实体类数据集信息</return>

        public virtual List<T> FindListByPage<T>(ISession session, ICriteria where = null, int page = 1, int size = 10) where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
			{
				return GetListByPage<T>(session,where,page,size);
			}
        }
		
        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="where">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        /// <param name="session"></param>
        ///<return>返回实体类数据集信息</return>

        public virtual List<T> FindListByPage<T>(IStatelessSession session, ICriteria where = null, int page = 1, int size = 10) where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
			{
				return GetListByPage<T>(session,where,page,size);
			}
        }
		
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public virtual Tuple<List<T>, int> FindTupleByPage<T>(IStatelessSession session,ICriteria where = null, int page = 1, int size = 10) where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
			{
				return GetTupleByPage<T>(session,where,page,size);
			}
        }
		
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>

        public virtual Tuple<List<T>, long> FindTupleByPage<T>(ISession session, ICriteria where = null, int page = 1, int size = 10) where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
			{
				return GetTupleByPage<T>(session,where,page,size);
			}
        }
		
	   
#region async



        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="where">条件</param>
        /// <param name="session"></param>
        /// <param name="cancellationToken"></param>
        ///<return>返回实体类数据集信息</return>

        public virtual  Task<List<T>> FindListAsync<T>(ISession session,ICriteria where = null,
            CancellationToken cancellationToken = default) where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
			{
				return GetListAsync<T>(session,where,cancellationToken);
			}
        }
		

        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="where">条件</param>
        /// <param name="session"></param>
        /// <param name="cancellationToken"></param>
        ///<return>返回实体类数据集信息</return>

        public virtual  Task<List<T>> FindListAsync<T>(IStatelessSession session,ICriteria where = null,
            CancellationToken cancellationToken = default) where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
			{
				return GetListAsync<T>(session,where,cancellationToken);
			}
        }
		
        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="where">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        /// <param name="session"></param>
        /// <param name="cancellationToken"></param>
        ///<return>返回实体类数据集信息</return>
        public virtual Task<List<T>> FindListByPageAsync<T>(ISession session,ICriteria where = null, int page=1, int size=10,
            CancellationToken cancellationToken = default) where T : class
        {
			using (ITransaction tx = session.BeginTransaction())
			{
				return GetListByPageAsync<T>(session,where,page,size,cancellationToken);
			}
        }
		

        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="where">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        /// <param name="session"></param>
        /// <param name="cancellationToken"></param>
        ///<return>返回实体类数据集信息</return>
       public virtual Task<List<T>> FindListByPageAsync<T>(IStatelessSession session,ICriteria where = null, int page=1, int size=10,
            CancellationToken cancellationToken = default) where T : class
        {
			using (ITransaction tx = session.BeginTransaction())
			{
				return GetListByPageAsync<T>(session,where,page,size,cancellationToken);
			}
        }
		
        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="where">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        /// <param name="session"></param>
        /// <param name="cancellationToken"></param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        public virtual Task<Tuple<List<T>, long>> FindTupleByPageAsync<T>(ISession session, ICriteria where = null, int page=1,
            int size=10,  CancellationToken cancellationToken = default) where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
			{
				return GetTupleByPageAsync<T>(session,where,page,size,cancellationToken);
			}
        }
		

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="session"></param>
        /// <param name="where">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
         public virtual Task<Tuple<List<T>, long>> FindTupleByPageAsync<T>(IStatelessSession session, ICriteria where = null, int page=1,
            int size=10,  CancellationToken cancellationToken = default) where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
			{
				return GetTupleByPageAsync<T>(session,where,page,size,cancellationToken);
			}
        }
		
#endregion async


        /// <summary>查找单个</summary>
        /// <param name="session"></param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual T FindSingle<T>(ISession session, Expression<Func<T, bool>> where = null) where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
			{
				return Get<T>(session,where);
			}
        }

		
        /// <summary>查找单个</summary>
        /// <param name="session"></param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual T FindSingle<T>(IStatelessSession session, Expression<Func<T, bool>> where = null) where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
			{
				return Get<T>(session,where);
			}
        }
		



        /// <summary> 查询数据  </summary>
        /// <param name="session"></param>
        /// <param name="where">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <returns></returns>
        public virtual IQueryable<T> FindByPage<T>(ISession session, Expression<Func<T, bool>> where = null, int page = 1, int size = 10) where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
			{
				return GetByPage<T>(session,where,page,size);
			}
        }
		
        /// <summary> 查询数据  </summary>
        /// <param name="session"></param>
        /// <param name="where">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <returns></returns>
        public virtual IQueryable<T> FindByPage<T>(IStatelessSession session, Expression<Func<T, bool>> where = null, int page = 1, int size = 10) where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
			{
				return GetByPage<T>(session,where,page,size);
			}
        }
	
#region async

        /// <summary>查找单个 </summary>
        /// <param name="session"></param>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<T> FindSingleAsync<T>(ISession session, Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default) where T : class
        {
			using (ITransaction tx = session.BeginTransaction())
			{
				return GetAsync<T>(session,where,cancellationToken);
			}
        }
		

        /// <summary> 查找单个  </summary>
        /// <param name="session"></param>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<T> FindSingleAsync<T>(IStatelessSession session,Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default) where T : class
        {
			using (ITransaction tx = session.BeginTransaction())
			{
				return GetAsync<T>(session,where,cancellationToken);
			}
        }
	
        /// <summary> 是否存在 </summary>
        /// <param name="session"></param>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> IsExistAsync<T>(ISession session, Expression<Func<T, bool>> where, CancellationToken cancellationToken = default) where T : class
        {
			using (ITransaction tx = session.BeginTransaction())
			{
				return HasExistAsync<T>(session,where,cancellationToken);
			}
        }
		

        /// <summary> 是否存在 </summary>
        /// <param name="session"></param>
        /// <param name="where">条件</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual    Task<bool> IsExistAsync<T>(IStatelessSession session, Expression<Func<T, bool>> where, CancellationToken cancellationToken = default) where T : class
        {
			using (ITransaction tx = session.BeginTransaction())
			{
				return HasExistAsync<T>(session,where,cancellationToken);
			}
        }
		


    
#endregion async

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public virtual Tuple<List<T>, long> FindTupleByPage<T>(ISession session,Expression<Func<T, bool>> where = null, int page = 1, int size = 10) where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
			{
				return GetTupleByPage<T>(session,where,page,size);
			}
        }
	

       /// <summary>
       /// 
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="session"></param>
       /// <param name="where"></param>
       /// <param name="page"></param>
       /// <param name="size"></param>
       /// <returns></returns>
        public virtual Tuple<List<T>, long> FindTupleByPage<T>(IStatelessSession session, Expression<Func<T, bool>> where = null, int page = 1, int size = 10) where T : class
        {
            using (ITransaction tx = session.BeginTransaction())
			{
				return GetTupleByPage<T>(session,where,page,size);
			}
        }
	  
    }
}
#endif