
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utility.Application.Services;
using Utility.Attributes;
using Utility.Demo.Domain.Entities;
using Utility.Domain.Repositories;

namespace Utility.Demo.Application.Services
{
    public interface IUserFriendAppService: ICrudAppService<UserFriendEntity, long>
    {
        int Agree(UserFriendEntity entity);
    }
    [Transtation]
    public class UserFriendAppService : CrudAppService<UserFriendEntity, long>, IUserFriendAppService

    {
        public UserFriendAppService(IRepository<UserFriendEntity, long> repository) : base(repository)
        {
        }
        /// <summary></summary>
        /// <param name="entity"></param>
        /// <returns>-2 exists</returns>
        public override int Insert(UserFriendEntity entity)
        {
            int res = Validate(entity);
            if (res != 0)
            {
                return res;
            }
            return base.Insert(entity);
        }

        int Validate(UserFriendEntity entity)
        {
            if (!UnitWork.IsExist<UserEntity>(entity.UserId)
                           || !UnitWork.IsExist<UserEntity>(entity.FriendId))
            {
                return -1;
            }
            var old = Repository.FindSingle(it => it.FriendId == entity.FriendId && it.UserId == entity.UserId);
            if (old != null)
            {
                return -2;
            }
            return 0;
        }
        public virtual int Agree(UserFriendEntity entity)
        {
            //未同意
            if (!UnitWork.IsExist<UserEntity>(entity.UserId)
                         || !UnitWork.IsExist<UserEntity>(entity.FriendId))
            {
                return -1;
            }
            var f = Repository.FindSingle(it => it.UserId == entity.FriendId && it.FriendId == entity.UserId);
            if (f == null)
            {
                return -1;
            }
            //好友 是否同意
            if (!f.Agree)
            {
                Repository.Update(it => it.Id == f.Id, it => new UserFriendEntity() { Agree = true }); 
            }
            //用户情况 存在 则修改 不存在 添加
            var old = Repository.FindSingle(it => it.UserId == entity.UserId && it.FriendId == entity.FriendId);
            if (old != null)
            {
                if (!old.Agree)
                {
                    old.Agree = true;
                    return Repository.Insert(old);
                }
                return 1;
            }
            entity.Agree = true;
            return Repository.Insert(entity);
        }
        public override int Update(UserFriendEntity entity)
        {
            int res = Validate(entity);
            if (res != 0)
            {
                return res;
            }
            return base.Update(entity);
        }
        public override int Delete(long id)
        {
            var old = Repository.FindSingle(id);
            if (old == null)
            {
                return -1;
            }
            //对方 未 同意 直接 删除 
            if (!old.Agree)
            {
                return base.Delete(id);
            }
            //对方 是否删除
            var f = Repository.FindSingle(it=>it.UserId==old.FriendId&&it.FriendId==old.UserId);
            if (f != null)
            {
                if (f.DeleteFlag == 1)
                {
                    return base.Delete(id);
                }
            }

            if (old.DeleteFlag ==1)
            {
                return 1;
            }
            return base.Update(it => it.Id == id, it => new UserFriendEntity()
            {
                DeleteFlag = 1
            });
        }
    }
}
