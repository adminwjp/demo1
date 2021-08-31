#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
//#if NET40 ||NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NET481 || NET482 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETCOREAPP3_2 || NETSTANDARD2_0 || NETSTANDARD2_1
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using Utility.Application.Services.Dtos;
using Utility.Attributes;
using Utility.Domain.Entities;
using Utility.Domain.Extensions;
using Utility.Domain.Uow;
using Utility.Helpers;
using Utility.Nhibernate.Infrastructure;
using Utility.Threads;

namespace Utility.Nhibernate.Uow
{
    /// <summary>
    /// ��� ���߳�ÿ��ִ�� Save��ôȷ���ύ (��ô׷��ÿ�ε�����)
    /// </summary>
    [Transtation]
    public class NhibernateUnitWork : IUnitWork
    {
        SessionProvider write;
        SessionProvider read;
        public bool UseTransaction { get;  set; } = true;
        /// <summary>
        ///Ĭ��ʵ�� д ��
        /// </summary>
        public Lazy<SessionProvider> WriteSession;

        /// <summary>
        ///Ĭ��ʵ�� 
        /// </summary>
        public SessionProvider Session;

        /// <summary>
        /// �� ��
        /// </summary>
        public Lazy<SessionProvider> ReadSession;

        /// <summary>
        ///Ĭ��ʵ�� д ��
        /// </summary>
        public SessionProvider Write
        {
            get
            {
                if (write != null)
                {
                    return write;
                }
                return write = Session ?? WriteSession.Value;
            }
            private set
            {
                write = value;
            }
        }

        /// <summary>
        ///Ĭ��ʵ��  �� ��
        /// </summary>
        public SessionProvider Read
        {
            get
            {
                if (read != null)
                {
                    return read;
                }
                return read = Session ?? ReadSession.Value;
            }
            private set
            {
                read = value;
            }
        }
        /// <summary>���ݿ����Ӷ��� </summary>
        public IDbConnection Connection
        {
            get
            {
                connection = connection ?? write?.Session.Connection ?? read?.Session.Connection ?? WriteSession.Value.Session.Connection;
                return connection;
            }
            private set => connection = value;
        }

        /// <summary>
        /// NhibernateTemplate
        /// </summary>
        protected NhibernateTemplate Template;
        private IDbConnection connection;
        private TransactionManager transtion;
        private TransactionManager readTranstion;
        private TransactionManager writeTranstion;


        /// <summary>
        /// Ĭ��ʵ��
        /// </summary>
        /// <param name="session"></param>
        public NhibernateUnitWork(SessionProvider session)
        {
            this.Session = session;
            this.write = session;
            this.read = session;
            this.Connection = session?.Session.Connection;
            this.Template = NhibernateTemplate.Empty;
        }
        /// <summary>
        ///  ��д����
        /// </summary>
        /// <param name="writeSession"> д��</param>
        /// <param name="readSession">����</param>
        public NhibernateUnitWork(Lazy<SessionProvider> writeSession, Lazy<SessionProvider> readSession)
        {
            this.WriteSession = writeSession;
            this.ReadSession = readSession;
            //this.Connection = writeSession.Value.Connection;
            this.Template = NhibernateTemplate.Empty;
            this.Single = false;
        }
        //public NhibernateUnitWork(AppSessionFactory sessionFactory) : base(sessionFactory)
        //{
        //    this.Connection = sessionFactory.OpenSession().Connection;
        //}



        /// <summary>
        /// ����
        /// </summary>
        protected bool Single { get; set; } = true;

