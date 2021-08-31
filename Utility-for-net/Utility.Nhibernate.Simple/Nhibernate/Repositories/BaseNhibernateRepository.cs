#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
//#if NET40 ||NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NET481 || NET482 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETCOREAPP3_2 || NETSTANDARD2_0 || NETSTANDARD2_1
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using Utility.Domain.Entities;
using Utility.Domain.Extensions;
using Utility.Domain.Repositories;
using Utility.Nhibernate.Uow;
#if !(NET20 || NET30 || NET35)
using System.Threading.Tasks;
using Utility.Application.Services.Dtos;
using Utility.Attributes;
#endif
#if !(NET20 || NET30)
using System.Linq;
using System.Linq.Expressions;
#endif
using System.Threading;


namespace Utility.Nhibernate.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    /// <typeparam name="Key"></typeparam>
    [Transtation]
    public class BaseNhibernateRepository<Entity,Key> : BaseNhibernateRepository<Entity>, IRepository<Entity, Key>, IRepository<Entity>
        where Entity : class, IEntity<Key>
   
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public BaseNhibernateRepository(SessionProvider session):base(session)
        {
            
        }

        /// <summary>
        ///  读写分离
        /// </summary>
        /// <param name="writeSession"> 写库</param>
        /// <param name="readSession">读库</param>
        public BaseNhibernateRepository(Lazy<SessionProvider> writeSession, Lazy<SessionProvider> readSession):base(writeSession,readSession)
        {

        }

        /// <summary> 删除</summary>
        /// <param name="id">实体 id</param>
        public virtual int Delete(Key id)
        {
           return base.UnitWork.Delete<Entity>(id);
        }

        /// <summary> 删除</summary>
        /// <param name="ids">实体 id</param>
        public virtual int DeleteList(Key[] ids)
        {
           return base.UnitWork.DeleteList<Entity,Key>(ids);
        }

#if !(NET20 || NET30 || NET35)
        /// <summary> 查询数据  默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="id">条件</param>
        /// <param name="cancellationToken">dapper ef 无效</param>
        /// <returns></returns>
        public virtual Task<int> DeleteAsync(Key id, CancellationToken cancellationToken = default)
        {
           return base.UnitWork.DeleteAsync<Entity>(id,cancellationToken);
        }

        /// <summary> 查询数据  默认实现 ef,nhibernate 支持 linq dapper 不支持linq  </summary>
        /// <param name="ids">条件</param>
        /// <param name="cancellationToken">dapper ef 无效</param>
        /// <returns></returns>
        public virtual  Task<int> DeleteListAsync(Key[] ids, CancellationToken cancellationToken = default)
        {
            return base.UnitWork.DeleteListAsync<Entity,Key>(ids, cancellationToken);
        }
#endif
    }

    /// <summary>
    /// Nhibernate 仓库
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    public class BaseNhibernateRepository<Entity> : BaseRepository<Entity>, IRepository<Entity> where Entity:class
    {
        /// <summary>
        /// Nhibernate 工作单元
        /// </summary>
        public new NhibernateUnitWork UnitWork { get;protected set; }
        /// <summary>
        /// Nhibernate 仓库
        /// </summary>
        /// <param name="session"></param>
        public BaseNhibernateRepository(SessionProvider session)
        {
            UnitWork = new NhibernateUnitWork(session);
            base.UnitWork = UnitWork;
        }

        /// <summary>
        ///  读写分离
        /// </summary>
        /// <param name="writeSession"> 写库</param>
        /// <param name="readSession">读库</param>
        public BaseNhibernateRepository(Lazy<SessionProvider> writeSession, Lazy<SessionProvider> readSession)
        {
            UnitWork = new NhibernateUnitWork(writeSession, readSession);
            base.UnitWork = UnitWork;
        }


        /// <summary>
        /// 获取表名
        /// </summary>
        /// <returns></returns>
        protected virtual string GetTable()
        {
            return string.Empty;
        }

        /// <summary>
        /// 模糊查询 通用查询 默认实现
        /// </summary>
        /// <param name="criterias"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual void QueryFilterByAnd(List<AbstractCriterion> criterias, Entity obj)
        {

        }

        /// <summary>
        /// 模糊查询 通用查询 默认实现
        /// </summary>
        /// <param name="criterias"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual void QueryFilterByOr(List<AbstractCriterion> criterias, Entity obj)
        {

        }

        /// <summary>
        /// 查询 条件 不支持 linq 需要 自己转换(组合linq)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual ICriteria GetWhere(Entity entity)
        {
            List<AbstractCriterion> ors = new List<AbstractCriterion>();
            List<AbstractCriterion> ands = new List<AbstractCriterion>();
            this.QueryFilterByOr(ors, entity);
            this.QueryFilterByAnd(ands, entity);
            bool res = ors.Count > 0 || ands.Count > 0;
            ICriteria criteria = null;
            if (res)
            {
                criteria = NhibernateTemplate.QueryWhere<Entity>(UnitWork.Read.Session, ors, ands);
            }
            return criteria;
        }
        //public NhibernateUnitWork(AppSessionFactory sessionFactory) : base(sessionFactory)
        //{
        //    
        //}
      

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="Key"></typeparam>
        /// <param name="id"></param>
        public override int Delete<Key>(Key id)
        {
          return  base.UnitWork.Delete<Entity>(id);
        }

        /// <summary>
        /// 多 删除
        /// </summary>
        /// <typeparam name="Key"></typeparam>
        /// <param name="ids"></param>
        public virtual void DeleteList<Key>(Key[] ids)
        {
            if (string.IsNullOrEmpty(GetTable()))
            {
                UnitWork.DeleteList<Entity, Key>(ids);
                return;
            }
            if (string.IsNullOrEmpty(GetTable()))
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    var obj = NhibernateTemplate.Empty.Get<Entity>(UnitWork.Write.Session, ids[i]);
                    NhibernateTemplate.Empty.Remove(UnitWork.Write.Session, obj);
                }
                return;
            }
            string sql = $"delete from {GetTable()} where id in(";
            for (int i = 0; i < ids.Length; i++)
            {
                sql += "?";
                if (i != ids.Length - 1)
                {
                    sql += ",";
                }
            }
            var sqlQuery = UnitWork.Write.Session.CreateSQLQuery(sql);
            IQuery query = null;
            for (int i = 0; i < ids.Length; i++)
            {
                if (query != null)
                {
                    query = query.SetParameter(i, ids[i]);
                }
                else
                {
                    query = sqlQuery.SetParameter(i, ids[i]);
                }
            }
            query.ExecuteUpdate();
        }


        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="where">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息</return>

        public virtual List<T> FindListByPage<T>(ICriteria where = null, int page = 1, int size = 10) where T : class
        {
            return UnitWork.FindListByPage<T>(where, page, size);
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="where">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        public virtual ResultDto<T> FindResultByPage<T>(ICriteria where = null, int page = 1, int size = 10) where T : class
        {
            return UnitWork.FindResultByPage<T>(where, page, size);
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="where">条件</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        public virtual Tuple<List<T>, long> FindTupleByPage<T>(ICriteria where = null, int page = 1, int size = 10) where T : class
        {
            return UnitWork.FindTupleByPage<T>(where, page, size);
        }
    }
}
#endif
