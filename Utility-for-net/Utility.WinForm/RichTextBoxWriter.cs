using System;
using System.Text;
using System.Windows.Forms;
using Utility.Helpers;

namespace Utility.WinForm
{
    public class RichTextBoxWriter : System.IO.TextWriter
    {
        private readonly RichTextBox  _richTextBox;
        delegate void VoidAction();

        public RichTextBoxWriter(RichTextBox  richTextBox)
        {
            this._richTextBox = richTextBox;
        }

        public override void Write(string value)
        {
            Action action = () => { _richTextBox.AppendText($"{CommonHelper.DateString("yyyy-MM-dd hh:mm:ss") }===>> {value}\r\n"); };
            WinFormHelper.Set(_richTextBox,action);
        }

        public override void WriteLine(string value)
        {
            this.Write(value);
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}