        public TransactionManager Transaction
        { get
            {
                if (transtion == null)
                {
                    transtion = Session.Transaction;
                }
                return transtion;
            }
            internal set => transtion = value; }
        public TransactionManager ReadTransaction
        { get
            {
                if (readTranstion == null)
                {
                    readTranstion = Read.Transaction;
                }
                return readTranstion;
            }
            internal set => readTranstion = value; }
        public TransactionManager WriteTransaction
        { get
            {
                if (writeTranstion == null)
                {
                    writeTranstion = Write.Transaction;
                }
                return writeTranstion;
            }
            internal set => writeTranstion = value; }
        public void UpdaeTransaction()
        {
            if (Single)
            {
                Transaction = Session.Transaction;
            }
            else
            {
                ReadTransaction = Read?.Transaction;
                WriteTransaction = Write?.Transaction;
            }
        }
        public void SetReadTranstion(SessionProvider session)
        {
            ReadTransaction = session.Transaction;
            Transaction = ReadTransaction;
        }
        public void SetWriteTranstion(SessionProvider session)
        {
            WriteTransaction = session.Transaction;
            Transaction = WriteTransaction;
        }
        /// <summary>
        /// �ֶ���ֵ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="flag">1 add 2 update 3 delete</param>
        protected virtual bool UpdateValue<T>(T entity, int flag = 1) where T : class
        {
            return entity.UpdateValue(flag);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public virtual ResultDto<T> FindResultByPage<T>(Expression<Func<T, bool>> exp = null, int pageindex = 1, int pagesize = 10) where T : class
        {
            var tuple = FindTupleByPage(exp, pageindex, pagesize);
            return new ResultDto<T>(tuple, pageindex, pagesize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public virtual Tuple<List<T>, long> FindTupleByPage<T>(Expression<Func<T, bool>> exp = null, int pageindex = 1, int pagesize = 10) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetTupleByPage(Read.Session, exp, pageindex, pagesize);
        }
        /// <summary>����������ѯʵ�������ݼ�������Ϣ</summary>
        /// <param name="where">����</param>
        ///<return>����ʵ�������ݼ�������Ϣ</return>

        public virtual long Count<T>(ICriteria where = null) where T : class
        {
            Read.Read = true&&UseTransaction;
            return Template.GetCount<T>(Read.Session, where);
        }

        /// <summary>����������ѯʵ�������ݼ���Ϣ</summary>
        /// <param name="where">����</param>
        ///<return>����ʵ�������ݼ���Ϣ</return>
        public virtual List<T> FindList<T>(ICriteria where = null) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetList<T>(Read.Session, where);
        }

        /// <summary>������������ҳ��ѯʵ�������ݼ���Ϣ</summary>
        /// <param name="where">����</param>
        /// <param name="page">ҳ��</param>
        /// <param name="size">ÿҳ��¼</param>
        ///<return>����ʵ�������ݼ���Ϣ</return>

        public virtual List<T> FindListByPage<T>(ICriteria where = null, int page = 1, int size = 10) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetListByPage<T>(Read.Session, where, page, size);
        }

        /// <summary>������������ҳ��ѯʵ�������ݼ���Ϣ</summary>
        /// <param name="where">����</param>
        /// <param name="page">ҳ��</param>
        /// <param name="size">ÿҳ��¼</param>
        ///<return>����ʵ�������ݼ���Ϣ</return>

        public virtual List<T> FindListByPageOrEntity<T>(T where, int page = 1, int size = 10) where T : class
        {
            Read.Read = true && UseTransaction;
            return FindListByPage<T>(null, page, size);
        }

        /// <summary>������������ҳ��ѯʵ�������ݼ���Ϣ��ʵ�������ݼ�������Ϣ</summary>
        /// <param name="where">����</param>
        /// <param name="page">ҳ��</param>
        /// <param name="size">ÿҳ��¼</param>
        ///<return>����ʵ�������ݼ���Ϣ��ʵ�������ݼ�������Ϣ</return>
        public virtual ResultDto<T> FindResultByPage<T>(ICriteria where = null, int page = 1, int size = 10) where T : class
        {
            Read.Read = true && UseTransaction;
            var res = FindTupleByPage<T>(where, page, size);
            return new ResultDto<T>(res.Item1, page, size, res.Item2);
        }


        /// <summary>������������ҳ��ѯʵ�������ݼ���Ϣ��ʵ�������ݼ�������Ϣ</summary>
        /// <param name="where">����</param>
        /// <param name="page">ҳ��</param>
        /// <param name="size">ÿҳ��¼</param>
        ///<return>����ʵ�������ݼ���Ϣ��ʵ�������ݼ�������Ϣ</return>
        public virtual Tuple<List<T>, long> FindTupleByPage<T>(ICriteria where = null, int page = 1, int size = 10) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetTupleByPage<T>(Read.Session, where, page, size);
        }

