using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Config.Domain.Entities
{
    /// <summary>
    /// 配置状态
    /// </summary>
    public enum ConfigStatus
    {
        None=0x0,
        Use=0x1,
        Start=0x2,
        Stop=0x3,
        Unkown=0x4
    }
}
