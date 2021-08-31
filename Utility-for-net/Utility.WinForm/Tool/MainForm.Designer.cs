namespace Tool
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.net平台ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.字符串转义ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.docker工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.winForm工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rabbitMQ工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView = new System.Windows.Forms.TreeView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConsole = new System.Windows.Forms.TabPage();
            this.groupBoxTotalLog = new System.Windows.Forms.GroupBox();
            this.richTextBoxTotalLog = new System.Windows.Forms.RichTextBox();
            this.groupBoxLog = new System.Windows.Forms.GroupBox();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.实体工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageConsole.SuspendLayout();
            this.groupBoxTotalLog.SuspendLayout();
            this.groupBoxLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.工具ToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(864, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // 工具ToolStripMenuItem
            // 
            this.工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.net平台ToolStripMenuItem,
            this.字符串转义ToolStripMenuItem,
            this.docker工具ToolStripMenuItem,
            this.winForm工具ToolStripMenuItem,
            this.rabbitMQ工具ToolStripMenuItem,
            this.工具ToolStripMenuItem1,
            this.实体工具ToolStripMenuItem});
            this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            this.工具ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.工具ToolStripMenuItem.Text = "工具";
            // 
            // net平台ToolStripMenuItem
            // 
            this.net平台ToolStripMenuItem.Name = "net平台ToolStripMenuItem";
            this.net平台ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.net平台ToolStripMenuItem.Text = "Net平台";
            this.net平台ToolStripMenuItem.Click += new System.EventHandler(this.net平台ToolStripMenuItem_Click);
            // 
            // 字符串转义ToolStripMenuItem
            // 
            this.字符串转义ToolStripMenuItem.Name = "字符串转义ToolStripMenuItem";
            this.字符串转义ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.字符串转义ToolStripMenuItem.Text = "字符串转义";
            this.字符串转义ToolStripMenuItem.Click += new System.EventHandler(this.字符串转义ToolStripMenuItem_Click);
            // 
            // docker工具ToolStripMenuItem
            // 
            this.docker工具ToolStripMenuItem.Name = "docker工具ToolStripMenuItem";
            this.docker工具ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.docker工具ToolStripMenuItem.Text = "Docker工具";
            this.docker工具ToolStripMenuItem.Click += new System.EventHandler(this.docker工具ToolStripMenuItem_Click);
            // 
            // winForm工具ToolStripMenuItem
            // 
            this.winForm工具ToolStripMenuItem.Name = "winForm工具ToolStripMenuItem";
            this.winForm工具ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.winForm工具ToolStripMenuItem.Text = "WinForm工具";
            this.winForm工具ToolStripMenuItem.Click += new System.EventHandler(this.winForm工具ToolStripMenuItem_Click);
            // 
            // rabbitMQ工具ToolStripMenuItem
            // 
            this.rabbitMQ工具ToolStripMenuItem.Name = "rabbitMQ工具ToolStripMenuItem";
            this.rabbitMQ工具ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rabbitMQ工具ToolStripMenuItem.Text = "RabbitMQ工具";
            this.rabbitMQ工具ToolStripMenuItem.Click += new System.EventHandler(this.rabbitMQ工具ToolStripMenuItem_Click);
            // 
            // 工具ToolStripMenuItem1
            // 
            this.工具ToolStripMenuItem1.Name = "工具ToolStripMenuItem1";
            this.工具ToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.工具ToolStripMenuItem1.Text = "工具";
            this.工具ToolStripMenuItem1.Click += new System.EventHandler(this.工具ToolStripMenuItem1_Click);
            // 
            // treeView
            // 
            this.treeView.Location = new System.Drawing.Point(0, 28);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(104, 508);
            this.treeView.TabIndex = 1;
            this.treeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView_DragDrop);
            this.treeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView_DragEnter);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelTime});
            this.statusStrip.Location = new System.Drawing.Point(0, 540);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(864, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabelTime
            // 
            this.toolStripStatusLabelTime.Name = "toolStripStatusLabelTime";
            this.toolStripStatusLabelTime.Size = new System.Drawing.Size(198, 17);
            this.toolStripStatusLabelTime.Text = "当前时间为：2011-11-12 12:00:00";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConsole);
            this.tabControl.Location = new System.Drawing.Point(111, 29);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(753, 507);
            this.tabControl.TabIndex = 3;
            // 
            // tabPageConsole
            // 
            this.tabPageConsole.Controls.Add(this.groupBoxTotalLog);
            this.tabPageConsole.Controls.Add(this.groupBoxLog);
            this.tabPageConsole.Location = new System.Drawing.Point(4, 22);
            this.tabPageConsole.Name = "tabPageConsole";
            this.tabPageConsole.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConsole.Size = new System.Drawing.Size(745, 481);
            this.tabPageConsole.TabIndex = 0;
            this.tabPageConsole.Text = "控制台";
            this.tabPageConsole.UseVisualStyleBackColor = true;
            // 
            // groupBoxTotalLog
            // 
            this.groupBoxTotalLog.Controls.Add(this.richTextBoxTotalLog);
            this.groupBoxTotalLog.Location = new System.Drawing.Point(4, 244);
            this.groupBoxTotalLog.Name = "groupBoxTotalLog";
            this.groupBoxTotalLog.Size = new System.Drawing.Size(735, 231);
            this.groupBoxTotalLog.TabIndex = 1;
            this.groupBoxTotalLog.TabStop = false;
            this.groupBoxTotalLog.Text = "统计区域";
            // 
            // richTextBoxTotalLog
            // 
            this.richTextBoxTotalLog.BackColor = System.Drawing.SystemColors.InfoText;
            this.richTextBoxTotalLog.ForeColor = System.Drawing.Color.Red;
            this.richTextBoxTotalLog.Location = new System.Drawing.Point(2, 20);
            this.richTextBoxTotalLog.Name = "richTextBoxTotalLog";
            this.richTextBoxTotalLog.Size = new System.Drawing.Size(730, 205);
            this.richTextBoxTotalLog.TabIndex = 0;
            this.richTextBoxTotalLog.Text = "";
            // 
            // groupBoxLog
            // 
            this.groupBoxLog.Controls.Add(this.richTextBoxLog);
            this.groupBoxLog.Location = new System.Drawing.Point(4, 7);
            this.groupBoxLog.Name = "groupBoxLog";
            this.groupBoxLog.Size = new System.Drawing.Size(735, 231);
            this.groupBoxLog.TabIndex = 0;
            this.groupBoxLog.TabStop = false;
            this.groupBoxLog.Text = "日志区域";
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.BackColor = System.Drawing.SystemColors.InfoText;
            this.richTextBoxLog.ForeColor = System.Drawing.SystemColors.Info;
            this.richTextBoxLog.Location = new System.Drawing.Point(2, 20);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(730, 205);
            this.richTextBoxLog.TabIndex = 0;
            this.richTextBoxLog.Text = "";
            // 
            // 实体工具ToolStripMenuItem
            // 
            this.实体工具ToolStripMenuItem.Name = "实体工具ToolStripMenuItem";
            this.实体工具ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.实体工具ToolStripMenuItem.Text = "实体工具";
            this.实体工具ToolStripMenuItem.Click += new System.EventHandler(this.实体工具ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 562);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "主窗体";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageConsole.ResumeLayout(false);
            this.groupBoxTotalLog.ResumeLayout(false);
            this.groupBoxLog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem 工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem net平台ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 字符串转义ToolStripMenuItem;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTime;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConsole;
        private System.Windows.Forms.GroupBox groupBoxLog;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.GroupBox groupBoxTotalLog;
        private System.Windows.Forms.RichTextBox richTextBoxTotalLog;
        private System.Windows.Forms.ToolStripMenuItem docker工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem winForm工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rabbitMQ工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工具ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 实体工具ToolStripMenuItem;
    }
}

