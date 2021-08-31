using System;
using System.Collections.Generic;

namespace Config.Remote.Entities
{

    /// <summary>
    /// 
    /// </summary>
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
    [Serializable]
#endif
    public class ServiceObjectListEntity:List<ServiceObjectEntity>
    {
    }
}
