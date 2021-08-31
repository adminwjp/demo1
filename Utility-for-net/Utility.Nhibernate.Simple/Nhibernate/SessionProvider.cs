using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Nhibernate
{
    public class SessionProvider : IDisposable
    {
        private ISession session;
        private IStatelessSession statelessSession;
        private NhibernateTransactionManager transaction;
        private bool read;
        private bool write;

        internal virtual bool Read { get => read; set { read = value; Transaction.Read = value; Transaction.Used = true; } }
        internal virtual bool Write { get => write; set { write = value; Transaction.Write = value; Transaction.Used = true; } }
        public SessionProvider()
        {

        }
        public SessionProvider(ISessionFactory sessionFactory)
        {
            this.SessionFactory = sessionFactory;
             //SetTran();//使用时则启动事务
        }
        public SessionProvider(ISession session)
        {
            this.session = session;
            //SetTran();//使用时则启动事务
        }

        public SessionProvider(IStatelessSession statelessSession)
        {
            this.statelessSession = statelessSession;
            // SetTran();//使用时则启动事务
        }

        public virtual NhibernateTransactionManager Transaction
        {
            get
            {
                SetTran();
                return transaction;
            }
            set => transaction = value;
        }
        public virtual ISession Session
        {
            get
            {
                if (session == null && SessionFactory != null)
                {
                    session = SessionFactory.OpenSession();
                }
                return session;
            }
            set => session = value;
        }
        public virtual NHibernate.IStatelessSession StatelessSession
        {
            get
            {
                if (statelessSession == null && SessionFactory != null)
                {
                    statelessSession = SessionFactory.OpenStatelessSession();
                }
                return statelessSession;
            }
            set => statelessSession = value;
        }
        protected virtual void SetTran()
        {
            if (transaction == null)
            {
                transaction = new NhibernateTransactionManager(Session);
            }
        }

        public virtual ISessionFactory SessionFactory { get; set; }

        public void Dispose()
        {
            session?.Dispose();
        }
    }
}
