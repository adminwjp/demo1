using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Wpf.Attributes
{
    /// <summary>
    ///mapp ”≥…‰ 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =true,Inherited =true)]
    public class MappTypeAttribute:Attribute
    {
        /// <summary>
        /// mapp ”≥…‰ 
        /// </summary>
        public MappTypeAttribute()
        {

        }
        /// <summary>
        /// mapp ”≥…‰ ¿‡–Õ
        /// </summary>
        /// <param name="type">”≥…‰ ¿‡–Õ</param>
        public MappTypeAttribute(Type type)
        {
            this.Type = type;
        }
        /// <summary>
        /// mapp ”≥…‰ ¿‡–Õ
        /// </summary>
        public Type Type { get; set; }
    }
}
