//<tunynetcopyright>
//--------------------------------------------------------------
//<version>v0.6</verion>
//<createdate>2017-01-20</createdate>
//<author>wanglei</author>
//<email>wanglei@tunynet.com</email>
//<log date="2017-01-20" version="0.5">新建</log>
//<log date="2017-03-15" version="0.6">修改</log>
//--------------------------------------------------------------
//</tunynetcopyright>

using System.ComponentModel.DataAnnotations;

namespace Tunynet.UI
{
    /// <summary>
    /// 导航类型（1：普通导航；2：栏目）
    /// </summary>
    public enum NavigationTypes
    {
        ///// <summary>
        ///// 平台定义
        ///// </summary>
        //[Display(Name ="平台")]
        //PresentArea = 0,

        /// <summary>
        /// 普通导航
        /// </summary>
        [Display(Name = "普通导航")]
        Application = 1,

        /// <summary>
        /// 栏目
        /// </summary>
        [Display(Name = "栏目")]
        AddCategory = 2,
    }
}