        #region async



        /// <summary>����������ѯʵ�������ݼ���Ϣ</summary>
        /// <param name="where">����</param>
        /// <param name="cancellationToken"></param>
        ///<return>����ʵ�������ݼ���Ϣ</return>

        public virtual Task<List<T>> FindListAsync<T>(ICriteria where = null,
            CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetListAsync<T>(Write.Session, where, cancellationToken);
        }

        /// <summary>������������ҳ��ѯʵ�������ݼ���Ϣ</summary>
        /// <param name="where">����</param>
        /// <param name="page">ҳ��</param>
        /// <param name="size">ÿҳ��¼</param>
        /// <param name="cancellationToken"></param>
        ///<return>����ʵ�������ݼ���Ϣ</return>
        public virtual Task<List<T>> FindListByPageAsync<T>(ICriteria where = null, int page = 1, int size = 10,
             CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetListByPageAsync<T>(Write.Session, where, page, size, cancellationToken);
        }
        /// <summary>������������ҳ��ѯʵ�������ݼ���Ϣ��ʵ�������ݼ�������Ϣ</summary>
        /// <param name="where">����</param>
        /// <param name="page">ҳ��</param>
        /// <param name="size">ÿҳ��¼</param>
        /// <param name="cancellationToken"></param>
        ///<return>����ʵ�������ݼ���Ϣ��ʵ�������ݼ�������Ϣ</return>
        public virtual Task<Tuple<List<T>, long>> FindTupleByPageAsync<T>(ICriteria where = null, int page = 1,
            int size = 10, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetTupleByPageAsync<T>(Write.Session, where, page, size, cancellationToken);
        }
        #endregion async


