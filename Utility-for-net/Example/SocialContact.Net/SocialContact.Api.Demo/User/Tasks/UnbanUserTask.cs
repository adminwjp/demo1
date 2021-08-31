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
    /// 执行解封用户任务
    /// </summary>
    public class UnbanUserTask : ITask
    {
        /// <summary>
        /// 任务执行的内容
        /// </summary>
        /// <param name="taskDetail">任务配置状态信息</param>
        public void Execute(TaskDetail taskDetail)
        {
            RoleService roleService = DIContainer.Resolve<RoleService>();
            UserService userService = DIContainer.Resolve<UserService>();
            UserQuery userQuery = new UserQuery();
            userQuery.IsBanned = true;
            IEnumerable<User> users = UserService.GetUsers(userQuery, 10000, 1);
            foreach (var user in users)
            {
                if (user.BanDeadline <= DateTime.Now)
                {
                    UserService.UnbanUser(user.UserId);
                }
            }
            UserSettings userSetting = SettingManager<UserSettings>.Get();
            //经验达到一定数值后自动解除管制
            userQuery = new UserQuery();
            userQuery.IsModerated = true;
            users = UserService.GetUsers(userQuery, 10000, 1);

            var userIds = users.Where(n => n.ExperiencePoints > userSetting.NoModeratedUserPoint || n.ExperiencePoints == userSetting.NoModeratedUserPoint).Select(n => n.UserId);

            UserService.SetModeratedStatus(userIds, false);
            foreach (var item in userIds)
            {
                roleService.AddUserToRole(item, RoleIds.Instance().TrustedUser());
            }
        }
    }
}