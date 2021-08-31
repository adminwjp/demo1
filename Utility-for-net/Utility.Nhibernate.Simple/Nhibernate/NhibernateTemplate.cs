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
        /// <summary>
        /// 
        /// </summary>
        public  IInterceptor Interceptor { get;private set; }

        /// <summary>
        /// 
        /// </summary>
        protected ISessionFactory SessionFactory { get; }
        /// <summary>
        /// 
        /// </summary>
        public AppSessionFactory AppSessionFactory { get; }
        /// <summary>
        ///构造函数 
        /// </summary>
        public NhibernateTemplate(AppSessionFactory sessionFactory)
        {
            AppSessionFactory = sessionFactory;
            SessionFactory = sessionFactory.SessionFactory;
            Interceptor = sessionFactory.Interceptor;
        } 
      
        /// <summary>
        /// 
        /// </summary>
        protected NhibernateTemplate()
        {

        }
        class Inner {
          public static readonly   NhibernateTemplate Instance = new NhibernateTemplate();
        }

          /// <summary>
        ///NhibernateTemplate 
        /// </summary>
        public static NhibernateTemplate Empty => Inner.Instance;

#region ISession
  
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public object Add<T>(ISession session, T obj) where T : class
		{
			object result = session.Save(obj);
			return result;
		}


        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
		 public virtual int BatchAdd<T>(ISession session, T[] obj) where T : class
		 {
			int res = 0;
			foreach (var item in obj)
			{
				session.Save(item);
				res++;
			}
			return res;
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
		Task<int> BatchAddAsync<T>(ISession session, T[] obj, CancellationToken cancellationToken = default) where T : class
		{
#if (NET40 || NET45 || NET451 || NET452 || NET46)
           return Task.FromResult(BatchAdd(session,obj));
#else
            int res = 0;
			foreach (var item in obj)
			{
			   await session.SaveAsync(item, cancellationToken);
			   res++;
			}
			return res;
#endif
		}


        /// <summary>
        /// 添加
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
		Task<object> AddAsync<T>(ISession session, T obj, CancellationToken cancellationToken = default) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return Task.FromResult(Add(session,obj));
#else
            var result =await session.SaveAsync(obj, cancellationToken);
			return result;
#endif			 
		}

        
        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="obj"></param>
		public virtual void Modify<T>(ISession session, T obj) where T : class
		{
			session.Clear();
			//session.Refresh();
			//session.Merge(obj);
			session.Update(obj);
		}

		
        /// <summary>
        /// 批量修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="obj"></param>
		public virtual void BatchModify<T>(ISession session, T[] obj) where T : class
		{
		    foreach (var item in obj)
			{
				session.Update(item);
			}
		}

       
		
        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="obj"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="tx"></param>
        /// <returns></returns>
		public virtual Task<int> ModifyAsync<T>(ISession session, T obj, CancellationToken cancellationToken = default,bool tx=true) where T : class
		{
#if NET40 || NET45 || NET451 || NET452 || NET46
            return Task.FromResult(Modify(session,obj));
#else
            Task task = session.UpdateAsync(obj,cancellationToken);
			return Task.FromResult(1);
#endif
		}


        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="obj"></param>
		public virtual void Remove<T>(ISession session, T obj) where T : class
		{
			 session.Delete(obj);
		}
		
		
        /// <summary>
        /// 根据 id 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="id"></param>
		public virtual void Remove<T>(ISession session, object id) where T : class
		{
		    var obj = session.Get<T>(id);
			session.Delete(obj);
		}

   

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="obj"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
		public virtual Task<int> RemoveAsync<T>(ISession session, T obj, CancellationToken cancellationToken = default) where T : class
		{
#if NET40 || NET45 || NET451 || NET452 || NET46
            return Task.FromResult(Remove(session,obj));
#else
            Task task = session.DeleteAsync(obj,cancellationToken);
            return Task.FromResult(1);
#endif
		}


        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
		 public virtual Task RemoveAsync<T>(ISession session, object id, CancellationToken cancellationToken = default) where T : class
		 {
#if NET40 || NET45 || NET451 || NET452 || NET46
           Remove<T>(session,id);
            return Task.CompletedTask;
#else
            Task task = session.DeleteAsync(session.GetAsync<T>(id, cancellationToken).Result, cancellationToken);
			return task;
#endif			
		 }

		
        /// <summary>
        /// 根据 id 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <returns></returns>
		public virtual T Get<T>(ISession session, object id) where T : class
		{
			return session.Get<T>(id);
		}
		

        /// <summary>
        /// 根据 id 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<T> GetAsync<T>(ISession session, object id) where T : class
		{
#if NET40 || NET45 || NET451 || NET452 || NET46
            return Task.FromResult(Get<T>(session,id));
#else
            return session.GetAsync<T>(id);
#endif
		}
		

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
		public virtual IQueryable<T> GetQuery<T>(ISession session, string entityName = null) where T : class
		{
		   return entityName == null ? session.Query<T>() : session.Query<T>(entityName);//同一个session 这样查询没问题 添加时有出现该问题
		}

     
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public virtual IQueryable<T> GetQuery<T>(IStatelessSession session, string entityName = null) where T : class
		{
			return entityName == null ? session.Query<T>() : session.Query<T>(entityName);//同一个session 这样查询没问题 添加时有出现该问题
		}
		

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IQueryable<T> GetQuery<T>(ISession session, Expression<Func<T, bool>> where = null) where T : class
		{
			return where != null ? session.Query<T>().Where(where) : session.Query<T>();;//同一个session 这样查询没问题 添加时有出现该问题
		}


        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <returns></returns>
		public virtual IQueryable<T> GetQuery<T>(IStatelessSession session, Expression<Func<T, bool>> where = null) where T : class
		{
			return where != null ? session.Query<T>().Where(where) : session.Query<T>();;//同一个session 这样查询没问题 添加时有出现该问题
		}


        /// <summary>
        /// 根据实体类型查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public virtual IList<T> GetList<T>(ISession session, Expression<Func<T>> alias = null) where T : class
		{
			return alias == null ? session.QueryOver<T>().List() : session.QueryOver<T>(alias).List();
		}



    


    

       

     


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual ISession GetSession()
        {
#pragma warning disable CS0618 // 类型或成员已过时
            ISession session = Interceptor == null ? SessionFactory.OpenSession() : SessionFactory.OpenSession(Interceptor);
#pragma warning restore CS0618 // 类型或成员已过时
            return session;
        }

        /// <summary>
        /// 根据 条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
		public virtual T Get<T>(ISession session,string sql, string[] param) where T : class
		{
			IQuery query = session.CreateQuery(sql);
			if (param != null)
			{
				for (int i = 0; i < param.Length; i++)
				{
					query = query.SetString(i, param[i]);
				}
			}
			return query.List<T>()[0];
		}

        #region linq https://nhibernate.info/previous-doc/v5.1/ref/querylinq.html

        /// <summary>
        /// 注意 重载(使用默认参数情况下) 可能调用 的是 自身。 构造函数 是的 堆载问题  函数签名最好不一致
        /// 根据条件 查询 数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<long> GetCountAsync<T>(ISession session, Expression<Func<T, bool>> where, CancellationToken cancellationToken = default) where T : class
		{

#if NET40 || NET45 || NET451 || NET452 || NET46
			return Task.FromResult((long)Count(session,where));
#else
            IFutureValue<Task<int>> value = Filter(session.Query<T>(), where).ToFutureValue(it => it.CountAsync(cancellationToken));
			return Task.FromResult((long)value.Value.Result);
#endif
        }


        /// <summary>
        /// 根据条件 查询 数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
		public virtual Task<int> GetCountAsync<T>(IStatelessSession session, Expression<Func<T, bool>> where, CancellationToken cancellationToken = default) where T : class
		{

#if NET40 || NET45 || NET451 || NET452 || NET46
            return Task.FromResult(Count(session,where));
#else
            IFutureValue<Task<int>> value = Filter(session.Query<T>(), where).ToFutureValue(it => it.CountAsync(cancellationToken));
			return value.Value;
#endif
        }

		
        /// <summary>
        /// 根据条件 查询 数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual long GetCount<T>(ISession session, Expression<Func<T, bool>> where=null) where T : class
        {
			IFutureValue<int> value = Filter(session.Query<T>(), where).ToFutureValue(it => it.Count());
            return (long)value.Value;
        }

     

        /// <summary>
        ///  获取 最大 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <returns></returns>

        public virtual T GetMax<T>(ISession session, Expression<Func<T, bool>> where=null) where T : class
        {
            return Filter(session.Query<T>(), where).Max();
        }
      

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
		public virtual IList<T> GetList<T>(ISession session, Expression<Func<T, bool>> where=null, string entityName = "") where T : class
		{
			return Query(session,where,entityName).ToList() ;
		}

        /// <summary>
        /// 过滤
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        protected virtual IQueryable<T> Filter<T>(IQueryable<T> source, Expression<Func<T, bool>> where) where T: class
        {
            return (where == null ? source : source.Where(where));
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public virtual IQueryable<T> Query<T>(ISession session, Expression<Func<T, bool>> where=null, string entityName = "") where T : class
        {
            return string.IsNullOrEmpty(entityName) ? Filter(session.Query<T>(), where) : Filter(session.Query<T>(entityName), where);
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IList<T> FindList<T>(IEnumerable<T> datas, Expression<Func<T, bool>> where=null) where T : class
        {
             return Filter(datas.AsQueryable(), where).ToList();
        }

       

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="where"></param>
        /// <returns></returns>
		public virtual int Remove<T>(ISession session, Expression<Func<T, bool>> where=null) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return -1;
#else
            return Filter(session.Query<T>(), where).Delete();
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
        public virtual   Task<int> RemoveAsync<T>(ISession session, Expression<Func<T, bool>> where=null, CancellationToken cancellation = default) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return Task.FromResult(Remove(session,where));
#else
            return Filter(session.Query<T>(), where).DeleteAsync(cancellation);
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
        public virtual   Task<int> ModifyAsync<T>(ISession session, Expression<Func<T, bool>> where, Expression<Func<T, T>> update, CancellationToken cancellation = default) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return Task.FromResult(Modify(session,where,update));
#else
            return Filter(session.Query<T>(), where).UpdateAsync(update, cancellation);
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
        public virtual int Modify<T>(ISession session, Expression<Func<T, bool>> where, Expression<Func<T, T>> update) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return -1;
#else
            return Filter(session.Query<T>(), where).Update(update);
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
        public virtual int Modify<T, TProp>(ISession session, Expression<Func<T, bool>> where, Expression<Func<T, TProp>>[] update) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return -1;
#else
            UpdateBuilder<T> updateBuilder = Filter(session.Query<T>(), where).UpdateBuilder<T>();
            for (int i = 0; i < update.Length/2; i++)
            {
                updateBuilder = updateBuilder.Set(update[i], update[i * 2 + 1]);
            }
            return updateBuilder.Update();
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
		public virtual int Modify<T>(ISession session, Expression<Func<T, bool>> where, Expression<Func<T, dynamic>> update) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return -1;
#else
            return Filter(session.Query<T>(), where).Update(update);
#endif
        }
		
#endregion linq https://nhibernate.info/previous-doc/v5.1/ref/querylinq.html

#endregion ISession

#region IStatelessSession

       
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
		public virtual object Add<T>(IStatelessSession session,T obj) where T : class
        {
            object result = session.Insert(obj);
			return result;
        }
		
		
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
		public virtual int BatchAdd<T>(IStatelessSession session, T[] obj) where T : class
        {
            int res = 0;
			foreach (var item in obj)
			{
				session.Insert(item);
				res++;
			}
			return res;
        }
       


        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="obj"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
		 public virtual Task<object> AddAsync<T>(IStatelessSession session, T obj, CancellationToken cancellationToken = default) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return Task.FromResult(Add(session,obj));
#else
            Task<object> result = session.InsertAsync(obj,cancellationToken) as Task<object>;
			 return result;
#endif
        }
      

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="obj"></param>
	    public virtual void Modify<T>(IStatelessSession session, T obj) where T : class
        {
             session.Update(obj);
        }


        /// <summary>
        /// 批量修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="obj"></param>
		public virtual void BatchModify<T>(IStatelessSession session, T[] obj) where T : class
        {
            foreach (var item in obj)
			{
				session.Update(item);
			}
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="obj"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
		public virtual Task ModifyAsync<T>(IStatelessSession session, T obj, CancellationToken cancellationToken = default) where T : class
        {
#if (NET40 || NET45 || NET451 || NET452 || NET46)
           Modify(session,obj);
           return Task.CompletedTask;
#else
            Task task = session.UpdateAsync(obj,cancellationToken);
			return task;
#endif
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="obj"></param>
		public virtual void Remove<T>(IStatelessSession session, T obj) where T : class
        {
           session.Delete(obj);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="id"></param>
		public virtual void Remove<T>(IStatelessSession session, object id) where T:class
        {
            session.Delete(session.Get<T>(id));
        }
     

        /// <summary>
        /// 删除 
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="session">会话</param>
        /// <param name="obj">对象</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
	    public virtual Task RemoveAsync<T>(IStatelessSession session, T obj, CancellationToken cancellationToken = default) where T : class
        {
#if (NET40 || NET45 || NET451 || NET452 || NET46)
           Remove(session,obj);
           return Task.CompletedTask;
#else
            Task task = session.DeleteAsync(obj,cancellationToken);
			return task;
#endif
        }

       

        /// <summary>
        /// 根据 id 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
		public virtual Task RemoveAsync<T>(IStatelessSession session, object id, CancellationToken cancellationToken = default) where T : class
        {
#if (NET40 || NET45 || NET451 || NET452 || NET46)
            Remove(session,id);
            return Task.CompletedTask;
#else
            Task task = session.DeleteAsync(session.GetAsync<T>(id,cancellationToken).Result,cancellationToken);
			 return task;
#endif
        }


        /// <summary>
        /// 根据 id 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <returns></returns>
		public virtual T Get<T>(IStatelessSession session, object id) where T : class
        {
            return session.Get<T>(id);
        }

     

        /// <summary>
        /// 根据 id 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
		public virtual Task<T> GetAsync<T>(IStatelessSession session, object id, CancellationToken cancellationToken = default) where T : class
        {
#if (NET40 || NET45 || NET451 || NET452 || NET46)
           return Task.FromResult(Get<T>(session,id));
#else
            return session.GetAsync<T>(id,cancellationToken);
#endif
        }
      
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
		public virtual IList<T> GetList<T>(IStatelessSession session, Expression<Func<T>> alias=null) where T : class
        {
			return alias == null ? session.QueryOver<T>().List() : session.QueryOver<T>(alias).List();
        }

       

      
       
#region linq https://nhibernate.info/previous-doc/v5.1/ref/querylinq.html
      
		public virtual int GetCount<T>(IStatelessSession session, Expression<Func<T, bool>> where) where T : class
        {
            IFutureValue<int> value = Filter(session.Query<T>(), where).ToFutureValue(it => it.Count());
			return value.Value;
        }

        public virtual IList<T> GetList<T>(IStatelessSession session, Expression<Func<T, bool>> where, string entityName = "") where T : class
        {
            return string.IsNullOrEmpty(entityName) ? Filter(session.Query<T>(), where).ToList() : Filter(session.Query<T>(entityName), where).ToList();
        }
		public virtual IQueryable<T> GetQuery<T>(IStatelessSession session, Expression<Func<T, bool>> where, string entityName = "") where T : class
        {
            return string.IsNullOrEmpty(entityName) ? Filter(session.Query<T>(), where) : Filter(session.Query<T>(entityName), where);
        }
      
		public virtual int Remove<T>(IStatelessSession session, Expression<Func<T, bool>> where) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return -1;
#else
			return Filter(session.Query<T>(), where).Delete();
#endif
        }

		public virtual Task<int> RemoveAsync<T>(IStatelessSession session, Expression<Func<T, bool>> where, System.Threading.CancellationToken cancellation= default) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return Task.FromResult(-1) ;
#else
            return Filter(session.Query<T>(), where).DeleteAsync(cancellation);
#endif
        }

		  public virtual   Task<int> ModifyAsync<T>(IStatelessSession session, Expression<Func<T, bool>> where, Expression<Func<T, T>> update, CancellationToken cancellation = default) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return Task.FromResult(-1) ;
#else
            return session.Query<T>().Where(where).UpdateAsync(update,cancellation);
#endif
        }

		public virtual int  Modify<T>(IStatelessSession session, Expression<Func<T, bool>> where,Expression<Func<T,T>> update) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return -1 ;
