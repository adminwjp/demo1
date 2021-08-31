namespace WinForm
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("配置样例");
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menu_config = new System.Windows.Forms.ToolStripMenuItem();
            this.status = new System.Windows.Forms.StatusStrip();
            this.lbl_time = new System.Windows.Forms.ToolStripStatusLabel();
            this.tree = new System.Windows.Forms.TreeView();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConsole = new System.Windows.Forms.TabPage();
            this.groupBoxLog = new System.Windows.Forms.GroupBox();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.menu.SuspendLayout();
            this.status.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageConsole.SuspendLayout();
            this.groupBoxLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_config});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(933, 25);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            this.menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menu_ItemClicked);
            // 
            // menu_config
            // 
            this.menu_config.Name = "menu_config";
            this.menu_config.Size = new System.Drawing.Size(68, 21);
            this.menu_config.Tag = "Config";
            this.menu_config.Text = "配置样例";
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_time});
            this.status.Location = new System.Drawing.Point(0, 364);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(933, 22);
            this.status.TabIndex = 1;
            this.status.Text = "statusStrip1";
            // 
            // lbl_time
            // 
            this.lbl_time.Name = "lbl_time";
            this.lbl_time.Size = new System.Drawing.Size(200, 17);
            this.lbl_time.Text = "当前时间为20121-01-10 10:000:00";
            // 
            // tree
            // 
            this.tree.Location = new System.Drawing.Point(0, 29);
            this.tree.Name = "tree";
            treeNode8.Name = "config";
            treeNode8.Tag = "Config";
            treeNode8.Text = "配置样例";
            this.tree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode8});
            this.tree.Size = new System.Drawing.Size(100, 328);
            this.tree.TabIndex = 2;
            this.tree.Click += new System.EventHandler(this.tree_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConsole);
            this.tabControl.Location = new System.Drawing.Point(107, 31);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(826, 329);
            this.tabControl.TabIndex = 4;
            // 
            // tabPageConsole
            // 
            this.tabPageConsole.Controls.Add(this.groupBoxLog);
            this.tabPageConsole.Location = new System.Drawing.Point(4, 26);
            this.tabPageConsole.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageConsole.Name = "tabPageConsole";
            this.tabPageConsole.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageConsole.Size = new System.Drawing.Size(818, 299);
            this.tabPageConsole.TabIndex = 0;
            this.tabPageConsole.Text = "控制台";
            this.tabPageConsole.UseVisualStyleBackColor = true;
            // 
            // groupBoxLog
            // 
            this.groupBoxLog.Controls.Add(this.richTextBoxLog);
            this.groupBoxLog.Location = new System.Drawing.Point(5, 4);
            this.groupBoxLog.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxLog.Name = "groupBoxLog";
            this.groupBoxLog.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxLog.Size = new System.Drawing.Size(809, 291);
            this.groupBoxLog.TabIndex = 0;
            this.groupBoxLog.TabStop = false;
            this.groupBoxLog.Text = "日志区域";
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.BackColor = System.Drawing.SystemColors.HotTrack;
            this.richTextBoxLog.ForeColor = System.Drawing.SystemColors.Info;
            this.richTextBoxLog.Location = new System.Drawing.Point(8, 24);
            this.richTextBoxLog.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(797, 259);
            this.richTextBoxLog.TabIndex = 0;
            this.richTextBoxLog.Text = "         ";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 386);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.tree);
            this.Controls.Add(this.status);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "后台管理";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageConsole.ResumeLayout(false);
            this.groupBoxLog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem menu_config;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel lbl_time;
        private System.Windows.Forms.TreeView tree;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConsole;
        private System.Windows.Forms.GroupBox groupBoxLog;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
    }
}

