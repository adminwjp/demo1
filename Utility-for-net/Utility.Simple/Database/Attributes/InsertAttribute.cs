using System;

namespace Utility.Database.Attributes
{
    /// <summary>
    /// 添加操作 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class InsertAttribute : Attribute
    {
        public string Insert { get; set; }
        public bool Procduture { get; set; }
    }
   
}