#else
			return Filter(session.Query<T>(), where).Update(update);
#endif
        }
#endregion linq https://nhibernate.info/previous-doc/v5.1/ref/querylinq.html

#endregion IStatelessSession

        /// <summary>条件拼接 </summary>
        /// <param name="session"></param>
        /// <param name="ors">or条件</param>
        /// <param name="ands">and 条件</param>
        /// <returns>组装条件</returns>
        public static ICriteria QueryWhere<T>(ISession session, List<AbstractCriterion> ors,
            List<AbstractCriterion> ands) where T : class
        {
            ICriteria criteria = session.CreateCriteria<T>();
            return QueryWhere(criteria, ors, ands);
        }

        /// <summary>条件拼接 </summary>
        /// <param name="session"></param>
        /// <param name="ors">or条件</param>
        /// <param name="ands">and 条件</param>
        /// <returns>组装条件</returns>
        public static ICriteria QueryWhere<T>(IStatelessSession session, List<AbstractCriterion> ors,
            List<AbstractCriterion> ands) where T : class
        {
            ICriteria criteria = session.CreateCriteria<T>();
            return QueryWhere(criteria, ors, ands);
        }

        /// <summary>条件拼接 </summary>
        /// <param name="criteria"></param>
        /// <param name="ors">or条件</param>
        /// <param name="ands">and 条件</param>
        /// <returns>组装条件</returns>
        public static ICriteria QueryWhere(ICriteria criteria, List<AbstractCriterion> ors,
            List<AbstractCriterion> ands)
        {
            AbstractCriterion abstractCriterion = ors.Any() ? ors[0] : ands[0];
            for (int i = ors.Any() ? 0 : 1; i < ands.Count; i++)
            {
                abstractCriterion &= ands[i];
            }
            for (int i = 1; i < ors.Count; i++)
            {
                abstractCriterion |= ors[i];
            }

            criteria = criteria.Add(abstractCriterion);
            return criteria;


        }
		
	    public virtual  Task<long> GetCountAsync<T>(ISession session,ICriteria where = null, CancellationToken cancellationToken = default) where T : class
        {
            if (where != null)
            {
#if NET40 || NET45 || NET451 || NET452 || NET46
				return Task.FromResult(where.SetProjection(Projections.RowCount()).UniqueResult<long>());
#else
                var count =  where.SetProjection(Projections.RowCount()).UniqueResultAsync<long>(cancellationToken);
				return count;
#endif
            }
            else
            {
#if NET40 || NET45 || NET451 || NET452 || NET46
                return Task.FromResult(GetCount<T>(session, (Expression<Func<T, bool>>)null));
#else
                var count =  GetCountAsync<T>(session, (Expression<Func<T, bool>>)null, cancellationToken);
                return count;
#endif
            }
        }

		
		public virtual  Task<int> GetCountAsync<T>(IStatelessSession session,ICriteria where = null, CancellationToken cancellationToken = default) where T : class
        {
            if (where != null)
            {
#if NET40 || NET45 || NET451 || NET452 || NET46
				return Task.FromResult(where.SetProjection(Projections.RowCount()).UniqueResult<int>());
#else
                var count =  where.SetProjection(Projections.RowCount()).UniqueResultAsync<int>(cancellationToken);
				return count;
#endif
            }
            else
            {
#if NET40 || NET45 || NET451 || NET452 || NET46
                return Task.FromResult(GetCount<T>(session, (Expression<Func<T, bool>>)null));
#else
                var count =  GetCountAsync<T>(session, (Expression<Func<T, bool>>)null, cancellationToken);
                return count;
#endif
            }
        }


		
		public virtual long GetCount<T>(ISession session,ICriteria where = null) where T : class
        {
            if (where != null)
            {
                var count = where.SetProjection(Projections.RowCount()).UniqueResult<long>();
                return count;
            }
            else
            {
                var count = this.GetCount<T>(session,(Expression<Func<T, bool>>)null);
                return count;
            }
        }
       
		
		public virtual int GetCount<T>(IStatelessSession session,ICriteria where = null) where T : class
        {
            if (where != null)
            {
                var count = where.SetProjection(Projections.RowCount()).UniqueResult<int>();
                return count;
            }
            else
            {
                var count = this.GetCount<T>(session,(Expression<Func<T, bool>>)null);
                return count;
            }
        }

    
		public virtual List<T> GetList<T>(ISession session,ICriteria where = null) where T : class
        {
            if (where != null)
            {
                var data = where.List<T>().ToList();
			    return data;
            }
            else
            {
                var data = this.GetQuery<T>(session,(Expression<Func<T, bool>>)null).ToList();
				return data;
            }
        }
     
		public virtual List<T> GetList<T>(IStatelessSession session,ICriteria where = null) where T : class
        {
            if (where != null)
            {
                var data = where.List<T>().ToList();
			    return data;
            }
            else
            {
                var data = this.GetQuery<T>(session,(Expression<Func<T, bool>>)null).ToList();
				return data;
            }
        }
      
		public virtual List<T> GetListByPage<T>(ISession session, ICriteria where = null, int page = 1, int size = 10) where T : class
        {
            PageHelper.Set(ref page, ref size);
            if (where != null)
            {
                var data = where.SetFirstResult((page - 1) * size).SetMaxResults(size).List<T>().ToList();
                return data;
            }
            else
            {
				var data = this.GetQuery<T>(session,(Expression<Func<T, bool>>)null).Skip((page - 1) * size).Take(size).ToList();// 修改后 查询  修改的数据 都是默认值(null or "" ) cs 模式下 
				//var data = session.Query<T>().Skip((page - 1) * size).Take(size).ToList();//修改后 查询异常
				return data;
            }
        }
        
		public virtual List<T> GetListByPage<T>(IStatelessSession session, ICriteria where = null, int page = 1, int size = 10) where T : class
        {
            PageHelper.Set(ref page, ref size);
            if (where != null)
            {
                var data = where.SetFirstResult((page - 1) * size).SetMaxResults(size).List<T>().ToList();
                return data;
            }
            else
            {
				var data = this.GetQuery<T>(session,(Expression<Func<T, bool>>)null).Skip((page - 1) * size).Take(size).ToList();// 修改后 查询  修改的数据 都是默认值(null or "" ) cs 模式下 
				//var data = session.Query<T>().Skip((page - 1) * size).Take(size).ToList();//修改后 查询异常
				return data;
            }
        }
       
		public virtual Tuple<List<T>, int> GetTupleByPage<T>(IStatelessSession session,ICriteria where = null, int page = 1, int size = 10) where T : class
        {
            PageHelper.Set(ref page, ref size);
            var datas = GetListByPage<T>(session,where, page, size);
            var count = GetCount<T>(session,where!=null ? (ICriteria)where.Clone():null);
            return new Tuple<List<T>, int>(datas, count);
        }

		
	    public virtual Tuple<List<T>, long> GetTupleByPage<T>(ISession session,ICriteria where = null, int page = 1, int size = 10) where T : class
        {
            PageHelper.Set(ref page, ref size);
            var datas = GetListByPage<T>(session,where, page, size);
            var count = GetCount<T>(session,where!=null ? (ICriteria)where.Clone():null);
            return new Tuple<List<T>, long>(datas, count);
        }
