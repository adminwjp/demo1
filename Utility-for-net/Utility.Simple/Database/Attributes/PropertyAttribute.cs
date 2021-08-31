using System;

namespace Utility.Database.Attributes
{
   

    /// <summary>列特性普通属性注解 </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class PropertyAttribute : IdAttribute
    {
        public PropertyAttribute()
        {
            this.IsNull = true;
        }
        /// <summary>列默认值 </summary>
        public string Default { get; set; }
    }

}
