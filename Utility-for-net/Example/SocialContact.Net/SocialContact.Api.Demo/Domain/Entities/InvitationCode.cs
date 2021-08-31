//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;

namespace Tunynet.Common
{
    /// <summary>
    /// 邀请码
    /// </summary>
    [Serializable]
    public class InvitationCode 
    {

        #region 需持久化属性

        /// <summary>
        ///(使用MD5_16生成)
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///是否可以多次使用
        /// </summary>
        public bool IsMultiple { get; set; }

        /// <summary>
        ///过期日期
        /// </summary>
        public long ExpiredDate { get; set; }

        /// <summary>
        ///创建日期
        /// </summary>
        public DateTime DateCreated { get; set; }

        #endregion 需持久化属性

       
    }
}