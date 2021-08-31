namespace Tool.Controls
{
    partial class MongControl
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
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxSendms = new System.Windows.Forms.TextBox();
            this.labelSendMsg = new System.Windows.Forms.Label();
            this.textBoxSendNum = new System.Windows.Forms.TextBox();
            this.labelSendNum = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(440, 25);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 10;
            this.buttonSend.Text = "发送";
            this.buttonSend.UseVisualStyleBackColor = true;
            // 
            // textBoxSendms
            // 
            this.textBoxSendms.Location = new System.Drawing.Point(313, 27);
            this.textBoxSendms.Name = "textBoxSendms";
            this.textBoxSendms.Size = new System.Drawing.Size(100, 21);
            this.textBoxSendms.TabIndex = 9;
            // 
            // labelSendMsg
            // 
            this.labelSendMsg.AutoSize = true;
            this.labelSendMsg.Location = new System.Drawing.Point(248, 30);
            this.labelSendMsg.Name = "labelSendMsg";
            this.labelSendMsg.Size = new System.Drawing.Size(59, 12);
            this.labelSendMsg.TabIndex = 8;
            this.labelSendMsg.Text = "发送内容:";
            // 
            // textBoxSendNum
            // 
            this.textBoxSendNum.Location = new System.Drawing.Point(122, 27);
            this.textBoxSendNum.Name = "textBoxSendNum";
            this.textBoxSendNum.Size = new System.Drawing.Size(100, 21);
            this.textBoxSendNum.TabIndex = 7;
            // 
            // labelSendNum
            // 
            this.labelSendNum.AutoSize = true;
            this.labelSendNum.Location = new System.Drawing.Point(41, 30);
            this.labelSendNum.Name = "labelSendNum";
            this.labelSendNum.Size = new System.Drawing.Size(65, 12);
            this.labelSendNum.TabIndex = 6;
            this.labelSendNum.Text = "发送次数：";
            // 
            // MongControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBoxSendms);
            this.Controls.Add(this.labelSendMsg);
            this.Controls.Add(this.textBoxSendNum);
            this.Controls.Add(this.labelSendNum);
            this.Name = "MongControl";
            this.Size = new System.Drawing.Size(589, 445);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox textBoxSendms;
        private System.Windows.Forms.Label labelSendMsg;
        private System.Windows.Forms.TextBox textBoxSendNum;
        private System.Windows.Forms.Label labelSendNum;
    }
}
