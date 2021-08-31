using System;

namespace Utility.Attributes
{
    /// <summary> 
    /// description attribute
    /// </summary>
    [AttributeUsage( AttributeTargets.All,AllowMultiple =true,Inherited =true)]
    public class DescAttribute:System.Attribute
    {

        /// <summary> 
        /// chinese description
        /// </summary>
        public string ChineseDesc { get; set; }


        /// <summary>
        /// english description 
        /// </summary>
        public string EnglishDesc { get; set; }

        /// <summary>
        /// resource  name
        /// </summary>
        public string ResourceName { get; set; }
    }
}
