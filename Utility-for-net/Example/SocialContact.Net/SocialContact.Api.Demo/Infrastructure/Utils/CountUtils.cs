using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tunynet.Common;

namespace SocialContact.Infrastructure.Utils
{
    public class CountUtils
    {
        public readonly static ConcurrentDictionary<string, Queue<CountQueueItem>> CountQueue = new ConcurrentDictionary<string, Queue<CountQueueItem>>();
        public readonly static ConcurrentDictionary<string, Queue<CountQueueItem>> CountPerDayQueue = new ConcurrentDictionary<string, Queue<CountQueueItem>>();

    }
}
