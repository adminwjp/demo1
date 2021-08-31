namespace Utility.WinForm.Ctrls
{
    partial class PageControl
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
            this.lbl_pre = new System.Windows.Forms.Label();
            this.btn_redrict = new System.Windows.Forms.Button();
            this.txt_page = new System.Windows.Forms.TextBox();
            this.cbo_size = new System.Windows.Forms.ComboBox();
            this.lbl_next = new System.Windows.Forms.Label();
            this.lbl_last = new System.Windows.Forms.Label();
            this.lbl_msg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_pre
            // 
            this.lbl_pre.AutoSize = true;
            this.lbl_pre.Location = new System.Drawing.Point(433, 105);
            this.lbl_pre.Name = "lbl_pre";
            this.lbl_pre.Size = new System.Drawing.Size(41, 12);
            this.lbl_pre.TabIndex = 0;
            this.lbl_pre.Text = "上一页";
            this.lbl_pre.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_pre_MouseClick);
            // 
            // btn_redrict
            // 
            this.btn_redrict.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_redrict.Location = new System.Drawing.Point(899, 96);
            this.btn_redrict.Name = "btn_redrict";
            this.btn_redrict.Size = new System.Drawing.Size(52, 31);
            this.btn_redrict.TabIndex = 1;
            this.btn_redrict.Text = "跳转";
            this.btn_redrict.UseVisualStyleBackColor = true;
            // 
            // txt_page
            // 
            this.txt_page.Location = new System.Drawing.Point(858, 96);
            this.txt_page.Multiline = true;
            this.txt_page.Name = "txt_page";
            this.txt_page.Size = new System.Drawing.Size(31, 31);
            this.txt_page.TabIndex = 2;
            this.txt_page.TextChanged += new System.EventHandler(this.txt_page_TextChanged);
            // 
            // cbo_size
            // 
            this.cbo_size.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbo_size.FormattingEnabled = true;
            this.cbo_size.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "25",
            "30"});
            this.cbo_size.Location = new System.Drawing.Point(766, 98);
            this.cbo_size.Name = "cbo_size";
            this.cbo_size.Size = new System.Drawing.Size(71, 29);
            this.cbo_size.TabIndex = 3;
            this.cbo_size.SelectedIndexChanged += new System.EventHandler(this.cbo_size_SelectedIndexChanged);
            // 
            // lbl_next
            // 
            this.lbl_next.AutoSize = true;
            this.lbl_next.Location = new System.Drawing.Point(505, 106);
            this.lbl_next.Name = "lbl_next";
            this.lbl_next.Size = new System.Drawing.Size(41, 12);
            this.lbl_next.TabIndex = 4;
            this.lbl_next.Text = "下一页";
            this.lbl_next.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_next_MouseClick);
            // 
            // lbl_last
            // 
            this.lbl_last.AutoSize = true;
            this.lbl_last.Location = new System.Drawing.Point(697, 106);
            this.lbl_last.Name = "lbl_last";
            this.lbl_last.Size = new System.Drawing.Size(29, 12);
            this.lbl_last.TabIndex = 5;
            this.lbl_last.Text = "尾页";
            this.lbl_last.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_last_MouseClick);
            // 
            // lbl_msg
            // 
            this.lbl_msg.AutoSize = true;
            this.lbl_msg.Location = new System.Drawing.Point(37, 105);
            this.lbl_msg.Name = "lbl_msg";
            this.lbl_msg.Size = new System.Drawing.Size(359, 12);
            this.lbl_msg.TabIndex = 6;
            this.lbl_msg.Text = "当前第2页，每页10条数据，共有15条数据，当页5条数据，共有2页";
            // 
            // PageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_msg);
            this.Controls.Add(this.lbl_last);
            this.Controls.Add(this.lbl_next);
            this.Controls.Add(this.cbo_size);
            this.Controls.Add(this.txt_page);
            this.Controls.Add(this.btn_redrict);
            this.Controls.Add(this.lbl_pre);
            this.Name = "PageControl";
            this.Size = new System.Drawing.Size(1004, 182);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_pre;
        private System.Windows.Forms.Button btn_redrict;
        private System.Windows.Forms.TextBox txt_page;
        private System.Windows.Forms.ComboBox cbo_size;
        private System.Windows.Forms.Label lbl_next;
        private System.Windows.Forms.Label lbl_last;
        private System.Windows.Forms.Label lbl_msg;
    }
}
