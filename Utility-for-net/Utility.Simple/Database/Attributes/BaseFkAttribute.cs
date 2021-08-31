using System;

namespace Utility.Database.Attributes
{

    /// <summary>外键基类特性属性注解 </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public abstract class BaseFkAttribute : AbstractAttribute
    {
        public BaseFkAttribute()
        {
            this.IsNull = true;
        }
        /// <summary>外键名称 </summary>
        public string Constraint { get; set; }
        /// <summary>列 </summary>
        public string Column { get; set; }
    }

   
}
