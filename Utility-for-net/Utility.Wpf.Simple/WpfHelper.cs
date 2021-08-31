#if NET10 || NET11 || NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Utility.Helpers;

namespace Utility.Wpf
{
    /// <summary>
    /// wpf 公共帮助类
    /// </summary>
    public static class WpfHelper
    {
        /// <summary>
        /// 获取RichTextBox 文本值 内容以字符串的形式取出 相当于 xml
        /// <para>https://www.cnblogs.com/wzwyc/p/6291895.html</para>
        /// </summary>
        /// <param name="richTexbox">RichTextBox<see cref="System.Windows.Controls.RichTextBox"/></param>
        /// <returns></returns>
        public static string GetRichTexboxText(System.Windows.Controls.RichTextBox richTexbox)
        {
            ValidateHelper.ValidateArgumentObjectNull("richTexbox", richTexbox);
            return System.Windows.Markup.XamlWriter.Save(richTexbox.Document);
        }
        /// <summary>
        /// 设置RichTextBox 文本值 将字符串转换为数据流赋值
        /// <para>https://www.cnblogs.com/wzwyc/p/6291895.html</para>
        /// </summary>
        /// <param name="richTexbox">RichTextBox<see cref="System.Windows.Controls.RichTextBox"/></param>
        /// <param name="text">RichTextBox<see cref="System.Windows.Controls.RichTextBox"/> text</param>
        /// <returns></returns>
        public static void SetRichTexboxText(System.Windows.Controls.RichTextBox richTexbox,string text)
        {
            ValidateHelper.ValidateArgumentObjectNull("richTexbox", richTexbox);
            ValidateHelper.ValidateArgumentNull("text", text);
            richTexbox.Document=(System.Windows.Documents.FlowDocument) System.Windows.Markup.XamlReader.Load(System.Xml.XmlReader.Create(new System.IO.StringReader(text)));
        }
        /// <summary>
        /// 设置RichTextBox 文本值 将字符串转换为数据流赋值
        /// <para>https://www.cnblogs.com/wzwyc/p/6291895.html</para>
        /// </summary>
        /// <param name="richTexbox">RichTextBox<see cref="System.Windows.Controls.RichTextBox"/></param>
        /// <param name="text">RichTextBox<see cref="System.Windows.Controls.RichTextBox"/> text</param>
        /// <returns></returns>
        public static void SetRichTexboxTextFormat(System.Windows.Controls.RichTextBox richTexbox, string text)
        {
            ValidateHelper.ValidateArgumentObjectNull("richTexbox", richTexbox);
            ValidateHelper.ValidateArgumentNull("text", text);
            richTexbox.Document = new System.Windows.Documents.FlowDocument(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(text)));
            System.Windows.Documents.FlowDocument doc = new System.Windows.Documents.FlowDocument();
            System.Windows.Documents.Paragraph p = new System.Windows.Documents.Paragraph(); // Paragraph 类似于 html 的 P 标签
            System.Windows.Documents.Run r = new System.Windows.Documents.Run(text); // Run 是一个 Inline 的标签
            p.Inlines.Add(r);
            doc.Blocks.Add(p);
            richTexbox.Document = doc;
        }
        /// <summary>
        /// 获取RichTextBox 文本值
        /// <para>https://www.cnblogs.com/wzwyc/p/6291895.html</para>
        /// </summary>
        /// <param name="richTexbox">RichTextBox<see cref="System.Windows.Controls.RichTextBox"/></param>
        /// <param name="fromat">fromat<see cref="System.Windows.DataFormats"/></param>
        /// <returns></returns>
        public static string GetRichTexboxTextFormat(System.Windows.Controls.RichTextBox richTexbox,string fromat="Text")
        {
            ValidateHelper.ValidateArgumentObjectNull("richTexbox", richTexbox);
            ValidateHelper.ValidateArgumentNull("fromat", fromat);
            string text = string.Empty;
            System.Windows.Documents.TextRange textRange = new System.Windows.Documents.TextRange(richTexbox.Document.ContentStart, richTexbox.Document.ContentEnd);
           // return textRange.Text;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                textRange.Save(ms, fromat);//System.Windows.DataFormats.Text
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                System.IO.StreamReader sr = new System.IO.StreamReader(ms);
                text = sr.ReadToEnd();
            }
            return text;
        }
        /// <summary>
        /// 全选 复选框 触发 表格复选框
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="sender"></param>
        public static void CheckBoxClick(DataGrid dataGrid, object sender)
        {
            CheckBox headercb = (CheckBox)sender;
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                //获取行
                DataGridRow neddrow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(i);
                //获取该行的某列
                CheckBox cb = (CheckBox)dataGrid.Columns[0].GetCellContent(neddrow);
                cb.IsChecked = headercb.IsChecked;
            }
        }
        /// <summary>
        /// 获取  tag
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static object[] GetCheckIds(DataGrid dataGrid)
        {
            return GetCheckItems(dataGrid, true);
        }
        /// <summary>
        /// 获取  元素
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static object[] GetCheckItems(DataGrid dataGrid)
        {
            return GetCheckItems(dataGrid,false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="id">true: 获取 tag false:获取 该 元素</param>
        /// <returns></returns>
        public static object[] GetCheckItems(DataGrid dataGrid,bool id=true)
        {
            List<object> ids = new List<object>();
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                //获取行
                DataGridRow neddrow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(i);
                //获取该行的某列
                CheckBox cb = (CheckBox)dataGrid.Columns[0].GetCellContent(neddrow);
                if (cb.IsChecked.HasValue && cb.IsChecked.Value)
                {
                    if (id&&cb.Tag != null)
                        ids.Add(cb.Tag);
                    else if(!id) 
                        ids.Add(neddrow.Item);
                }
            }
            return ids.ToArray();
        }
        /// <summary>
        /// 字节转 图像 格式
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            BitmapImage bmp = null;
            try
            {
                bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(byteArray);
                bmp.EndInit();
            }
            catch
            {
                bmp = null;
            }
            return bmp;
        }
        /// <summary>
        /// 图像转 字节 格式
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static byte[] BitmapImageToByteArray(BitmapImage bmp)
        {
            byte[] byteArray = null;
            try
            {
                Stream sMarket = bmp.StreamSource;
                if (sMarket != null && sMarket.Length > 0)
                {
                    //很重要，因为Position经常位于Stream的末尾，导致下面读取到的长度为0。 
                    sMarket.Position = 0;

                    using (BinaryReader br = new BinaryReader(sMarket))
                    {
                        byteArray = br.ReadBytes((int)sMarket.Length);
                    }
                }
            }
            catch
            {
                //other exception handling 
            }
            return byteArray;
        }
    }
  
}
#endif
