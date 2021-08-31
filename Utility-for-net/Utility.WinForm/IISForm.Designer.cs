namespace WinForm.Admin
{
    partial class IISForm
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
            this.lbl_path = new System.Windows.Forms.Label();
            this.txt_path = new System.Windows.Forms.TextBox();
            this.btn_upload = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IISIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.WebsiteName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsWebsite = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ApplicationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pattern = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Restart = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Start = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Stop = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Realase = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pageControl1 = new Utility.WinForm.Ctrls.PageControl();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_update = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_restart = new System.Windows.Forms.Button();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_realase = new System.Windows.Forms.Button();
            this.chk_website = new System.Windows.Forms.CheckBox();
            this.lbl_version = new System.Windows.Forms.Label();
            this.cbo_version = new System.Windows.Forms.ComboBox();
            this.lbl_pattern = new System.Windows.Forms.Label();
            this.cbo_pattern = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_path
            // 
            this.lbl_path.AutoSize = true;
            this.lbl_path.Location = new System.Drawing.Point(311, 52);
            this.lbl_path.Name = "lbl_path";
            this.lbl_path.Size = new System.Drawing.Size(65, 12);
            this.lbl_path.TabIndex = 0;
            this.lbl_path.Text = "文件路劲：";
            // 
            // txt_path
            // 
            this.txt_path.Enabled = false;
            this.txt_path.Location = new System.Drawing.Point(382, 49);
            this.txt_path.Name = "txt_path";
            this.txt_path.Size = new System.Drawing.Size(173, 21);
            this.txt_path.TabIndex = 1;
            // 
            // btn_upload
            // 
            this.btn_upload.Location = new System.Drawing.Point(574, 46);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(75, 23);
            this.btn_upload.TabIndex = 2;
            this.btn_upload.Text = "上传";
            this.btn_upload.UseVisualStyleBackColor = true;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(655, 46);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 3;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.IISIcon,
            this.WebsiteName,
            this.Port,
            this.IsWebsite,
            this.ApplicationName,
            this.Version,
            this.Pattern,
            this.Restart,
            this.Start,
            this.Stop,
            this.Realase});
            this.dataGridView1.Location = new System.Drawing.Point(0, 164);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1187, 299);
            this.dataGridView1.TabIndex = 4;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "";
            this.Id.Name = "Id";
            this.Id.Width = 50;
            // 
            // IISIcon
            // 
            this.IISIcon.DataPropertyName = "Icon";
            this.IISIcon.HeaderText = "图标";
            this.IISIcon.Name = "IISIcon";
            // 
            // WebsiteName
            // 
            this.WebsiteName.DataPropertyName = "Name";
            this.WebsiteName.HeaderText = "网站名称";
            this.WebsiteName.Name = "WebsiteName";
            // 
            // Port
            // 
            this.Port.DataPropertyName = "Port";
            this.Port.FillWeight = 50F;
            this.Port.HeaderText = "端口";
            this.Port.Name = "Port";
            // 
            // IsWebsite
            // 
            this.IsWebsite.DataPropertyName = "IsWebsite";
            this.IsWebsite.HeaderText = "网站";
            this.IsWebsite.Name = "IsWebsite";
            this.IsWebsite.Width = 50;
            // 
            // ApplicationName
            // 
            this.ApplicationName.DataPropertyName = "ApplicationName";
            this.ApplicationName.HeaderText = "应用程序池名称";
            this.ApplicationName.Name = "ApplicationName";
            this.ApplicationName.Width = 150;
            // 
            // Version
            // 
            this.Version.DataPropertyName = "Version";
            this.Version.HeaderText = ".Net Framewok版本";
            this.Version.Name = "Version";
            this.Version.Width = 150;
            // 
            // Pattern
            // 
            this.Pattern.DataPropertyName = "Pattern";
            this.Pattern.HeaderText = "模式";
            this.Pattern.Name = "Pattern";
            // 
            // Restart
            // 
            this.Restart.DataPropertyName = "Restart";
            this.Restart.HeaderText = "重新启动";
            this.Restart.Name = "Restart";
            this.Restart.Text = "重新启动";
            // 
            // Start
            // 
            this.Start.DataPropertyName = "Start";
            this.Start.HeaderText = "启动";
            this.Start.Name = "Start";
            this.Start.Text = "启动";
            this.Start.Width = 80;
            // 
            // Stop
            // 
            this.Stop.DataPropertyName = "Stop";
            this.Stop.HeaderText = "停止";
            this.Stop.Name = "Stop";
            this.Stop.Text = "停止";
            this.Stop.Width = 80;
            // 
            // Realase
            // 
            this.Realase.DataPropertyName = "Realase";
            this.Realase.HeaderText = "回收";
            this.Realase.Name = "Realase";
            this.Realase.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Realase.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Realase.Width = 80;
            // 
            // pageControl1
            // 
            this.pageControl1.Location = new System.Drawing.Point(0, 469);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.Size = new System.Drawing.Size(1187, 66);
            this.pageControl1.TabIndex = 5;
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(106, 92);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 23);
            this.btn_add.TabIndex = 6;
            this.btn_add.Text = "添加";
            this.btn_add.UseVisualStyleBackColor = true;
            // 
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(211, 92);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(75, 23);
            this.btn_update.TabIndex = 7;
            this.btn_update.Text = "修改";
            this.btn_update.UseVisualStyleBackColor = true;
            // 
            // btn_delete
            // 
            this.btn_delete.Location = new System.Drawing.Point(324, 92);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(75, 23);
            this.btn_delete.TabIndex = 8;
            this.btn_delete.Text = "删除";
            this.btn_delete.UseVisualStyleBackColor = true;
            // 
            // btn_restart
            // 
            this.btn_restart.Location = new System.Drawing.Point(417, 92);
            this.btn_restart.Name = "btn_restart";
            this.btn_restart.Size = new System.Drawing.Size(75, 23);
            this.btn_restart.TabIndex = 9;
            this.btn_restart.Text = "重新启动";
            this.btn_restart.UseVisualStyleBackColor = true;
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(519, 92);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 10;
            this.btn_start.Text = "启动";
            this.btn_start.UseVisualStyleBackColor = true;
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(622, 92);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(75, 23);
            this.btn_stop.TabIndex = 11;
            this.btn_stop.Text = "停止";
            this.btn_stop.UseVisualStyleBackColor = true;
            // 
            // btn_realase
            // 
            this.btn_realase.Location = new System.Drawing.Point(717, 92);
            this.btn_realase.Name = "btn_realase";
            this.btn_realase.Size = new System.Drawing.Size(75, 23);
            this.btn_realase.TabIndex = 12;
            this.btn_realase.Text = "回收";
            this.btn_realase.UseVisualStyleBackColor = true;
            // 
            // chk_website
            // 
            this.chk_website.AutoSize = true;
            this.chk_website.Checked = true;
            this.chk_website.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_website.Location = new System.Drawing.Point(37, 96);
            this.chk_website.Name = "chk_website";
            this.chk_website.Size = new System.Drawing.Size(48, 16);
            this.chk_website.TabIndex = 13;
            this.chk_website.Text = "网站";
            this.chk_website.UseVisualStyleBackColor = true;
            // 
            // lbl_version
            // 
            this.lbl_version.AutoSize = true;
            this.lbl_version.Location = new System.Drawing.Point(35, 137);
            this.lbl_version.Name = "lbl_version";
            this.lbl_version.Size = new System.Drawing.Size(125, 12);
            this.lbl_version.TabIndex = 17;
            this.lbl_version.Text = ".Net Framework版本：";
            // 
            // cbo_version
            // 
            this.cbo_version.FormattingEnabled = true;
            this.cbo_version.Items.AddRange(new object[] {
            ".Net Framework 4.0.30319",
            ".Net Framework 2.0.50727",
            "无托管模式"});
            this.cbo_version.Location = new System.Drawing.Point(170, 134);
            this.cbo_version.Name = "cbo_version";
            this.cbo_version.Size = new System.Drawing.Size(116, 20);
            this.cbo_version.TabIndex = 16;
            // 
            // lbl_pattern
            // 
            this.lbl_pattern.AutoSize = true;
            this.lbl_pattern.Location = new System.Drawing.Point(311, 137);
            this.lbl_pattern.Name = "lbl_pattern";
            this.lbl_pattern.Size = new System.Drawing.Size(41, 12);
            this.lbl_pattern.TabIndex = 19;
            this.lbl_pattern.Text = "模式：";
            // 
            // cbo_pattern
            // 
            this.cbo_pattern.FormattingEnabled = true;
            this.cbo_pattern.Items.AddRange(new object[] {
            "集成",
            "经典"});
            this.cbo_pattern.Location = new System.Drawing.Point(358, 134);
            this.cbo_pattern.Name = "cbo_pattern";
            this.cbo_pattern.Size = new System.Drawing.Size(116, 20);
            this.cbo_pattern.TabIndex = 18;
            // 
            // IISForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 533);
            this.Controls.Add(this.lbl_pattern);
            this.Controls.Add(this.cbo_pattern);
            this.Controls.Add(this.lbl_version);
            this.Controls.Add(this.cbo_version);
            this.Controls.Add(this.chk_website);
            this.Controls.Add(this.btn_realase);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.btn_restart);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.pageControl1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_upload);
            this.Controls.Add(this.txt_path);
            this.Controls.Add(this.lbl_path);
            this.Name = "IISForm";
            this.Text = "IISForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_path;
        private System.Windows.Forms.TextBox txt_path;
        private System.Windows.Forms.Button btn_upload;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Id;
        private System.Windows.Forms.DataGridViewImageColumn IISIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn WebsiteName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Port;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsWebsite;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApplicationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pattern;
        private System.Windows.Forms.DataGridViewButtonColumn Restart;
        private System.Windows.Forms.DataGridViewButtonColumn Start;
        private System.Windows.Forms.DataGridViewButtonColumn Stop;
        private System.Windows.Forms.DataGridViewButtonColumn Realase;
        private Utility.WinForm.Ctrls.PageControl pageControl1;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btn_restart;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_realase;
        private System.Windows.Forms.CheckBox chk_website;
        private System.Windows.Forms.Label lbl_version;
        private System.Windows.Forms.ComboBox cbo_version;
        private System.Windows.Forms.Label lbl_pattern;
        private System.Windows.Forms.ComboBox cbo_pattern;
    }
}