//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace Tunynet.Common
{
    /// <summary>通知的实体类 </summary>
    [Serializable]
    public class Notice 
    {
        #region 需持久化属性

        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 通知类型 Key
        /// </summary>
        public string NoticeTypeKey { get; set; }

        /// <summary>
        /// 通知接收人
        /// </summary>
        public long ReceiverId { get; set; }

        /// <summary>
        /// 主角 UserId
        /// </summary>
        public long LeadingActorUserId { get; set; }

        /// <summary>
        /// 主角
        /// </summary>
        public string LeadingActor { get; set; }

        /// <summary>
        /// 相关项对象名称
        /// </summary>
        public string RelativeObjectName { get; set; }

        /// <summary>
        /// 相关项对象 Id
        /// </summary>
        public long RelativeObjectId { get; set; }

        /// <summary>
        /// 相关项对象链接地址
        /// </summary>
        public string RelativeObjectUrl { get; set; }

        /// <summary>
        /// 触发通知的对象Id
        /// </summary>
        public long ObjectId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 处理状态   0=Unhandled:未处理;1=Readed  知道了;  2=Accepted 接受；3=Refused 拒绝；
        /// </summary>
        public NoticeStatus Status { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 上次发送时间
        /// </summary>
        public DateTime LastSendDate { get; set; }

        /// <summary>
        /// 通知发送次数
        /// </summary>
        public int Times { get; set; }

        #endregion 需持久化属性




    }
}