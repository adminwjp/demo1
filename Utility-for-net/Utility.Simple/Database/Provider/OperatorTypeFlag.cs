using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Database.Provider
{
    public enum OperatorTypeFlag
    {
        None = 0x0,
        Database = 0x1,
        Table = 0x2,
        Procedure = 0x3,
        View = 0x4,
        Index = 0x5
    }
}
