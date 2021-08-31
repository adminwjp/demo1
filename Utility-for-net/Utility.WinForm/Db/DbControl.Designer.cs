namespace Db
{
    partial class DbControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbControl));
            this.dgv_db = new RowMergeView();
            this.column_dbMysql = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.column_dbSqlServer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_dbSqlite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_dbPostgre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_dbOracle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_db)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_db
            // 
            this.dgv_db.AllowUserToAddRows = false;
            this.dgv_db.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_db.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_db.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.column_dbMysql,
            this.column_dbSqlServer,
            this.column_dbSqlite,
            this.column_dbPostgre,
            this.column_dbOracle});
            this.dgv_db.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_db.Location = new System.Drawing.Point(0, 0);
            this.dgv_db.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgv_db.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgv_db.MergeColumnNames")));
            this.dgv_db.Name = "dgv_db";
            this.dgv_db.RowTemplate.Height = 23;
            this.dgv_db.Size = new System.Drawing.Size(765, 476);
            this.dgv_db.TabIndex = 0;
            // 
            // column_dbMysql
            // 
            this.column_dbMysql.DataPropertyName = "column_dbMysql";
            this.column_dbMysql.HeaderText = "MySql数据库";
            this.column_dbMysql.Name = "column_dbMysql";
            this.column_dbMysql.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.column_dbMysql.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // column_dbSqlServer
            // 
            this.column_dbSqlServer.DataPropertyName = "column_dbSqlServer";
            this.column_dbSqlServer.HeaderText = "SqlServer数据库";
            this.column_dbSqlServer.Name = "column_dbSqlServer";
            this.column_dbSqlServer.Width = 130;
            // 
            // column_dbSqlite
            // 
            this.column_dbSqlite.DataPropertyName = "column_dbSqlite";
            this.column_dbSqlite.HeaderText = "Sqlite数据库";
            this.column_dbSqlite.Name = "column_dbSqlite";
            this.column_dbSqlite.Width = 110;
            // 
            // column_dbPostgre
            // 
            this.column_dbPostgre.DataPropertyName = "column_dbPostgre";
            this.column_dbPostgre.HeaderText = "Postgre数据库";
            this.column_dbPostgre.Name = "column_dbPostgre";
            this.column_dbPostgre.Width = 110;
            // 
            // column_dbOracle
            // 
            this.column_dbOracle.DataPropertyName = "column_dbOracle";
            this.column_dbOracle.HeaderText = "Oracle数据库";
            this.column_dbOracle.Name = "column_dbOracle";
            this.column_dbOracle.Width = 110;
            // 
            // DbControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv_db);
            this.Name = "DbControl";
            this.Size = new System.Drawing.Size(765, 476);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_db)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private RowMergeView dgv_db;
        private System.Windows.Forms.DataGridViewCheckBoxColumn column_dbMysql;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_dbSqlServer;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_dbSqlite;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_dbPostgre;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_dbOracle;
    }
}
