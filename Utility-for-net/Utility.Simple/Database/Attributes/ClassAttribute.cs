using System;

namespace Utility.Database.Attributes
{
  

    /// <summary>类特性类注解 </summary>
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Struct, Inherited = true, AllowMultiple = false)]
    public class ClassAttribute : Attribute
    {
        public ClassAttribute()
        {

        }
        public ClassAttribute(string table)
        {
            this.Table = table;
        }
        /// <summary>表 </summary>
        public string Table { get; set; }
    }
    
}