        /// <summary>���ҵ������Ҳ��������������� ef,nhibernate ֧�� linq dapper ��֧��linq </summary>
        /// <param name="where">����</param>
        /// <returns></returns>
        public virtual T FindSingle<T>(Expression<Func<T, bool>> where = null) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.Get(Read.Session, where);
        }

        /// <summary>���ҵ������Ҳ��������������� ef,nhibernate ֧�� linq dapper ��֧��linq </summary>
        /// <param name="where">����</param>
        /// <returns></returns>
        public virtual T FindSingleByEntity<T>(T where) where T : class
        {
            Read.Read = true && UseTransaction;
            throw new NotSupportedException();
        }

        /// <summary>���ҵ������Ҳ��������������� ef,nhibernate ֧�� linq dapper ��֧��linq </summary>
        /// <param name="id">����</param>
        /// <returns></returns>
        public virtual T FindSingle<T>(object id) where T : class
        {
            Read.Read = true && UseTransaction;
            return Read.Session.Get<T>(id);
        }
        /// <summary> �Ƿ���� Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq  </summary>
        /// <param name="where">����</param>
        /// <returns></returns>
        public virtual bool IsExist<T>(Expression<Func<T, bool>> where) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetCount<T>(Read.Session, where) >= 1;
        }

        /// <summary> �Ƿ���� Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq  </summary>
        /// <param name="where">����</param>
        /// <returns></returns>
        public virtual bool IsExistByEntity<T>(T where) where T : class
        {
            Read.Read = true && UseTransaction;
            throw new NotSupportedException();
        }

        /// <summary> �Ƿ���� Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq  </summary>
        /// <param name="id">����</param>
        /// <returns></returns>
        public virtual bool IsExist<T>(object id) where T : class
        {
            Read.Read = true && UseTransaction;
            return Read.Session.Get<T>(id) != null;
        }

        /// <summary> ��ѯ���� Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq dapperĬ�ϲ�ѯ���н���������ڴ� ������ѯ</summary>
        /// <param name="where">����</param>
        /// <returns></returns>
        public virtual IQueryable<T> Query<T>(Expression<Func<T, bool>> where = null) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetQuery(Read.Session, where);
        }

        /// <summary> ��ѯ���� Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq dapperĬ�ϲ�ѯ���н���������ڴ� ������ѯ</summary>
        /// <param name="where">����</param>
        /// <returns></returns>
        public virtual List<T> FindListByEntity<T>(T where) where T : class
        {
            Read.Read = true && UseTransaction;
            throw new NotSupportedException();
        }

        /// <summary> ��ѯ���� Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq orderby������Ч dapperĬ�ϲ�ѯ���н���������ڴ� ������ѯ </summary>
        /// <param name="where">����</param>
        /// <param name="page">ҳ��</param>
        /// <param name="size">��¼</param>
        /// <returns></returns>
        public virtual IQueryable<T> QueryByPage<T>(Expression<Func<T, bool>> where = null, int page = 1, int size = 10) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetByPage(Read.Session, where, page, size);
        }

        /// <summary> ��ѯ���� Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq orderby������Ч dapperĬ�ϲ�ѯ���н���������ڴ� ������ѯ </summary>
        /// <param name="where">����</param>
        /// <param name="page">ҳ��</param>
        /// <param name="size">��¼</param>
        /// <returns></returns>
        public virtual List<T> FindByListPage<T>(T where, int page = 1, int size = 10) where T : class
        {
            Read.Read = true && UseTransaction;
            return FindListByPage<T>(null, page, size);
        }

        /// <summary>��ѯ����  Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq  dapperĬ�ϲ�ѯ���н������  </summary>
        /// <param name="wehre">����</param>
        /// <returns></returns>
        public virtual long Count<T>(Expression<Func<T, bool>> wehre = null) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetCount<T>(Read.Session, wehre);
        }

        /// <summary>��ѯ����  Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq  dapperĬ�ϲ�ѯ���н������  </summary>
        /// <param name="wehre">����</param>
        /// <returns></returns>
        public virtual long CountByEntity<T>(T wehre) where T : class
        {
            Read.Read = true && UseTransaction;
            return Count<T>((ICriteria)null);
        }

        /// <summary> ��� </summary>
        /// <param name="entity">ʵ��</param>
        public virtual int Insert<T>(T entity) where T : class
        {
            Write.Write = true && UseTransaction;
            UpdateValue(entity);
            Template.Add<T>(Write.Session, entity);
            return 1;
        }

        /// <summary>���� ��� </summary>
        /// <param name="entities">ʵ��</param>
        public virtual int BatchInsert<T>(T[] entities) where T : class
        {
            Write.Write = true && UseTransaction;
            foreach (var entity in entities)
            {
                UpdateValue(entity);
            }
            return Template.BatchAdd<T>(Write.Session, entities);
        }

        /// <summary> ����һ��ʵ����������� </summary>
        /// <param name="entity">ʵ��</param>
        public virtual int Update<T>(T entity) where T : class
        {
            Write.Write = true && UseTransaction;
            return Update(entity, 2);
        }

        /// <summary> ����һ��ʵ����������� </summary>
        /// <param name="entity">ʵ��</param>
        public virtual int Update<T>(T entity, int flag) where T : class
        {
            Write.Write = true && UseTransaction;
            UpdateValue(entity, flag);
            Template.Modify<T>(Write.Session, entity);
            return 1;
        }

        /// <summary> ɾ��</summary>
        /// <param name="entity">ʵ��</param>
        public virtual int Delete<T>(T entity) where T : class
        {
            Write.Write = true && UseTransaction;
            if (entity is IHasDeletionTime)
            {
                Update(entity, 3);
            }
            else
            {
                Template.Remove<T>(Write.Session, entity);
            }
            return 1;
        }

        /// <summary> ����ɾ�� Ĭ��ʵ��  nhibernate ֧�� EF dapper δʵ��</summary>
        public virtual int Delete<T>(object id) where T : class
        {
            Write.Write = true && UseTransaction;
            if (typeof(T).IsAssignableFrom(typeof(IHasDeletionTime)))
            {
                if (string.IsNullOrEmpty(id?.ToString())) throw new ArgumentNullException("id");
                var obj = Write.Session.Get<T>(id);
                if (obj is IHasDeletionTime deletionTime)
                {
                    if (!deletionTime.IsDeleted)
                    {
                        UpdateValue(obj, 3);
                        Write.Session.Update(obj);
                    }
                }
            }
            else
            {
                Template.Remove<T>(Write.Session, id);
            }
            return 1;
        }

        /// <summary> ɾ��</summary>
        /// <param name="ids">ʵ�� id</param>
        public virtual int DeleteList<Entity, Key>(Key[] ids) where Entity : class
        {
            Write.Write = true && UseTransaction;
            int update = 0;
            if (typeof(Entity).IsAssignableFrom(typeof(IHasDeletionTime)))
            {
                if (ids == null && ids.Length == 0) throw new ArgumentNullException("ids");
                foreach (var id in ids)
                {
                    var obj = Write.Session.Get<Entity>(id);
                    if (obj is IHasDeletionTime deletionTime)
                    {
                        if (!deletionTime.IsDeleted)
                        {
                            UpdateValue(obj, 3);
                            Write.Session.Update(obj);
                            update++;
                        }
                    }
                }
            }
            else
            {
                foreach (var id in ids)
                {
                    var obj = Write.Session.Get<Entity>(id);
                    Template.Remove<Entity>(Write.Session, id);
                    update++;
                }
            }
            return update;
        }
        /// <summary>
        /// ʵ�ְ���Ҫֻ���²��ָ��� Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq dapper δʵ��
        /// <para>�磺Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">��������</param>
        /// <param name="update">���º��ʵ��</param>
        public virtual int Update<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> update) where T : class
        {
            Write.Write = true && UseTransaction;
            return Template.Modify<T>(Write.Session, where, update);
        }

        /// <summary> ����ɾ�� Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq dapper δʵ��</summary>
        /// <param name="where">����</param>

        public virtual int Delete<T>(Expression<Func<T, bool>> where = null) where T : class
        {
            Write.Write = true && UseTransaction;
            return Template.Remove<T>(Write.Session, where);
        }


        /// <summary> ִ��sql </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual int ExecuteSql(string sql)
        {
            Write.Write = true && UseTransaction;
            return Template.GetExecuteQuery(Write.Session, sql);
        }
        /// <summary> ִ��sql </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual Task<int> ExecuteSqlAsync(string sql)
        {
            Write.Write = true && UseTransaction;
            return Template.GetExecuteQueryAsync(Write.Session, sql);
        }
        /// <summary> �����ɹ� ���浽���� Ĭ��ʵ�� ef ֧��  dapper nhibernate ���κβ��� </summary>
        public virtual void Save()
        {
            Write?.Transaction?.Commit();
        }

        /// <summary>
        /// ��̬ ʱ�� ��Ч
        /// </summary>
        public void Close()
        {
            if (read == write)
            {
                write?.Dispose();
            }
            else
            {
                write?.Dispose();
                read?.Dispose();
            }
            write = null;
            read = null;
        }

        #region async
        /// <summary>���ҵ������Ҳ��������������� ef,nhibernate ֧�� linq dapper ��֧��linq </summary>
        /// <param name="where">����</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<T> FindSingleAsync<T>(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetAsync(Read.Session, where, cancellationToken);
        }

        /// <summary>���ҵ������Ҳ��������������� ef,nhibernate ֧�� linq dapper ��֧��linq </summary>
        /// <param name="where">����</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<T> FindSingleByEntityAsync<T>(T where, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Task.FromResult(FindSingleByEntity(where));
        }

        /// <summary>���ҵ������Ҳ��������������� ef,nhibernate ֧�� linq dapper ��֧��linq </summary>
        /// <param name="id">����</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<T> FindSingleAsync<T>(object id, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Task.FromResult(FindSingle<T>(id));
        }

        /// <summary> �Ƿ���� Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq  </summary>
        /// <param name="where">����</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> IsExistAsync<T>(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Task.FromResult(Template.GetCountAsync<T>(Read.Session, where).Result >= 1);
        }

        /// <summary> �Ƿ���� Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq  </summary>
        /// <param name="where">����</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> IsExistByEntityAsync<T>(T where, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Task.FromResult(IsExistByEntity(where));
        }

        /// <summary> �Ƿ���� Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq  </summary>
        /// <param name="id">����</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> IsExistAsync<T>(object id, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Task.FromResult(IsExist<T>(id));
        }

        /// <summary> ��ѯ���� Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq dapperĬ�ϲ�ѯ���н���������ڴ� ������ѯ</summary>
        /// <param name="where">����</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<IQueryable<T>> QueryAsync<T>(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default
            ) where T : class
        {
            Read.Read = true && UseTransaction;
            return Task.FromResult(Query(where));
        }

        /// <summary> ��ѯ���� Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq dapperĬ�ϲ�ѯ���н���������ڴ� ������ѯ</summary>
        /// <param name="where">����</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<List<T>> FindListByEntityAsync<T>(T where, CancellationToken cancellationToken = default
            ) where T : class
        {
            Read.Read = true && UseTransaction;
            return Task.FromResult(FindListByEntity(where));
        }

        /// <summary> ��ѯ���� Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq orderby������Ч dapperĬ�ϲ�ѯ���н���������ڴ� ������ѯ </summary>
        /// <param name="page">ҳ��</param>
        /// <param name="size">��¼</param>
        /// <param name="where">����</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<IQueryable<T>> QueryByPageAsync<T>(Expression<Func<T, bool>> where = null, int page = 1, int size = 10, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Task.FromResult(Template.GetByPage(Read.Session, where, page, size));
        }

        /// <summary> ��ѯ���� Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq orderby������Ч dapperĬ�ϲ�ѯ���н���������ڴ� ������ѯ </summary>
        /// <param name="page">ҳ��</param>
        /// <param name="size">��¼</param>
        /// <param name="where">����</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<List<T>> FindListByPageOrEntityAsync<T>(T where, int page = 1, int size = 10, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Task.FromResult(FindListByPageOrEntity(where, page, size));
        }

        /// <summary>��ѯ����  Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq  dapperĬ�ϲ�ѯ���н������  </summary>
        /// <param name="where">����</param>
        /// <returns></returns>
        /// <param name="cancellationToken"></param>
        public virtual Task<long> CountAsync<T>(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetCountAsync<T>(Read.Session, where, cancellationToken);
        }


        /// <summary>��ѯ����  Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq  dapperĬ�ϲ�ѯ���н������  </summary>
        /// <param name="where">����</param>
        /// <returns></returns>
        /// <param name="cancellationToken"></param>
        public virtual Task<long> CountAsync<T>(T where, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Task.FromResult(CountByEntity(where));
        }

        /// <summary>��ѯ����  Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq  dapperĬ�ϲ�ѯ���н������  </summary>
        /// <param name="where">����</param>
        /// <returns></returns>
        /// <param name="cancellationToken"></param>
        public virtual Task<long> CountAsync<T>(ICriteria where = null, CancellationToken cancellationToken = default) where T : class
        {
            Read.Read = true && UseTransaction;
            return Template.GetCountAsync<T>(Read.Session, where, cancellationToken);
        }
        /// <summary> ��� </summary>
        /// <param name="entity">ʵ��</param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> InsertAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
            Template.AddAsync<T>(Write.Session, entity, cancellationToken);
            return Task.FromResult(1);
        }

        /// <summary>���� ��� </summary>
        /// <param name="entities">ʵ��</param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> BatchInsertAsync<T>(T[] entities, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
            return Template.BatchAddAsync<T>(Write.Session, entities, cancellationToken);
        }

        /// <summary> ����һ��ʵ����������� </summary>
        /// <param name="entity">ʵ��</param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> UpdateAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
            return UpdateAsync(entity, 2, cancellationToken);
        }
        /// <summary> ����һ��ʵ����������� </summary>
        /// <param name="entity">ʵ��</param>
        /// <param name="flag"></param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> UpdateAsync<T>(T entity, int flag = 2, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
            UpdateValue(entity, flag);
            return Template.ModifyAsync<T>(Write.Session, entity, cancellationToken);
        }
        /// <summary> ɾ��</summary>
        /// <param name="entity">ʵ��</param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> DeleteAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
            if (entity is IHasDeletionTime)
            {
                return UpdateAsync(entity, 3, cancellationToken);
            }
            return Template.RemoveAsync<T>(Write.Session, entity, cancellationToken);
        }

        /// <summary> ����ɾ�� Ĭ��ʵ��  nhibernate ֧�� EF dapper δʵ��</summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> DeleteAsync<T>(object id, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
            if (typeof(T).IsAssignableFrom(typeof(IHasDeletionTime)))
            {
                if (string.IsNullOrEmpty(id?.ToString())) throw new ArgumentNullException("id");
#if NET40 || NET45 || NET451 || NET452 || NET46
                            var obj = Session.Get<T>(id);
#else
                var obj = Write.Session.GetAsync<T>(id, cancellationToken).Result;
#endif
                if (obj is IHasDeletionTime deletionTime)
                {
                    if (!deletionTime.IsDeleted)
                    {
                        UpdateValue(obj, 3);
#if NET40 || NET45 || NET451 || NET452 || NET46
                                    Session.Update(obj);
#else
                        Write.Session.UpdateAsync(obj).GetAwaiter().GetResult();
                    }
#endif
                }

            }
            else
            {
                Template.RemoveAsync<T>(Write.Session, id, cancellationToken);
            }
            return Task.FromResult(1);
        }
        /// <summary> ��ѯ����  Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq  </summary>
        /// <param name="ids">����</param>
        /// <param name="cancellationToken">dapper ef ��Ч</param>
        /// <returns></returns>
        public virtual async Task<int> DeleteListAsync<Entity, Key>(Key[] ids, CancellationToken cancellationToken = default) where Entity : class
        {
            Write.Write = true && UseTransaction;
            int update = 0;
            if (typeof(Entity).IsAssignableFrom(typeof(IHasDeletionTime)))
            {
                if (ids == null && ids.Length == 0) throw new ArgumentNullException("ids");

                foreach (var id in ids)
                {
                    var obj = await Write.Session.GetAsync<Entity>(id, cancellationToken);
                    if (obj is IHasDeletionTime deletionTime)
                    {
                        if (!deletionTime.IsDeleted)
                        {
                            obj.UpdateValue(3);
                            UpdateValue(obj, 3);
                            await NhibernateTemplate.Empty.ModifyAsync(Write.Session, obj, cancellationToken);
                            update++;
                        }
                    }
                }
            }
            else
            {
                foreach (var id in ids)
                {
                    var obj = await Write.Session.GetAsync<Entity>(id, cancellationToken);
                    await Template.RemoveAsync<Entity>(Write.Session, id, cancellationToken);
                    update++;
                }
            }
            return update;
        }
        /// <summary>
        /// ʵ�ְ���Ҫֻ���²��ָ��� Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq dapper δʵ��
        /// <para>�磺Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">��������</param>
        /// <param name="update">���º��ʵ��</param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> UpdateAsync<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> update, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
            return Template.ModifyAsync<T>(Write.Session, where, update, cancellationToken);
        }

        /// <summary> ����ɾ�� Ĭ��ʵ�� ef,nhibernate ֧�� linq dapper ��֧��linq dapper δʵ��</summary>
        /// <param name="where">����</param>
        /// <param name="cancellationToken"></param>
        public virtual Task<int> DeleteAsync<T>(Expression<Func<T, bool>> where = null, CancellationToken cancellationToken = default) where T : class
        {
            Write.Write = true && UseTransaction;
            return Template.RemoveAsync<T>(Write.Session, where, cancellationToken);
        }


        /// <summary> ִ��sql </summary>
        /// <param name="sql"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> ExecuteSqlAsync(string sql, CancellationToken cancellationToken = default)
        {
            Write.Write = true && UseTransaction;
            return Template.GetExecuteQueryAsync(Write.Session, sql, cancellationToken);
        }


        /// <summary> �����ɹ� ���浽���� Ĭ��ʵ�� ef ֧��  dapper nhibernate ���κβ��� </summary>
        /// <param name="cancellationToken"></param>
        public virtual Task SaveAsync(CancellationToken cancellationToken = default)
        {
            return Write?.Transaction?.CommitAsync();
        }
        #endregion async
        /// <summary>
        /// 
        /// </summary>
        void IDisposable.Dispose()
        {
            //SessionFactory.Dispose();
            if (this.Single)
            {
                Session?.Dispose();
                Session.Transaction?.Dispose();
            }
            else
            {
                write?.Dispose();
                write?.Transaction?.Dispose();
                read?.Dispose();
                read?.Transaction?.Dispose();
            }
        }


    }
}
#endif