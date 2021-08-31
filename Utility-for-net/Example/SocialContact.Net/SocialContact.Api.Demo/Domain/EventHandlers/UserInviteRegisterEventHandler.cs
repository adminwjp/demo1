using MediatR;
using NHibernate;
using SocialContact.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tunynet.Common;
using Tunynet.Settings;

namespace SocialContact.Domain.EventHandlers
{

    /// <summary>
    /// 邀请注册
    /// </summary>
    public class UserInviteRegisterEventHandler : INotificationHandler<UserInviteRegisterEvent>
    {
        private readonly Lazy<ISession> read;
        private readonly Lazy<ISession> write;

        public UserInviteRegisterEventHandler(Lazy<ISession> read, Lazy<ISession> write)
        {
            this.read = read;
            this.write = write;
        }

        public async Task Handle(UserInviteRegisterEvent notification, CancellationToken cancellationToken)
        {
            //获取邀请码实体
            InvitationCode invitation = await write.Value.GetAsync<InvitationCode>(notification.InvitationCode, cancellationToken);
            InviteFriendSettings inviteFriendSettings = SettingManager<InviteFriendSettings>.Get();
            if (invitation == null || inviteFriendSettings.AllowInvitationCodeUseOnce == invitation.IsMultiple)
                return ;
            long userId = invitation.UserId;//邀请者

            //创建邀请记录
            InviteFriendRecord inviteFriendRecord = InviteFriendRecord.New();
            inviteFriendRecord.Code = notification.InvitationCode;
            inviteFriendRecord.IsRewarded = false;
            inviteFriendRecord.InvitedUserId = notification.InvitedUserId;
            inviteFriendRecord.UserId = userId;
            await write.Value.SaveAsync(inviteFriendRecord, cancellationToken);

            //添加互相关注
            FollowService.Follow(notification.InvitedUserId, userId);
            FollowService.Follow(userId, notification.InvitedUserId);

            //邀请用户增加积分
            var pointItemKey = PointItemKeys.Instance().InviteUserRegister();
            string description = string.Format("邀请用户注册");
            PointService.GenerateByRole(userId, userId, pointItemKey, description);
        }
    }
}
