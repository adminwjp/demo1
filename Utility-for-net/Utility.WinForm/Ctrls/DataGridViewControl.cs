using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utility.WinForm.Ctrls
{
    public partial class DataGridViewControl : UserControl
    {
        public DataGridViewControl()
        {
            InitializeComponent();
            this.OnHeaderChangeEvent += OnHeaderChangeDefaultEvent;
        }

        /// <summary>
        /// 表单集合
        /// </summary>
        public readonly List<GridEntity> GridEntities = new List<GridEntity>();
        /// <summary>
        /// 表单表头 委托
        /// </summary>
        /// <param name="gridEntities"></param>
        public delegate void OnHeader(List<GridEntity> gridEntities=null);
        /// <summary>
        /// 表单表头 改变事件
        /// </summary>
        public OnHeader OnHeaderChangeEvent;
        /// <summary>
        /// 表单表头 改变默认事件
        /// </summary>
        /// <param name="gridEntities"></param>
        protected virtual void OnHeaderChangeDefaultEvent(List<GridEntity> gridEntities=null)
        {
            gridEntities = gridEntities ?? this.GridEntities;
            foreach (var item in gridEntities)
            {
                DataGridViewColumn column;
                if (item.GridFlag== GridFlag.CheckBox)
                {
                    column = new DataGridViewCheckBoxColumn();
                }
                else
                {
                    column = new DataGridViewTextBoxColumn();
                    column.DataPropertyName = item.Name;
                }
                column.Name = item.Name;
                column.HeaderText = item.Text;
                column.ReadOnly = item.Readonly;
                this.DataGridView.Columns.Add(column);
            }
        }
    }
   
   
}
