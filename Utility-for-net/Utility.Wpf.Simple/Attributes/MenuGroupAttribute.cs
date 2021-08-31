using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Wpf.Attributes
{
    /// <summary>
    /// �˵� ���� ����
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum, Inherited = true, AllowMultiple = false)]
    public class MenuGroupAttribute:Attribute
    {
        /// <summary>
        /// ���� ���� ��ַ
        /// </summary>
        public string Config { get; set; }
    }
}
