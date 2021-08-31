using System;
using System.Collections.Generic;
using System.Text;
using Utility.Domain.Entities;

namespace Utility.Demo.Domain.Entities
{
    /// <summary>好友信息 </summary>
    public class UserFriendEntity: UserFriendEntity<long>
    {

    }
    /// <summary>好友信息 </summary>
    public class UserFriendEntity<Key> : Entity<Key>
    {
        private Key userId;
        private Key friendId;
        private bool agree;
        private int deleteFlag;

        /// <summary>用户 id </summary>
        public virtual Key UserId { get => userId; set { Set(ref userId, value, "UserId"); } }
        /// <summary>添加好友id </summary>
        public virtual Key FriendId { get => friendId; set { Set(ref friendId, value, "FriendId"); } }
        /// <summary>对方是否同意 </summary>
        public virtual bool Agree { get => agree; set { Set(ref agree, value, "Agree"); } }
        /// <summary>0 未 删除 1 删除好友 2 对方 删除好友 </summary>
        public virtual int DeleteFlag { get => deleteFlag; set { Set(ref deleteFlag, value, "DeleteFlag"); } }
    }
}
