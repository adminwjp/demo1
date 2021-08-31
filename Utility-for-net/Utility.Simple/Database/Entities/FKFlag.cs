using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Database.Entities
{
    /// <summary>
    /// CONSTRAINT "bb"  FOREIGN KEY ("bb") REFERENCES "test1" ("b") ON DELETE Set Default ON UPDATE NO ACTION DEFERRABLE INITIALLY DEFERRED
    /// </summary>
    public enum DbFKFlag
    {
        /// <summary>
        /// 不做任何处理
        /// </summary>
        None = 0x0,
        Cascade = 0x1,
        SetNull = 0x2,
        NoAction = 0x3,
        Restrict = 0x4,
        /// <summary>
        /// sqlite 支持 mysql 不支持
        /// </summary>
        SetDefault = 0x5
    }

    public enum FKFlag
    {
        None = 0x0,
        Single = 0x1,
        Many = 0x2,
        Basic = 0x3
    }
}
