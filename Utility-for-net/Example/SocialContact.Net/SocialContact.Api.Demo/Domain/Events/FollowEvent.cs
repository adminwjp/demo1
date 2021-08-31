using MediatR;
using SocialContact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialContact.Domain.Events
{
    public class FollowEvent: FollowEntity, INotification
    {
    }
}
