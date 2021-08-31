
namespace Example.WinForm
{
    partial class TemplateControl
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_excel = new System.Windows.Forms.Button();
            this.btn_word = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this._db = new System.Windows.Forms.DataGridView();
            this.checkbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.remove = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel_db_top = new System.Windows.Forms.Panel();
            this.txt_db_name = new System.Windows.Forms.TextBox();
            this.btn_db = new System.Windows.Forms.Button();
            this.lbl_db_name = new System.Windows.Forms.Label();
            this.btn_db_add = new System.Windows.Forms.Button();
            this.btn_db_edit = new System.Windows.Forms.Button();
            this.btn_remove = new System.Windows.Forms.Button();
            this.panel_table_top = new System.Windows.Forms.Panel();
            this.treeView_db = new System.Windows.Forms.TreeView();
            this._table = new System.Windows.Forms.DataGridView();
            this.btn_table_remove = new System.Windows.Forms.Button();
            this.btn_table_edit = new System.Windows.Forms.Button();
            this.btn_table_add = new System.Windows.Forms.Button();
            this._table_name = new System.Windows.Forms.Label();
            this.btn_table = new System.Windows.Forms.Button();
            this.txt_table_name = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_col_remove = new System.Windows.Forms.Button();
            this.btn_col_edit = new System.Windows.Forms.Button();
            this.btn_col_add = new System.Windows.Forms.Button();
            this.lbl_col_name = new System.Windows.Forms.Label();
            this.btn_col = new System.Windows.Forms.Button();
            this.col_name = new System.Windows.Forms.TextBox();
            this._col = new System.Windows.Forms.DataGridView();
            this.treeView_tab = new System.Windows.Forms.TreeView();
            this.btn_csv = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._db)).BeginInit();
            this.panel_db_top.SuspendLayout();
            this.panel_table_top.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._table)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._col)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(6, 73);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(627, 457);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel_db_top);
            this.tabPage1.Controls.Add(this._db);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(619, 431);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "db";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this._table);
            this.tabPage2.Controls.Add(this.treeView_db);
            this.tabPage2.Controls.Add(this.panel_table_top);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(619, 431);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "table";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this._col);
            this.tabPage3.Controls.Add(this.treeView_tab);
            this.tabPage3.Controls.Add(this.panel3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(619, 431);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "column";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_csv);
            this.panel1.Controls.Add(this.checkBox3);
            this.panel1.Controls.Add(this.checkBox2);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.btn_word);
            this.panel1.Controls.Add(this.btn_excel);
            this.panel1.Location = new System.Drawing.Point(7, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(626, 64);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(7, 70);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(626, 463);
            this.panel2.TabIndex = 2;
            // 
            // btn_excel
            // 
            this.btn_excel.Location = new System.Drawing.Point(246, 18);
            this.btn_excel.Name = "btn_excel";
            this.btn_excel.Size = new System.Drawing.Size(105, 23);
            this.btn_excel.TabIndex = 0;
            this.btn_excel.Text = "Export Excel";
            this.btn_excel.UseVisualStyleBackColor = true;
            // 
            // btn_word
            // 
            this.btn_word.Location = new System.Drawing.Point(384, 18);
            this.btn_word.Name = "btn_word";
            this.btn_word.Size = new System.Drawing.Size(108, 23);
            this.btn_word.TabIndex = 1;
            this.btn_word.Text = "Export Word";
            this.btn_word.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(40, 22);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(36, 16);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "db";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(82, 22);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(54, 16);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "table";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(154, 22);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(60, 16);
            this.checkBox3.TabIndex = 5;
            this.checkBox3.Text = "column";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // _db
            // 
            this._db.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._db.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkbox,
            this.id,
            this.name,
            this.edit,
            this.remove});
            this._db.Location = new System.Drawing.Point(-3, 69);
            this._db.Name = "_db";
            this._db.RowTemplate.Height = 23;
            this._db.Size = new System.Drawing.Size(619, 255);
            this._db.TabIndex = 0;
            // 
            // checkbox
            // 
            this.checkbox.HeaderText = "checkbox";
            this.checkbox.Name = "checkbox";
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "name";
            this.name.Name = "name";
            // 
            // edit
            // 
            this.edit.HeaderText = "edit";
            this.edit.Name = "edit";
            this.edit.Text = "edit";
            // 
            // remove
            // 
            this.remove.HeaderText = "remove";
            this.remove.Name = "remove";
            this.remove.Text = "edit";
            // 
            // panel_db_top
            // 
            this.panel_db_top.Controls.Add(this.btn_remove);
            this.panel_db_top.Controls.Add(this.btn_db_edit);
            this.panel_db_top.Controls.Add(this.btn_db_add);
            this.panel_db_top.Controls.Add(this.lbl_db_name);
            this.panel_db_top.Controls.Add(this.btn_db);
            this.panel_db_top.Controls.Add(this.txt_db_name);
            this.panel_db_top.Location = new System.Drawing.Point(4, 6);
            this.panel_db_top.Name = "panel_db_top";
            this.panel_db_top.Size = new System.Drawing.Size(609, 57);
            this.panel_db_top.TabIndex = 1;
            // 
            // txt_db_name
            // 
            this.txt_db_name.Location = new System.Drawing.Point(97, 15);
            this.txt_db_name.Name = "txt_db_name";
            this.txt_db_name.Size = new System.Drawing.Size(100, 21);
            this.txt_db_name.TabIndex = 0;
            // 
            // btn_db
            // 
            this.btn_db.Location = new System.Drawing.Point(238, 13);
            this.btn_db.Name = "btn_db";
            this.btn_db.Size = new System.Drawing.Size(75, 23);
            this.btn_db.TabIndex = 1;
            this.btn_db.Text = "query";
            this.btn_db.UseVisualStyleBackColor = true;
            // 
            // lbl_db_name
            // 
            this.lbl_db_name.AutoSize = true;
            this.lbl_db_name.Location = new System.Drawing.Point(28, 18);
            this.lbl_db_name.Name = "lbl_db_name";
            this.lbl_db_name.Size = new System.Drawing.Size(29, 12);
            this.lbl_db_name.TabIndex = 2;
            this.lbl_db_name.Text = "name";
            // 
            // btn_db_add
            // 
            this.btn_db_add.Location = new System.Drawing.Point(319, 13);
            this.btn_db_add.Name = "btn_db_add";
            this.btn_db_add.Size = new System.Drawing.Size(75, 23);
            this.btn_db_add.TabIndex = 3;
            this.btn_db_add.Text = "add";
            this.btn_db_add.UseVisualStyleBackColor = true;
            // 
            // btn_db_edit
            // 
            this.btn_db_edit.Location = new System.Drawing.Point(400, 13);
            this.btn_db_edit.Name = "btn_db_edit";
            this.btn_db_edit.Size = new System.Drawing.Size(75, 23);
            this.btn_db_edit.TabIndex = 4;
            this.btn_db_edit.Text = "edit";
            this.btn_db_edit.UseVisualStyleBackColor = true;
            // 
            // btn_remove
            // 
            this.btn_remove.Location = new System.Drawing.Point(481, 13);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(75, 23);
            this.btn_remove.TabIndex = 5;
            this.btn_remove.Text = "remove";
            this.btn_remove.UseVisualStyleBackColor = true;
            // 
            // panel_table_top
            // 
            this.panel_table_top.Controls.Add(this.btn_table_remove);
            this.panel_table_top.Controls.Add(this.btn_table_edit);
            this.panel_table_top.Controls.Add(this.btn_table_add);
            this.panel_table_top.Controls.Add(this._table_name);
            this.panel_table_top.Controls.Add(this.btn_table);
            this.panel_table_top.Controls.Add(this.txt_table_name);
            this.panel_table_top.Location = new System.Drawing.Point(7, 6);
            this.panel_table_top.Name = "panel_table_top";
            this.panel_table_top.Size = new System.Drawing.Size(606, 85);
            this.panel_table_top.TabIndex = 0;
            // 
            // treeView_db
            // 
            this.treeView_db.Location = new System.Drawing.Point(7, 125);
            this.treeView_db.Name = "treeView_db";
            this.treeView_db.Size = new System.Drawing.Size(121, 234);
            this.treeView_db.TabIndex = 1;
            // 
            // _table
            // 
            this._table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._table.Location = new System.Drawing.Point(135, 125);
            this._table.Name = "_table";
            this._table.RowTemplate.Height = 23;
            this._table.Size = new System.Drawing.Size(478, 234);
            this._table.TabIndex = 2;
            // 
            // btn_table_remove
            // 
            this.btn_table_remove.Location = new System.Drawing.Point(492, 39);
            this.btn_table_remove.Name = "btn_table_remove";
            this.btn_table_remove.Size = new System.Drawing.Size(75, 23);
            this.btn_table_remove.TabIndex = 11;
            this.btn_table_remove.Text = "remove";
            this.btn_table_remove.UseVisualStyleBackColor = true;
            // 
            // btn_table_edit
            // 
            this.btn_table_edit.Location = new System.Drawing.Point(411, 39);
            this.btn_table_edit.Name = "btn_table_edit";
            this.btn_table_edit.Size = new System.Drawing.Size(75, 23);
            this.btn_table_edit.TabIndex = 10;
            this.btn_table_edit.Text = "edit";
            this.btn_table_edit.UseVisualStyleBackColor = true;
            // 
            // btn_table_add
            // 
            this.btn_table_add.Location = new System.Drawing.Point(330, 39);
            this.btn_table_add.Name = "btn_table_add";
            this.btn_table_add.Size = new System.Drawing.Size(75, 23);
            this.btn_table_add.TabIndex = 9;
            this.btn_table_add.Text = "add";
            this.btn_table_add.UseVisualStyleBackColor = true;
            // 
            // _table_name
            // 
            this._table_name.AutoSize = true;
            this._table_name.Location = new System.Drawing.Point(39, 44);
            this._table_name.Name = "_table_name";
            this._table_name.Size = new System.Drawing.Size(29, 12);
            this._table_name.TabIndex = 8;
            this._table_name.Text = "name";
            // 
            // btn_table
            // 
            this.btn_table.Location = new System.Drawing.Point(249, 39);
            this.btn_table.Name = "btn_table";
            this.btn_table.Size = new System.Drawing.Size(75, 23);
            this.btn_table.TabIndex = 7;
            this.btn_table.Text = "query";
            this.btn_table.UseVisualStyleBackColor = true;
            // 
            // txt_table_name
            // 
            this.txt_table_name.Location = new System.Drawing.Point(108, 41);
            this.txt_table_name.Name = "txt_table_name";
            this.txt_table_name.Size = new System.Drawing.Size(100, 21);
            this.txt_table_name.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btn_col_remove);
            this.panel3.Controls.Add(this.btn_col_edit);
            this.panel3.Controls.Add(this.btn_col_add);
            this.panel3.Controls.Add(this.lbl_col_name);
            this.panel3.Controls.Add(this.btn_col);
            this.panel3.Controls.Add(this.col_name);
            this.panel3.Location = new System.Drawing.Point(3, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(606, 85);
            this.panel3.TabIndex = 1;
            // 
            // btn_col_remove
            // 
            this.btn_col_remove.Location = new System.Drawing.Point(492, 39);
            this.btn_col_remove.Name = "btn_col_remove";
            this.btn_col_remove.Size = new System.Drawing.Size(75, 23);
            this.btn_col_remove.TabIndex = 11;
            this.btn_col_remove.Text = "remove";
            this.btn_col_remove.UseVisualStyleBackColor = true;
            // 
            // btn_col_edit
            // 
            this.btn_col_edit.Location = new System.Drawing.Point(411, 39);
            this.btn_col_edit.Name = "btn_col_edit";
            this.btn_col_edit.Size = new System.Drawing.Size(75, 23);
            this.btn_col_edit.TabIndex = 10;
            this.btn_col_edit.Text = "edit";
            this.btn_col_edit.UseVisualStyleBackColor = true;
            // 
            // btn_col_add
            // 
            this.btn_col_add.Location = new System.Drawing.Point(330, 39);
            this.btn_col_add.Name = "btn_col_add";
            this.btn_col_add.Size = new System.Drawing.Size(75, 23);
            this.btn_col_add.TabIndex = 9;
            this.btn_col_add.Text = "add";
            this.btn_col_add.UseVisualStyleBackColor = true;
            // 
            // lbl_col_name
            // 
            this.lbl_col_name.AutoSize = true;
            this.lbl_col_name.Location = new System.Drawing.Point(39, 44);
            this.lbl_col_name.Name = "lbl_col_name";
            this.lbl_col_name.Size = new System.Drawing.Size(29, 12);
            this.lbl_col_name.TabIndex = 8;
            this.lbl_col_name.Text = "name";
            // 
            // btn_col
            // 
            this.btn_col.Location = new System.Drawing.Point(249, 39);
            this.btn_col.Name = "btn_col";
            this.btn_col.Size = new System.Drawing.Size(75, 23);
            this.btn_col.TabIndex = 7;
            this.btn_col.Text = "query";
            this.btn_col.UseVisualStyleBackColor = true;
            // 
            // col_name
            // 
            this.col_name.Location = new System.Drawing.Point(108, 41);
            this.col_name.Name = "col_name";
            this.col_name.Size = new System.Drawing.Size(100, 21);
            this.col_name.TabIndex = 6;
            // 
            // _col
            // 
            this._col.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._col.Location = new System.Drawing.Point(131, 110);
            this._col.Name = "_col";
            this._col.RowTemplate.Height = 23;
            this._col.Size = new System.Drawing.Size(478, 234);
            this._col.TabIndex = 4;
            // 
            // treeView_tab
            // 
            this.treeView_tab.Location = new System.Drawing.Point(3, 110);
            this.treeView_tab.Name = "treeView_tab";
            this.treeView_tab.Size = new System.Drawing.Size(121, 234);
            this.treeView_tab.TabIndex = 3;
            // 
            // btn_csv
            // 
            this.btn_csv.Location = new System.Drawing.Point(513, 18);
            this.btn_csv.Name = "btn_csv";
            this.btn_csv.Size = new System.Drawing.Size(108, 23);
            this.btn_csv.TabIndex = 6;
            this.btn_csv.Text = "Export Csv";
            this.btn_csv.UseVisualStyleBackColor = true;
            // 
            // TemplateControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "TemplateControl";
            this.Size = new System.Drawing.Size(631, 536);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._db)).EndInit();
            this.panel_db_top.ResumeLayout(false);
            this.panel_db_top.PerformLayout();
            this.panel_table_top.ResumeLayout(false);
            this.panel_table_top.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._table)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._col)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btn_word;
        private System.Windows.Forms.Button btn_excel;
        private System.Windows.Forms.DataGridView _db;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkbox;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewButtonColumn edit;
        private System.Windows.Forms.DataGridViewButtonColumn remove;
        private System.Windows.Forms.Panel panel_db_top;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Button btn_db_edit;
        private System.Windows.Forms.Button btn_db_add;
        private System.Windows.Forms.Label lbl_db_name;
        private System.Windows.Forms.Button btn_db;
        private System.Windows.Forms.TextBox txt_db_name;
        private System.Windows.Forms.DataGridView _table;
        private System.Windows.Forms.TreeView treeView_db;
        private System.Windows.Forms.Panel panel_table_top;
        private System.Windows.Forms.Button btn_table_remove;
        private System.Windows.Forms.Button btn_table_edit;
        private System.Windows.Forms.Button btn_table_add;
        private System.Windows.Forms.Label _table_name;
        private System.Windows.Forms.Button btn_table;
        private System.Windows.Forms.TextBox txt_table_name;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_col_remove;
        private System.Windows.Forms.Button btn_col_edit;
        private System.Windows.Forms.Button btn_col_add;
        private System.Windows.Forms.Label lbl_col_name;
        private System.Windows.Forms.Button btn_col;
        private System.Windows.Forms.TextBox col_name;
        private System.Windows.Forms.DataGridView _col;
        private System.Windows.Forms.TreeView treeView_tab;
        private System.Windows.Forms.Button btn_csv;
    }
}
