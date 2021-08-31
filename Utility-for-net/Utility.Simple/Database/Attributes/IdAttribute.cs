using System;

namespace Utility.Database.Attributes
{

    /// <summary>主键特性普通属性注解 </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class IdAttribute : AbstractAttribute
    {
        /// <summary>列名 </summary>
        public string Column { get; set; }
    }

}
