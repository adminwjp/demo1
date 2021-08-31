using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Wpf.Attributes
{
    /// <summary>
    /// 菜单 注解
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class MenuAttribute:Attribute
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Header { get; set; }
        /// <summary>
        /// 分组 名称
        /// </summary>
        public string Group { get; set; }

    }
}
