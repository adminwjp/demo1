using System;

namespace Utility.Database.Attributes
{
    /// <summary>
    /// 查询 操作 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class SelectAttribute : Attribute
    {
        public string Select { get; set; }
        public bool Procduture { get; set; }
        public Type ReturnType { get; set; }
    }
}
