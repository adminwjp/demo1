using SocialContact.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utility.Domain.Uow;

namespace SocialContact.Domain.EventHandlers
{
    public class CancelFollowEventHandler: ConfirmFollowEventHandler
    {
        public CancelFollowEventHandler(IUnitWorkManagerAsyn unitWork):base(unitWork)
        {
         
        }
        public override async Task Handle(ConfirmFollowEvent notification, CancellationToken cancellationToken)
        {
            await Handle(notification, false, cancellationToken);
        }
    }
}
