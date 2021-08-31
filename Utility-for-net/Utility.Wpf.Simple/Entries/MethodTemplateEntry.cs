using System;

namespace Utility.Wpf.Entries
{
    /// <summary>
    /// 方法模板
    /// </summary>
    public class MethodTemplateEntry
    {
        /// <summary>
        /// 查询 条件 以及 分页 查询
        /// </summary>
        public Func<object,int, int, object> FindListByWhere { get; set; }
        /// <summary>
        /// 查询  分页 查询
        /// </summary>
        public Func<int, int, object> FindList { get; set; }
        /// <summary>
        /// 添加
        /// </summary>
        public Func<object, object> Add { get; set; }
        /// <summary>
        /// 修改
        /// </summary>
        public Action<object> Modify { get; set; }
        /// <summary>
        /// 统一 直接 传 对象 ,id不好控制 
        /// </summary>
        public Action<object> Delete { get; set; }
        /// <summary>
        /// 删除
        /// </summary>
        public Action<object[]> DeleteList { get; set; }
        /// <summary>
        /// 下载 csv
        /// </summary>
        public Action<string> DownCsv { get; set; }
        /// <summary>
        /// 下载 excel
        /// </summary>
        public Action<string> DownExecl { get; set; }
    }
}
