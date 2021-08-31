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
        ///  ��д����
        /// </summary>
        /// <param name="writeSession"> д��</param>
        /// <param name="readSession">����</param>
        public BaseNhibernateRepository(Lazy<SessionProvider> writeSession, Lazy<SessionProvider> readSession):base(writeSession,readSession)
        {

        }

        /// <summary> ɾ��</summary>
        /// <param name="id">ʵ�� id</param>
        public virtual int Delete(Key id)
        {
           return base.UnitWork.Delete<Entity>(id);
        }

        /// <summary> ɾ��</summary>
        /// <param name="ids">ʵ�� id</param>
        public virtual int DeleteList(Key[] ids)
        {
           return base.UnitWork.DeleteList<Entity,Key>(ids);
        }

#if !(NET20 || NET30 || NET35)
        /// <summary> ��ѯ����  Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq  </summary>
        /// <param name="id">����</param>
        /// <param name="cancellationToken">dapper ef ��Ч</param>
        /// <returns></returns>
        public virtual Task<int> DeleteAsync(Key id, CancellationToken cancellationToken = default)
        {
           return base.UnitWork.DeleteAsync<Entity>(id,cancellationToken);
        }

        /// <summary> ��ѯ����  Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq  </summary>
        /// <param name="ids">����</param>
        /// <param name="cancellationToken">dapper ef ��Ч</param>
        /// <returns></returns>
        public virtual  Task<int> DeleteListAsync(Key[] ids, CancellationToken cancellationToken = default)
        {
            return base.UnitWork.DeleteListAsync<Entity,Key>(ids, cancellationToken);
        }
#endif
    }

    /// <summary>
    /// Nhibernate �ֿ�
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    public class BaseNhibernateRepository<Entity> : BaseRepository<Entity>, IRepository<Entity> where Entity:class
    {
        /// <summary>
        /// Nhibernate ������Ԫ
        /// </summary>
        public new NhibernateUnitWork UnitWork { get;protected set; }
        /// <summary>
        /// Nhibernate �ֿ�
        /// </summary>
        /// <param name="session"></param>
        public BaseNhibernateRepository(SessionProvider session)
        {
            UnitWork = new NhibernateUnitWork(session);
            base.UnitWork = UnitWork;
        }

        /// <summary>
        ///  ��д����
        /// </summary>
        /// <param name="writeSession"> д��</param>
        /// <param name="readSession">����</param>
        public BaseNhibernateRepository(Lazy<SessionProvider> writeSession, Lazy<SessionProvider> readSession)
        {
            UnitWork = new NhibernateUnitWork(writeSession, readSession);
            base.UnitWork = UnitWork;
        }


        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns></returns>
        protected virtual string GetTable()
        {
            return string.Empty;
        }

        /// <summary>
        /// ģ����ѯ ͨ�ò�ѯ Ĭ��ʵ��
        /// </summary>
        /// <param name="criterias"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual void QueryFilterByAnd(List<AbstractCriterion> criterias, Entity obj)
        {

        }

        /// <summary>
        /// ģ����ѯ ͨ�ò�ѯ Ĭ��ʵ��
        /// </summary>
        /// <param name="criterias"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual void QueryFilterByOr(List<AbstractCriterion> criterias, Entity obj)
        {

        }

        /// <summary>
        /// ��ѯ ���� ��֧�� linq ��Ҫ �Լ�ת��(���linq)
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
        /// ɾ��
        /// </summary>
        /// <typeparam name="Key"></typeparam>
        /// <param name="id"></param>
        public override int Delete<Key>(Key id)
        {
          return  base.UnitWork.Delete<Entity>(id);
        }

        /// <summary>
        /// �� ɾ��
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


        /// <summary>������������ҳ��ѯʵ�������ݼ���Ϣ</summary>
        /// <param name="where">����</param>
        /// <param name="page">ҳ��</param>
        /// <param name="size">ÿҳ��¼</param>
        ///<return>����ʵ�������ݼ���Ϣ</return>

        public virtual List<T> FindListByPage<T>(ICriteria where = null, int page = 1, int size = 10) where T : class
        {
            return UnitWork.FindListByPage<T>(where, page, size);
        }

        /// <summary>������������ҳ��ѯʵ�������ݼ���Ϣ��ʵ�������ݼ�������Ϣ</summary>
        /// <param name="where">����</param>
        /// <param name="page">ҳ��</param>
        /// <param name="size">ÿҳ��¼</param>
        ///<return>����ʵ�������ݼ���Ϣ��ʵ�������ݼ�������Ϣ</return>
        public virtual ResultDto<T> FindResultByPage<T>(ICriteria where = null, int page = 1, int size = 10) where T : class
        {
            return UnitWork.FindResultByPage<T>(where, page, size);
        }

        /// <summary>������������ҳ��ѯʵ�������ݼ���Ϣ��ʵ�������ݼ�������Ϣ</summary>
        /// <param name="where">����</param>
        /// <param name="page">ҳ��</param>
        /// <param name="size">ÿҳ��¼</param>
        ///<return>����ʵ�������ݼ���Ϣ��ʵ�������ݼ�������Ϣ</return>
        public virtual Tuple<List<T>, long> FindTupleByPage<T>(ICriteria where = null, int page = 1, int size = 10) where T : class
        {
            return UnitWork.FindTupleByPage<T>(where, page, size);
        }
    }
}
#endif
