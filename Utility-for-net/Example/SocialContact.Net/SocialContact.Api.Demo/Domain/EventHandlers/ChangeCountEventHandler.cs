using Core;
using MediatR;
using NHibernate;
using SocialContact.Domain.Events;
using SocialContact.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tunynet.Common;

namespace SocialContact.Domain.EventHandlers
{
    public class ChangeCountEventHandler : INotificationHandler<ChangeCountEvent>
    {
        private readonly Lazy<ISession> read;
        private readonly Lazy<ISession> write;
        private object _lock = new object();
        public ChangeCountEventHandler(Lazy<ISession> read, Lazy<ISession> write)
        {
            this.read = read;
            this.write = write;
        }
        public Task Handle(ChangeCountEvent notification, CancellationToken cancellationToken)
        {
            List<string> countTypes = new List<string>() { notification.countType };
            //同时维护阶段计数
            if (notification.stageCountTypes != null)
                countTypes.AddRange(notification.stageCountTypes);

            lock (_lock)
            {
                //1.更新计数队列，使其Count+=changeCount
                if (!CountUtils.CountQueue.ContainsKey(notification.tenantTypeId))
                    CountUtils.CountQueue[notification.tenantTypeId] = new Queue<CountQueueItem>();

                Queue<CountQueueItem> countList = CountUtils.CountQueue[notification.tenantTypeId];

                foreach (var cType in countTypes)
                {
                    IEnumerable<CountQueueItem> countQueueItems = countList.Where(n => n.ObjectId == notification.objectId && n.CountType == cType);
                    CountQueueItem countQueueItem = null;
                    if (countQueueItems != null && countQueueItems.Count() > 0)
                        countQueueItem = countQueueItems.FirstOrDefault();
                    if (countQueueItem == null)
                    {
                        countQueueItem = notification.ToCountQueueItem(cType);
                        countList.Enqueue(countQueueItem);
                    }
                    else
                        countQueueItem.StatisticsCount += notification.changeCount;
                }

                //2.根据tenantTypeId、countType、objectId、ownerId更新每日计数队列CountPerDayQueue，使其Count+=changeCount
                //更新每日计数时，还需要检查当前日期是否已存在,如果记录不存在还需要创建
                if (notification.stageCountTypes != null)
                {
                    if (!CountUtils.CountPerDayQueue.ContainsKey(notification.tenantTypeId))
                        CountUtils.CountPerDayQueue[notification.tenantTypeId] = new Queue<CountQueueItem>();
                    CountQueueItem countPerDayQueueItem = CountUtils.CountPerDayQueue[notification.tenantTypeId].Where(n => n.ObjectId == notification.objectId && n.CountType == notification.countType).FirstOrDefault();
                    if (countPerDayQueueItem == null)
                    {
                        countPerDayQueueItem = notification.ToCountQueueItem();
                        CountUtils.CountPerDayQueue[notification.tenantTypeId].Enqueue(countPerDayQueueItem);
                    }
                    else
                        countPerDayQueueItem.StatisticsCount += notification.changeCount;
                }
            }

            //3.维护及时性
            if (notification.isRealTime)
            {
                foreach (var cType in countTypes)
                {
                    int count = Get(notification.tenantTypeId, cType, notification.objectId);
                    count += notification.changeCount;
                    string cacheKey = GetCacheKey_Count(notification.tenantTypeId, cType, notification.objectId);

                    GlobalHelper.Cache.Set(cacheKey, count, DateTime.Now.AddDays(1));
                }
            }
            throw new NotImplementedException();
        }
    }
}
