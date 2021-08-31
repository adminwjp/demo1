using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Wpf.Attributes
{
    /// <summary>
    /// 菜单 分组 特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum, Inherited = true, AllowMultiple = false)]
    public class MenuGroupAttribute:Attribute
    {
        /// <summary>
        /// 分组 数据 地址
        /// </summary>
        public string Config { get; set; }
    }
}
