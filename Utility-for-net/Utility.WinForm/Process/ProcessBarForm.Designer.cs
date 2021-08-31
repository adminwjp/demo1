namespace WinForm.Process
{
    partial class ProcessBarForm
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
            this.label_msg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_msg
            // 
            this.label_msg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label_msg.Location = new System.Drawing.Point(3, -1);
            this.label_msg.Name = "label_msg";
            this.label_msg.Size = new System.Drawing.Size(450, 83);
            this.label_msg.TabIndex = 0;
            this.label_msg.Text = "提示信息";
            this.label_msg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProcessBarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 82);
            this.Controls.Add(this.label_msg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProcessBarForm";
            this.Text = "ProcessBarForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_msg;
    }
}