#region async




		public virtual  Task<List<T>> GetListAsync<T>(ISession session,ICriteria where = null,
            CancellationToken cancellationToken = default) where T : class
        {
            if (where != null)
            {

#if NET40 || NET45 || NET451 || NET452 || NET46
                return Task.FromResult(((IList<T>)where.List()).ToList());
#else
                var data =  where.ListAsync<T>(cancellationToken).Result;
                return Task.FromResult(data.ToList());
#endif
            }
            else
            {
#if NET40 || NET45 || NET451 || NET452 || NET46
                return Task.FromResult(this.GetQuery<T>(session, (Expression<Func<T, bool>>)null).ToList());
#else
                var data =  this.GetQuery<T>(session,(Expression<Func<T, bool>>)null).ToListAsync(cancellationToken);
				return data;
#endif
            }
        }

   
		public virtual  Task<List<T>> GetListAsync<T>(IStatelessSession session,ICriteria where = null,
            CancellationToken cancellationToken = default) where T : class
        {
            if (where != null)
            {

#if NET40 || NET45 || NET451 || NET452 || NET46
                return Task.FromResult(((IList<T>)where.List()).ToList());
#else
                return Task.FromResult(where.ListAsync<T>(cancellationToken).Result.ToList());
#endif
            }
            else
            {
#if NET40 || NET45 || NET451 || NET452 || NET46
                return Task.FromResult(this.GetQuery<T>(session, (Expression<Func<T, bool>>)null).ToList());
#else
                var data =  this.GetQuery<T>(session,(Expression<Func<T, bool>>)null).ToListAsync(cancellationToken);
				return data;
#endif
            }
        }
       
		public virtual Task<List<T>> GetListByPageAsync<T>(ISession session,ICriteria where = null, int page=1, int size=10,
            CancellationToken cancellationToken = default) where T : class
        {
            PageHelper.Set(ref page, ref size);
            if (where != null)
            {
#if NET40 || NET45 || NET451 || NET452 || NET46
                return Task.FromResult(((IList<T>) where.SetFirstResult((page - 1) * size).SetMaxResults(size).List()).ToList());
#else
                var data =  where.SetFirstResult((page - 1) * size).SetMaxResults(size).ListAsync<T>(cancellationToken).Result;
                return Task.FromResult(data.ToList());
#endif
            }
            else
            {
#if NET40 || NET45 || NET451 || NET452 || NET46
			   return Task.FromResult(this.GetQuery<T>(session, (Expression<Func<T, bool>>)null).Skip((page - 1) * size).Take(size).ToList());
#else
                var data =  this.GetQuery<T>(session, (Expression<Func<T, bool>>)null).Skip((page - 1) * size).Take(size).ToListAsync(cancellationToken);
			   return data;
#endif
            }
        }

        public virtual Task<List<T>> GetListByPageAsync<T>(IStatelessSession session, ICriteria where = null, int page = 1, int size = 10,
            CancellationToken cancellationToken = default) where T : class
        {
            PageHelper.Set(ref page, ref size);
            if (where != null)
            {
#if NET40 || NET45 || NET451 || NET452 || NET46
                return Task.FromResult(((IList<T>) where.SetFirstResult((page - 1) * size).SetMaxResults(size).List()).ToList());
#else
                var data = where.SetFirstResult((page - 1) * size).SetMaxResults(size).ListAsync<T>(cancellationToken).Result;
                return Task.FromResult( data.ToList());
#endif
            }
            else
            {
#if NET40 || NET45 || NET451 || NET452 || NET46
			   return Task.FromResult(this.GetQuery<T>(session, (Expression<Func<T, bool>>)null).Skip((page - 1) * size).Take(size).ToList());
#else
                var data = this.GetQuery<T>(session, (Expression<Func<T, bool>>)null).Skip((page - 1) * size).Take(size).ToListAsync(cancellationToken);
                return data;
#endif
            }
        }
       
		 public virtual
