using System;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Utility.Wpf
{
    /// <summary>
    /// console.write 日志输出 到 控件上 
    /// </summary>
    public class RichTextBoxWriter : System.IO.TextWriter
    {
        private readonly RichTextBox  _richTextBox;
        delegate void VoidAction();

        /// <summary>
        /// RichTextBox
        /// </summary>
        /// <param name="richTextBox"></param>
        public RichTextBoxWriter(RichTextBox  richTextBox)
        {
            this._richTextBox = richTextBox;
        }

        /// <summary>
        /// console.write
        /// </summary>
        /// <param name="value"></param>
        public override void Write(string value)
        {
            if (this._richTextBox == null)
            {
                return;
            }
            System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(()=> {
                //追加
                this._richTextBox.Document.Blocks.Add(new Paragraph(new Run($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") }===>> {value}") { Foreground = Brushes.Red }));
            }));
           
        }

        /// <summary>
        /// console.writeline
        /// </summary>
        /// <param name="value"></param>

        public override void WriteLine(string value)
        {
            this.Write(value+"\r\n");
        }

        /// <summary>
        /// 编码 默认 utf8
        /// </summary>
        public override System.Text.Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
