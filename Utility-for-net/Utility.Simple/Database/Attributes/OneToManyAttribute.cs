using System;

namespace Utility.Database.Attributes
{

    /// <summary>外键一对多特性属性注解 </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class OneToManyAttribute : BaseFkAttribute
    {

    }

   
}