#if !(NET40 || NET45 || NET451 || NET452 || NET46)
            async
#endif
            Task<Tuple<List<T>, long>> GetTupleByPageAsync<T>(ISession session, ICriteria where = null, int page=1,
            int size=10,  CancellationToken cancellationToken = default) where T : class
        {
            PageHelper.Set(ref page, ref size);
#if NET40 || NET45 || NET451 || NET452 || NET46
            var da =  GetListByPage<T>(session, where, page, size);
            ICriteria pageCriteria = where!=null ? (ICriteria)where.Clone():null;
            var count =  GetCount<T>(session,pageCriteria);
            return  Task.FromResult(new Tuple<List<T>, int>(da, count));
#else
            var da = await GetListByPageAsync<T>(session, where, page, size, cancellationToken);
            ICriteria pageCriteria = where!=null ? (ICriteria)where.Clone():null;
            var count = await GetCountAsync<T>(session,pageCriteria, cancellationToken);
            return new Tuple<List<T>, long>(da, count);
#endif
        }

       
		 public virtual
#if !(NET40 || NET45 || NET451 || NET452 || NET46)
            async
#endif
            Task<Tuple<List<T>, long>> GetTupleByPageAsync<T>(IStatelessSession session, ICriteria where = null, int page=1,
            int size=10,  CancellationToken cancellationToken = default) where T : class
        {
            PageHelper.Set(ref page, ref size);
#if NET40 || NET45 || NET451 || NET452 || NET46
           var da =  GetListByPage<T>(session, where, page, size);
            ICriteria pageCriteria = where!=null ? (ICriteria)where.Clone():null;
            var count =  GetCount<T>(session,pageCriteria);
            return  Task.FromResult(new Tuple<List<T>, int>(da, count));
#else
            var da = await GetListByPageAsync<T>(session, where, page, size, cancellationToken);
            ICriteria pageCriteria =where!=null ? (ICriteria)where.Clone():null;
            var count = await GetCountAsync<T>(session,pageCriteria, cancellationToken);
            return new Tuple<List<T>, long>(da, count);
#endif
        }
