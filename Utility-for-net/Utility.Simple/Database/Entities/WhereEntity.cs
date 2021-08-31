using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Database.Provider;

namespace Utility.Database.Entities
{

    /// <summary>条件判断缓存 </summary>
    internal class WhereEntity : BaseEntity
    {
        public Type TargetType { get; set; }
        /// <summary>条件名称 </summary>
        public string Name { get; set; }
        /// <summary>条件逻辑 </summary>
        public WhereFlag Flag { get; set; }
        /// <summary>条件值 it.name=="1" 有效 </summary>
        public object Value { get; set; }
        public Type ValueType { get; set; }
        /// <summary>条件值 it.name==it1.name 有效 </summary>
        public string ValueName { get; set; }

    }
}
