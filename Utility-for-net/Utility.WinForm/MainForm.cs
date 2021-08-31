using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility.WinForm;
//using Utitlity.IIS;

namespace WinForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        //AutoSizeFormClass autoSizeFormClass = new AutoSizeFormClass();

        private void MainForm_Load(object sender, EventArgs e)
        {
            //IISHelper.Test();     
            //autoSizeFormClass.ControllInitializeSize(this);
            //this.SizeChanged -= MainForm_SizeChanged;
            //this.SizeChanged += MainForm_SizeChanged;
            //这个 自适应 需要 手动 调 位置 以及字体 大小 麻烦 每个窗体都需要 不然 规则 不同 导致 布局有问题
            //大 至 一致 的布局 也 麻烦复制 粘贴 有的不能移动 只能 放大 缩小 
        }

        //private void MainForm_SizeChanged(object sender, EventArgs e)
        //{
        //    autoSizeFormClass.ControlAutoSize(this);
        //}

        /// <summary>
        /// 菜单 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag != null)
            {
                string tag = e.ClickedItem.Tag.ToString();
                if ("Conifg".Equals(tag))
                {

                }
            }
        }

        private void tree_Click(object sender, EventArgs e)
        {

        }
    }
}
