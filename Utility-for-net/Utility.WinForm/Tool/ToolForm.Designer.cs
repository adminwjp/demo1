namespace Tool
{
    partial class ToolForm
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
            this.rtxb_expl = new System.Windows.Forms.RichTextBox();
            this.rtxb_code = new System.Windows.Forms.RichTextBox();
            this.lbl_expl = new System.Windows.Forms.Label();
            this.lbl_code = new System.Windows.Forms.Label();
            this.btn_parse = new System.Windows.Forms.Button();
            this.lbl_msg = new System.Windows.Forms.Label();
            this.btn_exit = new System.Windows.Forms.Button();
            this.rb_one = new System.Windows.Forms.RadioButton();
            this.rb_two = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rb_three = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtxb_expl
            // 
            this.rtxb_expl.Location = new System.Drawing.Point(49, 54);
            this.rtxb_expl.Name = "rtxb_expl";
            this.rtxb_expl.Size = new System.Drawing.Size(321, 383);
            this.rtxb_expl.TabIndex = 1;
            this.rtxb_expl.Text = "";
            // 
            // rtxb_code
            // 
            this.rtxb_code.Location = new System.Drawing.Point(466, 54);
            this.rtxb_code.Name = "rtxb_code";
            this.rtxb_code.Size = new System.Drawing.Size(342, 383);
            this.rtxb_code.TabIndex = 2;
            this.rtxb_code.Text = "";
            // 
            // lbl_expl
            // 
            this.lbl_expl.AutoSize = true;
            this.lbl_expl.Location = new System.Drawing.Point(12, 39);
            this.lbl_expl.Name = "lbl_expl";
            this.lbl_expl.Size = new System.Drawing.Size(41, 12);
            this.lbl_expl.TabIndex = 3;
            this.lbl_expl.Text = "示例：";
            // 
            // lbl_code
            // 
            this.lbl_code.AutoSize = true;
            this.lbl_code.Location = new System.Drawing.Point(416, 39);
            this.lbl_code.Name = "lbl_code";
            this.lbl_code.Size = new System.Drawing.Size(35, 12);
            this.lbl_code.TabIndex = 4;
            this.lbl_code.Text = "代码:";
            // 
            // btn_parse
            // 
            this.btn_parse.Location = new System.Drawing.Point(834, 236);
            this.btn_parse.Name = "btn_parse";
            this.btn_parse.Size = new System.Drawing.Size(75, 23);
            this.btn_parse.TabIndex = 5;
            this.btn_parse.Text = "转换";
            this.btn_parse.UseVisualStyleBackColor = true;
            this.btn_parse.Click += new System.EventHandler(this.btn_parse_Click);
            // 
            // lbl_msg
            // 
            this.lbl_msg.AutoSize = true;
            this.lbl_msg.Location = new System.Drawing.Point(47, 471);
            this.lbl_msg.Name = "lbl_msg";
            this.lbl_msg.Size = new System.Drawing.Size(53, 12);
            this.lbl_msg.TabIndex = 6;
            this.lbl_msg.Text = "转换成功";
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(834, 305);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(75, 23);
            this.btn_exit.TabIndex = 9;
            this.btn_exit.Text = "取消";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // rb_one
            // 
            this.rb_one.AutoSize = true;
            this.rb_one.Checked = true;
            this.rb_one.Location = new System.Drawing.Point(12, 10);
            this.rb_one.Name = "rb_one";
            this.rb_one.Size = new System.Drawing.Size(65, 16);
            this.rb_one.TabIndex = 10;
            this.rb_one.TabStop = true;
            this.rb_one.Text = "示例一:";
            this.rb_one.UseVisualStyleBackColor = true;
            this.rb_one.CheckedChanged += new System.EventHandler(this.rb_one_CheckedChanged);
            // 
            // rb_two
            // 
            this.rb_two.AutoSize = true;
            this.rb_two.Location = new System.Drawing.Point(98, 10);
            this.rb_two.Name = "rb_two";
            this.rb_two.Size = new System.Drawing.Size(65, 16);
            this.rb_two.TabIndex = 11;
            this.rb_two.Text = "示例二:";
            this.rb_two.UseVisualStyleBackColor = true;
            this.rb_two.CheckedChanged += new System.EventHandler(this.rb_two_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rb_three);
            this.panel1.Controls.Add(this.rb_one);
            this.panel1.Controls.Add(this.rb_two);
            this.panel1.Location = new System.Drawing.Point(14, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(393, 34);
            this.panel1.TabIndex = 12;
            // 
            // rb_three
            // 
            this.rb_three.AutoSize = true;
            this.rb_three.Location = new System.Drawing.Point(178, 10);
            this.rb_three.Name = "rb_three";
            this.rb_three.Size = new System.Drawing.Size(65, 16);
            this.rb_three.TabIndex = 12;
            this.rb_three.Text = "示例三:";
            this.rb_three.UseVisualStyleBackColor = true;
            // 
            // ToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 503);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.lbl_msg);
            this.Controls.Add(this.btn_parse);
            this.Controls.Add(this.lbl_code);
            this.Controls.Add(this.lbl_expl);
            this.Controls.Add(this.rtxb_code);
            this.Controls.Add(this.rtxb_expl);
            this.Name = "ToolForm";
            this.Text = "ToolForm";
            this.Load += new System.EventHandler(this.ToolForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox rtxb_expl;
        private System.Windows.Forms.RichTextBox rtxb_code;
        private System.Windows.Forms.Label lbl_expl;
        private System.Windows.Forms.Label lbl_code;
        private System.Windows.Forms.Button btn_parse;
        private System.Windows.Forms.Label lbl_msg;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.RadioButton rb_one;
        private System.Windows.Forms.RadioButton rb_two;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rb_three;
    }
}