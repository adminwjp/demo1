using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility.WinForm;
using static Tool.StringHelper;

namespace Tool
{
    public partial class StrToolControl :UserControl//: Form
    {
        public StrToolControl()
        {
            InitializeComponent();
            this.Resize += StrToolControl_Resize;
        }

        private void StrToolControl_Resize(object sender, EventArgs e)
        {
            WinFormHelper.Set(this,()=> {
                int height = (this.Height-20) / 10;
                gb_originial.Width = gb_result.Width = gb_operator.Width = this.Width - 10;
                gb_originial.Height = gb_result.Height = height * 4;
                gb_operator.Height = height * 2 - 10;

                gb_originial.Location = new Point(2, 10);


                gb_operator.Left= gb_result.Left = gb_originial.Left;
                gb_result.Top = gb_originial.Bottom + 10;

                gb_operator.Top = gb_result.Bottom + 10;


              

                lbl_name.Top = (gb_operator.Height - lbl_name.Height) / 2;
                lbl_name.Left = (this.Width-500)/2;

                cbo_type.Top= (gb_operator.Height - cbo_type.Height) / 2;
                cbo_type.Left = lbl_name.Right + 10;

                btn_single.Top = (gb_operator.Height - btn_single.Height) / 2;
                btn_single.Left = cbo_type.Right + 30;

                btn_double.Top = (gb_operator.Height - btn_double.Height) / 2;
                btn_double.Left = btn_single.Right + 10;

                btn_cleanTab.Top = (gb_operator.Height - btn_cleanTab.Height) / 2;
                btn_cleanTab.Left = btn_double.Right + 10;

                btn_clear.Top = (gb_operator.Height - btn_clear.Height) / 2;
                btn_clear.Left = btn_cleanTab.Right + 10;

                rtxt_originial.Left = gb_originial.Left + 2;
                rtxt_originial.Width = gb_originial.Width - 5;
                rtxt_originial.Height = gb_originial.Height - 5;

                rtxt_result.Left = gb_result.Left + 2;
                rtxt_result.Width = gb_result.Width - 5;
                rtxt_result.Height = gb_result.Height - 5;

            });
        }

        private string _originialStr
         {
            get
            {
                return this.rtxt_originial.Text;
            }
        }
        public StringType StringType
        {
            get
            {
                return (StringType)this.cbo_type.SelectedValue;
            }
            set
            {
                this.cbo_type.SelectedValue = value;
            }
        }
        private void StrToolForm_Load(object sender, EventArgs e)
        {
            InitCombox();
        }
        private void InitCombox()
        {
            this.cbo_type.DataSource = StringHelper.Initial();
            this.cbo_type.ValueMember = "StringType"; 
            this.cbo_type.DisplayMember = "Name";
        }
        private void btn_single_Click(object sender, EventArgs e)
        {
            if (this.StringType == StringType.String)
            {
                this.StringType = StringType.StringSingle;
            }
            this._resultStr = StringHelper.GetString(this._originialStr,this.StringType);
        }
        private string _resultStr
        {
            get
            {
                return this.rtxt_result.Text;
            }
            set
            {
                if (this.rtxt_result.InvokeRequired)
                {
                    this.rtxt_result.Invoke(new Action(()=> { this.rtxt_result.Text = value; }));
                }
                else
                {
                    this.rtxt_result.Text = value;
                }
            }
        }

        private void btn_double_Click(object sender, EventArgs e)
        {
            if (this.StringType == StringType.StringSingle)
            {
                this.StringType = StringType.String;
            }
            this._resultStr = StringHelper.GetString(this._originialStr, this.StringType);
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            if (this.rtxt_originial.InvokeRequired)
            {
               
                this.rtxt_originial.Invoke((new Action(()=> { this.rtxt_originial.Clear(); }))) ;
            }
            else
            {
                this.rtxt_originial.Clear();
            }
            if (this.rtxt_result.InvokeRequired)
            {

                this.rtxt_result.Invoke((new Action(() => { this.rtxt_result.Clear(); })));
            }
            else
            {
                this.rtxt_result.Clear();
            }
        }

        private void btn_cleanTab_Click(object sender, EventArgs e)
        {
            string str = this._originialStr;
            str = StringHelper.Clear(str);
            this._resultStr = str;
        }
    }
}
