using System.Windows.Controls;
using Utility.Wpf;

namespace Utility.Wpf.Extensions
{
    /// <summary>
    /// wpf 公共帮助类
    /// </summary>
    public static class WpfExtensions
    {
        /// <summary>
        /// 获取RichTextBox 文本值 内容以字符串的形式取出 相当于 xml
        /// <para>https://www.cnblogs.com/wzwyc/p/6291895.html</para>
        /// </summary>
        /// <param name="richTexbox">RichTextBox<see cref="System.Windows.Controls.RichTextBox"/></param>
        /// <returns></returns>
        public static string GetRichTexboxText(this System.Windows.Controls.RichTextBox richTexbox)
        {
            return WpfHelper.GetRichTexboxText(richTexbox);
        }
        /// <summary>
        /// 设置RichTextBox 文本值 将字符串转换为数据流赋值
        /// <para>https://www.cnblogs.com/wzwyc/p/6291895.html</para>
        /// </summary>
        /// <param name="richTexbox">RichTextBox<see cref="System.Windows.Controls.RichTextBox"/></param>
        /// <param name="text">RichTextBox<see cref="System.Windows.Controls.RichTextBox"/> text</param>
        /// <returns></returns>
        public static void SetRichTexboxText(this System.Windows.Controls.RichTextBox richTexbox, string text)
        {
            WpfHelper.SetRichTexboxText(richTexbox, text);
        }
        /// <summary>
        /// 设置RichTextBox 文本值 将字符串转换为数据流赋值
        /// <para>https://www.cnblogs.com/wzwyc/p/6291895.html</para>
        /// </summary>
        /// <param name="richTexbox">RichTextBox<see cref="System.Windows.Controls.RichTextBox"/></param>
        /// <param name="text">RichTextBox<see cref="System.Windows.Controls.RichTextBox"/> text</param>
        /// <returns></returns>
        public static void SetRichTexboxTextFormat(this System.Windows.Controls.RichTextBox richTexbox, string text)
        {
            WpfHelper.SetRichTexboxText(richTexbox, text);
        }
        /// <summary>
        /// 获取RichTextBox 文本值
        /// <para>https://www.cnblogs.com/wzwyc/p/6291895.html</para>
        /// </summary>
        /// <param name="richTexbox">RichTextBox<see cref="System.Windows.Controls.RichTextBox"/></param>
        /// <param name="fromat">fromat<see cref="System.Windows.DataFormats"/></param>
        /// <returns></returns>
        public static string GetRichTexboxTextFormat(this System.Windows.Controls.RichTextBox richTexbox, string fromat = "Text")
        {
            return WpfHelper.GetRichTexboxTextFormat(richTexbox, fromat);
        }
        public static void CheckBoxClick(this DataGrid dataGrid, object sender)
        {
            WpfHelper.CheckBoxClick(dataGrid, sender);
        }
        public static object[] GetCheckItems(this DataGrid dataGrid)
        {
            return WpfHelper.GetCheckItems(dataGrid);
        }
        public static object[] GetCheckIds(this DataGrid dataGrid)
        {
            return WpfHelper.GetCheckIds(dataGrid);
        }
    }
}
