using System;

namespace Utility.Database.Attributes
{
   

    /// <summary>外键一对一特性属性注解 </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class OneToOneAttribute : BaseFkAttribute
    {

    }


}
