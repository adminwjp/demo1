/**
 不要 拆了 ,继承了.不然拆分 好累 (全部扔到一起. 组合使用 细分麻烦)
 */
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using Utility.Helpers;

namespace Utility.Database
{

    #region FKColumnEntry
    /// <summary>查询外键信息缓存 </summary>
    internal class FKColumnDto
    {
        /// <summary> 外键列</summary>
        public string Column { get; set; }
        /// <summary> 外键列数据库值</summary>
        public object Value { get; set; }
        /// <summary> 外键列数据库值 不然值不好比较 全部转换为string </summary>
        public string StrValue { get; set; }

    }

    #endregion FKColumnEntry






 


    public enum DbDataType
    {
        Integer,
        SmallNumer,
        String,
        Date,
        Char,
        Bool,

    }
}

