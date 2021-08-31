using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Database.Provider
{ 
    public enum WhereFlag
    {
        None,
        /// <summary>
        /// column between val1 and val2
        /// </summary>
        Between,
        /// <summary>
        ///  column > val1 
        /// </summary>
        Gt,
        /// <summary>
        ///  column lt val1 
        /// </summary>
        Lt,
        GtEqual,
        LtEqual,
        Equal,
        /// <summary>
        /// column1!=val1 or column1 lt>val1 (2中写法有的数据库只支持其中一种)
        /// </summary>
        NotEqual,
        StartWith,
        EndWith,
        Exists,
        NotExists,
        In,
        NotIn,
        /// <summary>
        /// sqlserver 老版本不支持 后期支持 column1 some>(比较运算符组合使用) val1
        /// </summary>
        Some,
        SomeGt,
        SomeLt,
        SomeGtEqual,
        SomeLtEqual,
        /// <summary>
        ///  column1 some>(比较运算符组合使用 等同 some) val1
        /// </summary>
        Any,
        AnyGt,
        AnyLt,
        AnyGtEqual,
        AnyLtEqual,
        /// <summary>
        /// %% 模糊匹配 [1,2,3，%] %代表匹配字符  
        /// </summary>
        Like,
        Substring
    }
}
