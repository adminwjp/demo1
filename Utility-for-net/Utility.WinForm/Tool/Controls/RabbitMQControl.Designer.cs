namespace Tool.Controls
{
    partial class RabbitMQControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.发送信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.接收消息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelSendNum = new System.Windows.Forms.Label();
            this.textBoxSendNum = new System.Windows.Forms.TextBox();
            this.textBoxSendms = new System.Windows.Forms.TextBox();
            this.labelSendMsg = new System.Windows.Forms.Label();
            this.buttonSend = new System.Windows.Forms.Button();
            this.停止发送ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.发送信息ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(602, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 发送信息ToolStripMenuItem
            // 
            this.发送信息ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.接收消息ToolStripMenuItem,
            this.停止发送ToolStripMenuItem});
            this.发送信息ToolStripMenuItem.Name = "发送信息ToolStripMenuItem";
            this.发送信息ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.发送信息ToolStripMenuItem.Text = "发送信息";
            this.发送信息ToolStripMenuItem.Click += new System.EventHandler(this.发送信息ToolStripMenuItem_Click);
            // 
            // 接收消息ToolStripMenuItem
            // 
            this.接收消息ToolStripMenuItem.Name = "接收消息ToolStripMenuItem";
            this.接收消息ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.接收消息ToolStripMenuItem.Text = "接收消息";
            this.接收消息ToolStripMenuItem.Click += new System.EventHandler(this.接收消息ToolStripMenuItem_Click);
            // 
            // labelSendNum
            // 
            this.labelSendNum.AutoSize = true;
            this.labelSendNum.Location = new System.Drawing.Point(26, 56);
            this.labelSendNum.Name = "labelSendNum";
            this.labelSendNum.Size = new System.Drawing.Size(65, 12);
            this.labelSendNum.TabIndex = 1;
            this.labelSendNum.Text = "发送次数：";
            // 
            // textBoxSendNum
            // 
            this.textBoxSendNum.Location = new System.Drawing.Point(107, 53);
            this.textBoxSendNum.Name = "textBoxSendNum";
            this.textBoxSendNum.Size = new System.Drawing.Size(100, 21);
            this.textBoxSendNum.TabIndex = 2;
            // 
            // textBoxSendms
            // 
            this.textBoxSendms.Location = new System.Drawing.Point(298, 53);
            this.textBoxSendms.Name = "textBoxSendms";
            this.textBoxSendms.Size = new System.Drawing.Size(100, 21);
            this.textBoxSendms.TabIndex = 4;
            // 
            // labelSendMsg
            // 
            this.labelSendMsg.AutoSize = true;
            this.labelSendMsg.Location = new System.Drawing.Point(233, 56);
            this.labelSendMsg.Name = "labelSendMsg";
            this.labelSendMsg.Size = new System.Drawing.Size(59, 12);
            this.labelSendMsg.TabIndex = 3;
            this.labelSendMsg.Text = "发送内容:";
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(425, 51);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 5;
            this.buttonSend.Text = "发送";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // 停止发送ToolStripMenuItem
            // 
            this.停止发送ToolStripMenuItem.Name = "停止发送ToolStripMenuItem";
            this.停止发送ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.停止发送ToolStripMenuItem.Text = "停止发送";
            this.停止发送ToolStripMenuItem.Click += new System.EventHandler(this.停止发送ToolStripMenuItem_Click);
            // 
            // RabbitMQControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBoxSendms);
            this.Controls.Add(this.labelSendMsg);
            this.Controls.Add(this.textBoxSendNum);
            this.Controls.Add(this.labelSendNum);
            this.Controls.Add(this.menuStrip1);
            this.Name = "RabbitMQControl";
            this.Size = new System.Drawing.Size(602, 407);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 发送信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 接收消息ToolStripMenuItem;
        private System.Windows.Forms.Label labelSendNum;
        private System.Windows.Forms.TextBox textBoxSendNum;
        private System.Windows.Forms.TextBox textBoxSendms;
        private System.Windows.Forms.Label labelSendMsg;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.ToolStripMenuItem 停止发送ToolStripMenuItem;
    }
}
