using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialContact.Domain.Events
{
    /// <summary>批量更新计数表中的阶段计数</summary>
    public class UpdateStageCountPerDayEvent
    {
        /// <summary>租户类型Id</summary>
        public string tenantTypeId { get; set; }

        /// <summary>计数类型</summary>
        public string countType { get; set; }

        /// <summary>计数类型 -统计天数字典集合</summary>
        public Dictionary<string, int> countType2Days { get; set; }
    }
}
