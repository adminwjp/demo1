using System;

namespace Utility.Database.Attributes
{
    /// <summary>
    /// 修改操作 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ModifyAttribute : Attribute
    {
        public string Modify { get; set; }
        public bool Procduture { get; set; }
    }

}
