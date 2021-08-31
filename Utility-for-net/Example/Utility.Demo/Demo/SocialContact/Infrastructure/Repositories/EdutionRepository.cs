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
    public class EdutionRepository : BaseNhibernateRepository<EdutionEntity, long>
    {
        protected ILog<EdutionRepository> Log;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public EdutionRepository(SessionProvider session, ILog<EdutionRepository> log) : base(session)
        {
            this.Log = log;
        }

        int Init(EdutionEntity entity)
        {
            if (entity.CatagoryId > 0)
            {
                if (!UnitWork.IsExist<CatagoryEntity>(it => it.Id == entity.CatagoryId && it.Flag == CatalogFlag.Edution))
                {
                    Log.Log(LogLevel.Warn, "Edution  Catagory  not exists,Job Catagory id -->" + entity.CatagoryId);
                    return -2;
                }
            }
            else
            {
                entity.CatagoryId = 0;
            }
            if (entity.UserId > 0)
            {
                if (!UnitWork.IsExist<UserEntity>(it => it.Id == entity.UserId))
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
        async Task<int> InitAsync(EdutionEntity entity)
        {
            if (entity.CatagoryId > 0)
            {
                if (!await UnitWork.IsExistAsync<CatagoryEntity>(it => it.Id == entity.CatagoryId && it.Flag == CatalogFlag.Edution))
                {
                    Log.Log(LogLevel.Warn, "Edution  Catagory  not exists,Job Catagory id -->" + entity.CatagoryId);
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
        public override int Insert(EdutionEntity entity)
        {
            int res = Init(entity);
            if (res != 0)
            {
                return res;
            }
            return base.Insert(entity);
        }
        public override int Update(EdutionEntity entity)
        {
            int res = Init(entity);
            if (res != 0)
            {
                return res;
            }
            return base.Update(entity);
        }
        public override async Task<int> InsertAsync(EdutionEntity entity, CancellationToken cancellationToken = default)
        {
            int res = await InitAsync(entity);
            if (res != 0)
            {
                return res;
            }
            return await base.InsertAsync(entity, cancellationToken);
        }
        public override async Task<int> UpdateAsync(EdutionEntity entity, CancellationToken cancellationToken = default)
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
