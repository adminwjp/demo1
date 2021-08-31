using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility.WinForm;

namespace Tool
{
    public partial class WinFormToolForm : Form
    {
        public WinFormToolForm()
        {
            InitializeComponent();
            
            InitAssembly();
            foreach (var item in AssemblyNames)
            {
                this.comboBoxAssembly.Items.Add(item.FullName);
            }
        }

        private void 模板生成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }
        /// <summary>
        /// 加载当前所有的程序集
        /// </summary>
        private static readonly  List<AssemblyName> AssemblyNames = new List<AssemblyName>();
        public static void InitAssembly()
        {
            if (AssemblyNames.Count == 0)
            {
                var datas = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
                AssemblyNames.AddRange(datas);
            }
        }
      
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            var str = this.comboBoxType.Text;
            if (string.IsNullOrEmpty(str))
            {
                MessageBox.Show("生成的模板,类型不能为空!");
                return;
            }
            var a = typeof(ContainerEntity);
            Type type = Type.GetType(str);
            if (type == null)
            {
                MessageBox.Show("生成的模板,类型找不到!");
                return;
            }
            this._keys.Clear();
            this._templateEntities.Clear();

            foreach (var item in type.GetProperties())
            {
                this._keys.Add(item.Name, item.Name);
                this._templateEntities.Add(new TemplateEntity(item.Name));
            }
            if (this._keys.Count > 0)
            {
                ShowMsg = string.Empty;
                this.dataGridViewTemplate.DataSource = this._templateEntities;
            }
        }
        public string ShowMsg
        {
            get
            {
                return this.richTextBoxShow.Text;
            }
            set
            {
                Action action = () => {
                    this.richTextBoxShow.Text = WinFormHelper.DataGridViewCreate(this._keys);
                };
                WinFormHelper.Set(this.richTextBoxShow, action);
            }
        }
        /// <summary>
        /// 生成普通模板需要的信息
        /// </summary>
        private readonly Dictionary<string, string> _keys = new Dictionary<string, string>();
        /// <summary>
        /// 绑定的数据源信息
        /// </summary>
        private readonly  List<TemplateEntity> _templateEntities = new List<TemplateEntity>();
        private class TemplateEntity
        {
            public TemplateEntity(string name)
            {
                this.PropertyName = name;
                this.PropertyText = name;
            }
            /// <summary>
            /// 属性名称
            /// </summary>
            public string PropertyName { get; set; }
            /// <summary>
            /// 文本信息
            /// </summary>
            public string PropertyText { get; set; }
        }

        private void comboBoxAssembly_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(sender is ComboBox comboBox)
            {
                foreach (var item in AssemblyNames)
                {
                    if (item.FullName == comboBox.Text)
                    {
                        this.comboBoxType.Items.Clear();
                        foreach (var ty in Assembly.Load(item.FullName).GetTypes())
                        {
                            this.comboBoxType.Items.Add(ty.FullName+","+ item.Name);
                        }
                    }
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 选中时 没有处罚
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewTemplate_MultiSelectChanged(object sender, EventArgs e)
        {
            if(sender is DataGridView dataGridView)
            {
                var rows=dataGridView.SelectedRows;
                SetValue(rows[0]);
            }
        }

        private void WinFormToolForm_Load(object sender, EventArgs e)
        {

        }
        void SetValue(DataGridViewRow row)
        {
            WinFormHelper.Set(this.textBoxProName, row.Cells[0].Value?.ToString());
            WinFormHelper.Set(this.textBoxText, row.Cells[1].Value?.ToString());
        }
        private int _rowIndex = 1;
        private int _columnIndex = -1;
        private void dataGridViewTemplate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>-1)
            {
                var rows = this.dataGridViewTemplate.Rows;
                this._rowIndex = e.RowIndex;
                this._columnIndex = e.ColumnIndex;
                SetValue(rows[e.RowIndex]);
            }
            else
            {
                this._rowIndex = -1;
                this._columnIndex = -1;
            }
        }

        private void dataGridViewTemplate_EditModeChanged(object sender, EventArgs e)
        {
            ModifyShowMsg();
        }
        void ModifyShowMsg()
        {
            if (this._rowIndex > -1 && this._columnIndex > -1)
            {
                var rows = this.dataGridViewTemplate.Rows;
                var name = rows[this._rowIndex].Cells[0].Value?.ToString();
                var val = rows[this._rowIndex].Cells[this._columnIndex].Value?.ToString();
                this._keys[name] = val;
                ShowMsg = string.Empty;
            }
        }
        private void dataGridViewTemplate_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ModifyShowMsg();
        }

        private void dataGridViewTemplate_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //ModifyShowMsg();
        }
    }
}
