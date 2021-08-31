//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace Tunynet.Common
{
    /// <summary>
    /// 通知设置类
    /// </summary>
    [Serializable]
    public class NoticeTypeSettings 
    {

        /// <summary>
        ///Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///通知类型 Key
        /// </summary>
        public string NoticeTypeKey { get; set; }

        /// <summary>
        ///第几次通知
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// 距离上次通知的时间间隔(秒)
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// 发送方式（0=站内，1=Email，2=手机短信）
        /// </summary>
        public int SendMode { get; set; }


    }
}