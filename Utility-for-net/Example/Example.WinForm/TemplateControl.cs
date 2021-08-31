using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example.WinForm
{
    public partial class TemplateControl : UserControl
    {
        //datagridview tree
        //https://www.cnblogs.com/bfyx/p/11412899.html
        //excel word
        //https://www.cnblogs.com/dachuang/p/9040689.html
        //json fromat
        //https://www.cnblogs.com/unintersky/p/3884712.html
        public TemplateControl()
        {
            InitializeComponent(); 
            this.SizeChanged -= TemplateControl_SizeChanged;
            this.SizeChanged += TemplateControl_SizeChanged;           
        }

        private void TemplateControl_SizeChanged(object sender, EventArgs e)
        {
            if (this.Width < 1000)
            {
                this.Width = 1000;
            }
            if (this.Height < 500)
            {
                this.Height = 500;
            }

            panel1.Width = this.Width;
            panel1.Height = 50;

            panel2.Location = new Point(0,59);
            panel2.Width = this.Width;
            panel2.Height = this.Height-60;


            tabControl1.Width = this.panel2.Width;
            tabControl1.Height = this.panel2.Height;
        }
    }
}
