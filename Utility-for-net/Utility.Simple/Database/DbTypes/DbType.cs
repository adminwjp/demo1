using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Database.DbTypes
{
    /// <summary>
    /// 数据类型
    /// </summary>
    [Flags]
    public enum DbType
    {
        /// <summary>
        /// 
        /// </summary>
        None=0,
        /// <summary>
        /// 
        /// </summary>
        String,
        /// <summary>
        /// 
        /// </summary>
        Char,
        /// <summary>
        /// 
        /// </summary>
        Number,
        /// 
        /// </summary>
        Integer,
        /// <summary>
        /// 
        /// </summary>
        DateTime,
        /// <summary>
        /// 
        /// </summary>
        TimeStamp,
        /// <summary>
        /// 
        /// </summary>
        Byte,
        /// <summary>
        /// 
        /// </summary>
        Bit,
    }
}
