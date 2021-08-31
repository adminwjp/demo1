//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace Tunynet.Common
{
    /// <summary>
    /// 邀请好友的记录实体
    /// </summary>
    [Serializable]
    public class InviteFriendRecord 
    {

        #region 需持久化属性

        /// <summary>
        ///Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///邀请人
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///受邀人
        /// </summary>
        public long InvitedUserId { get; set; }

        /// <summary>
        ///邀请码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 邀请用户是否得到了奖励
        /// </summary>
        public bool IsRewarded { get; set; }

        /// <summary>
        ///邀请日期
        /// </summary>
        public DateTime InviteDate { get; set; }

        #endregion 需持久化属性


    }
}