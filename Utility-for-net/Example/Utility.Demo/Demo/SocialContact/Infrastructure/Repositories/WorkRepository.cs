using SocialContact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utility.Attributes;
using Utility.Logs;
using Utility.Nhibernate;
using Utility.Nhibernate.Repositories;

namespace SocialContact.Infrastructure.Repositories
{
    [Transtation]
    public class WorkRepository : BaseNhibernateRepository<WorkEntity, long>
    {
        protected ILog<WorkRepository> Log;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public WorkRepository(SessionProvider session, ILog<WorkRepository> log) : base(session)
        {
            this.Log = log;
        }

        int Init(WorkEntity entity)
        {
            if (entity.CatagoryId > 0)
            {
                if (!UnitWork.IsExist<CatagoryEntity>(it => it.Id == entity.CatagoryId && it.Flag == CatalogFlag.Job))
                {
                    Log.Log(LogLevel.Warn, "Job  Catagory  not exists,Job Catagory id -->" + entity.CatagoryId);
                    return -2;
                }
            }
            else
            {
                entity.CatagoryId = 0;
            }
            if (entity.UserId > 0)
            {
                if (!UnitWork.IsExist<UserEntity>(it => it.Id == entity.UserId ))
                {
                    Log.Log(LogLevel.Warn, "User  not exists,UserId -->" + entity.UserId);
                    return -3;
                }
            }
            else
            {
                entity.UserId = 0;
            }
            return 0;
        }
        async Task<int> InitAsync(WorkEntity entity)
        {
            if (entity.CatagoryId > 0)
            {
                if (! await UnitWork.IsExistAsync<CatagoryEntity>(it => it.Id == entity.CatagoryId && it.Flag == CatalogFlag.Job))
                {
                    Log.Log(LogLevel.Warn, "Job  Catagory  not exists,Job Catagory id -->" + entity.CatagoryId);
                    return -2;
                }
            }
            else
            {
                entity.CatagoryId = 0;
            }
            if (entity.UserId > 0)
            {
                if (!await UnitWork.IsExistAsync<UserEntity>(it => it.Id == entity.UserId))
                {
                    Log.Log(LogLevel.Warn, "User  not exists,UserId -->" + entity.UserId);
                    return -3;
                }
            }
            else
            {
                entity.UserId = 0;
            }
            return 0;
        }
        public override int Insert(WorkEntity entity)
        {
            int res = Init(entity);
            if (res != 0)
            {
                return res;
            }
            return base.Insert(entity);
        }
        public override int Update(WorkEntity entity)
        {
            int res = Init(entity);
            if (res != 0)
            {
                return res;
            }
            return base.Update(entity);
        }
        public override async Task<int> InsertAsync(WorkEntity entity, CancellationToken cancellationToken = default)
        {
            int res = await InitAsync(entity);
            if (res != 0)
            {
                return res;
            }
            return await base.InsertAsync(entity, cancellationToken);
        }
        public override async Task<int> UpdateAsync(WorkEntity entity, CancellationToken cancellationToken = default)
        {
            int res = await InitAsync(entity);
            if (res != 0)
            {
                return res;
            }
            return await base.UpdateAsync(entity, cancellationToken);
        }
    }
}
