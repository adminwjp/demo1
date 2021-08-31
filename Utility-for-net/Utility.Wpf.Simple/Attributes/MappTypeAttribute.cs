using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Wpf.Attributes
{
    /// <summary>
    ///mapp ӳ�� 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =true,Inherited =true)]
    public class MappTypeAttribute:Attribute
    {
        /// <summary>
        /// mapp ӳ�� 
        /// </summary>
        public MappTypeAttribute()
        {

        }
        /// <summary>
        /// mapp ӳ�� ����
        /// </summary>
        /// <param name="type">ӳ�� ����</param>
        public MappTypeAttribute(Type type)
        {
            this.Type = type;
        }
        /// <summary>
        /// mapp ӳ�� ����
        /// </summary>
        public Type Type { get; set; }
    }
}
