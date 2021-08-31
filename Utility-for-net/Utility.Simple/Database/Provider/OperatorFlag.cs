using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Database.Provider
{
    public enum OperatorFlag
    {
        None = 0x0,
        Drop = 0x1,
        DropIfExists = 0x2,
        Create = 0x3,
        CreateDropIfExists = 0x4,
        CreateIfNotExists = 0x5
    }
}