#endregion async


       

		public virtual T Get<T>(ISession session, Expression<Func<T, bool>> where = null) where T : class
        {
            return this.GetQuery<T>(session,where).FirstOrDefault() ;
        }
       
		public virtual T Get<T>(IStatelessSession session, Expression<Func<T, bool>> where = null) where T : class
        {
            return this.GetQuery<T>(session,where).FirstOrDefault() ;
        }

        /// <summary> 查询数据 </summary>
        /// <param name="session"></param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual IQueryable<T> Find<T>(ISession session, Expression<Func<T, bool>> where = null) where T : class
        {
            return this.Query<T>(session,where) ;
        }

        /// <summary> 查询数据 </summary>
        /// <param name="session"></param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual IQueryable<T> Find<T>(IStatelessSession session, Expression<Func<T, bool>> where = null) where T : class
        {
             return this.Query<T>(session,where) ;
        }


		public virtual IQueryable<T> GetByPage<T>(ISession session, Expression<Func<T, bool>> where = null, int page = 1, int size = 10) where T : class
        {
            PageHelper.Set(ref page, ref size);
            return this.GetQuery<T>(session, where).Skip((page - 1) * size).Take(size) ;
        }
        
		public virtual IQueryable<T> GetByPage<T>(IStatelessSession session, Expression<Func<T, bool>> where = null, int page = 1, int size = 10) where T : class
        {
            PageHelper.Set(ref page, ref size);
            return this.GetQuery<T>(session, where).Skip((page - 1) * size).Take(size) ;
        }
