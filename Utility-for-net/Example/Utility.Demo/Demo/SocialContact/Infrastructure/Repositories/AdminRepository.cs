#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using NHibernate;
using NHibernate.Criterion;
using SocialContact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Utility.Attributes;
using Utility.Demo.Domain.Entities;
using Utility.Helpers;
using Utility.Logs;
using Utility.Nhibernate;
using Utility.Nhibernate.Repositories;

namespace SocialContact.Infrastructure.Repositories
{
    [Transtation]
    public  class AdminRepository: BaseNhibernateRepository<AdminEntity, long>
    {
        protected ILog<AdminRepository> Log;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public AdminRepository(SessionProvider session, ILog<AdminRepository> log) : base(session)
        {
            this.Log = log;
        }

        /// <summary>
        ///  读写分离
        /// </summary>
        /// <param name="writeSession"> 写库</param>
        /// <param name="readSession">读库</param>
        //public AdminRepository(Lazy<SessionProvider> writeSession, Lazy<SessionProvider> readSession) : base(writeSession, readSession)
        //{

        //}
        int Init(AdminEntity entity)
        {
            if (entity.RoleId > 0)
            {
                if (!UnitWork.IsExist<CatagoryEntity>(it => it.Id == entity.RoleId && it.Flag == CatalogFlag.Role))
                {
                    Log.Log(LogLevel.Warn, "role not exists,role id -->" + entity.RoleId);
                    return -2;
                }
            }
            else
            {
                entity.RoleId = 0;
            }
            return 0;
        }
        async Task<int> InitAsync(AdminEntity entity)
        {
            if (entity.RoleId > 0)
            {
                if (! await UnitWork.IsExistAsync<CatagoryEntity>(it => it.Id == entity.RoleId && it.Flag == CatalogFlag.Role))
                {
                    Log.Log(LogLevel.Warn, "role not exists,role id -->" + entity.RoleId);
                    return -2;
                }
            }
            else
            {
                entity.RoleId = 0;
            }
            return 0;
        }
        public override int Insert(AdminEntity entity)
        {
            int res = Init(entity);
            if (res != 0)
            {
                return res;
            }
            return base.Insert(entity);
        }
        public override int Update(AdminEntity entity)
        {
            int res = Init(entity);
            if (res != 0)
            {
                return res;
            }
            return base.Update(entity);
        }
        public override async Task<int> InsertAsync(AdminEntity entity, CancellationToken cancellationToken = default)
        {
            int res =await  InitAsync(entity);
            if (res != 0)
            {
                return res;
            }
            return await base.InsertAsync(entity, cancellationToken);
        }
        public override async Task<int> UpdateAsync(AdminEntity entity, CancellationToken cancellationToken = default)
        {
            int res = await InitAsync(entity);
            if (res != 0)
            {
                return res;
            }
            return await base.UpdateAsync(entity, cancellationToken);
        }

        protected override void QueryFilterByOr(List<AbstractCriterion> criterias, AdminEntity obj)
        {
            if (!string.IsNullOrEmpty(obj.Account))
            {
                criterias.Add(Expression.Like("Account", $"%{obj.Account}%"));
            }
            if (!string.IsNullOrEmpty(obj.NickName))
            {
                criterias.Add(Expression.Like("NickName", $"%{obj.NickName}%"));
            }
            //if (obj.BirthdayDate != null && obj.BirthdayDate.Length == 2)
            //{
            //    criterias.Add(Expression.Between("Birthday", obj.BirthdayDate[0], obj.BirthdayDate[1]));
            //}
            if (obj.RoleId >0)
            {
                // criterias.Add(Expression.Eq("Role.Id", obj.RoleId));
                criterias.Add(Expression.Eq("RoleId", obj.RoleId));
            }
            if (!string.IsNullOrEmpty(obj.RealName))
            {
                criterias.Add(Expression.Like("RealName", $"%{obj.RealName}%"));
            }
            if (!string.IsNullOrEmpty(obj.Phone))
            {
                criterias.Add(Expression.Like("Phone", $"%{obj.Phone}%"));
            }
            if (!string.IsNullOrEmpty(obj.Email))
            {
                criterias.Add(Expression.Like("Email", $"%{obj.Email}%"));
            }
            if (obj.LoginDates != null && obj.LoginDates.Length == 2)
            {
                criterias.Add(Expression.Between("LoginDate", CommonHelper.TotalMilliseconds(obj.LoginDates[0]),
                    CommonHelper.TotalMilliseconds(obj.LoginDates[1])
                    ));
            };
        }

    }
}
#endif