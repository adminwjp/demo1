namespace Tool
{
    partial class WinFormToolForm
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
            this.dataGridView模板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模板生成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewTemplate = new System.Windows.Forms.DataGridView();
            this.LabelType = new System.Windows.Forms.Label();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.richTextBoxShow = new System.Windows.Forms.RichTextBox();
            this.labelShow = new System.Windows.Forms.Label();
            this.buttonOperator = new System.Windows.Forms.Button();
            this.textBoxProName = new System.Windows.Forms.TextBox();
            this.labelProName = new System.Windows.Forms.Label();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.labelText = new System.Windows.Forms.Label();
            this.comboBoxAssembly = new System.Windows.Forms.ComboBox();
            this.labelAssembly = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.PropertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PropertyText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataGridView模板ToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // dataGridView模板ToolStripMenuItem
            // 
            this.dataGridView模板ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.模板生成ToolStripMenuItem});
            this.dataGridView模板ToolStripMenuItem.Name = "dataGridView模板ToolStripMenuItem";
            this.dataGridView模板ToolStripMenuItem.Size = new System.Drawing.Size(123, 21);
            this.dataGridView模板ToolStripMenuItem.Text = "DataGridView模板";
            // 
            // 模板生成ToolStripMenuItem
            // 
            this.模板生成ToolStripMenuItem.Name = "模板生成ToolStripMenuItem";
            this.模板生成ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.模板生成ToolStripMenuItem.Text = "模板生成";
            this.模板生成ToolStripMenuItem.Click += new System.EventHandler(this.模板生成ToolStripMenuItem_Click);
            // 
            // dataGridViewTemplate
            // 
            this.dataGridViewTemplate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTemplate.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PropertyName,
            this.PropertyText});
            this.dataGridViewTemplate.Location = new System.Drawing.Point(-12, 116);
            this.dataGridViewTemplate.Name = "dataGridViewTemplate";
            this.dataGridViewTemplate.RowTemplate.Height = 23;
            this.dataGridViewTemplate.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewTemplate.Size = new System.Drawing.Size(800, 150);
            this.dataGridViewTemplate.TabIndex = 1;
            this.dataGridViewTemplate.EditModeChanged += new System.EventHandler(this.dataGridViewTemplate_EditModeChanged);
            this.dataGridViewTemplate.MultiSelectChanged += new System.EventHandler(this.dataGridViewTemplate_MultiSelectChanged);
            this.dataGridViewTemplate.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTemplate_CellClick);
            this.dataGridViewTemplate.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTemplate_CellEndEdit);
            this.dataGridViewTemplate.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTemplate_CellValueChanged);
            // 
            // LabelType
            // 
            this.LabelType.AutoSize = true;
            this.LabelType.Location = new System.Drawing.Point(298, 39);
            this.LabelType.Name = "LabelType";
            this.LabelType.Size = new System.Drawing.Size(35, 12);
            this.LabelType.TabIndex = 2;
            this.LabelType.Text = "类型:";
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(549, 34);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(75, 23);
            this.buttonCreate.TabIndex = 4;
            this.buttonCreate.Text = "生成";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // richTextBoxShow
            // 
            this.richTextBoxShow.Location = new System.Drawing.Point(0, 305);
            this.richTextBoxShow.Name = "richTextBoxShow";
            this.richTextBoxShow.Size = new System.Drawing.Size(800, 144);
            this.richTextBoxShow.TabIndex = 5;
            this.richTextBoxShow.Text = "";
            // 
            // labelShow
            // 
            this.labelShow.AutoSize = true;
            this.labelShow.Location = new System.Drawing.Point(362, 279);
            this.labelShow.Name = "labelShow";
            this.labelShow.Size = new System.Drawing.Size(53, 12);
            this.labelShow.TabIndex = 6;
            this.labelShow.Text = "输出结果";
            // 
            // buttonOperator
            // 
            this.buttonOperator.Location = new System.Drawing.Point(382, 75);
            this.buttonOperator.Name = "buttonOperator";
            this.buttonOperator.Size = new System.Drawing.Size(75, 23);
            this.buttonOperator.TabIndex = 9;
            this.buttonOperator.Text = "添加";
            this.buttonOperator.UseVisualStyleBackColor = true;
            // 
            // textBoxProName
            // 
            this.textBoxProName.Location = new System.Drawing.Point(81, 77);
            this.textBoxProName.Name = "textBoxProName";
            this.textBoxProName.ReadOnly = true;
            this.textBoxProName.Size = new System.Drawing.Size(100, 21);
            this.textBoxProName.TabIndex = 8;
            // 
            // labelProName
            // 
            this.labelProName.AutoSize = true;
            this.labelProName.Location = new System.Drawing.Point(16, 80);
            this.labelProName.Name = "labelProName";
            this.labelProName.Size = new System.Drawing.Size(59, 12);
            this.labelProName.TabIndex = 7;
            this.labelProName.Text = "属性名称:";
            // 
            // textBoxText
            // 
            this.textBoxText.Location = new System.Drawing.Point(264, 77);
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(100, 21);
            this.textBoxText.TabIndex = 11;
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(213, 80);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(35, 12);
            this.labelText.TabIndex = 10;
            this.labelText.Text = "文本:";
            // 
            // comboBoxAssembly
            // 
            this.comboBoxAssembly.FormattingEnabled = true;
            this.comboBoxAssembly.Location = new System.Drawing.Point(81, 34);
            this.comboBoxAssembly.Name = "comboBoxAssembly";
            this.comboBoxAssembly.Size = new System.Drawing.Size(197, 20);
            this.comboBoxAssembly.TabIndex = 12;
            this.comboBoxAssembly.SelectedIndexChanged += new System.EventHandler(this.comboBoxAssembly_SelectedIndexChanged);
            // 
            // labelAssembly
            // 
            this.labelAssembly.AutoSize = true;
            this.labelAssembly.Location = new System.Drawing.Point(28, 39);
            this.labelAssembly.Name = "labelAssembly";
            this.labelAssembly.Size = new System.Drawing.Size(47, 12);
            this.labelAssembly.TabIndex = 13;
            this.labelAssembly.Text = "程序集:";
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Location = new System.Drawing.Point(339, 36);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(188, 20);
            this.comboBoxType.TabIndex = 14;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(643, 34);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 15;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // PropertyName
            // 
            this.PropertyName.DataPropertyName = "PropertyName";
            this.PropertyName.HeaderText = "属性名称";
            this.PropertyName.Name = "PropertyName";
            this.PropertyName.ReadOnly = true;
            // 
            // PropertyText
            // 
            this.PropertyText.DataPropertyName = "PropertyText";
            this.PropertyText.HeaderText = "文本";
            this.PropertyText.Name = "PropertyText";
            // 
            // WinFormToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.labelAssembly);
            this.Controls.Add(this.comboBoxAssembly);
            this.Controls.Add(this.textBoxText);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.buttonOperator);
            this.Controls.Add(this.textBoxProName);
            this.Controls.Add(this.labelProName);
            this.Controls.Add(this.labelShow);
            this.Controls.Add(this.richTextBoxShow);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.LabelType);
            this.Controls.Add(this.dataGridViewTemplate);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "WinFormToolForm";
            this.Text = "WinForm工具";
            this.Load += new System.EventHandler(this.WinFormToolForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTemplate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem dataGridView模板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 模板生成ToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridViewTemplate;
        private System.Windows.Forms.Label LabelType;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.RichTextBox richTextBoxShow;
        private System.Windows.Forms.Label labelShow;
        private System.Windows.Forms.Button buttonOperator;
        private System.Windows.Forms.TextBox textBoxProName;
        private System.Windows.Forms.Label labelProName;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.ComboBox comboBoxAssembly;
        private System.Windows.Forms.Label labelAssembly;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyText;
    }
}