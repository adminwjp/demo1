using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Wpf.Attributes
{
    /// <summary>
    /// �˵� ע��
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class MenuAttribute:Attribute
    {
        /// <summary>
        /// ����
        /// </summary>
        public string Header { get; set; }
        /// <summary>
        /// ���� ����
        /// </summary>
        public string Group { get; set; }

    }
}
