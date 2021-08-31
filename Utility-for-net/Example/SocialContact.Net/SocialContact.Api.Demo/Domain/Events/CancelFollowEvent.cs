using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialContact.Domain.Events
{
    public class CancelFollowEvent: INotification
    {
        /// <summary>Id </summary>
        public long Id { get; set; }

        /// <summary>关注用户Id </summary>
        public long UserId { get; set; }

        /// <summary>被关注用户Id </summary>
        public long FollowedUserId { get; set; }
    }
}
