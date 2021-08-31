using MediatR;
using SocialContact.Domain.Entities;
using SocialContact.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utility.Domain.Uow;

namespace SocialContact.Domain.EventHandlers
{
    public class ConfirmFollowEventHandler : INotificationHandler<ConfirmFollowEvent>
    {
        private IUnitWorkManagerAsyn unitWork;
        public ConfirmFollowEventHandler(IUnitWorkManagerAsyn unitWork) 
        {
            this.unitWork = unitWork;
        }
        public virtual async Task Handle(ConfirmFollowEvent notification, CancellationToken cancellationToken)
        {
            await Handle(notification,true,cancellationToken);
        }

        protected virtual async Task Handle<T>(T notification,bool confirm, CancellationToken cancellationToken) where T :CancelFollowEvent
        {
            await this.unitWork.UpdateAsyn<FollowEntity>(it => it.Id == notification.Id, it => new FollowEntity() { IsDelete = confirm }, cancellationToken);
        }
    }
}
