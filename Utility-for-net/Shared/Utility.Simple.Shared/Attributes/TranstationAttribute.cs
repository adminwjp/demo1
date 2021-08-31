using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Attributes
{
    /// <summary>
    ///  事务 注解
    /// </summary>
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class TranstationAttribute : System.Attribute
    {
        public virtual string Name { get; set; }
        /// <summary>
        ///注意 ef 事务 需要 每次重新 new dbcontext 
        /// </summary>
        public virtual bool UseTranstation { get; set; } = true;
        public virtual bool Read { get; set; }
        public virtual bool Write { get; set; }

    }
    
}
