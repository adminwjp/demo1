using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialContact.Domain.Events
{
    /// <summary>
    /// 邀请注册
    /// </summary>
    public class UserInviteRegisterEvent : INotification
    {

        /// <summary>
        /// 邀请注册
        /// </summary>
        public UserInviteRegisterEvent()
        {
        }

        /// <summary>
        /// 邀请注册
        /// </summary>
        /// <param name="invitationCode">邀请注册 码</param>
        /// <param name="invitedUserId">邀请注册 用户</param>
        public UserInviteRegisterEvent(string invitationCode, long invitedUserId)
        {
            InvitationCode = invitationCode;
            InvitedUserId = invitedUserId;
        }

        /// <summary>
        /// 邀请注册 码
        /// </summary>
        public string InvitationCode { get; set; }
        /// <summary>
        /// 邀请注册 用户
        /// </summary>
        public long InvitedUserId { get; set; }
    }
}
