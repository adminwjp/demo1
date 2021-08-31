namespace Tool
{
    partial class ToolControl
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
            this.components = new System.ComponentModel.Container();
            this.rtxt_original = new System.Windows.Forms.RichTextBox();
            this.rtxt_current = new System.Windows.Forms.RichTextBox();
            this.btn_parse = new System.Windows.Forms.Button();
            this.btn_clean = new System.Windows.Forms.Button();
            this.cmeustrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.示例1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_sample = new System.Windows.Forms.Label();
            this.cbo_sample = new System.Windows.Forms.ComboBox();
            this.btn_sample = new System.Windows.Forms.Button();
            this.btn_addSample = new System.Windows.Forms.Button();
            this.btn_parseString = new System.Windows.Forms.Button();
            this.txt_parseFormat = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lbl_解析格式 = new System.Windows.Forms.Label();
            this.cmeustrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtxt_original
            // 
            this.rtxt_original.Location = new System.Drawing.Point(24, 138);
            this.rtxt_original.Name = "rtxt_original";
            this.rtxt_original.Size = new System.Drawing.Size(465, 330);
            this.rtxt_original.TabIndex = 0;
            this.rtxt_original.Text = "";
            // 
            // rtxt_current
            // 
            this.rtxt_current.Location = new System.Drawing.Point(529, 138);
            this.rtxt_current.Name = "rtxt_current";
            this.rtxt_current.Size = new System.Drawing.Size(465, 330);
            this.rtxt_current.TabIndex = 1;
            this.rtxt_current.Text = "";
            // 
            // btn_parse
            // 
            this.btn_parse.Location = new System.Drawing.Point(501, 64);
            this.btn_parse.Name = "btn_parse";
            this.btn_parse.Size = new System.Drawing.Size(75, 23);
            this.btn_parse.TabIndex = 2;
            this.btn_parse.Text = "转换";
            this.btn_parse.UseVisualStyleBackColor = true;
            this.btn_parse.Click += new System.EventHandler(this.Btn_parse_Click);
            // 
            // btn_clean
            // 
            this.btn_clean.Location = new System.Drawing.Point(595, 64);
            this.btn_clean.Name = "btn_clean";
            this.btn_clean.Size = new System.Drawing.Size(75, 23);
            this.btn_clean.TabIndex = 3;
            this.btn_clean.Text = "清空";
            this.btn_clean.UseVisualStyleBackColor = true;
            // 
            // cmeustrip
            // 
            this.cmeustrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.示例1ToolStripMenuItem});
            this.cmeustrip.Name = "cmeustrip";
            this.cmeustrip.Size = new System.Drawing.Size(108, 26);
            // 
            // 示例1ToolStripMenuItem
            // 
            this.示例1ToolStripMenuItem.Name = "示例1ToolStripMenuItem";
            this.示例1ToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.示例1ToolStripMenuItem.Text = "示例1";
            // 
            // lbl_sample
            // 
            this.lbl_sample.AutoSize = true;
            this.lbl_sample.Location = new System.Drawing.Point(220, 69);
            this.lbl_sample.Name = "lbl_sample";
            this.lbl_sample.Size = new System.Drawing.Size(41, 12);
            this.lbl_sample.TabIndex = 5;
            this.lbl_sample.Text = "样例：";
            // 
            // cbo_sample
            // 
            this.cbo_sample.FormattingEnabled = true;
            this.cbo_sample.Location = new System.Drawing.Point(277, 65);
            this.cbo_sample.Name = "cbo_sample";
            this.cbo_sample.Size = new System.Drawing.Size(121, 20);
            this.cbo_sample.TabIndex = 6;
            // 
            // btn_sample
            // 
            this.btn_sample.Location = new System.Drawing.Point(420, 64);
            this.btn_sample.Name = "btn_sample";
            this.btn_sample.Size = new System.Drawing.Size(75, 23);
            this.btn_sample.TabIndex = 7;
            this.btn_sample.Text = "样例";
            this.btn_sample.UseVisualStyleBackColor = true;
            // 
            // btn_addSample
            // 
            this.btn_addSample.Location = new System.Drawing.Point(698, 63);
            this.btn_addSample.Name = "btn_addSample";
            this.btn_addSample.Size = new System.Drawing.Size(75, 23);
            this.btn_addSample.TabIndex = 8;
            this.btn_addSample.Text = "添加样例";
            this.btn_addSample.UseVisualStyleBackColor = true;
            // 
            // btn_parseString
            // 
            this.btn_parseString.Location = new System.Drawing.Point(813, 65);
            this.btn_parseString.Name = "btn_parseString";
            this.btn_parseString.Size = new System.Drawing.Size(75, 23);
            this.btn_parseString.TabIndex = 9;
            this.btn_parseString.Text = "转换为字符串";
            this.btn_parseString.UseVisualStyleBackColor = true;
            this.btn_parseString.Click += new System.EventHandler(this.Btn_parseString_Click);
            // 
            // txt_parseFormat
            // 
            this.txt_parseFormat.Location = new System.Drawing.Point(103, 64);
            this.txt_parseFormat.Name = "txt_parseFormat";
            this.txt_parseFormat.Size = new System.Drawing.Size(100, 21);
            this.txt_parseFormat.TabIndex = 10;
            this.txt_parseFormat.Text = "n";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // lbl_解析格式
            // 
            this.lbl_解析格式.AutoSize = true;
            this.lbl_解析格式.Location = new System.Drawing.Point(32, 69);
            this.lbl_解析格式.Name = "lbl_解析格式";
            this.lbl_解析格式.Size = new System.Drawing.Size(65, 12);
            this.lbl_解析格式.TabIndex = 12;
            this.lbl_解析格式.Text = "解析格式：";
            // 
            // ToolControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_解析格式);
            this.Controls.Add(this.txt_parseFormat);
            this.Controls.Add(this.btn_parseString);
            this.Controls.Add(this.btn_addSample);
            this.Controls.Add(this.btn_sample);
            this.Controls.Add(this.cbo_sample);
            this.Controls.Add(this.lbl_sample);
            this.Controls.Add(this.btn_clean);
            this.Controls.Add(this.btn_parse);
            this.Controls.Add(this.rtxt_current);
            this.Controls.Add(this.rtxt_original);
            this.Name = "ToolControl";
            this.Size = new System.Drawing.Size(1045, 533);
            this.cmeustrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxt_original;
        private System.Windows.Forms.RichTextBox rtxt_current;
        private System.Windows.Forms.Button btn_parse;
        private System.Windows.Forms.Button btn_clean;
        private System.Windows.Forms.ContextMenuStrip cmeustrip;
        private System.Windows.Forms.ToolStripMenuItem 示例1ToolStripMenuItem;
        private System.Windows.Forms.Label lbl_sample;
        private System.Windows.Forms.ComboBox cbo_sample;
        private System.Windows.Forms.Button btn_sample;
        private System.Windows.Forms.Button btn_addSample;
        private System.Windows.Forms.Button btn_parseString;
        private System.Windows.Forms.TextBox txt_parseFormat;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label lbl_解析格式;
    }
}
