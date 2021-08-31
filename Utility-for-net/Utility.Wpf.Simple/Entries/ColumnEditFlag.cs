namespace Utility.Wpf.Entries
{
    /// <summary>
    /// 文本框 
    /// </summary>
    public enum ColumnEditFlag
    {
        /// <summary>
        /// 无
        /// </summary>
        None= 0x0,
        /// <summary>
        /// 可以 编辑
        /// </summary>
        Edit = 0x1,
        /// <summary>
        /// 添加 时 可用,其他情况 隐藏
        /// </summary>
        Hiddern= 0x2,
        /// <summary>
        /// 除添加 时 禁用
        /// </summary>
        Disabled= 0x3
    }
}
