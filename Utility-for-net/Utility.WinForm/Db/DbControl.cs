using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Db
{
    public partial class DbControl : UserControl
    {
        public DbControl()
        {
            InitializeComponent();
            InitDatatable();
            //this.dgv_db.DataSource = _dataTable;
            ////列标题高度
            //this.dgv_db.ColumnHeadersHeight = 40;
            ////设置不能调整列标题高度
            //this.dgv_db.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            ////合并的列（columnName）
            //this.dgv_db.MergeColumnNames.Add("Column1");
            ////合并的列标题，第3列起，共2列
            //this.dgv_db.AddSpanHeader(2, 2, "性别");
            //DataGridView绑定数据
            DataTable dt = new DataTable();
            dt.Columns.Add("国家");
            dt.Columns.Add("城市");
            dt.Columns.Add("余额");
            dt.Columns.Add("金额");
            dt.Rows.Add("中国", "上海", "5000", "7000");
            dt.Rows.Add("中国", "北京", "3000", "5600");
            dt.Rows.Add("美国", "纽约", "6000", "8600");
            dt.Rows.Add("美国", "华劢顿", "8000", "9000");
            dt.Rows.Add("英国", "伦敦", "7000", "8800");
           // this.dgv_db.DataSource = dt;
            //列标题高度
            this.dgv_db.ColumnHeadersHeight = 40;
            //设置不能调整列标题高度
            this.dgv_db.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //合并的列（columnName）
            //this.dgv_db.MergeColumnNames.Add("Column1");
            //合并的列标题，第3列起，共2列
            this.dgv_db.AddSpanHeader(0, 5, "数据库");
            for (int i = 0; i < 5; i++)
            {
                int index = this.dgv_db.Rows.Add();
                (this.dgv_db.Rows[index].Cells[this.column_dbMysql.Index] as DataGridViewCheckBoxCell).Value = false;
            }
            for (int i = 0; i < this.dgv_db.RowCount; i++)
            {
               ;
                (this.dgv_db.Rows[i].Cells[this.column_dbMysql.Index] as DataGridViewCheckBoxCell).Value = true;
            }
        }
        DataTable _dataTable = new DataTable();
        List<string> _dbTypes = new List<string>() { "SqlServer", "Oracle", "Postgre", "Mysql", "Sqlite" };
        void InitDatatable()
        {
            _dataTable.Columns.AddRange(new DataColumn[] {
                new DataColumn("column_dbMySql",typeof(string)),
                new DataColumn("column_dbSqlServer",typeof(string)),
                new DataColumn("column_dbSqlite",typeof(string)),
                new DataColumn("column_dbPostgre",typeof(string)),
                new DataColumn("column_dbOracle",typeof(string)),
            });
        }
    }
}
