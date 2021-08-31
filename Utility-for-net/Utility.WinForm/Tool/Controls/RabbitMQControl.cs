using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility.WinForm;
using System.Threading;

namespace Tool.Controls
{
    public partial class RabbitMQControl : UserControl
    {
        public RabbitMQControl()
        {
            InitializeComponent();
            this.Resize += RabbitMQControl_Resize;
        }

        private void RabbitMQControl_Resize(object sender, EventArgs e)
        {
            WinFormHelper.Set(this, () => {
                int width = (this.Width - 600) / 2;
                this.labelSendNum.Left = width;
                this.labelSendNum.Top = 20;
                this.textBoxSendNum.Left = this.labelSendNum.Right+10;
                this.textBoxSendNum.Top = 20;
                this.labelSendMsg.Left = textBoxSendNum.Right+20;
                this.labelSendMsg.Top = 20;
                this.textBoxSendms.Left = this.labelSendMsg.Right + 10;
                this.textBoxSendms.Top = 20;
                this.buttonSend.Left = this.textBoxSendms.Right + 20;
                this.buttonSend.Top = 20;
            });
        }

        private void 发送信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 接收消息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CoreManager.RabbitMQPop("crawl");
        }

        private void 停止发送ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cancellationTokenSource != null && !cancellationTokenSource.IsCancellationRequested)
            {
                cancellationTokenSource.Cancel();
            }
        }
        CancellationTokenSource cancellationTokenSource;
        private void buttonSend_Click(object sender, EventArgs e)
        {
            //if (cancellationTokenSource != null && !cancellationTokenSource.IsCancellationRequested)
            //{
            //    MessageBox.Show("发送信息正在执行中,无法进行下一个任务!");
            //    return;
            //}
            //cancellationTokenSource = new CancellationTokenSource();
            //var msg = textBoxSendms.Text;
            //if (!string.IsNullOrEmpty(msg))
            //{
            //    int.TryParse(textBoxSendNum.Text, out int num);
            //    Task.Factory.StartNew(() => {
            //        int i = 0;
            //        do
            //        {
            //            if (cancellationTokenSource.IsCancellationRequested)
            //            {
            //                break;
            //            }
            //            CoreManager.RabbitMQPush("crawl", msg);
            //            Thread.Sleep(500);
            //            i++;
            //        } while (i < num);
            //        cancellationTokenSource.Cancel();
            //    }, cancellationTokenSource.Token);
            //}
            //else
            //{
            //    MessageBox.Show("发送信息不能为空!");
            //}
        }
    }
}
