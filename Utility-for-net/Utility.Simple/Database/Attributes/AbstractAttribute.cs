using System;

namespace Utility.Database.Attributes
{
    public abstract class AbstractAttribute : BaseAttribute
    {
        /// <summary>数据类型 </summary>
        public string DataType { get; set; }
        /// <summary>长度 </summary>
        public int Length { get; set; } = 255;
        /// <summary>列注释 </summary>
        public string Comment { get; set; }
        /// <summary>列默认是否为null 默认 not null </summary>
        public bool IsNull { get; set; }
    }
  
}
