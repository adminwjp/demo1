//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using PetaPoco;
using System;
using Tunynet.Caching;

namespace Tunynet.Common
{  /// <summary>
   /// 私信的会话
   /// </summary>
    [TableName("tn_MessageSessions")]
    [PrimaryKey("SessionId", autoIncrement = true)]
    [CacheSetting(true)]
    [Serializable]
    public class MessageSession { 

        /// <summary>
        ///SessionId
        /// </summary>
        public long SessionId { get; set; }

        /// <summary>
        ///会话拥有者UserId
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///会话参与人UserId
        /// </summary>
        public long OtherUserId { get; set; }

        /// <summary>
        ///会话中最新的私信MessageId
        /// </summary>
        public long LastMessageId { get; set; }

        /// <summary>
        ///信息数统计
        /// </summary>
        public int MessageCount { get; set; }

        /// <summary>
        ///未读信息数统计（用来显示未读私信统计数和和标示会话的阅读状态）
        /// </summary>
        public int UnreadMessageCount { get; set; }

        /// <summary>
        ///消息类型
        /// </summary>
        public int MessageType { get; set; }

        /// <summary>
        ///最后回复日期
        /// </summary>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// 作为匿名用户
        /// </summary>
        public bool AsAnonymous { get; set; }

        /// <summary>
        ///附表ID
        /// </summary>
        public long SenderSessionId { get; set; }

 
    }
}