using System;

namespace Utility.Database.Attributes
{
   

    /// <summary>外键特性普通属性注解 </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public  class FKAttribute : BaseFkAttribute
    {
        /// <summary>外键引用 </summary>
        public Type ReferenceType { get; set; }
    }

   
}
