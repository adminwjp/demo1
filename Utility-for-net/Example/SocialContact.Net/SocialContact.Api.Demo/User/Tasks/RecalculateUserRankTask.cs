//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Tunynet.Common.Configuration;
using Tunynet.Settings;
using Tunynet.Tasks;

namespace Tunynet.Common
{
    /// <summary>
    /// 重新计算用户等级
    /// </summary>
    public class RecalculateUserRankTask : ITask
    {
        /// <summary>
        /// 任务执行的内容
        /// </summary>
        /// <param name="taskDetail">任务配置状态信息</param>
        public void Execute(TaskDetail taskDetail)
        {
            DIContainer.Resolve<UserRankService>().ResetAllUser();
        }
    }
}