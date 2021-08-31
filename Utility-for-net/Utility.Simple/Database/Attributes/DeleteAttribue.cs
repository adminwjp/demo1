using System;

namespace Utility.Database.Attributes
{
    /// <summary>
    /// 删除操作 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method| AttributeTargets.Interface| AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class DeleteAttribue : Attribute
    {
        /// <summary>
        /// 删除 sql
        /// </summary>
        public string Delete { get; set; }
        /// <summary>
        /// 删除 储存过程 
        /// </summary>
        public bool Procduture { get; set; }
    }

  
}
