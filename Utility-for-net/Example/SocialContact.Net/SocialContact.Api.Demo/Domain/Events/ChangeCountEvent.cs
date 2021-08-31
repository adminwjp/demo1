using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tunynet.Common;

namespace SocialContact.Domain.Events
{
    /// <summary>调整计数</summary>
    public class ChangeCountEvent : INotification
    {
        /// <summary>租户类型Id</summary>
        public string tenantTypeId { get; set; }

        /// <summary>计数类型</summary>
        public string countType { get; set; }

        /// <summary>计数对象Id</summary>
        public long objectId { get; set; }

        /// <summary>ownerId</summary>
        public long ownerId { get; set; }

        /// <summary>变化数</summary>
        public int changeCount { get; set; } = 1;

        /// <summary>阶段计数集合</summary>
        public IList<string> stageCountTypes { get; set; } = null;

        /// <summary>是否立即更新显示计数</summary>
        public  bool isRealTime { get; set; } = false;

        public CountQueueItem ToCountQueueItem(string cType)
        {
           return new CountQueueItem(cType, objectId, ownerId, changeCount, tenantTypeId);
        }
        public CountQueueItem ToCountQueueItem()
        {
            return new CountQueueItem(countType, objectId, ownerId, changeCount, tenantTypeId);
        }
    }
}
