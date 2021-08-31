
namespace WinForm.Example.Config
{
    partial class ConfigControl
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
            this.gb_config = new System.Windows.Forms.GroupBox();
            this.gb_service = new System.Windows.Forms.GroupBox();
            this.panel_config = new System.Windows.Forms.Panel();
            this.lbl_name = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.dgv_config = new System.Windows.Forms.DataGridView();
            this.lbl_flag = new System.Windows.Forms.Label();
            this.cbo_flag = new System.Windows.Forms.ComboBox();
            this.btn_query = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nmae = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddressTemplate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Flag = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pwd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel_config_page = new System.Windows.Forms.Panel();
            this.llbl_index = new System.Windows.Forms.LinkLabel();
            this.llbl_pre = new System.Windows.Forms.LinkLabel();
            this.llbl_next = new System.Windows.Forms.LinkLabel();
            this.llbl_last = new System.Windows.Forms.LinkLabel();
            this.lbl_records = new System.Windows.Forms.Label();
            this.cbo_sizes = new System.Windows.Forms.ComboBox();
            this.txt_page = new System.Windows.Forms.TextBox();
            this.btn_redirect = new System.Windows.Forms.Button();
            this.lbl_exmapl_config = new System.Windows.Forms.Label();
            this.gb_config.SuspendLayout();
            this.panel_config.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_config)).BeginInit();
            this.panel_config_page.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_config
            // 
            this.gb_config.Controls.Add(this.dgv_config);
            this.gb_config.Controls.Add(this.panel_config);
            this.gb_config.Location = new System.Drawing.Point(0, 40);
            this.gb_config.Name = "gb_config";
            this.gb_config.Size = new System.Drawing.Size(1152, 192);
            this.gb_config.TabIndex = 0;
            this.gb_config.TabStop = false;
            this.gb_config.Text = "配置信息";
            // 
            // gb_service
            // 
            this.gb_service.Location = new System.Drawing.Point(0, 285);
            this.gb_service.Name = "gb_service";
            this.gb_service.Size = new System.Drawing.Size(1152, 166);
            this.gb_service.TabIndex = 1;
            this.gb_service.TabStop = false;
            this.gb_service.Text = "服务信息";
            // 
            // panel_config
            // 
            this.panel_config.Controls.Add(this.btn_cancel);
            this.panel_config.Controls.Add(this.btn_query);
            this.panel_config.Controls.Add(this.cbo_flag);
            this.panel_config.Controls.Add(this.lbl_flag);
            this.panel_config.Controls.Add(this.txt_name);
            this.panel_config.Controls.Add(this.lbl_name);
            this.panel_config.Location = new System.Drawing.Point(0, 23);
            this.panel_config.Name = "panel_config";
            this.panel_config.Size = new System.Drawing.Size(1146, 66);
            this.panel_config.TabIndex = 0;
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Location = new System.Drawing.Point(23, 20);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(32, 17);
            this.lbl_name.TabIndex = 0;
            this.lbl_name.Text = "名称";
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(72, 17);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(100, 23);
            this.txt_name.TabIndex = 1;
            // 
            // dgv_config
            // 
            this.dgv_config.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_config.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Nmae,
            this.AddressTemplate,
            this.Ip,
            this.Port,
            this.Flag,
            this.User,
            this.Pwd,
            this.Desc,
            this.CreateDate,
            this.UpdateDate});
            this.dgv_config.Location = new System.Drawing.Point(6, 111);
            this.dgv_config.Name = "dgv_config";
            this.dgv_config.RowTemplate.Height = 25;
            this.dgv_config.Size = new System.Drawing.Size(1144, 128);
            this.dgv_config.TabIndex = 1;
            // 
            // lbl_flag
            // 
            this.lbl_flag.AutoSize = true;
            this.lbl_flag.Location = new System.Drawing.Point(198, 20);
            this.lbl_flag.Name = "lbl_flag";
            this.lbl_flag.Size = new System.Drawing.Size(32, 17);
            this.lbl_flag.TabIndex = 2;
            this.lbl_flag.Text = "标识";
            // 
            // cbo_flag
            // 
            this.cbo_flag.FormattingEnabled = true;
            this.cbo_flag.Location = new System.Drawing.Point(245, 15);
            this.cbo_flag.Name = "cbo_flag";
            this.cbo_flag.Size = new System.Drawing.Size(121, 25);
            this.cbo_flag.TabIndex = 3;
            // 
            // btn_query
            // 
            this.btn_query.Location = new System.Drawing.Point(388, 14);
            this.btn_query.Name = "btn_query";
            this.btn_query.Size = new System.Drawing.Size(75, 23);
            this.btn_query.TabIndex = 4;
            this.btn_query.Text = "查询";
            this.btn_query.UseVisualStyleBackColor = true;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(487, 14);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 5;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // Nmae
            // 
            this.Nmae.HeaderText = "名称";
            this.Nmae.Name = "Nmae";
            // 
            // AddressTemplate
            // 
            this.AddressTemplate.HeaderText = "地址模板";
            this.AddressTemplate.Name = "AddressTemplate";
            // 
            // Ip
            // 
            this.Ip.HeaderText = "Ip";
            this.Ip.Name = "Ip";
            // 
            // Port
            // 
            this.Port.HeaderText = "端口";
            this.Port.Name = "Port";
            // 
            // Flag
            // 
            this.Flag.HeaderText = "标识";
            this.Flag.Name = "Flag";
            this.Flag.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Flag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // User
            // 
            this.User.HeaderText = "账户";
            this.User.Name = "User";
            // 
            // Pwd
            // 
            this.Pwd.HeaderText = "密码";
            this.Pwd.Name = "Pwd";
            // 
            // Desc
            // 
            this.Desc.HeaderText = "描述";
            this.Desc.Name = "Desc";
            // 
            // CreateDate
            // 
            this.CreateDate.HeaderText = "创建时间";
            this.CreateDate.Name = "CreateDate";
            this.CreateDate.ReadOnly = true;
            // 
            // UpdateDate
            // 
            this.UpdateDate.HeaderText = "修改时间";
            this.UpdateDate.Name = "UpdateDate";
            this.UpdateDate.ReadOnly = true;
            // 
            // panel_config_page
            // 
            this.panel_config_page.Controls.Add(this.btn_redirect);
            this.panel_config_page.Controls.Add(this.txt_page);
            this.panel_config_page.Controls.Add(this.cbo_sizes);
            this.panel_config_page.Controls.Add(this.lbl_records);
            this.panel_config_page.Controls.Add(this.llbl_last);
            this.panel_config_page.Controls.Add(this.llbl_next);
            this.panel_config_page.Controls.Add(this.llbl_pre);
            this.panel_config_page.Controls.Add(this.llbl_index);
            this.panel_config_page.Location = new System.Drawing.Point(6, 248);
            this.panel_config_page.Name = "panel_config_page";
            this.panel_config_page.Size = new System.Drawing.Size(1140, 31);
            this.panel_config_page.TabIndex = 2;
            // 
            // llbl_index
            // 
            this.llbl_index.AutoSize = true;
            this.llbl_index.Location = new System.Drawing.Point(305, 7);
            this.llbl_index.Name = "llbl_index";
            this.llbl_index.Size = new System.Drawing.Size(32, 17);
            this.llbl_index.TabIndex = 0;
            this.llbl_index.TabStop = true;
            this.llbl_index.Text = "首页";
            // 
            // llbl_pre
            // 
            this.llbl_pre.AutoSize = true;
            this.llbl_pre.Location = new System.Drawing.Point(391, 7);
            this.llbl_pre.Name = "llbl_pre";
            this.llbl_pre.Size = new System.Drawing.Size(48, 17);
            this.llbl_pre.TabIndex = 1;
            this.llbl_pre.TabStop = true;
            this.llbl_pre.Text = "上 一页";
            // 
            // llbl_next
            // 
            this.llbl_next.AutoSize = true;
            this.llbl_next.Location = new System.Drawing.Point(481, 7);
            this.llbl_next.Name = "llbl_next";
            this.llbl_next.Size = new System.Drawing.Size(44, 17);
            this.llbl_next.TabIndex = 2;
            this.llbl_next.TabStop = true;
            this.llbl_next.Text = "下一页";
            // 
            // llbl_last
            // 
            this.llbl_last.AutoSize = true;
            this.llbl_last.Location = new System.Drawing.Point(553, 7);
            this.llbl_last.Name = "llbl_last";
            this.llbl_last.Size = new System.Drawing.Size(56, 17);
            this.llbl_last.TabIndex = 3;
            this.llbl_last.TabStop = true;
            this.llbl_last.Text = "最后一页";
            // 
            // lbl_records
            // 
            this.lbl_records.AutoSize = true;
            this.lbl_records.Location = new System.Drawing.Point(6, 7);
            this.lbl_records.Name = "lbl_records";
            this.lbl_records.Size = new System.Drawing.Size(229, 17);
            this.lbl_records.TabIndex = 4;
            this.lbl_records.Text = "当前有 20 条数据,每页10 条数据,共有2页";
            // 
            // cbo_sizes
            // 
            this.cbo_sizes.FormattingEnabled = true;
            this.cbo_sizes.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50"});
            this.cbo_sizes.Location = new System.Drawing.Point(944, 3);
            this.cbo_sizes.Name = "cbo_sizes";
            this.cbo_sizes.Size = new System.Drawing.Size(51, 25);
            this.cbo_sizes.TabIndex = 5;
            this.cbo_sizes.Text = "10";
            // 
            // txt_page
            // 
            this.txt_page.Location = new System.Drawing.Point(1001, 4);
            this.txt_page.Name = "txt_page";
            this.txt_page.Size = new System.Drawing.Size(45, 23);
            this.txt_page.TabIndex = 6;
            this.txt_page.Text = "1";
            // 
            // btn_redirect
            // 
            this.btn_redirect.Location = new System.Drawing.Point(1052, 2);
            this.btn_redirect.Name = "btn_redirect";
            this.btn_redirect.Size = new System.Drawing.Size(63, 25);
            this.btn_redirect.TabIndex = 7;
            this.btn_redirect.Text = "跳转";
            this.btn_redirect.UseVisualStyleBackColor = true;
            // 
            // lbl_exmapl_config
            // 
            this.lbl_exmapl_config.AutoSize = true;
            this.lbl_exmapl_config.Location = new System.Drawing.Point(487, 20);
            this.lbl_exmapl_config.Name = "lbl_exmapl_config";
            this.lbl_exmapl_config.Size = new System.Drawing.Size(56, 17);
            this.lbl_exmapl_config.TabIndex = 3;
            this.lbl_exmapl_config.Text = "配置样例";
            // 
            // ConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_exmapl_config);
            this.Controls.Add(this.panel_config_page);
            this.Controls.Add(this.gb_service);
            this.Controls.Add(this.gb_config);
            this.Name = "ConfigControl";
            this.Size = new System.Drawing.Size(1153, 451);
            this.gb_config.ResumeLayout(false);
            this.panel_config.ResumeLayout(false);
            this.panel_config.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_config)).EndInit();
            this.panel_config_page.ResumeLayout(false);
            this.panel_config_page.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_config;
        private System.Windows.Forms.GroupBox gb_service;
        private System.Windows.Forms.DataGridView dgv_config;
        private System.Windows.Forms.Panel panel_config;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.ComboBox cbo_flag;
        private System.Windows.Forms.Label lbl_flag;
        private System.Windows.Forms.Button btn_query;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nmae;
        private System.Windows.Forms.DataGridViewTextBoxColumn AddressTemplate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn Port;
        private System.Windows.Forms.DataGridViewComboBoxColumn Flag;
        private System.Windows.Forms.DataGridViewTextBoxColumn User;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pwd;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpdateDate;
        private System.Windows.Forms.Panel panel_config_page;
        private System.Windows.Forms.Label lbl_records;
        private System.Windows.Forms.LinkLabel llbl_last;
        private System.Windows.Forms.LinkLabel llbl_next;
        private System.Windows.Forms.LinkLabel llbl_pre;
        private System.Windows.Forms.LinkLabel llbl_index;
        private System.Windows.Forms.ComboBox cbo_sizes;
        private System.Windows.Forms.TextBox txt_page;
        private System.Windows.Forms.Button btn_redirect;
        private System.Windows.Forms.Label lbl_exmapl_config;
    }
}
