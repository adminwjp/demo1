namespace Tool
{
    partial class StrToolControl
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
            this.gb_originial = new System.Windows.Forms.GroupBox();
            this.rtxt_originial = new System.Windows.Forms.RichTextBox();
            this.gb_result = new System.Windows.Forms.GroupBox();
            this.rtxt_result = new System.Windows.Forms.RichTextBox();
            this.gb_operator = new System.Windows.Forms.GroupBox();
            this.lbl_name = new System.Windows.Forms.Label();
            this.cbo_type = new System.Windows.Forms.ComboBox();
            this.btn_cleanTab = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_double = new System.Windows.Forms.Button();
            this.btn_single = new System.Windows.Forms.Button();
            this.gb_originial.SuspendLayout();
            this.gb_result.SuspendLayout();
            this.gb_operator.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_originial
            // 
            this.gb_originial.Controls.Add(this.rtxt_originial);
            this.gb_originial.Location = new System.Drawing.Point(13, 23);
            this.gb_originial.Name = "gb_originial";
            this.gb_originial.Size = new System.Drawing.Size(775, 170);
            this.gb_originial.TabIndex = 0;
            this.gb_originial.TabStop = false;
            this.gb_originial.Text = "转义内容";
            // 
            // rtxt_originial
            // 
            this.rtxt_originial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxt_originial.Location = new System.Drawing.Point(3, 17);
            this.rtxt_originial.Name = "rtxt_originial";
            this.rtxt_originial.Size = new System.Drawing.Size(769, 150);
            this.rtxt_originial.TabIndex = 0;
            this.rtxt_originial.Text = "";
            // 
            // gb_result
            // 
            this.gb_result.Controls.Add(this.rtxt_result);
            this.gb_result.Location = new System.Drawing.Point(13, 208);
            this.gb_result.Name = "gb_result";
            this.gb_result.Size = new System.Drawing.Size(775, 171);
            this.gb_result.TabIndex = 1;
            this.gb_result.TabStop = false;
            this.gb_result.Text = "转义结果";
            // 
            // rtxt_result
            // 
            this.rtxt_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxt_result.Location = new System.Drawing.Point(3, 17);
            this.rtxt_result.Name = "rtxt_result";
            this.rtxt_result.Size = new System.Drawing.Size(769, 151);
            this.rtxt_result.TabIndex = 1;
            this.rtxt_result.Text = "";
            // 
            // gb_operator
            // 
            this.gb_operator.Controls.Add(this.lbl_name);
            this.gb_operator.Controls.Add(this.cbo_type);
            this.gb_operator.Controls.Add(this.btn_cleanTab);
            this.gb_operator.Controls.Add(this.btn_clear);
            this.gb_operator.Controls.Add(this.btn_double);
            this.gb_operator.Controls.Add(this.btn_single);
            this.gb_operator.Location = new System.Drawing.Point(12, 395);
            this.gb_operator.Name = "gb_operator";
            this.gb_operator.Size = new System.Drawing.Size(775, 93);
            this.gb_operator.TabIndex = 2;
            this.gb_operator.TabStop = false;
            this.gb_operator.Text = "操作";
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Location = new System.Drawing.Point(15, 50);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(59, 12);
            this.lbl_name.TabIndex = 5;
            this.lbl_name.Text = "转换类型:";
            // 
            // cbo_type
            // 
            this.cbo_type.FormattingEnabled = true;
            this.cbo_type.Location = new System.Drawing.Point(80, 46);
            this.cbo_type.Name = "cbo_type";
            this.cbo_type.Size = new System.Drawing.Size(121, 20);
            this.cbo_type.TabIndex = 4;
            // 
            // btn_cleanTab
            // 
            this.btn_cleanTab.Location = new System.Drawing.Point(411, 44);
            this.btn_cleanTab.Name = "btn_cleanTab";
            this.btn_cleanTab.Size = new System.Drawing.Size(75, 23);
            this.btn_cleanTab.TabIndex = 3;
            this.btn_cleanTab.Text = "清空制表符";
            this.btn_cleanTab.UseVisualStyleBackColor = true;
            this.btn_cleanTab.Click += new System.EventHandler(this.btn_cleanTab_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(502, 44);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(75, 23);
            this.btn_clear.TabIndex = 2;
            this.btn_clear.Text = "清空";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_double
            // 
            this.btn_double.Location = new System.Drawing.Point(330, 44);
            this.btn_double.Name = "btn_double";
            this.btn_double.Size = new System.Drawing.Size(75, 23);
            this.btn_double.TabIndex = 1;
            this.btn_double.Text = "转义多字符串";
            this.btn_double.UseVisualStyleBackColor = true;
            this.btn_double.Click += new System.EventHandler(this.btn_double_Click);
            // 
            // btn_single
            // 
            this.btn_single.Location = new System.Drawing.Point(249, 45);
            this.btn_single.Name = "btn_single";
            this.btn_single.Size = new System.Drawing.Size(75, 23);
            this.btn_single.TabIndex = 0;
            this.btn_single.Text = "转义单字符串";
            this.btn_single.UseVisualStyleBackColor = true;
            this.btn_single.Click += new System.EventHandler(this.btn_single_Click);
            // 
            // StrToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 506);
            this.Controls.Add(this.gb_operator);
            this.Controls.Add(this.gb_result);
            this.Controls.Add(this.gb_originial);
            this.Name = "StrToolForm";
            this.Text = "转义工具";
            this.Load += new System.EventHandler(this.StrToolForm_Load);
            this.gb_originial.ResumeLayout(false);
            this.gb_result.ResumeLayout(false);
            this.gb_operator.ResumeLayout(false);
            this.gb_operator.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_originial;
        private System.Windows.Forms.GroupBox gb_result;
        private System.Windows.Forms.GroupBox gb_operator;
        private System.Windows.Forms.RichTextBox rtxt_originial;
        private System.Windows.Forms.RichTextBox rtxt_result;
        private System.Windows.Forms.Button btn_single;
        private System.Windows.Forms.Button btn_double;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_cleanTab;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.ComboBox cbo_type;
    }
}