namespace Tool
{
    partial class DockerToolControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.镜像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取所有镜像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取所有镜像IDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除镜像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.搜索镜像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.拉取镜像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止镜像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.启动镜像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.容器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取所有容器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取所有容器IDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除容器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.启动容器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.运行容器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止容器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.启动DockerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止DockerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.启动DockerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.labelImage = new System.Windows.Forms.Label();
            this.labelContainer = new System.Windows.Forms.Label();
            this.dataGridViewContainer = new Utility.WinForm.Ctrls.DataGridViewControl();
            this.dataGridViewImage = new Utility.WinForm.Ctrls.DataGridViewControl();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageList = new System.Windows.Forms.TabPage();
            this.tabPageOperator = new System.Windows.Forms.TabPage();
            this.richTextBoxCmd = new System.Windows.Forms.RichTextBox();
            this.labelCmd = new System.Windows.Forms.Label();
            this.labelRes = new System.Windows.Forms.Label();
            this.richTextBoxRes = new System.Windows.Forms.RichTextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonPull = new System.Windows.Forms.Button();
            this.buttonRunCmd = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageList.SuspendLayout();
            this.tabPageOperator.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.镜像ToolStripMenuItem,
            this.容器ToolStripMenuItem,
            this.启动DockerToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // 镜像ToolStripMenuItem
            // 
            this.镜像ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.获取所有镜像ToolStripMenuItem,
            this.获取所有镜像IDToolStripMenuItem,
            this.删除镜像ToolStripMenuItem,
            this.搜索镜像ToolStripMenuItem,
            this.拉取镜像ToolStripMenuItem,
            this.停止镜像ToolStripMenuItem,
            this.启动镜像ToolStripMenuItem});
            this.镜像ToolStripMenuItem.Name = "镜像ToolStripMenuItem";
            this.镜像ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.镜像ToolStripMenuItem.Text = "镜像";
            // 
            // 获取所有镜像ToolStripMenuItem
            // 
            this.获取所有镜像ToolStripMenuItem.Name = "获取所有镜像ToolStripMenuItem";
            this.获取所有镜像ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.获取所有镜像ToolStripMenuItem.Text = "获取所有镜像";
            this.获取所有镜像ToolStripMenuItem.Click += new System.EventHandler(this.获取所有镜像ToolStripMenuItem_Click);
            // 
            // 获取所有镜像IDToolStripMenuItem
            // 
            this.获取所有镜像IDToolStripMenuItem.Name = "获取所有镜像IDToolStripMenuItem";
            this.获取所有镜像IDToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.获取所有镜像IDToolStripMenuItem.Text = "获取所有镜像ID";
            this.获取所有镜像IDToolStripMenuItem.Click += new System.EventHandler(this.获取所有镜像IDToolStripMenuItem_Click);
            // 
            // 删除镜像ToolStripMenuItem
            // 
            this.删除镜像ToolStripMenuItem.Name = "删除镜像ToolStripMenuItem";
            this.删除镜像ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.删除镜像ToolStripMenuItem.Text = "删除镜像";
            this.删除镜像ToolStripMenuItem.Click += new System.EventHandler(this.删除镜像ToolStripMenuItem_Click);
            // 
            // 搜索镜像ToolStripMenuItem
            // 
            this.搜索镜像ToolStripMenuItem.Name = "搜索镜像ToolStripMenuItem";
            this.搜索镜像ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.搜索镜像ToolStripMenuItem.Text = "搜索镜像";
            this.搜索镜像ToolStripMenuItem.Click += new System.EventHandler(this.搜索镜像ToolStripMenuItem_Click);
            // 
            // 拉取镜像ToolStripMenuItem
            // 
            this.拉取镜像ToolStripMenuItem.Name = "拉取镜像ToolStripMenuItem";
            this.拉取镜像ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.拉取镜像ToolStripMenuItem.Text = "拉取镜像";
            this.拉取镜像ToolStripMenuItem.Click += new System.EventHandler(this.拉取镜像ToolStripMenuItem_Click);
            // 
            // 停止镜像ToolStripMenuItem
            // 
            this.停止镜像ToolStripMenuItem.Name = "停止镜像ToolStripMenuItem";
            this.停止镜像ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.停止镜像ToolStripMenuItem.Text = "停止镜像";
            this.停止镜像ToolStripMenuItem.Click += new System.EventHandler(this.停止镜像ToolStripMenuItem_Click);
            // 
            // 启动镜像ToolStripMenuItem
            // 
            this.启动镜像ToolStripMenuItem.Name = "启动镜像ToolStripMenuItem";
            this.启动镜像ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.启动镜像ToolStripMenuItem.Text = "启动镜像";
            this.启动镜像ToolStripMenuItem.Click += new System.EventHandler(this.启动镜像ToolStripMenuItem_Click);
            // 
            // 容器ToolStripMenuItem
            // 
            this.容器ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.获取所有容器ToolStripMenuItem,
            this.获取所有容器IDToolStripMenuItem,
            this.删除容器ToolStripMenuItem,
            this.启动容器ToolStripMenuItem,
            this.运行容器ToolStripMenuItem,
            this.停止容器ToolStripMenuItem});
            this.容器ToolStripMenuItem.Name = "容器ToolStripMenuItem";
            this.容器ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.容器ToolStripMenuItem.Text = "容器";
            // 
            // 获取所有容器ToolStripMenuItem
            // 
            this.获取所有容器ToolStripMenuItem.Name = "获取所有容器ToolStripMenuItem";
            this.获取所有容器ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.获取所有容器ToolStripMenuItem.Text = "获取所有容器";
            this.获取所有容器ToolStripMenuItem.Click += new System.EventHandler(this.获取所有容器ToolStripMenuItem_Click);
            // 
            // 获取所有容器IDToolStripMenuItem
            // 
            this.获取所有容器IDToolStripMenuItem.Name = "获取所有容器IDToolStripMenuItem";
            this.获取所有容器IDToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.获取所有容器IDToolStripMenuItem.Text = "获取所有容器ID";
            this.获取所有容器IDToolStripMenuItem.Click += new System.EventHandler(this.获取所有容器IDToolStripMenuItem_Click);
            // 
            // 删除容器ToolStripMenuItem
            // 
            this.删除容器ToolStripMenuItem.Name = "删除容器ToolStripMenuItem";
            this.删除容器ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.删除容器ToolStripMenuItem.Text = "删除容器";
            this.删除容器ToolStripMenuItem.Click += new System.EventHandler(this.删除容器ToolStripMenuItem_Click);
            // 
            // 启动容器ToolStripMenuItem
            // 
            this.启动容器ToolStripMenuItem.Name = "启动容器ToolStripMenuItem";
            this.启动容器ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.启动容器ToolStripMenuItem.Text = "启动容器";
            this.启动容器ToolStripMenuItem.Click += new System.EventHandler(this.启动容器ToolStripMenuItem_Click);
            // 
            // 运行容器ToolStripMenuItem
            // 
            this.运行容器ToolStripMenuItem.Name = "运行容器ToolStripMenuItem";
            this.运行容器ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.运行容器ToolStripMenuItem.Text = "运行容器";
            this.运行容器ToolStripMenuItem.Click += new System.EventHandler(this.运行容器ToolStripMenuItem_Click);
            // 
            // 停止容器ToolStripMenuItem
            // 
            this.停止容器ToolStripMenuItem.Name = "停止容器ToolStripMenuItem";
            this.停止容器ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.停止容器ToolStripMenuItem.Text = "停止容器";
            this.停止容器ToolStripMenuItem.Click += new System.EventHandler(this.停止容器ToolStripMenuItem_Click);
            // 
            // 启动DockerToolStripMenuItem
            // 
            this.启动DockerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.停止DockerToolStripMenuItem,
            this.启动DockerToolStripMenuItem1});
            this.启动DockerToolStripMenuItem.Name = "启动DockerToolStripMenuItem";
            this.启动DockerToolStripMenuItem.Size = new System.Drawing.Size(86, 21);
            this.启动DockerToolStripMenuItem.Text = "Docker操作";
            // 
            // 停止DockerToolStripMenuItem
            // 
            this.停止DockerToolStripMenuItem.Name = "停止DockerToolStripMenuItem";
            this.停止DockerToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.停止DockerToolStripMenuItem.Text = "停止Docker";
            this.停止DockerToolStripMenuItem.Click += new System.EventHandler(this.停止DockerToolStripMenuItem_Click);
            // 
            // 启动DockerToolStripMenuItem1
            // 
            this.启动DockerToolStripMenuItem1.Name = "启动DockerToolStripMenuItem1";
            this.启动DockerToolStripMenuItem1.Size = new System.Drawing.Size(142, 22);
            this.启动DockerToolStripMenuItem1.Text = "启动Docker";
            this.启动DockerToolStripMenuItem1.Click += new System.EventHandler(this.启动DockerToolStripMenuItem1_Click);
            // 
            // labelImage
            // 
            this.labelImage.AutoSize = true;
            this.labelImage.Location = new System.Drawing.Point(353, 20);
            this.labelImage.Name = "labelImage";
            this.labelImage.Size = new System.Drawing.Size(53, 12);
            this.labelImage.TabIndex = 3;
            this.labelImage.Text = "镜像列表";
            // 
            // labelContainer
            // 
            this.labelContainer.AutoSize = true;
            this.labelContainer.Location = new System.Drawing.Point(353, 215);
            this.labelContainer.Name = "labelContainer";
            this.labelContainer.Size = new System.Drawing.Size(53, 12);
            this.labelContainer.TabIndex = 4;
            this.labelContainer.Text = "容器列表";
            // 
            // dataGridViewContainer
            // 
            this.dataGridViewContainer.Location = new System.Drawing.Point(3, 240);
            this.dataGridViewContainer.Name = "dataGridViewContainer";
            this.dataGridViewContainer.Size = new System.Drawing.Size(786, 152);
            this.dataGridViewContainer.TabIndex = 5;
            // 
            // dataGridViewImage
            // 
            this.dataGridViewImage.Location = new System.Drawing.Point(3, 39);
            this.dataGridViewImage.Name = "dataGridViewImage";
            this.dataGridViewImage.Size = new System.Drawing.Size(786, 163);
            this.dataGridViewImage.TabIndex = 6;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageList);
            this.tabControl.Controls.Add(this.tabPageOperator);
            this.tabControl.Location = new System.Drawing.Point(0, 28);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 421);
            this.tabControl.TabIndex = 7;
            // 
            // tabPageList
            // 
            this.tabPageList.Controls.Add(this.labelImage);
            this.tabPageList.Controls.Add(this.dataGridViewContainer);
            this.tabPageList.Controls.Add(this.dataGridViewImage);
            this.tabPageList.Controls.Add(this.labelContainer);
            this.tabPageList.Location = new System.Drawing.Point(4, 22);
            this.tabPageList.Name = "tabPageList";
            this.tabPageList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageList.Size = new System.Drawing.Size(792, 395);
            this.tabPageList.TabIndex = 0;
            this.tabPageList.Text = "Docker列表";
            this.tabPageList.UseVisualStyleBackColor = true;
            // 
            // tabPageOperator
            // 
            this.tabPageOperator.Controls.Add(this.buttonSave);
            this.tabPageOperator.Controls.Add(this.buttonRunCmd);
            this.tabPageOperator.Controls.Add(this.buttonPull);
            this.tabPageOperator.Controls.Add(this.buttonSearch);
            this.tabPageOperator.Controls.Add(this.labelRes);
            this.tabPageOperator.Controls.Add(this.richTextBoxRes);
            this.tabPageOperator.Controls.Add(this.labelCmd);
            this.tabPageOperator.Controls.Add(this.richTextBoxCmd);
            this.tabPageOperator.Location = new System.Drawing.Point(4, 22);
            this.tabPageOperator.Name = "tabPageOperator";
            this.tabPageOperator.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOperator.Size = new System.Drawing.Size(792, 395);
            this.tabPageOperator.TabIndex = 1;
            this.tabPageOperator.Text = "Docker操作";
            this.tabPageOperator.UseVisualStyleBackColor = true;
            // 
            // richTextBoxCmd
            // 
            this.richTextBoxCmd.Location = new System.Drawing.Point(75, 23);
            this.richTextBoxCmd.Name = "richTextBoxCmd";
            this.richTextBoxCmd.Size = new System.Drawing.Size(437, 139);
            this.richTextBoxCmd.TabIndex = 0;
            this.richTextBoxCmd.Text = "";
            // 
            // labelCmd
            // 
            this.labelCmd.AutoSize = true;
            this.labelCmd.Location = new System.Drawing.Point(19, 26);
            this.labelCmd.Name = "labelCmd";
            this.labelCmd.Size = new System.Drawing.Size(41, 12);
            this.labelCmd.TabIndex = 1;
            this.labelCmd.Text = "命令：";
            // 
            // labelRes
            // 
            this.labelRes.AutoSize = true;
            this.labelRes.Location = new System.Drawing.Point(19, 199);
            this.labelRes.Name = "labelRes";
            this.labelRes.Size = new System.Drawing.Size(41, 12);
            this.labelRes.TabIndex = 3;
            this.labelRes.Text = "结果：";
            // 
            // richTextBoxRes
            // 
            this.richTextBoxRes.Location = new System.Drawing.Point(75, 196);
            this.richTextBoxRes.Name = "richTextBoxRes";
            this.richTextBoxRes.Size = new System.Drawing.Size(437, 139);
            this.richTextBoxRes.TabIndex = 2;
            this.richTextBoxRes.Text = "";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(614, 26);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 4;
            this.buttonSearch.Text = "搜索镜像";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonPull
            // 
            this.buttonPull.Location = new System.Drawing.Point(614, 78);
            this.buttonPull.Name = "buttonPull";
            this.buttonPull.Size = new System.Drawing.Size(75, 23);
            this.buttonPull.TabIndex = 5;
            this.buttonPull.Text = "拉取镜像";
            this.buttonPull.UseVisualStyleBackColor = true;
            this.buttonPull.Click += new System.EventHandler(this.buttonPull_Click);
            // 
            // buttonRunCmd
            // 
            this.buttonRunCmd.Location = new System.Drawing.Point(614, 139);
            this.buttonRunCmd.Name = "buttonRunCmd";
            this.buttonRunCmd.Size = new System.Drawing.Size(75, 23);
            this.buttonRunCmd.TabIndex = 6;
            this.buttonRunCmd.Text = "执行命令";
            this.buttonRunCmd.UseVisualStyleBackColor = true;
            this.buttonRunCmd.Click += new System.EventHandler(this.buttonRunCmd_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(614, 194);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "保存文件";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // DockerToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip);
            //this.MainMenuStrip = this.menuStrip;
            this.Name = "DockerToolForm";
            this.Text = "Docker工具";
            this.Load += new System.EventHandler(this.DockerToolForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageList.ResumeLayout(false);
            this.tabPageList.PerformLayout();
            this.tabPageOperator.ResumeLayout(false);
            this.tabPageOperator.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem 镜像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取所有镜像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取所有镜像IDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除镜像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 搜索镜像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 拉取镜像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止镜像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 启动镜像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 容器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 启动DockerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止DockerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 启动DockerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 获取所有容器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取所有容器IDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除容器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 启动容器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 运行容器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止容器ToolStripMenuItem;
        private System.Windows.Forms.Label labelImage;
        private System.Windows.Forms.Label labelContainer;
        private Utility.WinForm.Ctrls.DataGridViewControl dataGridViewContainer;
        private Utility.WinForm.Ctrls.DataGridViewControl dataGridViewImage;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageList;
        private System.Windows.Forms.TabPage tabPageOperator;
        private System.Windows.Forms.Label labelCmd;
        private System.Windows.Forms.RichTextBox richTextBoxCmd;
        private System.Windows.Forms.Label labelRes;
        private System.Windows.Forms.RichTextBox richTextBoxRes;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonPull;
        private System.Windows.Forms.Button buttonRunCmd;
        private System.Windows.Forms.Button buttonSave;
    }
}