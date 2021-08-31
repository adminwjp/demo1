using MediatR;
using Microsoft.Extensions.Logging;
using NHibernate;
using NHibernate.Linq;
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
    public class FollowEventHandler : INotificationHandler<FollowEvent>
    {
        //private ILogger<FollowEventHandler> logger;
        private IUnitWorkManagerAsyn unitWork;
        public FollowEventHandler(IUnitWorkManagerAsyn unitWork):this(unitWork,null)
        {
           
        }
        protected FollowEventHandler(IUnitWorkManagerAsyn unitWork, ILogger<FollowEventHandler> logger)
        {
            this.unitWork = unitWork;
           // this.logger = logger;
        }
        public async Task Handle(FollowEvent notification, CancellationToken cancellationToken)
        {
            await this.unitWork.BeginAsyn(cancellationToken);
            if (notification.Id > 0)
            {
                /*  follow =await write.Value.Query<FollowEvent>().Where(it => it.Id == notification.Id).FirstOrDefaultAsync(cancellationToken);
                  if (follow == null)
                  {
                      logger.LogWarning("Follow update fail,Follow  get {Id} is null ",notification.Id);
                      return;
                  }*/
                await unitWork.UpdateAsyn(notification,cancellationToken);
            }
            else
            {
                await unitWork.InsertAsyn(notification,cancellationToken);
            }
            await this.unitWork.CommitAsyn(cancellationToken);
        }
    }
}