#region async

    
		public virtual Task<T> GetAsync<T>(ISession session, Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return Task.FromResult(this.GetQuery<T>(session, where).FirstOrDefault());
#else
            return this.GetQuery<T>(session, where).FirstOrDefaultAsync(cancellationToken) ;
#endif
        }

        public virtual Task<T> GetAsync<T>(IStatelessSession session, Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default) where T : class
        {
#if NET40 || NET45 || NET451 || NET452 || NET46
            return Task.FromResult(this.GetQuery<T>(session, where).FirstOrDefault());
#else
            return this.GetQuery<T>(session, where).FirstOrDefaultAsync(cancellationToken);
#endif

        }
		public virtual 
#if !(NET40 || NET45 || NET451 || NET452 || NET46)
        async
#endif
        Task<bool> HasExistAsync<T>(ISession session, Expression<Func<T, bool>> where, CancellationToken cancellationToken = default) where T : class
        {
#if (NET40 || NET45 || NET451 || NET452 || NET46)
           return Task.FromResult(this.GetCount<T>(session, where) >= 1);
#else
            return await this.GetCountAsync<T>(session, where, cancellationToken) >= 1;
#endif
        }

		public virtual 
#if !(NET40 || NET45 || NET451 || NET452 || NET46)
        async
#endif
        Task<bool> HasExistAsync<T>(IStatelessSession session, Expression<Func<T, bool>> where, CancellationToken cancellationToken = default) where T : class
        {
#if (NET40 || NET45 || NET451 || NET452 || NET46)
           return Task.FromResult(this.GetCount<T>(session, where) >= 1);
#else
            return await this.GetCountAsync<T>(session, where, cancellationToken) >= 1;
#endif
        }

        /// <summary> 查询数据 </summary>
        /// <param name="session"></param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual Task<IQueryable<T>> FindAsync<T>(ISession session, Expression<Func<T, bool>> where = null) where T : class
        {
            var res =this.Query<T>(session, where);
            return Task.FromResult(res);
        }

        /// <summary> 查询数据 </summary>
        /// <param name="session"></param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual Task<IQueryable<T>> FindAsync<T>(IStatelessSession session,Expression<Func<T, bool>> where = null) where T : class
        {
            var res = this.Query<T>(session,where) ;
            return Task.FromResult(res);
        }

        /// <summary> 查询数据  </summary>
        /// <param name="session"></param>
        /// <param name="where">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <returns></returns>
        public virtual Task<IQueryable<T>> FindByPageAsync<T>(ISession session, Expression<Func<T, bool>> where = null, int page = 1, int size = 10) where T : class
        {
            PageHelper.Set(ref page, ref size);
            var res =  this.Query<T>(session, where).Skip((page - 1) * size).Take(page) ;
            return Task.FromResult(res);
        }

        public virtual int GetExecuteQuery(ISession session, string sql)
        { 
            //select table 
          //int res = session.CreateQuery(sql).ExecuteUpdate();  //from tble
            int res = session.CreateSQLQuery(sql).ExecuteUpdate();  ////select table 
            return res;
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
            Task<int> GetExecuteQueryAsync(ISession session, string sql, CancellationToken cancellationToken = default)
        {
#if (NET40 || NET45 || NET451 || NET452 || NET46)
           return Task.FromResult(GetExecuteQuery(session,sql));
#else

            //select table 
            //int res = session.CreateQuery(sql).ExecuteUpdate();  //from tble
            int res = await session.CreateSQLQuery(sql).ExecuteUpdateAsync(cancellationToken);  ////select table 
            return res;
#endif
        }
        /// <summary> 查询数据  </summary>
        /// <param name="session"></param>
        /// <param name="where">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">记录</param>
        /// <returns></returns>
        public virtual Task<IQueryable<T>> FindByPageAsync<T>(IStatelessSession session, Expression<Func<T, bool>> where = null, int page = 1, int size = 10) where T : class
        {
            PageHelper.Set(ref page, ref size);
            var res = this.Query<T>(session,where).Skip((page - 1) * size).Take(page);
            return Task.FromResult(res);
        }
#endregion async

    
		public virtual Tuple<List<T>, long> GetTupleByPage<T>(ISession session,Expression<Func<T, bool>> where = null, int page = 1, int size = 10) where T : class
        {
            PageHelper.Set(ref page, ref size);
            List<T> data =  this.GetQuery<T>(session, where).Skip((page - 1) * page).Take(size).ToList();
            long count = this.GetCount(session, where);
            return new Tuple<List<T>, long>(data, count);
        }

  
	    public virtual Tuple<List<T>, long> GetTupleByPage<T>(IStatelessSession session,Expression<Func<T, bool>> where = null, int page = 1, int size = 10) where T : class
        {
            PageHelper.Set(ref page, ref size);
            List<T> data =  this.GetQuery<T>(session, where).Skip((page - 1) * page).Take(size).ToList();
            long count = this.GetCount(session, where);
            return new Tuple<List<T>, long>(data, count);
        }
    }
}
#endif