using System;

namespace Utility.Database.Attributes
{
   

    /// <summary>忽略特性普通属性注解 </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class IgnoreAttribute : BaseAttribute
    {
    }

   
}
