using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Utility.WinForm
{
    /// <summary>
    /// WinForm 公共类
    /// </summary>
    public class WinFormHelper
    {
        public readonly static DataGridViewFactory DefaultDataGridViewFactory = new DataGridViewFactory();
      
        public static void Set(Control control,Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action); 
            }
            else
            {
                action();
            }
        }
        public static void Set(Control control,string msg)
        {
            Action action = () => { control.Text = msg; };
            Set(control, action);
        }
        public static void Set(RichTextBox control, string msg)
        {
            Action action = () => { control.AppendText($"{msg} \r\n"); };
            Set(control, action);
        }

        public static string DataGridViewCreate(Dictionary<string, string> keys)
        {
            return DefaultDataGridViewFactory.Craret(keys);
        }

        public class DataGridViewFactory{

            public string Craret(Dictionary<string, string> keys)
            {
                var builder = new StringBuilder();
                var builder1 = new StringBuilder();
                var builder2 = new StringBuilder();
                foreach (var item in keys)
                {
                    builder.Append($"this.{item.Key} = new System.Windows.Forms.DataGridViewTextBoxColumn();\n");
                    builder1.Append($"private System.Windows.Forms.DataGridViewTextBoxColumn {item.Key};\n");
                    builder2.Append("//\n").Append(item.Value).Append("\n//\n");
                    builder2.Append($"this.{item.Key}.HeaderText = \"{item.Value}\";\n");
                    builder2.Append($"this.{item.Key}.DataPropertyName = \"{item.Key}\";\n");
                    builder2.Append($"this.{item.Key}.Name = \"{item.Key}\";\n");
                }
                var str = $"{builder.ToString()}\n\n{builder1.ToString()}\n\n{builder2.ToString()}";
                return str;
            }

            /// <summary>绑定DataGridView 数据 表单格式 </summary>
            /// <param name="dataGridView"></param>
            /// <param name="columnModels"></param>
            //public virtual void Bind(DataGridView dataGridView,List<ColumnModel> columnModels = null)
            //{
            //    foreach (var item in columnModels)
            //    {
            //        DataGridViewColumn column;
            //        switch (item.Flag)
            //        {
            //            case ColumnFlag.TextBox:
            //                column = new DataGridViewTextBoxColumn();
            //                column.DataPropertyName = item.Name;
            //                break;
            //            case ColumnFlag.Checkbox:
            //                column = new DataGridViewCheckBoxColumn();
            //                break;
            //            case ColumnFlag.Combox:
            //                column = new DataGridViewComboBoxColumn();
            //                column.DataPropertyName = item.Name;
            //                break;
            //            case ColumnFlag.None:
            //            default:
            //                column = new DataGridViewColumn();
            //                column.DataPropertyName = item.Name;
            //                break;
            //        }
            //        column.Name = item.Name;
            //        column.HeaderText = item.Text;
            //        column.ReadOnly = item.Readonly;
            //        dataGridView.Columns.Add(column);
            //    }
            //}

            /// <summary>绑定DataGridView 复选框全选事件 默认在左上角 其他位置无效</summary>
            /// <param name="dataGridView"></param>

            public void BindAllChecked(DataGridView dataGridView)
            {
                dataGridView.CellClick -= DataGridView_CellClick;
                dataGridView.CellClick += DataGridView_CellClick;
            }

            /// <summary>绑定DataGridView 复选框单选事件 默认在左边位置 其他位置无效</summary>
            /// <param name="dataGridView"></param>

            public void BindChecked(DataGridView dataGridView)
            {
                dataGridView.CellContentClick -= DataGridView_CellContentClick;
                dataGridView.CellContentClick += DataGridView_CellContentClick; 
            }

            private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex > -1 && e.ColumnIndex == 0)
                {
                    if (sender is DataGridView gridView)
                    {
                        if (gridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value is bool flag)
                        {
                            gridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !flag;
                        }
                        else
                        {
                            gridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
                        }
                    }
                }
            }

            private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex == -1 && e.ColumnIndex == 0)
                {
                    if (sender is DataGridView gridView)
                    {
                        object val = gridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                        //_all = !_all;
                        gridView.CurrentCell = null;//选中的单元格 失效bug  清除光标选中
                        //var flag = _all;
                        var flag = val!=null&&val is bool?(bool)val:false;
                        gridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = flag;
                        for (int i = 0; i < gridView.Rows.Count; i++)
                        {
                            gridView.Rows[i].Cells[0].Value = flag;
                        }
                    }
                }
            }
        }
    }




}
