using NHibernate;
using NHibernate.Criterion;
using Spring.Data.NHibernate.Generic.Support;
using System;
using System.Collections.Generic;

namespace Adverts
{
    public abstract class ShopRepositoryBase<TEntity, TPrimaryKey> : HibernateDaoSupport
        where TEntity : class
    {
        protected ShopRepositoryBase() 
        {

        }

        //add common methods for all repositories

      
    }

    public abstract class ShopRepositoryBase<TEntity> : ShopRepositoryBase<TEntity, string>
        where TEntity : BaseEntity,new()
    {
        protected ShopRepositoryBase() 
        {

        }
        public  TEntity Insert(TEntity entity)
        {
            entity.CreationTime = DateTime.Now;
            entity.Id = Guid.NewGuid().ToString("N");
            base.HibernateTemplate.Save(entity);
            return entity;
        }

        protected virtual AbstractCriterion GetWhere(TEntity entity)
        {
            AbstractCriterion criterion = NHibernate.Criterion.Expression.Eq("IsDeleted", false);
            if (!string.IsNullOrEmpty(entity.Id))
            {
                criterion |= NHibernate.Criterion.Expression.Eq("Id", entity.Id);
            }
            return criterion;
        }

        public virtual IList<TEntity> Find(TEntity entity)
        {
            //abp 内部异常 asp.net error asp.net core pass
            // using (var tx=Session.BeginTransaction())
            {
                ICriteria criteria = GetCriteria(entity);
                return criteria.List<TEntity>();
            }
        }

        internal ICriteria GetCriteria(TEntity entity)
        {
            var where = GetWhere(entity);
            ICriteria criteria = Session.CreateCriteria<TEntity>();
            criteria.AddOrder(NHibernate.Criterion.Order.Desc("CreationTime"));
            if (where != null)
            {
                criteria = criteria.Add(where);
            }
            return criteria;
        }

        public virtual IList<TEntity> FindByPage(TEntity entity,int page,int size)
        {
            //abp 内部异常 asp.net error asp.net core pass
            // using (var tx=Session.BeginTransaction())
            {
                ICriteria criteria = GetCriteria(entity);
                return criteria.SetFirstResult((page - 1) * size).SetMaxResults(size).List<TEntity>();
            }
        }
        public virtual int Count(TEntity entity)
        {
            //abp 内部异常 asp.net error asp.net core pass
           // using (var tx=Session.BeginTransaction())
            {
                ICriteria criteria = GetCriteria(entity);
                return criteria.SetProjection(Projections.RowCount()).UniqueResult<int>();
            }
        }
        public  void Delete(string id)
        {
           base.HibernateTemplate.Execute(session=> {
               //版本过低 没法 使用 linq update
               //session.Query<TEntity>().Where(it => it.Id == id && !it.IsDeleted).Update(it => new TEntity() { IsDeleted = true, LastModificationTime = DateTime.Now });
               using (var tx=session.BeginTransaction())
               {
                   var res=session.CreateSQLQuery($"update { GetTable()} set DeletionTime='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',IsDeleted=1 where id=? and IsDeleted=0")
                   .SetString(0, id).ExecuteUpdate();
                   tx.Commit();
                   return res;
               }
           });
        }
        public  TEntity Update(TEntity entity)
        {
            //已删除 的不能 修改
            var result=base.HibernateTemplate.Execute(session => {
                //版本过低 没法 使用 linq update
                using (var tx = session.BeginTransaction())
                {
                    var res = session.CreateSQLQuery($"select CreationTime from  { GetTable()}  where id=? and IsDeleted=0")
                    .SetString(0, entity.Id).UniqueResult<DateTime?>();
                    return res;
                }
            });
            //var old = base.GetAll().Where(it => it.Id == entity.Id&&!it.IsDeleted).Select(it=>new { it.CreationTime }).FirstOrDefault();
            if (result == null)
            {
                throw new Exception("not exists!");
            }
            entity.CreationTime = result.Value;
            entity.LastModificationTime = DateTime.Now;
            base.HibernateTemplate.Update(entity);
            return entity;
        }

        //do not add any method here, add to the class above (since this inherits it)

        public void Delete(string[] ids)
        {
            //abp 异常  支持 组合 linq 只支持 一次 到位 
            //Expression<Func<TEntity, bool>> where=null;
            //for (int i = 0; i < ids.Length; i++)
            //{
            //    if (where == null)
            //    {
            //        where = it => it.Id == ids[i];
            //    }
            //    else
            //    {
            //        where = where.Or(it => it.Id == ids[i]);
            //    }
            //}
            //base.Delete(where);//之前版本 nhibernate 是不支持 的 abp 做了处理？
            //return;
            //string sql = $"delete from {GetTable()} where ";

            string sql = $"update  {GetTable()} set DeletionTime='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',IsDeleted=1 where id in(";

            for (int i = 0; i < ids.Length; i++)
            {
                sql += "?";
                if (i != ids.Length - 1)
                    sql += ",";
            }
            sql += ");";
            //Cannot access a disposed object
            //提交成功 abp appservice 异常 拦截器 框架 已组织 好 
            using (var tx=Session.BeginTransaction())
            {
                IQuery sQLQuery = Session.CreateSQLQuery(sql);
                for (int i = 0; i < ids.Length; i++)
                {
                    sQLQuery = sQLQuery.SetString(i, ids[i]);
                }
                int res = sQLQuery.ExecuteUpdate();
                tx.Commit();
            }
        }

       

        protected virtual string GetTable()
        {
            return string.Empty;
        }
    }
}