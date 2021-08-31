using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Database.Entities
{

    public enum FetchFlag
    {
        /// <summary>普通查询 </summary>
        None,
        /// <summary>普通查询再次关联查询只嵌套第一层(不递归嵌套再次查询)  </summary>
        Select,
        /// <summary>左连接查询(不递归嵌套) </summary>
        Join
    }
}
