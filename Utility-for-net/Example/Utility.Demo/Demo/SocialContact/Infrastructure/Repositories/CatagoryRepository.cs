#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using NHibernate;
using SocialContact.Domain.Entities;
using System;
using Utility.Attributes;
using Utility.Nhibernate;
using Utility.Nhibernate.Repositories;

namespace SocialContact.Infrastructure.Repositories
{
    [Transtation]
    public class CatagoryRepository : BaseNhibernateRepository<CatagoryEntity, long>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public CatagoryRepository(SessionProvider session) : base(session)
        {

        }

        /// <summary>
        ///  读写分离
        /// </summary>
        /// <param name="writeSession"> 写库</param>
        /// <param name="readSession">读库</param>
        //public CatagoryRepository(Lazy<SessionProvider> writeSession, Lazy<SessionProvider> readSession) : base(writeSession, readSession)
        //{

        //}
        protected virtual void Before(CatagoryEntity entity)
        {
        }
    }
}
#endif