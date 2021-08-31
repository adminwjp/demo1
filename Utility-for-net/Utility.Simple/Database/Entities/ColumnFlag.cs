using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Database.Entities
{
    [Flags]
    public enum ColumnFlag
    {
        /// <summary>
        /// 不做任何处理
        /// </summary>
        None,

        Column,
        /// <summary>
        /// 主键属性
        /// </summary>
        PrimaryKey,
        /// <summary>
        /// 外键属性
        /// </summary>
        ForeignKey,
        /// <summary>
        /// 忽略的属性
        /// </summary>
        Ignore
    }
}
